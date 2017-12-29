using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using AUTO_SCHE.CLASS;

namespace AUTO_SCHE
{
    public partial class WIRES_trans : Form
    {
        thomas_class ts_conn = new thomas_class();
        string v_sqlstr;

        public WIRES_trans()
        {
            InitializeComponent();
        }

        private void WIRES_trans_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'dataSet1.WIRES_TRANS_QUEUE' 資料表。您可以視需要進行移動或移除。
            this.wIRES_TRANS_QUEUETableAdapter.Fill(this.dataSet1.WIRES_TRANS_QUEUE);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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
            MethodInvoker mi = new MethodInvoker(Do_job);
            this.BeginInvoke(mi);

            //每天午夜轉出LOG
            if (DateTime.Now.ToString("HHmm") == "2330")
            {
                ExportLog(rTB_message);

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Refresh_DV();
        }

        //重整Dataview資料
        private void Refresh_DV()
        {
            this.wIRES_TRANS_QUEUETableAdapter.Fill(this.dataSet1.WIRES_TRANS_QUEUE);
            dataGridView1.DataSource = wIRESTRANSQUEUEBindingSource;
            dataGridView1.AutoResizeColumns();
        }

        //MS SQL 轉 ORACLE
        private void Do_job()
        {
            string v_wireId;

            SqlConnection conn;
            SqlCommand cmd;
            SqlDataReader dr;
            

            //撈出MS SQL的資料
            v_sqlstr = "SELECT  WIRE_ID, TRANS_DATE, TRANS_EMP, LOCATION " +
                       " FROM   WIRES_TRANS_QUEUE " +
                        "WHERE  STATUS = 'N' ";

            conn = new SqlConnection(AUTO_SCHE.Properties.Settings.Default.jh10gConnection);
            conn.Open();
            cmd = new SqlCommand(v_sqlstr, conn);
            dr = cmd.ExecuteReader();

            try
            {
                //迴圈
                //寫入Oracle
                //更新Sql資料狀態
                while (dr.Read())
                {
                    v_wireId = dr[0].ToString().Replace("\n", "").Replace("\r", "");

                    try
                    {
                        
                        v_sqlstr = "INSERT INTO WIRES_TRANS_QUEUE( SN, WIRE_ID, TRANSFER_DATE,TRANSFER_EMP, LOCATION, STATUS) "+
                                   " VALUES(WIRES_TRANS_QUEUE_SEQ.NEXTVAL,'" + v_wireId + "',to_date('" + dr[1].ToString() + "','YYYY/MM/DD HH24:MI:SS'),'" + dr[2].ToString() + "','" + dr[3].ToString() + "','N') ";
                        ts_conn.trans_oleDb(v_sqlstr,"insert");

                        v_sqlstr = "UPDATE WIRES_TRANS_QUEUE SET STATUS = 'Y' WHERE WIRE_ID = '" + dr[0].ToString() + "' AND STATUS = 'N' ";
                        ts_conn.trans_MSsql(v_sqlstr,"insert",rTB_message);

                        rTB_message.Text += "[成功]盤元編號：" + v_wireId + "。" + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " \n";
                        Refresh_DV();
                    }
                    catch (Exception ex)
                    {
                        rTB_message.Text += "[失敗]盤元編號：" + v_wireId + "。無法匯入ORACLE。" + ex.ToString() + " \n";
                    }
                }
                dr.Close();
                conn.Close();


            }
            catch (Exception ex)
            {

                rTB_message.Text += ex.ToString() + " \n";

            }
            finally
            {

            }
        }

        //匯出LOG
        private void ExportLog(RichTextBox tTB)
        {
            //string log = @"C:\log.txt";
            string log = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\wires_log\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_LOG.txt";
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

        private void button1_Click(object sender, EventArgs e)
        {
            MethodInvoker mi = new MethodInvoker(Do_job);
            this.BeginInvoke(mi);


        }
    }
}
