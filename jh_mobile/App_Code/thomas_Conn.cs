using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Net.Mail;

/// <summary>
/// thomas_Conn 的摘要描述
/// </summary>
public class thomas_Conn
{
	 OracleDataAdapter adp;
    OracleConnection conn;
    OracleCommand cmd;
    OracleDataReader dr;
    string v_sql;
    static string ConnStr = "jh815"; //連線字串

    public thomas_Conn()
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

        public void trans_oracle(string v_cmd, string v_ddl, Label v_Leabel)
        {
            v_Leabel.Text = "";
            try
            {
            //建立連線
            //使用web.config conn string
            adp = new OracleDataAdapter();
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString);
            conn.Open();

            
            cmd = new OracleCommand(v_cmd, conn);
            

            
                switch (v_ddl)
                {
                    case "select":
                        adp.SelectCommand = cmd;
                        adp.SelectCommand.ExecuteNonQuery();
                        break;

                    case "insert":

                        adp.InsertCommand = cmd;
                        adp.InsertCommand.ExecuteNonQuery();
                        v_Leabel.Text = "新增資料成功";
                        break;

                    case "update":
                        adp.UpdateCommand = cmd;
                        adp.UpdateCommand.ExecuteNonQuery();
                        v_Leabel.Text = "更新資料成功";
                        break;

                    case "delete":
                        adp.DeleteCommand = cmd;
                        adp.DeleteCommand.ExecuteNonQuery();
                        v_Leabel.Text = "刪除資料成功";
                        break;

                    default:
                        break;
                }
               

                
            }
            catch (Exception ex)
            {
                v_Leabel.Text = ex.Message;

            }
            finally
            {
                cmd.Dispose();
                adp.Dispose();
                conn.Close();
            }

