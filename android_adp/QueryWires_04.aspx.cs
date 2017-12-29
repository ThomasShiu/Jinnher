using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QueryWires_04 : System.Web.UI.Page
{
    string v_sqlstr, v_loc;
    thomas_Conn tsconn = new thomas_Conn();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["loc"] != null)
            {
                v_loc = Request.Params["loc"].ToString();

                v_sqlstr = "SELECT  I.LOCATE,NVL(W.WIR_KIND,'卷號錯誤') WIR_KIND,W.DRAW_DIA,W.LUO_NO, ";
                v_sqlstr += "             SUM(W.COIL_WT) COIL_WT,COUNT(*) CNT ";
                v_sqlstr += "FROM WIRES_INV I,V_ASK3412Q W ";
                v_sqlstr += "WHERE 1=1 AND I.LOCATE = '" + v_loc + "' ";
                v_sqlstr += "AND I.COIL_NO = W.COIL_NO(+) ";
                v_sqlstr += "GROUP BY I.LOCATE,W.WIR_KIND,W.DRAW_DIA,W.LUO_NO ";
                v_sqlstr += "ORDER BY 1,2,3 ";

                SDS_wireinv1.SelectCommand = v_sqlstr;

                v_sqlstr = "SELECT  I.COIL_NO,I.LOCATE,I.CREATE_EMP,I.CREATE_DATE, ";
                v_sqlstr += "       W.COIL_WT,W.LOCATE LOCATE2,NVL(W.WIR_KIND,'卷號錯誤') WIR_KIND,W.DRAW_DIA, ";
                v_sqlstr += "       W.ORI_DRAW_DIA,W.ASSM_NO,W.LUO_NO,W.INP_DATE ";
                v_sqlstr += " FROM WIRES_INV I,V_ASK3412Q W ";
                v_sqlstr += " WHERE 1=1 AND I.LOCATE = '" + v_loc + "' ";
                v_sqlstr += " AND I.COIL_NO = W.COIL_NO(+) ";
                v_sqlstr += " ORDER BY I.LOCATE,W.WIR_KIND,W.DRAW_DIA,I.COIL_NO";

                SDS_wires1.SelectCommand = v_sqlstr;
                DataList1.DataBind();

                tsconn.save_log("guest", "QueryWires_04", "192.168.0.19", "查詢盤點卷號，儲區:" + v_loc);
            }
            else
            {
                Response.Write("No Data Found!");
            }
        }
    }
}