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

public partial class PackingListitem : System.Web.UI.Page
{
    string Sql_str, Sql_str2;

    thomas_Conn tsconn = new thomas_Conn();

    string v_pacno = "";
    string v_stockid = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if ((Request.Params["pv_pacno"] != null) //& (Request.Params["pv_stockid"] != null)
                )
            {
                v_pacno = Request.Params["pv_pacno"].ToString();
                //v_stockid = Request.Params["pv_stockid"].ToString();

                //檢查作業ID存不存在
                //chk_stockid(v_pacno, v_stockid);

                //組成JSON字串給PDA下載
                download_StockID(v_pacno);

            }
            else
            {
                Response.Write("No data exists!");
            }
        }
        catch (Exception ex)
        {
            Response.Write("Param Error:");
        }
    }

    private void download_StockID(string v_pacno)
    {
        Sql_str2 = " SELECT DISTINCT P.PAC_NO,STOCK_ID ";
        Sql_str2 += "FROM  PACKINGS P,LISTITEMS L  ";
        Sql_str2 += "WHERE P.PAC_ID = L.PAC_ID  ";
        Sql_str2 += "AND PAC_NO = :PAC_NO ";

        //建立連線
        //使用web.config conn string
        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
        conn.Open();

        OracleCommand cmd;
        OracleDataReader dr;

        //判斷有無資料
        try
        {
            cmd = new OracleCommand(Sql_str2, conn);
            cmd.Parameters.Add(new OracleParameter("PAC_NO", OracleType.VarChar, 100));
            cmd.Parameters["PAC_NO"].Value = v_pacno;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                //Response.Write("Success");
                //建立假資料
                //List<Packings> Packings_Items = new List<Packings>();
                //while (dr.Read())
                //{
                //    Packings_Items.Add(new Packings() { pac_no = dr[0].ToString(), stock_id = dr[1].ToString() });
                //}
                var dataQuery = from d in dr.Cast<DbDataRecord>()
                                select new
                                {
                                    //STATUS = "success",
                                    PAC_NO = (String)dr["PAC_NO"],
                                    STOCK_ID = (String)dr["STOCK_ID"]
                                };

                string json_data = JsonConvert.SerializeObject(dataQuery);//存放序列後的文字
                conn.Close();
                Response.Write(json_data);
            }
            else
            {
                Response.Write("");
            }
        }
        catch (Exception ex)
        {
            //Response.Write("Duplicate Error:"+ex.ToString());
            Response.Write(ex.ToString());
            conn.Close();
            return;
        }
        finally
        {
            //conn.Close();
        }
    }
    private void chk_stockid(string v_pacno, string v_stockid)
    {
        //string v_wire_id, v_wire_ids, v_wire_rawmtrl, v_wire_diameter, v_wire_heatno, v_wire_coid;

        Sql_str2 = " SELECT count(*) CNT";
        Sql_str2 += "FROM PACKINGS P,LISTITEMS L ";
        Sql_str2 += "WHERE P.PAC_ID = L.PAC_ID ";
        Sql_str2 += "AND PAC_NO = :PAC_NO ";
        Sql_str2 += "AND STOCK_ID = :STOCK_ID ";

        //建立連線
        //使用web.config conn string
        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
        conn.Open();

        OracleCommand cmd;
        OracleDataReader dr;

        //判斷有無資料
        try
        {
            cmd = new OracleCommand(Sql_str2, conn);
            cmd.Parameters.Add(new OracleParameter("PAC_NO", OracleType.VarChar, 100));
            cmd.Parameters["PAC_NO"].Value = v_pacno;
            cmd.Parameters.Add(new OracleParameter("STOCK_ID", OracleType.VarChar, 100));
            cmd.Parameters["STOCK_ID"].Value = v_stockid;

            //cmd.Parameters.Add(new OracleParameter("PDATE", OracleType.VarChar, 100));
            //cmd.Parameters["PDATE"].Value = v_create_date.Substring(0, 10);

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                Response.Write("Success");
                conn.Close();
                return;
            }
        }
        catch (Exception ex)
        {
            //Response.Write("Duplicate Error:"+ex.ToString());
            Response.Write("Duplicate Error:" + ex.ToString());
            conn.Close();
            return;
        }
        finally
        {
            //conn.Close();
        }


    }
}