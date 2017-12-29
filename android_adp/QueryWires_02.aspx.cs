using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QueryWires_02 : System.Web.UI.Page
{
    string v_sqlstr,v_wireids;
    thomas_Conn tsconn = new thomas_Conn();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["wireids"] != null)
            {
                v_wireids = Request.Params["wireids"].ToString(); ;

                v_sqlstr = "SELECT W.WIRE_ID,W.WIRE_IDS,W.RAWMTRL_ID,W.HEAT_NO,W.HEAT_NO_NEW,W.DIAMETER,W.WEIGHT, ";
                v_sqlstr += "       W.CO_ID,C.CO_ABBR,TO_CHAR(W.STORE_DATE,'YYYY/MM/DD') STORE_DATE,W.LOCATION ";
                v_sqlstr += " FROM WIRES W,COMPANIES C ";
                v_sqlstr += " WHERE WIRE_IDS LIKE '" + v_wireids + "%' ";
                v_sqlstr += " AND W.CO_ID = C.CO_ID ";


                SDS_wires1.SelectCommand = v_sqlstr;
                DataList1.DataBind();

                v_sqlstr = "SELECT   V.WIRE_ID, V.WIRE_IDS, TO_CHAR(V.PDATE,'YYYY/MM/DD') PDATE, ";
                v_sqlstr += "   V.RAWMTRL_ID, V.DIAMETER, V.HEAT_NO, ";
                v_sqlstr += "   V.PROCESS,V.CO_ID,C.CO_ABBR ";
                v_sqlstr += "FROM  V_WIRES_JOURNEY V,COMPANIES C ";
                v_sqlstr += "WHERE WIRE_IDS LIKE  '" + v_wireids + "%' ";
                v_sqlstr += "AND V.CO_ID = C.CO_ID ";
                v_sqlstr += "ORDER BY PDATE ";

                SDS_wires2.SelectCommand = v_sqlstr;
                GridView1.DataBind();

                tsconn.save_log("guest", "QueryWires_02", "192.168.0.19", "查詢卷號:" + v_wireids);
            }
            else
            {
                Response.Write("No Data Found!");
            }
        }
    }
}