using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using System.Data.Common;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

public partial class Login : System.Web.UI.Page
{
    string Sql_str, v_id, v_pw;
    thomas_Conn tsconn = new thomas_Conn();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string v_barcode1 = "";
            if ((Request.Params["id"] != null) & (Request.Params["pw"] != null))
            {
                v_id = Request.Params["id"].ToString();
                v_pw = Request.Params["pw"].ToString();

                Sql_str = " SELECT  S.USER_ID, S.USERNAME, S.PASSWORD,  ";
                Sql_str += "   S.LOGIN_NAME, S.EMP_NO, S.EMP_NAME,  ";
                Sql_str += "     S.DEPT_NO, S.DEPT_NAME ";
                Sql_str += "  FROM EAGLE.SYS_USERS S ";
                Sql_str += "  WHERE USERNAME = :v_ID ";
                Sql_str += "  AND PASSWORD =  :v_PW ";

                //建立連線
                //使用web.config conn string
                OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
                conn.Open();


                OracleCommand cmd = new OracleCommand(Sql_str, conn);
                cmd.Parameters.Add(new OracleParameter("v_ID", OracleType.VarChar, 100));
                cmd.Parameters["v_ID"].Value = v_id;
                cmd.Parameters.Add(new OracleParameter("v_PW", OracleType.VarChar, 100));
                cmd.Parameters["v_PW"].Value = v_pw;

                OracleDataReader dr = cmd.ExecuteReader();

                try
                {
                    //while (dr.Read())
                    //{
                    //組成JSON字串
                    if (dr.HasRows)
                    {
                        var dataQuery = from d in dr.Cast<DbDataRecord>()
                                        select new
                                        {
                                            STATUS = "PASS",
                                            USERNAME = (String)dr["USERNAME"],
                                            EMP_NAME = (String)dr["EMP_NAME"]
                                        };
                        string v_json = (JsonConvert.SerializeObject(dataQuery));
                        tsconn.save_log("guest", "APP Login", "192.168.0.19", v_json);
                        Response.Write(v_json);

                        //}
                    }
                    else
                    {
                        var dataQuery = new
                                        {
                                            STATUS = "REJECT",
                                            USERNAME = v_id
                                        };
                        string v_json = (JsonConvert.SerializeObject(dataQuery));
                        tsconn.save_log("guest", "APP Login", "192.168.0.19", v_json);
                        Response.Write(v_json);
                    }

                }
                catch (Exception ex)
                {
                    var dataQuery = new
                    {
                        STATUS = "REJECT",
                        USERNAME = v_id
                    };
                    string v_json = (JsonConvert.SerializeObject(dataQuery));
                    tsconn.save_log("guest", "APP Login", "192.168.0.19", v_json);
                    Response.Write(v_json);
                }
                finally
                {
                    cmd.Dispose();
                    dr.Close();
                    dr.Dispose();
                    conn.Close();
                    conn.Dispose();
                }

                //tsconn.trans_oracle815(Sql_str, "insert");
                //Response.Write("Insert success!");
            }
            //else
            //{
            //    Response.Write("No data insert!");
            //}
        }
        catch (Exception ex)
        {
            var dataQuery = new
            {
                STATUS = "REJECT",
                USERNAME = v_id
            };
            string v_json = (JsonConvert.SerializeObject(dataQuery));
            tsconn.save_log("guest", "APP Login", "192.168.0.19", v_json);
            Response.Write(v_json);
        }
    }
}