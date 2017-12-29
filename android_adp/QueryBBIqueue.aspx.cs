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

public partial class QueryBBIqueue : System.Web.UI.Page
{
    string Sql_str;
    thomas_Conn tsconn = new thomas_Conn();
    thomas_function ts_Fun = new thomas_function();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ts_Fun.chk_verify())
        {
            //Server.Transfer("error.htm");
            Response.Redirect("error2.aspx", false);
        }

        try
        {

            string v_barcode1 = "";


            if ((Request.Params["barcode1"] != null))
            {
                v_barcode1 = Request.Params["barcode1"].ToString().Replace("%QP6","").Replace("\n","").Replace("\r","").Substring(0,6);

                Sql_str = " SELECT   B.BARCODE_1, B.BARCODE_2, B.BBI_SO,B.EMP_NO, TO_CHAR(B.CREATE_DATE,'YY/MM/DD HH24:MI') CREATE_DATE, B.STATUS ";
                Sql_str += "FROM BBI_SHIP_QUEUE B ";
                Sql_str += "WHERE SUBSTR(BARCODE_1,0,6) like :v_BARCODE_1 ";
                Sql_str += "ORDER BY 1 ";

                //建立連線
                //使用web.config conn string
                OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
                conn.Open();


                OracleCommand cmd = new OracleCommand(Sql_str, conn);
                cmd.Parameters.Add(new OracleParameter("v_BARCODE_1", OracleType.VarChar, 100));
                cmd.Parameters["v_BARCODE_1"].Value = v_barcode1;

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

                                            BARCODE_1 = (String)dr["BARCODE_1"],
                                            BARCODE_2 = (String)dr["BARCODE_2"],
                                            PAC_NO = (String)dr["BBI_SO"],
                                            EMP_NO = (String)dr["EMP_NO"],
                                            CREATE_DATE = (String)dr["CREATE_DATE"]
                                            
                                        };
                        string v_json = (JsonConvert.SerializeObject(dataQuery));
                        tsconn.save_log("guest", "QueryBBIqueue", "192.168.0.19", "查詢BBI條碼:" + v_barcode1);
                        Response.Write(v_json);

                        //}
                    }

                }
                catch (Exception ex)
                {
                    //tsconn.save_log("guest", "QueryWires_01", "192.168.0.19", ex.ToString());
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

        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
}