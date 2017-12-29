using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;

public partial class wiresinv : System.Web.UI.Page
{
    string Sql_str, Sql_str2;
    thomas_Conn tsconn = new thomas_Conn();

    string v_barcode1 = "";
    string v_rawmtrl = "";
    string v_diameter_new = "";
    string v_process = "";
    string v_weight = "";
    string v_create_emp = "";
    string v_create_date = "";
    string v_coid = "";
    string v_heatno = "";
    string v_location = "";
    string v_storedate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if ((Request.Params["barcode1"] != null) & (Request.Params["location"] != null) &
                (Request.Params["weight"] != null) &
                (Request.Params["emp"] != null) & (Request.Params["date"] != null) )
            {
                v_barcode1 = Request.Params["barcode1"].ToString();
                v_location = Request.Params["location"].ToString();
                v_weight   = Request.Params["weight"].ToString();
                v_create_emp = Request.Params["emp"].ToString();
                v_create_date = Request.Params["date"].ToString();

                if (v_weight.Equals(""))
                {
                    v_weight = "0";
                }
                //盤元盤點
                insertWINV(v_barcode1, v_location, v_weight, v_create_emp, v_create_date);

            }
            else
            {
                Response.Write("No data insert!");
            }
        }
        catch (Exception ex)
        {
            Response.Write("Param Error:");
        }
    }

    private void insertWINV(string v_barcode1, string v_location,  string v_weight, string v_create_emp, string v_create_date)
    {
        //string v_wire_id, v_wire_ids, v_wire_rawmtrl, v_wire_diameter, v_wire_heatno, v_wire_coid;

        Sql_str2 = " SELECT  W.STK_KIND, W.COIL_NO, W.WIR_KIND, "; 
        Sql_str2 += "W.DRAW_DIA, W.ASSM_NO, W.COIL_WT,  ";
        Sql_str2 += "W.MAKER, W.LUO_NO, W.LOCATE,  ";
        Sql_str2 += "W.INP_DATE ";
        Sql_str2 += "FROM  WIRES_INV W ";
        Sql_str2 += "WHERE COIL_NO = :WIRE_IDS ";
 
        //建立連線
        //使用web.config conn string
        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jheip"].ConnectionString);
        conn.Open();

        OracleCommand cmd;
        OracleDataReader dr;

        //判斷當天有無此卷號
        try
        {
            cmd = new OracleCommand(Sql_str2, conn);
            cmd.Parameters.Add(new OracleParameter("WIRE_IDS", OracleType.VarChar, 100));
            cmd.Parameters["WIRE_IDS"].Value = v_barcode1;

            //cmd.Parameters.Add(new OracleParameter("PDATE", OracleType.VarChar, 100));
            //cmd.Parameters["PDATE"].Value = v_create_date.Substring(0, 10);

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                Response.Write("Error!卷號重複 Wire Duplicate!");
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


        try
        {

            //v_wire_id = "";
            //v_wire_ids = "";
            //v_wire_rawmtrl = "";
            //v_wire_diameter = "";
            //v_wire_heatno = "";
            //v_wire_coid = "";
            //v_weight = "";

            Sql_str = "INSERT INTO WIRES_INV(STK_KIND, COIL_NO, COIL_WT, LOCATE,CREATE_DATE,CREATE_EMP) ";
            Sql_str += " VALUES( '1','" + v_barcode1 + "'," + v_weight + " ,'" + v_location 
                + "', TO_DATE('" + v_create_date + "','YYYY/MM/DD HH24:MI:SS'),'" + v_create_emp + "' )";
            tsconn.trans_oracle("jheip", Sql_str, "insert");

            Response.Write("Success");
        }
        catch (Exception ex)
        {

            Response.Write("Insert Error:" + ex.ToString());
            conn.Close();
        }
        finally
        {

            conn.Close();
        }
    }
}