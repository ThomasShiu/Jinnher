using System;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using AUTO_SCHE.CLASS;
using System.Threading;

namespace AUTO_SCHE
{
    public partial class EMAIL_JOBS : Form
    {
        thomas_class ts_conn = new thomas_class();
        string v_sqlstr;

        public EMAIL_JOBS()
        {
            InitializeComponent();

        }
        //每10秒更新DataView
        private void timer2_Tick(object sender, EventArgs e)
        {
            //Refresh_DV();
            Thread t = new Thread(Filldv);
            t.IsBackground = true;
            t.Start();

        }

        private void EMAIL_JOBS_Load(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(DateTime.Now.ToString("yyyyMMdd")) >= 20160808)
            {
                Application.Exit();
            }
            // TODO: 這行程式碼會將資料載入 'dataSet1.ODS_JH_EMAIL' 資料表。您可以視需要進行移動或移除。
            //Refresh_DV();
            //Thread t = new Thread(Filldv);
            //t.IsBackground = true;
            //t.Start();
       
        }

        //重整Dataview資料
        private void Refresh_DV()
        {
            this.oDS_JH_EMAILTableAdapter.Fill(this.dataSet1.ODS_JH_EMAIL);
            //dataGridView1.DataSource = bindingSource1;
            //dataGridView1.AutoResizeColumns();
        }

        private void CB_start_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_start.Checked)
            {
                rTB_message.Text += "排程啟動：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " \n";
                rTB_message.ScrollToCaret();
                toolStripStatusLabel1.Text = "排程啟動：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                timer1.Start();

            }
            else
            {
                rTB_message.Text += "排程停止：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " \n";
                toolStripStatusLabel1.Text = "排程停止：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            Thread t = new Thread(Do_job);
            t.IsBackground = true;
            t.Start();

            System.Threading.Thread.Sleep(5000);

            Thread t2 = new Thread(Filldv);
            t2.IsBackground = true;
            t2.Start();

            //每天午夜轉出LOG
            if (DateTime.Now.ToString("HHmm") == "2330")
            {
                ExportLog(rTB_message);
             
            }
        }

        public delegate void FillDVcallback();
        public void Filldv()
        {
            if (dataGridView1.InvokeRequired)
            {
                try
                {
                    FillDVcallback d = new FillDVcallback(Filldv);
                    Invoke(d);
                }
                catch (Exception e)
                {
                    myUI(e.Message, rTB_message);
                }
            }
            else
            {
                try
                {
                    this.oDS_JH_EMAILTableAdapter.Fill(this.dataSet1.ODS_JH_EMAIL);
                }
                catch (Exception e)
                {
                    myUI(e.Message, rTB_message);
                }
            }
        }
        public delegate void myUICallBack(string myStr, Control ctl);
        public void myUI(string myStr, Control ctl)
        {
            if (this.InvokeRequired)
            {
                myUICallBack myUpdate = new myUICallBack(myUI);
                this.Invoke(myUpdate, myStr, ctl);
            }
            else
            {
                ctl.Text += myStr;
            }
        }


        private void btn_sendmail_Click(object sender, EventArgs e)
        {

            Thread t = new Thread(Do_job);
            t.IsBackground = true;
            t.Start();

            System.Threading.Thread.Sleep(5000);

            Thread t2 = new Thread(Filldv);
            t2.IsBackground = true;
            t2.Start();
        }

        //匯出LOG
        private void ExportLog(RichTextBox tTB)
        {
            //string log = @"C:\log.txt";
            string log = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\email_log\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_LOG.txt";
            using (FileStream fs = new FileStream(log, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(tTB.Text);
                    tTB.Text = "";
                }
            }
            tTB.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":已匯出LOG檔，請至程式根目錄底下查看。\n";
        }

        //寄發e-mail排程
        public void Do_job()
        {
            OleDbConnection conn;
            OleDbCommand cmd;
            OleDbDataReader dr;
            string v_TO_addr = "";

            v_sqlstr = "SELECT  O.TO_ADDR,O.CONTENT, O.SUBJECT,  O.CREATE_DATE,  " +
                        "   O.CREATE_EMP, O.STATUS,O.SN " +
                        "FROM ODS_JH_EMAIL O " +
                        "WHERE STATUS = 'N' AND ROWNUM <= 10 ";
            try
            {
                conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jh815Connection);
                conn.Open();
                cmd = new OleDbCommand(v_sqlstr, conn);
                dr = cmd.ExecuteReader();


                //迴圈
                //寫入Oracle
                //更新Sql資料狀態
                while (dr.Read())
                {
                    try
                    {
                        v_TO_addr = dr[0].ToString().Replace(";", ",");

                        //將第一個字是分號、逗號都刪除
                        if (v_TO_addr.Substring(0, 1).Equals(";") || v_TO_addr.Substring(0, 1).Equals(","))
                            v_TO_addr = v_TO_addr.Substring(1, v_TO_addr.Length - 1);
                        //將最後一字是分號、逗號都刪除
                        if (v_TO_addr.Substring(v_TO_addr.Length - 1, 1).Equals(";") || v_TO_addr.Substring(v_TO_addr.Length - 1, 1).Equals(","))
                            v_TO_addr = v_TO_addr.Substring(0, v_TO_addr.Length - 1);


                        //發送e-mail       收件者,標題,內容
                        ts_conn.Send_Mail2(v_TO_addr.ToString(), dr[1].ToString(), dr[2].ToString());

                        v_sqlstr = "UPDATE ODS_JH_EMAIL SET STATUS = 'Y' ,MODIFY_DATE = SYSDATE  WHERE SN =" + dr[6].ToString();

                        ts_conn.trans_oleDb(v_sqlstr, "update");

                        //rTB_message.Text += "[成功]序號：" + dr[6].ToString() + "。已經寄送至" + v_TO_addr.ToString() + "-" + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " \n";
                        myUI("[成功]序號：" + dr[6].ToString() + "。已經寄送至" + v_TO_addr.ToString() + "-" + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " \n", rTB_message);

                        //Application.DoEvents();
                        System.Threading.Thread.Sleep(500);
                    }
                    catch (Exception ex)
                    {
                        //rTB_message.Text += "[失敗]序號：" + dr[6].ToString() + "。無法寄送。" + v_TO_addr.ToString()+"。" + ex.ToString() + "-" + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " \n";
                        myUI("[失敗]序號：" + dr[6].ToString() + "。無法寄送。" + v_TO_addr.ToString() + "。" + ex.Message + "-" + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " \n", rTB_message);
                    }
                }
                dr.Close();
                conn.Close();
                //全部寄完才更新
                //Refresh_DV();

            }
            catch (Exception ex)
            {

                rTB_message.Text += ex.ToString() + " \n";

            }
            finally
            {

            }
        }


       

    }
}
