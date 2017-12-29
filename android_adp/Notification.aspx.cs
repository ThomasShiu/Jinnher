using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using System.Data.Common;
using Newtonsoft.Json;

public partial class Notification : System.Web.UI.Page
{
    string Sql_str;
    thomas_Conn tsconn = new thomas_Conn();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string v_ver = "";
            if ((Request.Params["version"] != null))
            {
                v_ver = Request.Params["version"].ToString();

                //Y = 啟動  , N = 停用
                Sql_str = " SELECT  TITTLE, CONTENT,VERSION FROM PDA_NOTIFICATION WHERE STATUS = 'Y' AND VERSION > '" + v_ver + "'  ";

                //建立連線
                //使用web.config conn string
                OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jheip"].ConnectionString);
                conn.Open();

                OracleCommand cmd = new OracleCommand(Sql_str, conn);
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
                                            TITTLE = (String)dr["TITTLE"],
                                            CONTENT = (String)dr["CONTENT"],
                                            VERSION = (String)dr["VERSION"]
                                        };
                        string v_json = (JsonConvert.SerializeObject(dataQuery));
                        //tsconn.save_log("guest", "NOTFICATION PASS", "192.168.0.19", v_json);
                        Response.Write(v_json);

                        //}
                    }
                   

                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
                finally
                {
                    cmd.Dispose();
                    dr.Close();
                    dr.Dispose();
                    conn.Close();
                    conn.Dispose();
                }

            }
        }
        catch (Exception ex)
        {
            var dataQuery = new
            {
                STATUS = "REJECT"
            };
            string v_json = (JsonConvert.SerializeObject(dataQuery));
            //tsconn.save_log("guest", "NOTFICATION REJECT", "192.168.0.19", v_json);
            Response.Write(v_json);
        }
    }
    
}