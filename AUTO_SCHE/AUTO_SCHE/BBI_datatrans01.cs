using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AUTO_SCHE.CLASS;
using System.IO;
using System.Web;
 

namespace AUTO_SCHE
{
    

    public partial class BBI_datatrans01 : Form
    {
        string v_sql;
        thomas_class ts_conn = new thomas_class();
        PDAWebService.PDAServiceForVendorSoapClient ws = new PDAWebService.PDAServiceForVendorSoapClient();
        public BBI_datatrans01()
        {
            InitializeComponent();
        }
        

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void BBI_datatrans01_Load(object sender, EventArgs e)
        {

        }

        private void CB_timerStart_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_timerStart.Checked)
            {
                timer1.Start();

            }
            else
            {
                timer1.Stop();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string v_EMP_NO, v_CREATE_DATE, v_BARCODE_1, v_BARCODE_2, v_BBI_SO, v_Error;
            v_Error = "";
            //從SQL SERVER 取回資料集
            v_sql = "SELECT EMP_NO,CONVERT(char(20), CREATE_DATE, 20) CREATE_DATE,BARCODE_1,BARCODE_2,BBI_SO,STATUS FROM BBI_SHIP_QUEUE WHERE STATUS = 'N' ";


            string sConnection = "Data Source=192.168.0.9,1433;Initial Catalog=db;User ID=sa;Password=jhjolihi";
            //string sConnection = "Data Source=192.168.0.9,1433;Initial Catalog=db;Persist Security Info=True;User ID=sa;Password=jhjolihi";

            SqlCommand comm = new SqlCommand(v_sql, new SqlConnection(sConnection));
            SqlDataReader dr = null;
 

            try
            {
                comm.Connection.Open();
                dr = comm.ExecuteReader();

                //迴圈
                //寫入Oracle
                //更新Sql資料狀態
                while (dr.Read())
                {
                    try
                    {
                        //dr[0] EMP_NO
                         v_EMP_NO = dr[0].ToString().Replace("\n", "").Replace("\r", "");
                        //dr[1] CREATE_DATE
                         v_CREATE_DATE = dr[1].ToString().Replace("\n", "").Replace("\r", "");
                        //dr[2] BARCODE_1
                         v_BARCODE_1 = dr[2].ToString().Replace("%QP6", "").Replace("\n", "").Replace("\r", "");
                        //dr[3] BARCODE_2
                         v_BARCODE_2 = dr[3].ToString().Substring(0, 8).Replace("%QP6", "").Replace("\n", "").Replace("\r", "");
                        //dr[4] BBI_SO
                         v_BBI_SO = dr[4].ToString().Replace("\n", "").Replace("\r", "");
                        //dr[5] STATUS

                        string v_create_date = dr[1].ToString();
                        //檢查STOCK_ID是否正確
                        if (Chk_Stock_id(v_BBI_SO, v_BARCODE_2))
                        {
                            
                            v_sql = "INSERT INTO BBI_SHIP_QUEUE(SN,EMP_NO,CREATE_DATE,BARCODE_1,BARCODE_2,BBI_SO,STATUS) ";
                            v_sql += "VALUES(BBI_SHIP_QUEUE_SEQ.NEXTVAL,TRIM('" + v_EMP_NO + "'),to_date('" + v_CREATE_DATE + "','YYYY-MM-DD HH24:MI:SS'),TRIM('" + v_BARCODE_1 + "'),TRIM('" + v_BARCODE_2 + "'),TRIM('" + v_BBI_SO + "'),'N') ";
                            ts_conn.trans_oleDb(v_sql, "insert");

                            rTB_log.Text += "資料新增成功：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":" + v_BARCODE_1 + "-" + v_BARCODE_2 + "\n";
                            rTB_log.ScrollToCaret();

                            //更新MS SQL 上資料的狀態，避免一直處理
                            v_sql = "UPDATE BBI_SHIP_QUEUE SET STATUS = 'Y' WHERE BARCODE_1 = '" + dr[2].ToString() + "' ";
                            ts_conn.trans_MSsql(v_sql, "update", rTB_log);
                        }
                        else
                        {
                            v_sql = "INSERT INTO BBI_SHIP_QUEUE_ERR(SN,EMP_NO,CREATE_DATE,BARCODE_1,BARCODE_2,BBI_SO,STATUS) ";
                            v_sql += "VALUES(BBI_SHIP_QUEUE_ERR_SEQ.NEXTVAL,TRIM('" + v_EMP_NO + "'),to_date('" + v_CREATE_DATE + "','YYYY-MM-DD HH24:MI:SS'),TRIM('" + v_BARCODE_1 + "'),TRIM('" + v_BARCODE_2 + "'),TRIM('" + v_BBI_SO + "'),'N') ";
                            ts_conn.trans_oleDb(v_sql, "insert");

                            rTB_log.Text += "PAC NO 錯誤：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":" + v_BBI_SO + "-" + v_BARCODE_1 + "-" + v_BARCODE_2 + "\n";
                            rTB_log.ScrollToCaret();

                            //寫入MS SQL錯誤紀錄
                            //v_sql2 = "INSERT INTO BBI_SHIP_QUEUE_ERR(EMP_NO,CREATE_DATE,BARCODE_1,BARCODE_2,BBI_SO,STATUS) ";
                            //v_sql2 += "VALUES('" + v_EMP_NO + "','" + v_CREATE_DATE + "','" + v_BARCODE_1 + "','" + v_BARCODE_2 + "','" + v_BBI_SO + "','N') ";
                            //ts_conn.trans_MSsql(v_sql2, "insert", rTB_log);

                            v_Error +=  DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":\n" + v_BBI_SO + "-" + v_BARCODE_1 + "-" + v_BARCODE_2 + "\n";

                        }

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());
                        rTB_log.Text += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":" + ex.ToString()+ "\n";
                        rTB_log.ScrollToCaret();
                    }
                }

                if (!v_Error.Equals(""))
                {
                    v_Error = "<br>資料比對錯誤：" + v_Error;
                    v_Error += "<br>請檢查PAC NO ,STOCK ID，並提供資訊室正確資料以利修正。";
                    ts_conn.Send_Mail2("sharon@jinnher.com.tw,thomas@jinnher.com.tw", v_Error, "BBI轉檔異常通知-PAC NO、STOCK ID不符");
                    //ts_conn.Send_Mail2("sharon@jinnher.com.tw", v_Error, "BBI轉檔異常通知-PAC NO、STOCK ID不符");
                }
                //MessageBox.Show("資料新增完成");
                //dr = comm.ExecuteReader();
                //rTB_log.Text += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":資料新增完成\n";

                //while (dr.Read())
                //    cboCountries.Items.Add(dr[0]);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                rTB_log.Text += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") +":"+ ex.Message + " \n";
                rTB_log.ScrollToCaret();
                return;
            }
            dr.Close();
            dr.Dispose();
            comm.Connection.Close();
            comm.Connection.Dispose();

