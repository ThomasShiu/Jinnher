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

public partial class testJSON : System.Web.UI.Page
{
    string Sql_str;
    thomas_Conn tsconn = new thomas_Conn();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string v_barcode1 = "";


            if ((Request.Params["barcode1"] != null))
            {
                v_barcode1 = Request.Params["barcode1"].ToString();

                Sql_str = "SELECT WIRE_ID,WIRE_IDS,RAWMTRL_ID,CO_ID,HEAT_NO,DIAMETER,WEIGHT,to_char(STORE_DATE,'YYYY/MM/DD') as STORE_DATE,LOCATION ";
                Sql_str += "FROM   WIRES ";
                Sql_str += "WHERE (WIRE_ID = :v_ID OR WIRE_IDS = :v_ID ) ";

                //建立連線
                //使用web.config conn string
                OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
                conn.Open();


                OracleCommand cmd = new OracleCommand(Sql_str, conn);
                cmd.Parameters.Add(new OracleParameter("v_ID", OracleType.VarChar, 100));
                cmd.Parameters["v_ID"].Value = v_barcode1;

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
                                            WIRE_ID = (String)dr["WIRE_ID"],
                                            WIRE_IDS = (String)dr["WIRE_IDS"],
                                            RAWMTRL_ID = (String)dr["RAWMTRL_ID"],
                                            CO_ID = (String)dr["CO_ID"],
                                            HEAT_NO = (String)dr["HEAT_NO"],
                                            DIAMETER = (Decimal)dr["DIAMETER"],
                                            WEIGHT = (Decimal)dr["WEIGHT"],
                                            STORE_DATE = (String)dr["STORE_DATE"],
                                            LOCATION = (String)dr["LOCATION"]
                                            
                                        };
                        string v_json = (JsonConvert.SerializeObject(dataQuery));
                        //tsconn.save_log("guest", "testJSON", "192.168.0.19", v_json);
                        Response.Write(v_json);

                        //}
                    }
                  
                }
                catch (Exception ex)
                {
                    //tsconn.save_log("guest", "testJSON", "192.168.0.19", ex.ToString());
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

                //tsconn.trans_oracle815(Sql_str, "insert");
                //Response.Write("Insert success!");
            }
            else
            {
                Response.Write("No data insert!");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
    
}