            //建立資料集
            //DataSet ds = new DataSet();
            //adp.Fill(ds, "");
        }

        public void trans_oracle_inv(string v_cmd, string v_ddl, Label v_Leabel)
        {
            try
            {
                //建立連線
                //使用web.config conn string
                adp = new OracleDataAdapter();
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings["tp"].ConnectionString);
                conn.Open();


                cmd = new OracleCommand(v_cmd, conn);



                switch (v_ddl)
                {
                    case "select":
                        adp.SelectCommand = cmd;
                        adp.SelectCommand.ExecuteNonQuery();
                        break;

                    case "insert":

                        adp.InsertCommand = cmd;
                        adp.InsertCommand.ExecuteNonQuery();
                        break;

                    case "update":
                        adp.UpdateCommand = cmd;
                        adp.UpdateCommand.ExecuteNonQuery();
                        break;

                    case "delete":
                        adp.DeleteCommand = cmd;
                        adp.DeleteCommand.ExecuteNonQuery();
                        break;

                    default:
                        break;
                }



            }
            catch (Exception ex)
            {
                v_Leabel.Text = ex.Message;

            }
            finally
            {
                cmd.Dispose();
                adp.Dispose();
                conn.Close();
            }

            //建立資料集
            //DataSet ds = new DataSet();
            //adp.Fill(ds, "");
        }

        public  void trans_oracle(string v_cmd, string v_ddl)
        {
            try
            {
            //建立連線
            //使用web.config conn string
            adp = new OracleDataAdapter();
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString);
            conn.Open();


            cmd = new OracleCommand(v_cmd, conn);


           
                switch (v_ddl)
                {
                    case "select":
                        adp.SelectCommand = cmd;
                        adp.SelectCommand.ExecuteNonQuery();
                        break;

                    case "insert":

                        adp.InsertCommand = cmd;
                        adp.InsertCommand.ExecuteNonQuery();
                        break;

                    case "update":
                        adp.UpdateCommand = cmd;
                        adp.UpdateCommand.ExecuteNonQuery();
                        break;

                    case "delete":
                        adp.DeleteCommand = cmd;
                        adp.DeleteCommand.ExecuteNonQuery();
                        break;

                    default:
                        break;
                }



            }
            catch
            {
                cmd.Dispose();
                adp.Dispose();
                conn.Close();

            }
            finally
            {
                cmd.Dispose();
                adp.Dispose();
                conn.Close();
            }

            //建立資料集
            //DataSet ds = new DataSet();
            //adp.Fill(ds, "");
        }


        //紀錄LOG
        public void save_log(string v_userid, string v_content, string v_ip,string v_memo)
        {
            try
            {
                //建立連線
                //使用web.config conn string
                adp = new OracleDataAdapter();
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jheip"].ConnectionString);
                conn.Open();

                string v_cmd = "INSERT INTO ODS_JH_LOG(SN,LOG_CONTENT,LOG_DATE,LOG_IP,USER_ID,LOG_MEMO) " +
                               "VALUES(ODS_JH_LOG_SQ.NEXTVAL,'" + v_content + "',SYSDATE,'" + v_ip + "','" + v_userid + "','" + v_memo + "')";
                cmd = new OracleCommand(v_cmd, conn);

                adp.InsertCommand = cmd;
                adp.InsertCommand.ExecuteNonQuery();

            }
            catch
            {
                //cmd.Dispose();
                adp.Dispose();
                conn.Close();

            }
            finally
            {
                //cmd.Dispose();
                adp.Dispose();
                conn.Close();
            }

            //建立資料集
            //DataSet ds = new DataSet();
            //adp.Fill(ds, "");
        }

      

        //取得會員資料
        public string Get_member_data(string v_id, string v_pw,string v_mode )
        {

           
           v_sql = "SELECT USER_ID,USER_NAME,USER_PASSWD FROM ODS_JH_PWD WHERE USER_ID =  :USER_ID  AND USER_PASSWD = :USER_PASSWD ";
               
            try
            {
            //建立連線
            //使用web.config conn string
            //conn = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
            conn.Open();

            cmd = new OracleCommand(v_sql, conn);
            cmd.Parameters.Add(new OracleParameter("USER_ID", OracleType.VarChar, 10));
            cmd.Parameters["USER_ID"].Value = v_id;
            cmd.Parameters.Add(new OracleParameter("USER_PASSWD", OracleType.Char, 32));
            cmd.Parameters["USER_PASSWD"].Value = get_Encrypt_pwd(v_pw);

            string v_user = "";
            //string v_email = "";
            

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    switch (v_mode)
                    {
                        case "id":
                            {
                                v_user = dr["USER_ID"].ToString();
                                dr.Close();
                                conn.Close();
                                return v_user;

                            }
                        case "name":
                            {
                                v_user = dr["USER_NAME"].ToString();
                                dr.Close();
                                conn.Close();
                                return v_user;

                            }
                        
                        default:
                            {
                                return "";

                            }

                    }

                }
                return v_user;

            }
            catch (Exception)
            {
                return "";

            }
            finally
            {
                cmd.Dispose();
                conn.Close();

            }

        }


        //取得月份天數
        public int Get_Month_day(string v_date)
        {

            v_sql = "SELECT COUNT(O.WORK_DATE) AS DAY_COUNT FROM EDW.ODS_EDW_WORKDATE O WHERE TO_CHAR(WORK_DATE,'YYYY/MM/DD') LIKE '" + v_date + "%' ";
          
            try
            {
                //建立連線
                //使用web.config conn string
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings["edwbi"].ConnectionString);
                conn.Open();

                cmd = new OracleCommand(v_sql, conn);

                int v_DayCount = 0;
                //string v_email = "";


                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    v_DayCount = Convert.ToInt16(dr[0].ToString());

                }
                return v_DayCount;

            }
            catch (Exception)
            {
                return 0;

            }
            finally
            {
                cmd.Dispose();
                conn.Close();

            }

        }
        //取得會員資料_從宏誌系統登入用
        public string Get_member_data(string v_id, string v_mode)
        {
            v_sql = "SELECT DISTINCT USER_ID,USER_NAME FROM DW_EDW_MNGUSER WHERE USER_ID =  :USER_ID ";

            try
            {
                //建立連線
                //使用web.config conn string
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString);
                conn.Open();

                cmd = new OracleCommand(v_sql, conn);
                cmd.Parameters.Add(new OracleParameter("USER_ID", OracleType.VarChar, 10));
                cmd.Parameters["USER_ID"].Value = v_id;
                

                string v_user = "";
                //string v_email = "";


                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    switch (v_mode)
                    {
                        case "id":
                            {
                                v_user = dr["USER_ID"].ToString();
                                dr.Close();
                                conn.Close();
                                return v_user;

                            }
                        case "name":
                            {
                                v_user = dr["USER_NAME"].ToString();
                                dr.Close();
                                conn.Close();
                                return v_user;

                            }

                        default:
                            {
                                return "";

                            }

                    }

                }
                return v_user;

            }
            catch (Exception)
            {
                return "";

            }
            finally
            {
                cmd.Dispose();
                conn.Close();

            }

        }

        //判斷是否有此目錄
        public bool chkQueryString(string v_id)
        {
            string v_sql = "select * from tree_root " +
                           "where SERVER_VALUE = '" + v_id + "' ";
            bool v_return = false;
            try
            {
            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings["edwbi"].ConnectionString);
            conn.Open();


            cmd = new OracleCommand(v_sql, conn);


            

            

                dr = cmd.ExecuteReader();

                if (!dr.Read())
                {
                    dr.Close();
                    conn.Close();
                    v_return = false;//無此目錄
                }
                else
                {
                    dr.Close();
                    conn.Close();
                    v_return = true;  //有此目錄

                }
                return v_return;

            }
            catch (Exception ex)
            {
                string v_Leabel = ex.Message;
                return v_return;

            }
            finally
            {
                cmd.Dispose();
                conn.Close();

            }

        }

        
        //取得目錄名稱
        public string GetServName(string v_id)
        {
            string v_sql = "select sevname from tree_root " +
                           "where SERVER_VALUE = '" + v_id + "' ";
            string v_return = string.Empty;
            try
            {
            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings["edwbi"].ConnectionString);
            conn.Open();


            cmd = new OracleCommand(v_sql, conn);


            

            

                dr = cmd.ExecuteReader();

                if (!dr.Read())
                {
                    dr.Close();
                    conn.Close();
                    //v_return = false;//無此目錄
                }
                else
                {
                    v_return = dr[0].ToString();  //有此目錄
                    dr.Close();
                    conn.Close();
                    

                }
                return v_return;

            }
            catch (Exception ex)
            {
                string v_Leabel = ex.Message;
                return v_return;

            }
            finally
            {
                cmd.Dispose();
                conn.Close();

            }

        }

    
        //取得目錄資料
        public DataSet RunQuery(string v_Qrystr)
        {
            DataSet dataSet = new DataSet();

            try
            {
                //建立連線
                //使用web.config conn string
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings["edwbi"].ConnectionString);
                conn.Open();


                cmd = new OracleCommand(v_Qrystr, conn);
                adp = new OracleDataAdapter(cmd);
                

            


                adp.Fill(dataSet, "tree_root");


                return dataSet;

            }
            catch (Exception ex)
            {
                string v_Leabel = ex.Message;
                return dataSet;

            }
            finally
            {
                cmd.Dispose();
                conn.Close();

            }

        }

        //檢查是否為數字
        public bool CheckIsNumber(string s)
        {
            char[] tmp = s.ToCharArray();
            for (int i = 0; i < tmp.Length; i++)
            {
                if ((int)tmp[i] < 48 || (int)tmp[i] > 57)
                    return false;

            }
            return true;
        }

        //登入控制台判斷
        public bool Check_login_control(string v_id, string v_pw,Label v_message)
        {
            bool v_return = false;
            v_sql = "SELECT USER_ID,USER_PASSWD FROM ODS_JH_PWD WHERE USER_ID =  :USER_ID  AND USER_PASSWD = :USER_PASSWD";


            try
            {
                //建立連線
                //使用web.config conn string
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString);
                conn.Open();

            }
            catch (Exception ex)
            {
                v_message.Text = ex.Message;
                return v_return;
            }

            finally
            {
            }

            //string v_pw2 = get_Encrypt_pwd(v_pw);
            cmd = new OracleCommand(v_sql, conn);
            cmd.Parameters.Add(new OracleParameter("USER_ID", OracleType.VarChar, 10));
            cmd.Parameters["USER_ID"].Value = v_id;
            cmd.Parameters.Add(new OracleParameter("USER_PASSWD", OracleType.VarChar, 100));
            cmd.Parameters["USER_PASSWD"].Value = get_Encrypt_pwd(v_pw);


            try
            {

                dr = cmd.ExecuteReader();

                if (!dr.Read())
                {
                    dr.Close();
                    conn.Close();
                    v_return = false;//登入失敗
                }
                else
                {
                    dr.Close();
                    conn.Close();
                    v_return = true;  //登入成功

                }
                return v_return;
            }
            catch (Exception ex)
            {
                string v_Leabel = ex.Message;
                return v_return;

            }
            finally
            {
                cmd.Dispose();
                conn.Close();

            }


        }
        
        //變更密碼，總部帳號專用
        public bool Change_PW(string v_id, string v_oldpw, string v_newpw, string v_LoginSelect)
        {
            //bool v_return = false;

            //string v_sql;

            switch (v_LoginSelect)
            {
                case "TP":
                    {
                        v_sql = "SELECT USER_ID,USER_PASSWD FROM ODS_TP_IMSETEK_MNGUSER WHERE USER_ID =  :USER_ID  AND USER_PASSWD = :USER_PASSWD";
                        break;
                    }
                case "MALL":
                    {
                        v_sql = "SELECT USER_ID,USER_PASSWD FROM ODS_MALL_IMSETEK_MNGUSER WHERE USER_ID =  :USER_ID  AND TRIM(USER_PASSWD) = :USER_PASSWD";
                        break;
                    }
                case "SKH":
                    {
                        v_sql = "SELECT USER_ID,USER_PASSWD FROM ODS_MALL_IMSETEK_MNGUSER WHERE USER_ID =  :USER_ID  AND USER_PASSWD = :USER_PASSWD";
                        break;
                    }
                case "EDD":
                    {
                        v_sql = "UPDATE  ODS_EDW_IMSETEK_MNGUSER SET USER_PASSWD =:NEW_USER_PASSWD  WHERE USER_ID = :USER_ID  AND USER_PASSWD =:USER_PASSWD";
                        break;
                    }
            }

            try
            {
                //建立連線
                //使用web.config conn string
                adp = new OracleDataAdapter();
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings["edwbi"].ConnectionString);
                conn.Open();

            }
            catch (Exception ex)
            {
                string v_Leabel = ex.Message;
                return false;
            }

            finally
            {
            }

            //string v_pw2 = get_Encrypt_pwd(v_pw);
            cmd = new OracleCommand(v_sql, conn);
            cmd.Parameters.Add(new OracleParameter("USER_ID", OracleType.VarChar, 10));
            cmd.Parameters["USER_ID"].Value = v_id;
            cmd.Parameters.Add(new OracleParameter("USER_PASSWD", OracleType.Char, 64));
            cmd.Parameters["USER_PASSWD"].Value = get_Encrypt_pwd(v_oldpw); 
            cmd.Parameters.Add(new OracleParameter("NEW_USER_PASSWD", OracleType.Char, 64));
            cmd.Parameters["NEW_USER_PASSWD"].Value = get_Encrypt_pwd(v_newpw); 

            try
            {

                adp.UpdateCommand = cmd;
                adp.UpdateCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                string v_Leabel = ex.Message;
                return false;

            }
            finally
            {
                cmd.Dispose();
                conn.Close();

            }
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
            //smtp.Credentials = new System.Net.NetworkCredential("service", "p@ssw0rd");
            //smtp.Host = "smtp.edamall.com.tw";
            smtp.Host = "192.168.10.200";
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
                    string v_clientIP = HttpContext.Current.Request.UserHostAddress.ToString();
                    string v_cmd = "insert into order_log values(order_log_sq.nextval,sysdate,'send mail failed!" + T_email + "','" + v_clientIP + "')";
                    trans_oracle(v_cmd, "insert" );
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


            //string[] v_mailto = { T_email, "k0095@edd.e-united.com.tw" };

            //string[] v_mailto = { "skylark@edaskylark.com.tw","k0095@edd.e-united.com.tw" };

            // '构建SmtpClient对象
            SmtpClient smtp = new SmtpClient();
            //smtp.Credentials = new System.Net.NetworkCredential("service", "p@ssw0rd");
            smtp.Host = "smtp.edathemepark.com.tw";
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

            from = new MailAddress("呷粗霸<dinbandon@e-united.com.tw>");// '發件箱地址
            //mto = new MailAddress(v_mailto[i]);// '收件箱地址
            mto = new MailAddress(T_email);// '收件箱地址
            mailobj = new MailMessage(from, mto);

            // '完善MailMessage对象
            mailobj.Subject = v_subject;  //'主題
            mailobj.Body = v_body;
            mailobj.IsBodyHtml = true;//
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
                string v_clientIP = HttpContext.Current.Request.UserHostAddress.ToString();
                string v_cmd = "insert into order_log values(order_log_sq.nextval,sysdate,'send mail failed!"+T_email+"','" + v_clientIP + "')";
                trans_oracle(v_cmd, "insert");
            }


        }

        //判斷是否有此權限
        public bool chkUserGrant(string v_userid,string v_pid)
        {
            string v_sql = "SELECT T_KEY FROM ODS_EDW_PROGRAM_GRANT WHERE USER_ID = :USER_ID AND P_ID = :P_ID ";
            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings["edwbi"].ConnectionString);
            conn.Open();

            cmd = new OracleCommand(v_sql, conn);
            cmd.Parameters.Add(new OracleParameter("USER_ID", OracleType.VarChar, 10));
            cmd.Parameters["USER_ID"].Value = v_userid;
            cmd.Parameters.Add(new OracleParameter("P_ID", OracleType.VarChar, 100));
            cmd.Parameters["P_ID"].Value = v_pid;


            try
            {
                //bool v_return = false;

                dr = cmd.ExecuteReader();

                if (!dr.Read())
                {
                    cmd.Dispose();
                    conn.Close();
                    return false;

                }
                else
                {
                    cmd.Dispose();
                    conn.Close();
                    return true;
                }

            }
            catch (Exception)
            {
                cmd.Dispose();
                conn.Close();

                return false;

            }
            finally
            {
                cmd.Dispose();
                conn.Close();

            }
        }

        public string get_Encrypt_pwd(string v_pwd)
        {
            string v_password="";

            //呼叫ORACLE PACKAGE加密函式
            using (OracleConnection objConn = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString))
            {
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_DES.ENCRYPT";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("I_chaPlainText", OracleType.VarChar, 64).Value = v_pwd;
                objCmd.Parameters.Add("return_value", OracleType.VarChar, 32767).Direction = ParameterDirection.ReturnValue;

                try
                {
                    objConn.Open();


                    objCmd.ExecuteNonQuery();
                    v_password = objCmd.Parameters["return_value"].Value.ToString();
                    return v_password;
                }
                catch (Exception ex)
                {
                    string v_Leabel = ex.Message;
                    return v_password;
                }
                finally
                {
                    objConn.Close();
                }
            }
        }

        public string get_Decrypt_pwd(string v_pwd)
        {
            string v_password = "";

            //呼叫ORACLE PACKAGE加密函式
            using (OracleConnection objConn = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString))
            {
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_DES.DECRYPT";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("I_chaPlainText", OracleType.VarChar, 64).Value = v_pwd;
                objCmd.Parameters.Add("return_value", OracleType.VarChar, 32767).Direction = ParameterDirection.ReturnValue;

                try
                {
                    objConn.Open();


                    objCmd.ExecuteNonQuery();
                    v_password = objCmd.Parameters["return_value"].Value.ToString();
                    return v_password;
                }
                catch (Exception ex)
                {
                    string v_Leabel = ex.Message;
                    return v_password;
                }
                finally
                {
                    objConn.Close();
                }
            }
        }

       //取的資料更新時間
        public string get_Update_time(Literal v_Li_UpdateTime, string v_sql)
        {
           
            //string v_sql = "SELECT PARA_CONTENT FROM   EDW.ODS_EDW_PARAMETER WHERE  SN =1 ";
            string v_return="";
            try
            {
                //建立連線
                //使用web.config conn string
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString);
                conn.Open();

                cmd = new OracleCommand(v_sql, conn);

                dr = cmd.ExecuteReader();

                if (!dr.Read())
                {
                    dr.Close();
                    conn.Close();
                    
                }
                else
                {
                    v_return = dr[0].ToString();  //取得參數
                    dr.Close();
                    conn.Close();


                }
                return v_return;

            }
            catch (Exception ex)
            {
                string v_Leabel = ex.Message;
                return v_return;

            }
            finally
            {
                cmd.Dispose();
                conn.Close();

            }
        }

        //取得營收總計
        public int get_Amount_Total(string v_sql)
        {
            int v_return= 0;
            try
            {
                //建立連線
                //使用web.config conn string
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings["edwbi"].ConnectionString);
                conn.Open();

                cmd = new OracleCommand(v_sql, conn);

                dr = cmd.ExecuteReader();

                if (!dr.Read())
                {
                    dr.Close();
                    conn.Close();

                }
                else
                {
                    //dr = cmd.ExecuteReader();
                    v_return =Convert.ToInt32(dr[0].ToString());  //取得參數
                    dr.Close();
                    conn.Close();


                }
                return v_return;

            }
            catch (Exception ex)
            {
                string v_Leabel = ex.Message;
                return 0;

            }
            finally
            {
                cmd.Dispose();
                conn.Close();

            }
        }
}