            if (DateTime.Now.ToString("HHmm")=="2355")
            {
                ExportLog();
                //ts_conn.ExportLog(rTB_log,"BBI_READY_MILL");
                
            }
        }

        private bool Chk_Stock_id(string v_pac_no,string v_stock_id)
        {
            bool v_true_false =false;

            //OleDbDataAdapter ole_adp;
            OleDbConnection ole_conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jh815Connection); ;
            OleDbCommand ole_cmd;
            OleDbDataReader ole_dr = null;

            int v_cnt = 0;

            try
            {

                //判斷有無排出貨資料
                v_sql = "select count(*) as cnt from packings p, listitems i where p.pac_id = I.pac_id and p.pac_no = '"+ v_pac_no +"' and i.stock_id = '"+ v_stock_id +"' ";
                ole_cmd = new OleDbCommand(v_sql, ole_conn);
                ole_cmd.Connection.Open();
                ole_dr = ole_cmd.ExecuteReader();

                ole_dr.Read();
                v_cnt = Convert.ToInt16(ole_dr[0].ToString());
                ole_cmd.Connection.Close();
                ole_dr.Dispose();


                if (v_cnt > 0)
                {
                    v_true_false =  true;
                }
                else
                {
                    v_true_false =  false;
                }
            }

            catch (Exception err)
            {

                //MessageBox.Show(String.Format("{0}", err.Message), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                rTB_log2.Text += "資料比對錯誤：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "," + err.Message + "\n";
                rTB_log2.ScrollToCaret();
            }
            return v_true_false;
        }
        private void clear_btn_Click(object sender, EventArgs e)
        {
            //ExportLog();
            ts_conn.ExportLog(rTB_log, "BBI_READY_MILL");
            rTB_log.Text = "";
        }

        private void ExportLog()
        {
            //string log = @"C:\log.txt";
            string log = System.IO.Path.GetDirectoryName(Application.ExecutablePath)+"\\BBI_log\\"+DateTime.Now.ToString("yyyyMMddHHmmss")+"_LOG.txt";
            using (FileStream fs = new FileStream(log, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(rTB_log.Text);
                    rTB_log.Text = "";
                }
            }
            rTB_log.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":已匯出LOG檔，請至程式根目錄底下查看。\n" ;
            rTB_log.ScrollToCaret();
        }

        private void btn_savelog_Click(object sender, EventArgs e)
        {
            ExportLog();
        }

        //進行Web Service上傳至BBI主機
        private void BBI_WS_upload()
        {
            OleDbDataAdapter ole_adp;
            OleDbConnection ole_conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jh815Connection); ;
            OleDbCommand ole_cmd;
            OleDbDataReader ole_dr = null;

            int v_cnt = 0;

            try
            {
       
                //判斷有無未上傳的資料
                    v_sql = "SELECT COUNT(*) FROM BBI_SHIP_QUEUE WHERE STATUS = 'N' ";
                    ole_cmd = new OleDbCommand(v_sql, ole_conn);
                    ole_cmd.Connection.Open();
                    ole_dr = ole_cmd.ExecuteReader();

                    ole_dr.Read();
                    v_cnt = Convert.ToInt16(ole_dr[0].ToString());
                    ole_cmd.Connection.Close();
                    ole_dr.Dispose();


                    if (v_cnt > 0)
                    {
                        DataTable PDA_DataTable = new DataTable("PDA_Data");

                        DataSet PDA_DataSet = new DataSet();

                        //PDAWebService.PDAServiceForVendorSoapClient ws1 = new PDAWebService.PDAServiceForVendorSoapClient();
                        PDAWebService.Result ExeResult = new PDAWebService.Result();

                        int row = 0;
                        string SQL = "SELECT trantype,prepno,sono,item,fromMaker,toMaker,palletNo,rstat,adduser,addtime,chguser,chgtime,barcode,seqno,tdate,lotno FROM BBI_SHIP_QUEUE_V " +
                            " UNION ALL SELECT trantype,prepno,sono,item,fromMaker,toMaker,palletNo,rstat,adduser,addtime,chguser,chgtime,barcode,seqno,tdate,lotno FROM BBI_SHIP_QUEUE_V2";

                        ole_cmd = new OleDbCommand(SQL, ole_conn);
                        ole_adp = new OleDbDataAdapter(ole_cmd);

                        ole_conn.Open();

                        ole_adp.Fill(PDA_DataTable);

                        PDA_DataSet.Tables.Add(PDA_DataTable);

                        //呼叫Web Service上傳資料

                        string ParamXML = String.Format("<Parameters SQLTableName=\"{0}\" MakerID=\"{1}\" UploadTime=\"{2}\"></Parameters>", "VendorTransDetl", "J250", DateTime.Now);

                        ExeResult = ws.UploadData(PDA_DataSet, ParamXML);

                        row = ExeResult.RowCount;

                        if (ExeResult.success)
                        {
                            //上傳成功的話要做的事...
                            //rTB_log2.Text += "資料上成功：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")  + "\n";
                            v_sql = "UPDATE BBI_SHIP_QUEUE SET STATUS = 'Y'  ";
                            ts_conn.trans_oleDb(v_sql, "update");
                        }

                        else if ((ExeResult.success && row == 0) || !ExeResult.success)
                        {

                            //MessageBox.Show(String.Format("{0}", ExeResult.message), "Web Service Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            rTB_log2.Text += "資料上傳錯誤：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\n";
                            rTB_log2.ScrollToCaret();
                        }

                        PDA_DataTable.Clear();

                        PDA_DataSet.Tables.Remove(PDA_DataTable);


                        PDA_DataTable.Dispose();

                        PDA_DataSet.Dispose();

                        ole_conn.Close();
                        ole_adp.Dispose();

                        if ((ExeResult.success && row == 0 && ExeResult.message.Length > 0) || !ExeResult.success)
                        {
                            MessageBox.Show(String.Format("Web Service:{0}", ExeResult.message), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            rTB_log2.Text += "資料上傳錯誤：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "," + ExeResult.message + "\n";
                            rTB_log2.ScrollToCaret();
                        }
                        else
                        {
                            //MessageBox.Show("資料上傳作業已完成!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            rTB_log2.Text += "資料上傳作業已完成：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\n";
                            rTB_log2.ScrollToCaret();
                        }
                    }
            }

            catch (Exception err)
            {

                //MessageBox.Show(String.Format("{0}", err.Message), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                rTB_log2.Text += "資料上傳錯誤：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "," + err.Message + "\n";
                rTB_log2.ScrollToCaret();
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            BBI_WS_upload();
        }

        private void CB_timerStart2_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_timerStart2.Checked)
            {
                timer2.Start();

            }
            else
            {
                timer2.Stop();
                timer2.Dispose();
            }
        }

        private void clear_btn2_Click(object sender, EventArgs e)
        {
            rTB_log2.Text = "";
        }
    }
}
