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

public partial class QueryWires_01 : System.Web.UI.Page
{
    string Sql_str;
    thomas_Conn tsconn = new thomas_Conn();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string v_process = "",v_pdate = "";


            if ((Request.Params["process"] != null) & (Request.Params["pdate"] != null))
            {
                v_process = Request.Params["process"].ToString();
                v_pdate = Request.Params["pdate"].ToString();

                Sql_str = " SELECT   W.RAWMTRL_ID, W.HEAT_NO, W.DIAMETER,nvl(decode(W.NEXT,'A','A洗球','D','D洗抽','N','N螺帽',W.NEXT),'null') NEXT,to_char(W.PDATE,'yyyy/mm/dd') PDATE, COUNT(*) CNT ";
                Sql_str += " FROM EAGLE.WIRES_PICKLING W ";
                Sql_str += " WHERE PROCESS = :process ";
                Sql_str += " AND TO_CHAR(PDATE,'YYYY/MM/DD') = :pdate ";
                Sql_str += " GROUP BY  W.RAWMTRL_ID, W.HEAT_NO, W.DIAMETER,nvl(decode(W.NEXT,'A','A洗球','D','D洗抽','N','N螺帽',W.NEXT),'null'),to_char(W.PDATE,'yyyy/mm/dd')  ";
                Sql_str += " ORDER BY 4,1,2,3,5 ";

                //建立連線
                //使用web.config conn string
                OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
                conn.Open();


                OracleCommand cmd = new OracleCommand(Sql_str, conn);
                cmd.Parameters.Add(new OracleParameter("process", OracleType.VarChar, 100));
                cmd.Parameters["process"].Value = v_process;

                cmd.Parameters.Add(new OracleParameter("pdate", OracleType.VarChar, 100));
                cmd.Parameters["pdate"].Value = v_pdate;

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
                                            RAWMTRL_ID = (String)dr["RAWMTRL_ID"],
                                            HEAT_NO = (String)dr["HEAT_NO"],
                                            PDATE = (String)dr["PDATE"],
                                            NEXT = (String)dr["NEXT"],
                                            DIAMETER = (Decimal)dr["DIAMETER"],
                                            CNT = (Decimal)dr["CNT"] 
                                        };
                        string v_json = (JsonConvert.SerializeObject(dataQuery));
                        tsconn.save_log("guest", "QueryWires_01", "192.168.0.19", v_json);
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