using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenPop.Mime;
using OpenPop.Mime.Header;
using OpenPop.Pop3;
using OpenPop.Pop3.Exceptions;
using OpenPop.Common.Logging;
using Message = OpenPop.Mime.Message;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace AUTO_SCHE
{
    public partial class ScanMail_Auto : Form
    {
        private readonly Dictionary<int, Message> messages = new Dictionary<int, Message>();
        private readonly Pop3Client pop3Client;

        static string CRM_SALES_EMAIL = "SELECT  EMP_NO, EMP_NAME, EMAIL_ADDR, LOGIN_ID, PASSWORD FROM CRM_SALES_EMAIL WHERE STOP_YN =  'N' ORDER BY 1";

        OleDbDataAdapter adp = null;
        OleDbConnection conn = null;
        OleDbCommand cmd = null;
        OleDbDataReader dr = null;
        OleDbDataAdapter myAdapter = null;
        DataSet ds = null;
        DataTable dt = null;

        //string v_sql;
        int v_cnt;
        int totalSuccess = 0;

        private void ScanMail_Auto_Load(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(DateTime.Now.ToString("yyyyMMdd")) >= 20160808)
            {
                Application.Exit();
            }
        }

        public ScanMail_Auto()
        {
            InitializeComponent();
            FillGrid_MailList();
            DefaultLogger.SetLog(new FileLogger());

            // Enable file logging and include verbose information
            FileLogger.Enabled = true;
            FileLogger.Verbose = true;

            pop3Client = new Pop3Client();

            //讀取設定檔
            // This is only for faster debugging purposes
            // We will try to load in default values for the hostname, port, ssl, username and password from a file
            string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string file = Path.Combine(myDocs, "OpenPopLogin.txt");
            if (File.Exists(file))
            {
                using (StreamReader reader = new StreamReader(File.OpenRead(file)))
                {
                    // This describes how the OpenPOPLogin.txt file should look like
                    popServerTextBox.Text = reader.ReadLine(); // Hostname
                    portTextBox.Text = reader.ReadLine(); // Port
                    useSslCheckBox.Checked = bool.Parse(reader.ReadLine() ?? "true"); // Whether to use SSL or not
                    //loginTB.Text = reader.ReadLine(); // Username
                    //passwordTB.Text = reader.ReadLine(); // Password
                }
            }

        }

        private OleDbDataReader GetDr()
        {
            try
            {
                adp = new OleDbDataAdapter();
                conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jh815Connection);
                conn.Open();
                cmd = new OleDbCommand(CRM_SALES_EMAIL, conn);
                dr = cmd.ExecuteReader();
            }
            catch
            {
                dr = null;
            }
            return dr;
        }

        
         private void startScheCB_CheckedChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "排程啟動:" + DateTime.Now.ToString("hh:mm:ss");
            toolStripStatusLabel2.Text = "倒數:" + (Convert.ToInt16(timerValTB.Text) - v_cnt);
            if (startScheCB.Checked)
                timer1.Start();
            else
                timer1.Stop();
        }

        //顯示要偵測的email清單
         private void FillGrid_MailList()
         {
             conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jh815Connection);
             myAdapter = new OleDbDataAdapter();

             ds = new DataSet();

             myAdapter.SelectCommand = new OleDbCommand(CRM_SALES_EMAIL, conn);
             myAdapter.Fill(ds, "CRM_SALES_EMAIL");
             dt = ds.Tables["CRM_SALES_EMAIL"];
             this.dataGridView1.DataSource = dt;

         }
        private void connectAndRetrieveButton_Click(object sender, System.EventArgs e)
        {
            toolStripStatusLabel1.Text = "掃描執行中:" + DateTime.Now.ToString("hh:mm:ss");
            ReceiveMails();
        }

        //計時器啟動
        private void timer1_Tick(object sender, EventArgs e)
        {
            v_cnt++;
            toolStripStatusLabel2.Text = "倒數:" + (Convert.ToInt16(timerValTB.Text) - v_cnt);
            if (v_cnt == Convert.ToInt16(timerValTB.Text))
            {
                ReceiveMails();
                v_cnt = 0;
            }
           
        }

        private void ReceiveMails()
        {
            //取得昨天的日期
            string v_yesterday = DateTime.Now.AddDays(-2).ToString("yyyy/MM/dd");
            //取得今天的日期
            string v_today = DateTime.Now.AddHours(-6).ToString("yyyy/MM/dd");

            // Disable buttons while working
            connectAndRetrieveButton.Enabled = false;
            //uidlButton.Enabled = false;
            progressBar.Value = 0;
            string v_sender,v_mailTo,v_subject, v_attachment;
            
            //讀取CRM_SALES_EMAIL 帳號、密碼
            //EMP_NO 0, EMP_NAME 1, EMAIL_ADDR 2, LOGIN_ID 3, PASSWORD 4
            OleDbDataReader dr2 = GetDr();

            //沒有EMAIL清單，就跳出
            if (!dr2.HasRows) return;


            try
            {
                totalSuccess = 0;

                while (dr2.Read())
                {
                    if (pop3Client.Connected) pop3Client.Disconnect();
                    pop3Client.Connect(popServerTextBox.Text, int.Parse(portTextBox.Text), useSslCheckBox.Checked);
                    pop3Client.Authenticate(dr2[3].ToString(), dr2[4].ToString());
                    int count = pop3Client.GetMessageCount();
                    counterLabel.Text = count.ToString();
                    IDlabel.Text = dr2[3].ToString();
                    nameLabel.Text = dr2[1].ToString();
                    messages.Clear();
                    mailListTB.Clear();

                    int success = 0;
                    int fail = 0;
                    for (int i = count; i >= 1; i -= 1)
                    {
                        
                        // Check if the form is closed while we are working. If so, abort
                        if (IsDisposed)
                            return;

                        // Refresh the form while fetching emails
                        // This will fix the "Application is not responding" problem
                        Application.DoEvents();

                        try
                        {
                            
                            Message message = pop3Client.GetMessage(i);

                            success++;

                            //寄件者
                            v_sender = message.Headers.From.ToString().Replace("'", "");
                            if (v_sender.Trim().Equals("")) v_sender = "no sender";
                            if (v_sender.Length > 200) v_sender = v_sender.Substring(0, 200);

                            //收件者資料
                            v_mailTo = "";
                            foreach (RfcMailAddress to in message.Headers.To)
                            {
                                if (v_mailTo.Trim().Equals("")) v_mailTo = to.ToString(); else v_mailTo += ";" + to.ToString();
                            }
                            v_mailTo = v_mailTo.Replace("'","");
                            if (v_mailTo.Equals("")) v_mailTo = "no recevie";
                            if (v_mailTo.Length > 100) v_mailTo = v_mailTo.Substring(0, 100);

                            //主旨
                            v_subject = Regex.Replace(message.Headers.Subject, @"<[^>]*>", "NO SUBJECT");
                            v_subject = v_subject.Replace("'", "").Replace("&", " and ");
                            if (v_subject.Trim().Equals("")) v_subject = "no subject";
                            if (v_subject.Length > 500) v_subject = v_subject.Substring(0, 500);

                            //附件資料
                            v_attachment = "";
                            List<MessagePart> attachments = message.FindAllAttachments();
                            foreach (MessagePart attachment in attachments)
                            {
                                if (v_attachment.Equals("")) v_attachment = attachment.FileName; else v_attachment += ";" + attachment.FileName;
                            }
                            if (v_attachment.Length > 200) v_attachment = v_attachment.Substring(0, 200);
                            //v_attachment = v_attachment.Replace("'", "");
                            bool hadAttachments = attachments.Count > 0;
                            if (hadAttachments == false) v_attachment = "";
                            
                            //接收日
                            string v_receiveDate = message.Headers.DateSent.ToString("yyyy/MM/dd");

                            //比對日期，今天的郵件才寫入資料庫
                            DateTime recevDate = DateTime.ParseExact(v_receiveDate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                            DateTime beforeDate = DateTime.ParseExact(v_yesterday, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                            if (recevDate >= beforeDate)
                            //if (v_recevice_date.Equals(v_today))
                            {
                                //寫入資料庫
                                
                                
                                //檢查是否已有資料
                                if (chk_data_exist(v_sender, v_mailTo, v_subject, v_receiveDate))
                                {
                                    bool v_flag = insertDB(dr2[0].ToString(), dr2[1].ToString(), v_sender, v_mailTo, v_subject,v_attachment, v_receiveDate);

                                    //有成功寫入資料庫才紀錄
                                    if (v_flag)
                                    {
                                        totalSuccess++;
                                        mailListTB.Text += dr2[1].ToString() + "," + v_sender + "," + v_subject + "," + v_receiveDate + "\r\n";
                                    }
                                }
                            }
                            counter2Label.Text = success.ToString();
                            counter3Label.Text = totalSuccess.ToString();

                        }
                        catch (Exception e)
                        {
                            //工號、姓名、帳號、密碼、郵件編號
                            HeadersFromAndSubject(dr2[0].ToString(), dr2[1].ToString(), dr2[3].ToString(), dr2[4].ToString(), i);
                            //errorTB.Text += "sn:"+i.ToString()+","+e.Message +  "\r\n";
                            DefaultLogger.Log.LogError(
                                "TestForm: Message fetching failed: " + e.Message + "\r\n" +
                                "Stack trace:\r\n" +
                                e.StackTrace);
                            fail++;
                            System.Threading.Thread.Sleep(100);
                        }

                        progressBar.Value = (int)(((double)(count - i) / count) * 100);
                        counter4Label.Text = fail.ToString();
                    }

                    //MessageBox.Show(this, "Mail received!\nSuccesses: " + success + "\nFailed: " + fail, "Message fetching done");
                    //counter3Label.Text = fail.ToString();
                }
            }
            catch (InvalidLoginException)
            {
                errorTB.Text += "POP3 Server Authentication:The server did not accept the user credentials!\r\n";
                //MessageBox.Show(this, "The server did not accept the user credentials!", "POP3 Server Authentication");
            }
            catch (PopServerNotFoundException)
            {
                errorTB.Text += "POP3 Retrieval。The server could not be found.\r\n";
                //MessageBox.Show(this, "The server could not be found", "POP3 Retrieval");
            }
            catch (PopServerLockedException)
            {
                errorTB.Text += "POP3 Account Locked。The mailbox is locked. It might be in use or under maintenance. Are you connected elsewhere?\r\n";
                //MessageBox.Show(this, "The mailbox is locked. It might be in use or under maintenance. Are you connected elsewhere?", "POP3 Account Locked");
            }
            catch (LoginDelayException)
            {
                errorTB.Text += "POP3 Account Login Delay。Login not allowed. Server enforces delay between logins. Have you connected recently?\r\n";
                //MessageBox.Show(this, "Login not allowed. Server enforces delay between logins. Have you connected recently?", "POP3 Account Login Delay");
            }
            catch (Exception)
            {
                
                errorTB.Text += "POP3 Retrieval。Error occurred retrieving mail.\r\n ";
                //MessageBox.Show(this, "Error occurred retrieving mail. " + e.Message, "POP3 Retrieval");
            }
            finally
            {
                dr2.Close();
                conn.Close();
                toolStripStatusLabel1.Text = "掃描已結束:" + DateTime.Now.ToString("hh:mm:ss");
                // Enable the buttons again
                connectAndRetrieveButton.Enabled = true;
                //uidlButton.Enabled = true;
                progressBar.Value = 100;
            }
        }

        public void HeadersFromAndSubject(string vEmpno,string vEmpName,string username, string password, int messageNumber)
        {
            try
            {
                //取得昨天的日期
                string v_yesterday = DateTime.Now.AddDays(-2).ToString("yyyy/MM/dd");
                //取得今天的日期
                string v_today = DateTime.Now.AddHours(-6).ToString("yyyy/MM/dd");

                string v_sender, v_mailTo, v_subject;

                if (pop3Client.Connected) pop3Client.Disconnect();
                pop3Client.Connect(popServerTextBox.Text, int.Parse(portTextBox.Text), useSslCheckBox.Checked);
                pop3Client.Authenticate(username, password);
                int count = pop3Client.GetMessageCount();
                //string v_sender, v_mailTo, v_subject, v_attachment;

                // We want to check the headers of the message before we download
                // the full message
                MessageHeader headers = pop3Client.GetMessageHeaders(messageNumber);

                //寄件者
                v_sender = headers.From.ToString().Replace("'", "");
                if (v_sender.Trim().Equals("")) v_sender = "no sender";
                if (v_sender.Length > 200) v_sender = v_sender.Substring(0, 200);

                //收件者資料
                v_mailTo = "";
                foreach (RfcMailAddress to in headers.To)
                {
                    if (v_mailTo.Trim().Equals("")) v_mailTo = to.ToString(); else v_mailTo += ";" + to.ToString();
                }
                v_mailTo = v_mailTo.Replace("'", "");
                if (v_mailTo.Equals("")) v_mailTo = "no recevie";
                if (v_mailTo.Length > 100) v_mailTo = v_mailTo.Substring(0, 100);

                //主旨
                v_subject = Regex.Replace(headers.Subject, @"<[^>]*>", "NO SUBJECT");
                v_subject = v_subject.Replace("'", "").Replace("&", " and ");
                if (v_subject.Trim().Equals("")) v_subject = "no subject";
                if (v_subject.Length > 500) v_subject = v_subject.Substring(0, 500);

                //接收日
                string v_receiveDate = headers.DateSent.ToString("yyyy/MM/dd");

                //errorTB.Text += v_sender + "," + v_mailTo + "," + v_subject + "\r\n ";

                //比對日期，今天的郵件才寫入資料庫
                DateTime recevDate = DateTime.ParseExact(v_receiveDate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime beforeDate = DateTime.ParseExact(v_yesterday, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                if (recevDate >= beforeDate)
                {
                    //檢查是否已有資料
                    if (chk_data_exist(v_sender, v_mailTo, v_subject, v_receiveDate))
                    {
                        bool v_flag = insertDB(vEmpno, vEmpName, v_sender, v_mailTo, v_subject, "", v_receiveDate);

                        //有成功寫入資料庫才紀錄
                        if (v_flag)
                        {
                            totalSuccess++;
                            mailListTB.Text += vEmpName + "," + v_sender + "," + v_subject + "," + v_receiveDate + "\r\n";
                        }
                    }
                }
                // Download the full message
                //Message message = pop3Client.GetMessage(messageNumber);

                // We know the message contains an attachment with the name "useful.pdf".
                // We want to save this to a file with the same name

            }
            catch (Exception)
            {
                errorTB.Text += "編碼錯誤:" + vEmpName + "," + messageNumber + "\r\n ";
            }
        }
        private void mailListTB_TextChanged(object sender, EventArgs e)
        {
            mailListTB.SelectionStart = mailListTB.TextLength;

            // Scrolls the contents of the control to the current caret position.
            mailListTB.ScrollToCaret();
        }

        private void errorTB_TextChanged(object sender, EventArgs e)
        {
            errorTB.SelectionStart = errorTB.TextLength;

            // Scrolls the contents of the control to the current caret position.
            errorTB.ScrollToCaret();
        }



        private bool insertDB(string v_emp_no,string v_emp_name,string v_sender,string v_receive,string v_subject,string vattachfile,string v_receviedate)
        {
            string v_insert;
            v_insert = "INSERT INTO CRM_MAILLIST_M(SN, EMP_NO, EMP_NAME, SENDER_ADDRESS, RECEIVER, SUBJECT,  ATTACH_FILE, RECEIVER_DATE) ";
            v_insert += " VALUES(CRM_MAILLIST_M_SEQ.NEXTVAL, :EMP_NO,:EMP_NAME,:SENDER_ADDRESS,:RECEIVER,:SUBJECT,:ATTACH_FILE,to_date(:RECEIVER_DATE,'YYYY/MM/DD'))";

            //v_insert = "INSERT INTO CRM_MAILLIST_M(SN, EMP_NO, EMP_NAME, SENDER_ADDRESS, RECEIVER, SUBJECT,  ATTACH_FILE, RECEIVER_DATE) ";
            //v_insert += " VALUES(CRM_MAILLIST_M_SEQ.NEXTVAL, '" + v_emp_no + "','" + v_emp_name + "','" + v_sender + "','" + v_receive + "','" + v_subject + "','" + vattachfile + "',to_date('" + v_receviedate + "','YYYY/MM/DD'))";
            try
            {
                adp = new OleDbDataAdapter();
                conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jh815Connection);
                conn.Open();
                cmd = new OleDbCommand(v_insert, conn);

                cmd.Parameters.Add(new OleDbParameter("EMP_NO", OleDbType.VarChar, 100));
                cmd.Parameters["EMP_NO"].Value = v_emp_no;
                cmd.Parameters.Add(new OleDbParameter("EMP_NAME", OleDbType.VarChar, 100));
                cmd.Parameters["EMP_NAME"].Value = v_emp_name;

                cmd.Parameters.Add(new OleDbParameter("SENDER_ADDRESS", OleDbType.VarChar, 200));
                cmd.Parameters["SENDER_ADDRESS"].Value = v_sender;
                cmd.Parameters.Add(new OleDbParameter("RECEIVER", OleDbType.VarChar, 500));
                cmd.Parameters["RECEIVER"].Value = v_receive;
                cmd.Parameters.Add(new OleDbParameter("SUBJECT", OleDbType.VarChar, 1000));
                cmd.Parameters["SUBJECT"].Value = v_subject;
                cmd.Parameters.Add(new OleDbParameter("ATTACH_FILE", OleDbType.VarChar, 200));
                cmd.Parameters["ATTACH_FILE"].Value = vattachfile;
                cmd.Parameters.Add(new OleDbParameter("RECEIVER_DATE", OleDbType.VarChar, 100));
                cmd.Parameters["RECEIVER_DATE"].Value = v_receviedate;

            }
            catch (Exception e)
            {
                errorTB.Text += e.ToString() + "\r\n";
            }
            bool v_flag = false;

            try
            {
                adp.InsertCommand = cmd;
                adp.InsertCommand.ExecuteNonQuery();
                v_flag = true;
            }
            catch (Exception e)
            {
                //richtb_status.Text += "Insert data:" + ex.ToString() + "。" + v_machset + "。" + line.ToString() + "\r\n";
                //myUI("Insert data:" + ex.ToString() + "。" + v_machset + "。" + line.ToString() + "\r\n", richtb_status);
                cmd.Dispose();
                adp.Dispose();
                conn.Close();
                errorTB.Text += e.Message + "..." + v_sender + " ; " + v_receive + " ; " + v_subject + " ; " + v_receviedate + "\r\n";
                v_flag = false;
            }
            finally
            {
                cmd.Dispose();
                adp.Dispose();
                conn.Close();
            }

            return v_flag;
        }

        private bool chk_data_exist(string v_SENDER_ADDRESS,string v_RECEIVER,string v_SUBJECT,string v_RECEIVER_DATE)
        {
            //SENDER_ADDRESS, RECEIVER, SUBJECT,RECEIVER_DATE
            //bool v_flag = false;
            string v_sql = "SELECT COUNT(SN) FROM CRM_MAILLIST_M " +
                " WHERE SENDER_ADDRESS = :SENDER_ADDRESS  AND  RECEIVER = :RECEIVER AND  SUBJECT = :SUBJECT AND TO_CHAR(RECEIVER_DATE,'YYYY/MM/DD') = :RECEIVER_DATE  ";
            int v_dr ;
            bool v_return = false;
            try
            {
                adp = new OleDbDataAdapter();
                conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jh815Connection);
                conn.Open();
                cmd = new OleDbCommand(v_sql, conn);
                cmd.Parameters.Add(new OleDbParameter("SENDER_ADDRESS", OleDbType.VarChar, 200));
                cmd.Parameters["SENDER_ADDRESS"].Value = v_SENDER_ADDRESS;
                cmd.Parameters.Add(new OleDbParameter("RECEIVER", OleDbType.VarChar, 500));
                cmd.Parameters["RECEIVER"].Value = v_RECEIVER;
                cmd.Parameters.Add(new OleDbParameter("SUBJECT", OleDbType.VarChar, 1000));
                cmd.Parameters["SUBJECT"].Value = v_SUBJECT;
                cmd.Parameters.Add(new OleDbParameter("RECEIVER_DATE", OleDbType.VarChar, 100));
                cmd.Parameters["RECEIVER_DATE"].Value = v_RECEIVER_DATE;

                dr = cmd.ExecuteReader();

                dr.Read();
                v_dr = Convert.ToInt16(dr[0]);

                if (v_dr > 0)
                {
                    //errorTB.Text +=  v_SENDER_ADDRESS + " ; " + v_RECEIVER + " ; " + v_SUBJECT + " ; " + v_RECEIVER_DATE + "\r\n";
                    v_return= false; //已存在資料
                }
                else
                {
                    //errorTB.Text += v_SENDER_ADDRESS + " ; " + v_RECEIVER + " ; " + v_SUBJECT + " ; " + v_RECEIVER_DATE + "\r\n";
                    v_return= true; //沒資料才執行
                }

            }
            catch (Exception e)
            {


                errorTB.Text += e.ToString() + "..." + v_SENDER_ADDRESS + " ; " + v_RECEIVER + " ; " + v_SUBJECT + " ; " + v_RECEIVER_DATE + "\r\n";
            }
            finally
            {
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }

            return v_return;
        }

        

       
    }
}
