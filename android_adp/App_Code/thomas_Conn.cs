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
    static string ConnStr = "jheip"; //連線字串
    static string ConnStr815 = "jh815"; //連線字串
    static string ConnStrjherp = "JHERPDB"; //連線字串
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

        public void trans_oracle(string Conn_str,string v_cmd, string v_ddl)
        {
            //v_Leabel.Text = "";
            
            //建立連線
            //使用web.config conn string
            adp = new OracleDataAdapter();
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings[Conn_str].ConnectionString);
            conn.Open();

            
            cmd = new OracleCommand(v_cmd, conn);

            try
            {
            
                switch (v_ddl)
                {
                    case "select":
                        adp.SelectCommand = cmd;
                        adp.SelectCommand.ExecuteNonQuery();
                        break;

                    case "insert":

                        adp.InsertCommand = cmd;
                        adp.InsertCommand.ExecuteNonQuery();
                        //v_Leabel.Text = "新增資料成功";
                        break;

                    case "update":
                        adp.UpdateCommand = cmd;
                        adp.UpdateCommand.ExecuteNonQuery();
                        //v_Leabel.Text = "更新資料成功";
                        break;

                    case "delete":
                        adp.DeleteCommand = cmd;
                        adp.DeleteCommand.ExecuteNonQuery();
                        //v_Leabel.Text = "刪除資料成功";
                        break;

                    default:
                        break;
                }
               

                
            }
            catch (Exception ex)
            {
                //v_Leabel.Text = ex.Message;

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
        public void save_log(string v_userid, string v_content, string v_ip, string v_memo)
        {
            
                //建立連線
                //使用web.config conn string
                adp = new OracleDataAdapter();
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString);
                conn.Open();

                string v_cmd = "INSERT INTO ODS_JH_LOG(SN,LOG_CONTENT,LOG_DATE,LOG_IP,USER_ID,LOG_MEMO) " +
                               "VALUES(ODS_JH_LOG_SQ.NEXTVAL,'" + v_content + "',SYSDATE,'" + v_ip + "','" + v_userid + "','" + v_memo + "')";
                cmd = new OracleCommand(v_cmd, conn);
                try
                {
                adp.InsertCommand = cmd;
                adp.InsertCommand.ExecuteNonQuery();

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
        public void save_log(string v_userid, string v_content, string v_ip)
        {
            
                //建立連線
                //使用web.config conn string
                adp = new OracleDataAdapter();
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString);
                conn.Open();

                string v_cmd = "INSERT INTO ODS_JH_LOG(SN,LOG_CONTENT,LOG_DATE,LOG_IP,USER_ID,LOG_MEMO) " +
                               "VALUES(ODS_JH_LOG_SQ.NEXTVAL,'" + v_content + "',SYSDATE,'" + v_ip + "','" + v_userid + "','')";
                cmd = new OracleCommand(v_cmd, conn);
                try
                {
                adp.InsertCommand = cmd;
                adp.InsertCommand.ExecuteNonQuery();

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

        //取得會員資料
        public string Get_member_data(string v_id, string v_pw,string v_mode )
        {

           
           v_sql = "SELECT USER_ID,USER_NAME,USER_PASSWD FROM ODS_JH_PWD WHERE USER_ID =  :USER_ID  AND USER_PASSWD = :USER_PASSWD ";
               
            
            //建立連線
            //使用web.config conn string
           conn = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString);
            conn.Open();

            cmd = new OracleCommand(v_sql, conn);
            cmd.Parameters.Add(new OracleParameter("USER_ID", OracleType.VarChar, 10));
            cmd.Parameters["USER_ID"].Value = v_id;
            cmd.Parameters.Add(new OracleParameter("USER_PASSWD", OracleType.Char, 32));
            cmd.Parameters["USER_PASSWD"].Value = get_Encrypt_pwd(v_pw);

            string v_user = "";
            //string v_email = "";

            try
            {
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
          
            
                //建立連線
                //使用web.config conn string
                conn = new OracleConnection("Data Source=jh815;Persist Security Info=True;User ID=eagle;Password=shefalls;Unicode=True");
                conn.Open();

                cmd = new OracleCommand(v_sql, conn);

                int v_DayCount = 0;
                //string v_email = "";
                try
                {

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

            
                //建立連線
                //使用web.config conn string
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString);
                conn.Open();

                cmd = new OracleCommand(v_sql, conn);
                cmd.Parameters.Add(new OracleParameter("USER_ID", OracleType.VarChar, 10));
                cmd.Parameters["USER_ID"].Value = v_id;
                

                string v_user = "";
                //string v_email = "";

                try
                {
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
            
            //建立連線
            //使用web.config conn string
            conn = new OracleConnection("Data Source=jh815;Persist Security Info=True;User ID=eagle;Password=shefalls;Unicode=True");
            conn.Open();


            cmd = new OracleCommand(v_sql, conn);


            try
            { 

            

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
            
            //建立連線
            //使用web.config conn string
            conn = new OracleConnection("Data Source=jh815;Persist Security Info=True;User ID=eagle;Password=shefalls;Unicode=True");
            conn.Open();


            cmd = new OracleCommand(v_sql, conn);



            try
            {
            

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


            //建立連線
            //使用web.config conn string
            conn = new OracleConnection("Data Source=jh815;Persist Security Info=True;User ID=eagle;Password=shefalls;Unicode=True");
            conn.Open();


            cmd = new OracleCommand(v_Qrystr, conn);
            adp = new OracleDataAdapter(cmd);


            try
            {


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
        
        //變更密碼
        public bool Change_PW(string v_id, string v_oldpw, string v_newpw)
        {
            //bool v_return = false;

            //string v_sql;
            v_sql = "UPDATE  ODS_JH_PWD SET USER_PASSWD =:NEW_USER_PASSWD  WHERE USER_ID = :USER_ID  AND USER_PASSWD =:USER_PASSWD";

            try
            {
                //建立連線
                //使用web.config conn string
                adp = new OracleDataAdapter();
                conn = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString);
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

       

        //判斷是否有此權限
        public bool chkUserGrant(string v_userid,string v_pid)
        {
            string v_sql = "SELECT T_KEY FROM ODS_JH_PROGRAM_GRANT WHERE USER_ID = :USER_ID AND P_ID = :P_ID ";
            //string v_sql = "SELECT T_KEY FROM ODS_JH_PROGRAM_GRANT WHERE USER_ID = '" + v_userid + "' AND P_ID = '" + v_pid + "' ";
            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString);
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

        //判斷是否有資料存在
        public bool chkDataExist(string v_sql)
        {
            //string v_sql = "SELECT T_KEY FROM ODS_EDW_PROGRAM_GRANT WHERE USER_ID = :USER_ID AND P_ID = :P_ID ";
            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings["JH815"].ConnectionString);
            conn.Open();
            cmd = new OracleCommand(v_sql, conn);

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
            string v_return = "";

            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString);
            conn.Open();

            cmd = new OracleCommand(v_sql, conn);
            try
            {
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
                conn = new OracleConnection("Data Source=jh815;Persist Security Info=True;User ID=eagle;Password=shefalls;Unicode=True");
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