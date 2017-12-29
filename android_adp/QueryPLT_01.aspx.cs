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

public partial class QueryPLT_01 : System.Web.UI.Page
{
    string Sql_str, v_loc, v_pdate;
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

            //string v_process = "", v_pdate = "";


            if (Request.Params["location"] != null & Request.Params["pdate"] != null)
            {
            v_loc = Request.Params["location"].ToString();
            v_pdate = Request.Params["pdate"].ToString();

            Sql_str = " SELECT  JOB_CARD,  ";
            Sql_str += "   CAT||'-'||DIA||'-'||LEN||'-'||TH||'-'||PL PRODUCTS,  ";
            Sql_str += "   WEIGHT,RAWMTRL_ID RAWMTRL, PDTSIZE,LOCATION, CREATE_DATE  ";
            Sql_str += "FROM WIP_PLT_TRANS  ";
            Sql_str += "WHERE 1=1 ";
            Sql_str += "AND LOCATION = NVL(:location,LOCATION) ";
            Sql_str += "AND TO_CHAR(CREATE_DATE,'YYYY/MM/DD') = :pdate ";
            Sql_str += "ORDER BY 1 ";

            //Sql_str = "SELECT LOT_NO||'-'||CTRLOT_NO||'-'||KEG_NO JOB_CARD, ";
            //Sql_str += "            CAT||'-'||DIA||'-'||LEN||'-'||TH||'-'||PL PRODUCTS , ";
            //Sql_str += "             P_SIZE,RAWMTRL_ID,LOCATION ";
            //Sql_str += "  FROM wip_plt_scheduled S ";
            //Sql_str += " WHERE STATUS = 'N' AND LOCATION = :location ";

            //建立連線
            //使用web.config conn string
            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
            conn.Open();


            OracleCommand cmd = new OracleCommand(Sql_str, conn);
            cmd.Parameters.Add(new OracleParameter("location", OracleType.VarChar, 100));
            cmd.Parameters["location"].Value = v_loc;

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
                                        JOB_CARD = (String)dr["JOB_CARD"],
                                        PRODUCTS = (String)dr["PRODUCTS"],
                                        RAWMTRL = (String)dr["RAWMTRL"],
                                        PDTSIZE = (String)dr["PDTSIZE"],
                                        LOCATION = (String)dr["LOCATION"],
                                        WEIGHT = (Decimal)dr["WEIGHT"]
                                    };
                    string v_json = (JsonConvert.SerializeObject(dataQuery));
                    tsconn.save_log("guest", "QueryPLT_01", "192.168.0.19", "電鍍預排查詢:庫位機台 " + v_loc + ",日期 " + v_pdate);
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