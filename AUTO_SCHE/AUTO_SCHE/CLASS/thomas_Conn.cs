using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

using System.Configuration;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

/// <summary>
/// thomas_Conn 的摘要描述
/// </summary>
public class thomas_Conn
{
    SqlDataAdapter s_adp;
    SqlConnection s_conn;
    SqlCommand s_cmd;
    SqlDataReader s_dr;

    //OracleDataAdapter adp;
    //OracleConnection conn;
    //OracleCommand cmd;
    //OracleDataReader dr;

    string v_sql;
    static string ConnStr = "jheip"; //連線字串
    static string ConnStr815 = "jh815"; //連線字串

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

    public void trans_MSsql( string v_cmd, string v_ddl, Label v_Label)
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
                    v_Label.Text = "新增資料成功";
                    break;

                case "update":
                    s_adp.UpdateCommand = s_cmd;
                    s_adp.UpdateCommand.ExecuteNonQuery();
                    v_Label.Text = "更新資料成功";
                    break;

                case "delete":
                    s_adp.DeleteCommand = s_cmd;
                    s_adp.DeleteCommand.ExecuteNonQuery();
                    v_Label.Text = "刪除資料成功";
                    break;

                default:
                    break;
            }



        }
        catch (Exception ex)
        {
            v_Label.Text = ex.Message;

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
    //public void trans_oracle(string Conn_str, string v_cmd, string v_ddl, Label v_Leabel)
    //{
    //    v_Leabel.Text = "";

    //    //建立連線
    //    //使用web.config conn string
    //    adp = new OracleDataAdapter();
    //    conn = new OracleConnection("Data Source=jh815;Persist Security Info=True;User ID=eagle;Password=shefalls;Unicode=True");
    //    conn.Open();


    //    cmd = new OracleCommand(v_cmd, conn);

    //    try
    //    {

    //        switch (v_ddl)
    //        {
    //            case "select":
    //                adp.SelectCommand = cmd;
    //                adp.SelectCommand.ExecuteNonQuery();
    //                break;

    //            case "insert":

    //                adp.InsertCommand = cmd;
    //                adp.InsertCommand.ExecuteNonQuery();
    //                v_Leabel.Text = "新增資料成功";
    //                break;

    //            case "update":
    //                adp.UpdateCommand = cmd;
    //                adp.UpdateCommand.ExecuteNonQuery();
    //                v_Leabel.Text = "更新資料成功";
    //                break;

    //            case "delete":
    //                adp.DeleteCommand = cmd;
    //                adp.DeleteCommand.ExecuteNonQuery();
    //                v_Leabel.Text = "刪除資料成功";
    //                break;

    //            default:
    //                break;
    //        }



    //    }
    //    catch (Exception ex)
    //    {
    //        v_Leabel.Text = ex.Message;

    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        adp.Dispose();
    //        conn.Close();
    //    }

    //    //建立資料集
    //    //DataSet ds = new DataSet();
    //    //adp.Fill(ds, "");
    //}

  
}