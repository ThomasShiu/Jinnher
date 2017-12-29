using System;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using System.Configuration;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.IO;

namespace AUTO_SCHE.CLASS
{
    
    class thomas_class
    {
        SqlDataAdapter s_adp;
        SqlConnection s_conn;
        SqlCommand s_cmd;
        SqlDataReader s_dr;

        //OracleDataAdapter adp;
        //OracleConnection conn;
        //OracleCommand cmd;
        //OracleDataReader dr;

        OleDbDataAdapter ole_adp;
        OleDbConnection ole_conn;
        OleDbCommand ole_cmd;
        OleDbDataReader ole_dr;

        string v_sql;
        static string ConnStr = "jheip"; //連線字串
        static string ConnStr815 = "jh815"; //連線字串

         public void thomas_Conn()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    public void conn_mssql(string v_DataSource, string v_ID, string v_Password)
    {
        string v_conn_str;
        v_conn_str = "Data Source=" + v_DataSource + ";Persist Security Info=True;User ID=" + v_ID + ";Password=" + v_Password + ";";

        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlConnection conn = new SqlConnection(v_conn_str);

    }
    public void conn_mssql()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }
    public void conn_access()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }
    public static string GetLocalIp()
    {
        System.Net.IPHostEntry ipEntry =  Dns.GetHostByAddress(Environment.MachineName);

        foreach (System.Net.IPAddress ip in ipEntry.AddressList)
        {
            if (System.Net.IPAddress.IsLoopback(ip) == false)
                return ip.ToString();
        }

        return System.Net.IPAddress.Loopback.ToString();
    }

    public void trans_MSsql( string v_cmd, string v_ddl, RichTextBox v_rTB)
    {

        //建立連線
        //使用web.config conn string
        s_adp = new SqlDataAdapter();
        s_conn = new SqlConnection(AUTO_SCHE.Properties.Settings.Default.jh10gConnection);
        s_conn.Open();


        s_cmd = new SqlCommand(v_cmd, s_conn);

        try
        {

            switch (v_ddl)
            {
                case "select":
                    s_adp.SelectCommand = s_cmd;
                    s_adp.SelectCommand.ExecuteNonQuery();
                    break;

                case "insert":

                    s_adp.InsertCommand = s_cmd;
                    s_adp.InsertCommand.ExecuteNonQuery();
                    //v_rTB.Text += "新增資料成功";
                    break;

                case "update":
                    s_adp.UpdateCommand = s_cmd;
                    s_adp.UpdateCommand.ExecuteNonQuery();
                    //v_rTB.Text += "更新資料成功";
                    break;

                case "delete":
                    s_adp.DeleteCommand = s_cmd;
                    s_adp.DeleteCommand.ExecuteNonQuery();
                    //v_rTB.Text += "刪除資料成功";
                    break;

                default:
                    break;
            }



        }
        catch (Exception ex)
        {
            v_rTB.Text += ex.Message;

        }
        finally
        {
            s_cmd.Dispose();
            s_adp.Dispose();
            s_conn.Close();
        }

        //建立資料集
        //DataSet ds = new DataSet();
        //ora_adp.Fill(ds, "");
    }

    public void trans_oleDbjherp(string v_cmd, string v_ddl)
    {

        //建立連線
        //使用web.config conn string
        //Data Source=jh815;User Id=eagle;Password=shefalls;
        //Data Source=jh815;Persist Security Info=True;User ID=eagle;Password=shefalls;Unicode=True
        ole_adp = new OleDbDataAdapter();
        ole_conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jherpdbeip);
        ole_conn.Open();


        ole_cmd = new OleDbCommand(v_cmd, ole_conn);


        try
        {
            switch (v_ddl)
            {
                case "select":
                    ole_adp.SelectCommand = ole_cmd;
                    ole_adp.SelectCommand.ExecuteNonQuery();
                    break;

                case "insert":

                    ole_adp.InsertCommand = ole_cmd;
                    ole_adp.InsertCommand.ExecuteNonQuery();
                    break;

                case "update":
                    ole_adp.UpdateCommand = ole_cmd;
                    ole_adp.UpdateCommand.ExecuteNonQuery();
                    break;

                case "delete":
                    ole_adp.DeleteCommand = ole_cmd;
                    ole_adp.DeleteCommand.ExecuteNonQuery();
                    break;

                default:
                    break;
            }



        }
        catch (Exception ex)
        {
            string v_message = ex.ToString();
            ole_cmd.Dispose();
            ole_adp.Dispose();
            ole_conn.Close();

        }
        finally
        {
            ole_cmd.Dispose();
            ole_adp.Dispose();
            ole_conn.Close();
        }

        //建立資料集
        //DataSet ds = new DataSet();
        //adp.Fill(ds, "");
    }

    public void trans_oleDb(string v_cmd, string v_ddl)
    {

        //建立連線
        //使用web.config conn string
        //Data Source=jh815;User Id=eagle;Password=shefalls;
        //Data Source=jh815;Persist Security Info=True;User ID=eagle;Password=shefalls;Unicode=True
        ole_adp = new OleDbDataAdapter();
        ole_conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jh815Connection);
        ole_conn.Open();


        ole_cmd = new OleDbCommand(v_cmd, ole_conn);


        try
        {
            switch (v_ddl)
            {
                case "select":
                    ole_adp.SelectCommand = ole_cmd;
                    ole_adp.SelectCommand.ExecuteNonQuery();
                    break;

                case "insert":

                    ole_adp.InsertCommand = ole_cmd;
                    ole_adp.InsertCommand.ExecuteNonQuery();
                    break;

                case "update":
                    ole_adp.UpdateCommand = ole_cmd;
                    ole_adp.UpdateCommand.ExecuteNonQuery();
                    break;

                case "delete":
                    ole_adp.DeleteCommand = ole_cmd;
                    ole_adp.DeleteCommand.ExecuteNonQuery();
                    break;

                default:
                    break;
            }



        }
        catch (Exception ex)
        {
            string v_message = ex.ToString();
            ole_cmd.Dispose();
            ole_adp.Dispose();
            ole_conn.Close();

        }
        finally
        {
            ole_cmd.Dispose();
            ole_adp.Dispose();
            ole_conn.Close();
        }

        //建立資料集
        //DataSet ds = new DataSet();
        //adp.Fill(ds, "");
    }


    //紀錄LOG
    public void save_log(string v_userid, string v_content, string v_ip, string v_memo)
    {

        //建立連線
        //使用web.config conn string
        ole_adp = new OleDbDataAdapter();
        ole_conn = new OleDbConnection("Data Source=jh815;Persist Security Info=True;User ID=eagle;Password=shefalls;Unicode=True");
        ole_conn.Open();

        string v_cmd = "INSERT INTO ODS_JH_LOG(SN,LOG_CONTENT,LOG_DATE,LOG_IP,USER_ID,LOG_MEMO) " +
                       "VALUES(ODS_JH_LOG_SQ.NEXTVAL,'" + v_content + "',SYSDATE,'" + v_ip + "','" + v_userid + "','" + v_memo + "')";
        ole_cmd = new OleDbCommand(v_cmd, ole_conn);
        try
        {
            ole_adp.InsertCommand = ole_cmd;
            ole_adp.InsertCommand.ExecuteNonQuery();

        }
        catch
        {
            ole_cmd.Dispose();
            ole_adp.Dispose();
            ole_conn.Close();

        }
        finally
        {
            ole_cmd.Dispose();
            ole_adp.Dispose();
            ole_conn.Close();
        }

        //建立資料集
        //DataSet ds = new DataSet();
        //adp.Fill(ds, "");
    }

    //紀錄LOG
    public void save_log(string v_userid, string v_content, string v_ip)
    {

        //建立連線
        //使用web.config conn string
        ole_adp = new OleDbDataAdapter();
        ole_conn = new OleDbConnection("Data Source=jh815;Persist Security Info=True;User ID=eagle;Password=shefalls;Unicode=True");
        ole_conn.Open();

        string v_cmd = "INSERT INTO ODS_JH_LOG(SN,LOG_CONTENT,LOG_DATE,LOG_IP,USER_ID,LOG_MEMO) " +
                       "VALUES(ODS_JH_LOG_SQ.NEXTVAL,'" + v_content + "',SYSDATE,'" + v_ip + "','" + v_userid + "','')";
        ole_cmd = new OleDbCommand(v_cmd, ole_conn);
        try
        {
            ole_adp.InsertCommand = ole_cmd;
            ole_adp.InsertCommand.ExecuteNonQuery();

        }
        catch
        {
            ole_cmd.Dispose();
            ole_adp.Dispose();
            ole_conn.Close();

        }
        finally
        {
            ole_cmd.Dispose();
            ole_adp.Dispose();
            ole_conn.Close();
        }

        //建立資料集
        //DataSet ds = new DataSet();
        //adp.Fill(ds, "");
    }

   


    
    public void Send_Mail(string T_email, string v_body, string v_subject)
    {
        // 's_from 發件箱地址
        //'pwd 發件箱密碼
        //' s_to 收件箱地主之誼
        //'m_title 郵件主題
        //'m_body 郵件內容
        //'m_file 附件
        MailAddress from;
        MailAddress mto;
        MailMessage mailobj;


        //string[] v_mailto = { T_email, "k0095@edd.e-united.com.tw" };

        //string[] v_mailto = { "skylark@edaskylark.com.tw","k0095@edd.e-united.com.tw" };

        // '构建SmtpClient对象
        SmtpClient smtp = new SmtpClient();
        smtp.Credentials = new System.Net.NetworkCredential("mis", "jinnher9801");
        //smtp.Host = "smtp.edamall.com.tw";
        smtp.Host = "jinnher.com.tw";
        //smtp.Host = "smtp.edaskylark.com.tw";
        //smtp.Host = "pop3.e-united.com.tw";
        //smtp.Host = "smtp.edathemepark.com.tw";
        //smtp.Host = "smtp.edaskylark.com.tw";
        //SmtpClient smtp = new SmtpClient("msa.hinet.net");
        //smtp.UseDefaultCredentials = false;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;


        // '构建MailMessage对象

        from = new MailAddress("呷粗霸<dinbandon@e-united.com.tw>");// '發件箱地址
        mto = new MailAddress(T_email);// '收件箱地址
        mailobj = new MailMessage(from, mto);

        // '完善MailMessage对象
        mailobj.Subject = v_subject;  //'主題

        mailobj.Body = v_body;
        mailobj.IsBodyHtml = true;
        mailobj.BodyEncoding = System.Text.Encoding.GetEncoding("BIG5");
        mailobj.Priority = System.Net.Mail.MailPriority.Normal;

        //if (m_file.Trim() != "")// '附件
        //{
        //    mailobj.Attachments.Add(new System.Net.Mail.Attachment(m_file));
        //}
        try
        {
            smtp.Send(mailobj);
        }
        catch
        {
            //string v_clientIP = HttpContext.Current.Request.UserHostAddress.ToString();
            //string v_cmd = "insert into order_log values(order_log_sq.nextval,sysdate,'send mail failed!" + T_email + "','" + v_clientIP + "')";
            //trans_oracle(v_cmd, "insert");
        }
        //}
        //return "已發送E-mail通知!";


    }

    public void Send_Mail2(string T_email, string v_body, string v_subject)
    {
        // 's_from 發件箱地址
        //'pwd 發件箱密碼
        //' s_to 收件箱地主之誼
        //'m_title 郵件主題
        //'m_body 郵件內容
        //'m_file 附件
        MailAddress from;
        MailAddress mto;
        MailMessage mailobj;


        //分解字串
        char[] delimiterChars = { ';', ',' };
        string[] v_email_addr = T_email.Trim().Split(delimiterChars);

        // '构建SmtpClient对象
        SmtpClient smtp = new SmtpClient();
        smtp.Credentials = new System.Net.NetworkCredential("system", "Rup4ck66229801");
        smtp.Host = "mail.jinnher.com.tw";
        //smtp.Host = "smtp.edamall.com.tw";
        //smtp.Host = "192.168.10.200";
        //smtp.Host = "smtp.edaskylark.com.tw";
        //smtp.Host = "pop3.e-united.com.tw";
        //SmtpClient smtp = new SmtpClient("msa.hinet.net");
        //smtp.UseDefaultCredentials = false;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

        //for (int i = 0; i < v_mailto.Count(); i++)
        //{
        // '构建MailMessage对象

        

        //if (m_file.Trim() != "")// '附件
        //{
        //    mailobj.Attachments.Add(new System.Net.Mail.Attachment(m_file));
        //}

        try
        {
            for (int i = 0; i <= v_email_addr.Length - 1; i++)
            {

                from = new MailAddress("System<system@jinnher.com.tw>");// '發件箱地址
                //mto = new MailAddress(v_mailto[i]);// '收件箱地址
                //mto = new MailAddress(T_email);// '收件箱地址
                mto = new MailAddress(v_email_addr[i]);// '收件箱地址
                mailobj = new MailMessage(from, mto);
                //mailobj.Bcc.Add("thomas@jinnher.com.tw");
                // '完善MailMessage对象
                mailobj.Subject = v_subject;  //'主題
                mailobj.Body = v_body + "<br><br><br>***本郵件是由系統自動寄送，請勿直接回覆 (晉禾資訊室 #115)***";
                mailobj.IsBodyHtml = true;
                mailobj.BodyEncoding = System.Text.Encoding.GetEncoding("BIG5");
                mailobj.Priority = System.Net.Mail.MailPriority.Normal;
                smtp.Send(mailobj);
            }

        }
        catch (Exception ex)
        {

            string v_error = ex.ToString();
            string v_clientIP = GetLocalIp();
            ////string v_cmd = "insert into order_log values(order_log_sq.nextval,sysdate,'send mail failed!"+T_email+"','" + v_clientIP + "')";
            //string v_cmd = "INSERT INTO ODS_JH_LOG(SN,LOG_CONTENT,LOG_DATE,LOG_IP,USER_ID,LOG_MEMO) " +
            //              "VALUES(ODS_JH_LOG_SQ.NEXTVAL,'send mail failed!" + T_email + "',SYSDATE,'" + v_clientIP + "','Guest','系統發信失敗')";
            //trans_oracle(v_cmd, "insert");
        }
    }
    public string ToBase64(string instr)
    {
        byte[] bt = Encoding.GetEncoding("utf-8").GetBytes(instr);
        return Convert.ToBase64String(bt);
    } 
        //匯出LOG文字檔
    public void ExportLog(RichTextBox v_TB, string v_LogName)
    {
        //string log = @"C:\log.txt";
        string log = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Log\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + v_LogName + ".txt";
        using (FileStream fs = new FileStream(log, FileMode.Create))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(v_TB.Text);
                sw.Close();
                sw.Dispose();
            }
        }
        v_TB.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":已匯出LOG檔，請至程式根目錄底下查看。";
    }
    //判斷是否有此權限
    //public bool chkUserGrant(string v_userid, string v_pid)
    //{
    //    string v_sql = "SELECT T_KEY FROM ODS_EDW_PROGRAM_GRANT WHERE USER_ID = :USER_ID AND P_ID = :P_ID ";
    //    //建立連線
    //    //使用web.config conn string
    //    conn = new OracleConnection("Data Source=jh815;Persist Security Info=True;User ID=eagle;Password=shefalls;Unicode=True");
    //    conn.Open();

    //    cmd = new OracleCommand(v_sql, conn);
    //    cmd.Parameters.Add(new OracleParameter("USER_ID", OracleDbType.Varchar2, 10));
    //    cmd.Parameters["USER_ID"].Value = v_userid;
    //    cmd.Parameters.Add(new OracleParameter("P_ID", OracleDbType.Varchar2, 100));
    //    cmd.Parameters["P_ID"].Value = v_pid;


    //    try
    //    {
    //        //bool v_return = false;

    //        dr = cmd.ExecuteReader();

    //        if (!dr.Read())
    //        {
    //            cmd.Dispose();
    //            conn.Close();
    //            return false;

    //        }
    //        else
    //        {
    //            cmd.Dispose();
    //            conn.Close();
    //            return true;
    //        }

    //    }
    //    catch (Exception)
    //    {
    //        cmd.Dispose();
    //        conn.Close();

    //        return false;

    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        conn.Close();

    //    }
    //}

    ////判斷是否有資料存在
    //public bool chkDataExist(string v_sql)
    //{
    //    //string v_sql = "SELECT T_KEY FROM ODS_EDW_PROGRAM_GRANT WHERE USER_ID = :USER_ID AND P_ID = :P_ID ";
    //    //建立連線
    //    //使用web.config conn string
    //    conn = new OracleConnection("Data Source=jh815;Persist Security Info=True;User ID=eagle;Password=shefalls;Unicode=True");
    //    conn.Open();
    //    cmd = new OracleCommand(v_sql, conn);

    //    try
    //    {
    //        //bool v_return = false;

    //        dr = cmd.ExecuteReader();

    //        if (!dr.Read())
    //        {
    //            cmd.Dispose();
    //            conn.Close();
    //            return false;

    //        }
    //        else
    //        {
    //            cmd.Dispose();
    //            conn.Close();
    //            return true;
    //        }

    //    }
    //    catch (Exception)
    //    {
    //        cmd.Dispose();
    //        conn.Close();

    //        return false;

    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        conn.Close();

    //    }
    //}
    //public string get_Encrypt_pwd(string v_pwd)
    //{
    //    string v_password = "";

    //    //呼叫ORACLE PACKAGE加密函式
    //    using (OracleConnection objConn = new OracleConnection("Data Source=jh815;Persist Security Info=True;User ID=eagle;Password=shefalls;Unicode=True"))
    //    {
    //        OracleCommand objCmd = new OracleCommand();
    //        objCmd.Connection = objConn;
    //        objCmd.CommandText = "PKG_DES.ENCRYPT";
    //        objCmd.CommandType = CommandType.StoredProcedure;
    //        objCmd.Parameters.Add("I_chaPlainText", OracleDbType.Varchar2, 64).Value = v_pwd;
    //        objCmd.Parameters.Add("return_value", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.ReturnValue;

    //        try
    //        {
    //            objConn.Open();


    //            objCmd.ExecuteNonQuery();
    //            v_password = objCmd.Parameters["return_value"].Value.ToString();
    //            return v_password;
    //        }
    //        catch (Exception ex)
    //        {
    //            string v_Leabel = ex.Message;
    //            return v_password;
    //        }
    //        finally
    //        {
    //            objConn.Close();
    //        }
    //    }
    //}

    //public string get_Decrypt_pwd(string v_pwd)
    //{
    //    string v_password = "";

    //    //呼叫ORACLE PACKAGE加密函式
    //    using (OracleConnection objConn = new OracleConnection("Data Source=jh815;Persist Security Info=True;User ID=eagle;Password=shefalls;Unicode=True"))
    //    {
    //        OracleCommand objCmd = new OracleCommand();
    //        objCmd.Connection = objConn;
    //        objCmd.CommandText = "PKG_DES.DECRYPT";
    //        objCmd.CommandType = CommandType.StoredProcedure;
    //        objCmd.Parameters.Add("I_chaPlainText", OracleDbType.Varchar2, 64).Value = v_pwd;
    //        objCmd.Parameters.Add("return_value", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.ReturnValue;

    //        try
    //        {
    //            objConn.Open();


    //            objCmd.ExecuteNonQuery();
    //            v_password = objCmd.Parameters["return_value"].Value.ToString();
    //            return v_password;
    //        }
    //        catch (Exception ex)
    //        {
    //            string v_Leabel = ex.Message;
    //            return v_password;
    //        }
    //        finally
    //        {
    //            objConn.Close();
    //        }
    //    }
    //}

   
    }
}
