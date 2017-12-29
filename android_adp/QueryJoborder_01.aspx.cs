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

public partial class QueryJoborder_01 : System.Web.UI.Page
{
    string Sql_str;
    thomas_Conn tsconn = new thomas_Conn();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string v_joborder = "";


            if ((Request.Params["joborder"] != null))
            {
                v_joborder = Request.Params["joborder"].ToString();

                Sql_str = " SELECT J.ITEM_NO, J.WIR_KIND,replace(to_char(J.DRAW_DIA,'99.99'),'.00','') DRAW_DIA,NVL(J.LUO_NO,'x') LUO_NO,J.ASSM_NO||'-'||J.ASSM_NAME ASSM, ";
                Sql_str += "       J.REQU_QTY,J.ISSU_QTY,J.FNSH_QTY ,J.END_CODE ";
                Sql_str += "FROM v_apc_job_orde J ";
                Sql_str += "WHERE JOB_NO = :joborder ";
                Sql_str += "ORDER BY 1 ";

                //建立連線
                //使用web.config conn string
                OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jheip"].ConnectionString);
                conn.Open();


                OracleCommand cmd = new OracleCommand(Sql_str, conn);
                cmd.Parameters.Add(new OracleParameter("joborder", OracleType.VarChar, 100));
                cmd.Parameters["joborder"].Value = v_joborder;

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
                                            ITEM_NO = (Decimal)dr["ITEM_NO"],
                                            WIR_KIND = (String)dr["WIR_KIND"],
                                            DRAW_DIA = (String)dr["DRAW_DIA"],
                                            LUO_NO = (String)dr["LUO_NO"],
                                            ASSM = (String)dr["ASSM"],
                                            REQU_QTY = (Decimal)dr["REQU_QTY"],
                                            ISSU_QTY = (Decimal)dr["ISSU_QTY"],
                                            FNSH_QTY = (Decimal)dr["FNSH_QTY"],
                                        };
                        string v_json = (JsonConvert.SerializeObject(dataQuery));
                        tsconn.save_log("guest", "QueryJoborder_01", "192.168.0.19", v_json);
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