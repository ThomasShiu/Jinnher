using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using System.Net;
using System.Threading;
using System.Data.OleDb;
using AUTO_SCHE.CLASS;

namespace AUTO_SCHE
{
    public partial class CSC_SOE3_IMP : Form
    {
        //中鋼WebService網址
        static string m_strWS = "https://cscqs01.csc.com.tw/QS_HOME/QSWS/QSAUW0.ashx";
        static string[] m_strDelim = { "$$$" };
        //static string OrdItem = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static string OrdItem = "ZYXWVUTSRQPONMLKJIHGFEDCBA";
        string v_ord;
        string[] v_str1;
        string v_sql, v_sql_title, v_sql_content, v_DateTime;
        string v_col_1_mail, v_col_23, v_col_23_mail;
        int v_cnt2 = 0;
        StreamReader reader;
        Stream respStream;
        OleDbDataReader dr1 = null;
        thomas_class ts_conn = new thomas_class();
        OleDbDataAdapter adp = new OleDbDataAdapter();
  
        OleDbConnection conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.JHERPDB);
        OleDbCommand cmd ;
        OleDbDataReader dr = null;
        //thomas_function ts_func = new thomas_function();

        public CSC_SOE3_IMP()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                timer1.Start();
                //richTextBox1.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":TIMER啟動\r\n";
                myUI(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":TIMER啟動\r\n", richTextBox1);
                //toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":TIMER啟動中，每10秒執行";
            }
            else
            {
                timer1.Stop();
                //richTextBox1.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":TIMER停止\r\n";
                myUI(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":TIMER停止\r\n", richTextBox1);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.ToString("HH:mm") == tB_hhmm.Text | DateTime.Now.ToString("HH:mm") == tB_hhmm2.Text)
            {
                richTextBox1.Text = "";
                Thread t = new Thread(ImpCSC_SOE3);
                t.IsBackground = true;
                t.Start();

            }
            //每天午夜轉出LOG
            if (DateTime.Now.ToString("HHmm") == "2359")
            {
                richTextBox1.Text = "";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            Thread t = new Thread(ImpCSC_SOE3);
            t.IsBackground = true;
            t.Start();
        }
        public void ImpCSC_SOE3()
        {
            //下載txt並轉檔
            string CustNo;
            string FilePath;
            string Ord;
            //string Header;

            Ord = "LW5354W";
            CustNo = "88680066";
            FilePath = ".\\SOE3_Data.txt";
            v_col_23_mail = "";
            //string rtn_msg;
            ArrayList LoadNo_list = new ArrayList();
            //依裝車編號查詢鋼捲明細
            //rtn_msg = GetOrderItem(CustNo, Ord, FilePath);
            conn.Open();
            OleDbDataReader dr1 = Get_dr();
            if (dr1.HasRows )
            {
                while (dr1.Read())
                {
                    Ord = dr1[0].ToString();
                    for (int j = 0; j < OrdItem.Length; j++)
                    {
                        string v_ord = Ord + OrdItem.Substring(j, 1);
                        GetOrderItem(CustNo, v_ord);
                        System.Threading.Thread.Sleep(100);

                    }

                }
            }
            conn.Close();
            dr.Close();
            dr1.Close();

            if (!v_col_23_mail.Equals(""))
            {
                v_col_23_mail = "底下中鋼分合約項次有異動，請盤元採購人員依據最新分合約更新採購單，謝謝~<br><br>" + v_col_23_mail;
                //發送e-mail       收件者,標題,內容
                ts_conn.Send_Mail2("mis@jinnher.com.tw", v_col_23_mail, "[通知]盤元採購合約異動 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                ts_conn.Send_Mail2("jenny@jinnher.com.tw", v_col_23_mail, "[通知]盤元採購合約異動 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                //ts_conn.Send_Mail2("noah2513@jinnher.com.tw", v_col_23_mail, "[通知]盤元採購合約異動 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            //myUI(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + rtn_msg + "\r\n", richTextBox1);
        }

        //**************************************************
        //* 依提單查詢鋼捲明細資料
        //**************************************************
        public void  GetOrderItem(string CustNo, string Ord)
        {
            string strParams = "";
            string rtn_msg = "";
            string rtnStus;
            //FileInfo fi = null;

            //Console.WriteLine("Url= " + m_strWS);

            try
            {
                //開啟目標檔案
                //fi = new FileInfo(FilePath);
                //StreamWriter wrtDest = new StreamWriter(fi.FullName, false, System.Text.Encoding.GetEncoding("Big5"));
                int nLoadCnt = 0;
                strParams = "CUSTNO=" + CustNo + "&INFO=SOE3" + "&ORD=" + Ord + "&FORMAT=T";
                //Console.WriteLine("Params= " + strParams);
                //呼叫 Web Service
                Stream respStream = CallWebService(m_strWS, strParams);
                //拆解取得SOE3資料字串
                StreamReader reader = new StreamReader(respStream, System.Text.Encoding.Default);

                //寫入資料庫
                //insert_SOE3(reader);

                string strLine = "";
               

                while (reader.EndOfStream == false)
                {
                    strLine = reader.ReadLine();
                    if (strLine.Substring(0, 2) == "//")
                    {
                        rtn_msg = strLine.Substring(2);
                        rtnStus = strLine.Split(m_strDelim, StringSplitOptions.None)[1];
                        if (rtnStus == "*")
                        {
                            //Console.WriteLine(rtn_msg);
                            //logfile.writeLog(rtn_msg);
                            myUI(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":" + rtn_msg + "\r\n", richTextBox1);
                        }
                        else
                        {
                            //Console.WriteLine(rtn_msg);
                            myUI(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":" + rtn_msg + "\r\n", richTextBox1);
                        }
                    }
                    else
                    {

                        string[] v_Body = strLine.Split(m_strDelim, StringSplitOptions.None);


                        if (v_Body.Length == 53)
                        {
                            //開始新增資料
                            v_ord = v_Body[5].ToString().Trim() ;
                            //myUI(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":訂單號 " + v_Body[5].ToString() + "\r\n", richTextBox1);
                        }
                        else
                        {

                            //myUI(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":訂單號 " + v_ord + v_Body[2].ToString().Trim() + "," + v_Body[29].ToString().Trim() + "\r\n", richTextBox1);
                            v_DateTime = System.DateTime.Now.ToString("yyyyMMdd");

                            if (Convert.ToDecimal(v_Body[29].ToString().Trim()) >= Convert.ToDecimal(v_DateTime))
                            {
                                myUI(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":近期異動之訂單號 " + v_ord + v_Body[2].ToString().Trim() + "," + v_Body[29].ToString().Trim() + "\r\n", richTextBox1);
                                v_col_23_mail += "訂單號 " + v_ord + v_Body[2].ToString() + ",異動日:" + v_Body[29].ToString().Trim()+"<br>";
                                
                            }
                        }

                        nLoadCnt++;
                    }
                }
                reader.Dispose();
                
              
            }
            catch (Exception e)
            {
                //reader.Dispose();
                string v_ex = e.ToString();
                //throw e;
            }
        }
        //**************************************************
        //* 呼叫Web Service
        //**************************************************
        public static  Stream CallWebService(string url, string postdata)
        {
            HttpWebRequest httpReq = null;
            HttpWebResponse httpResp = null;

            System.Uri httpURL = new Uri(url);

            try
            {
                //以WebRequest.Create(httpURL)取得HttpWebRequest物件
                httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
                //設定GET或POST
                if (postdata != "")
                {
                    httpReq.Method = "POST";
                }
                else
                {
                    httpReq.Method = "GET";
                }
                httpReq.KeepAlive = false;
                Console.WriteLine("Request Method= " + httpReq.Method);
                //設定ContentType
                httpReq.ContentType = "application/x-www-form-urlencoded";

                //若為POST將參數寫在BODY內
                if (httpReq.Method == "POST")
                {
                    StreamWriter mgStreamWriter = new StreamWriter(httpReq.GetRequestStream(), System.Text.Encoding.GetEncoding("Big5"));
                    mgStreamWriter.Write(postdata);
                    mgStreamWriter.Close();
                }
                //發出HTTP REQUEST 並取得回應
                httpResp = (HttpWebResponse)httpReq.GetResponse();
            }
            catch (WebException e)
            {
                string v_ex = e.ToString();
                throw e;
                    
                //using (WebResponse response = e.Response)
                //{
                //    HttpWebResponse httpResponse = (HttpWebResponse)response;
                //    //Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                //    myUI(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":Error code: " + httpResponse.StatusCode + "\r\n", richTextBox1);
                //    using (Stream data = response.GetResponseStream())
                //    {
                //        string text = new StreamReader(data).ReadToEnd();
                //        //Console.WriteLine(text);
                //        myUI(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + text + "\r\n", richTextBox1);
                //    }
                //}
            }
            catch (Exception ex)
            {
                string v_ex = ex.ToString();
                //myUI(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + ex.ToString() + "\r\n", richTextBox1);
            }
            //將回應的資料以STRING方式暫存
            Stream respStream = httpResp.GetResponseStream();
            return respStream;
        }

        private delegate void myUICallBack(string myStr, Control ctl);
        private void myUI(string myStr, Control ctl)
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

     
        
        private OleDbDataReader Get_dr()
        {
            v_sql = "SELECT  M.VEN_ORDE_NO ";
            v_sql += "FROM PUR_DRW_ORDE_M M ";
            v_sql += "WHERE M.VEN_ORDE_NO IS NOT NULL ";
            v_sql += "AND M.YYYY_SEC >= (SELECT TO_CHAR(SYSDATE,'YYYY')||'0'||TO_CHAR(SYSDATE-7,'Q') FROM DUAL)  ";
            v_sql += "GROUP BY M.VEN_ORDE_NO ";


            
            OleDbCommand cmd = new OleDbCommand(v_sql, conn);
            try
            {
                dr = cmd.ExecuteReader();
                
            }
            catch (Exception)
            {
                //richtb_status.Text += "Insert data:" + ex.ToString() + "。" + v_machset + "。" + line.ToString() + "\r\n";
                //myUI("Insert data:" + ex.ToString() + "。" + v_machset + "。" + line.ToString() + "\r\n", richtb_status);
                cmd.Dispose();
                adp.Dispose();
                //conn.Close();
            }
            finally
            {
                cmd.Dispose();
                adp.Dispose();
                //conn.Close();
            }

           
            return dr;
        }

       
    }
}
