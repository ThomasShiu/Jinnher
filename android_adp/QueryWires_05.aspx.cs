using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QueryWires_05 : System.Web.UI.Page
{
    string v_sqlstr, v_wireids;
    thomas_Conn tsconn = new thomas_Conn();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["wireids"] != null)
            {
                v_wireids = Request.Params["wireids"].ToString(); ;

                //v_sqlstr = "SELECT W.COIL_NO,W.WIR_KIND,W.DRAW_DIA,W.ASSM_NO,DECODE(W.MAKER,'CSC','中鋼','XING','邢台',W.MAKER) MAKER ";
                //v_sqlstr += "     ,W.LUO_NO,W.COIL_WT,W.STOK_DATE,W.LOCATE,DECODE(W.WIR_TYPE,'W','盤元','D','線材') WIR_TYPE ";
                //v_sqlstr += " FROM ASK_DRW_STOK W ";
                //v_sqlstr += " WHERE COIL_NO LIKE '" + v_wireids + "%' ";


                v_sqlstr = "SELECT  ";
                v_sqlstr += "   COIL_NO, ";   // 卷號 
                v_sqlstr += "   WIR_KIND, ";  // 材質 
                v_sqlstr += "   DRAW_DIA, ";  // 線徑 
                v_sqlstr += "   ASSM_NO,  ";  // 製程組合 
                v_sqlstr += "   ASSM_ABBR, "; // 製程簡稱 
                v_sqlstr += "   ASSM_NAME, "; // 製程流程 
                v_sqlstr += "   DECODE(MAKER,'CSC','中鋼','XING','邢台',MAKER) MAKER,    ";  // 廠牌 
                v_sqlstr += "   LUO_NO,  ";   // 爐號 
                v_sqlstr += "   NOW_WT,   ";  // 目前庫存重 
                v_sqlstr += "   W_STATUS   "; // 狀態 
                v_sqlstr += "  FROM V_HEA_DRW_STOK ";
                v_sqlstr += "  WHERE COIL_NO LIKE '" + v_wireids + "%' ";

                SDS_wires1.SelectCommand = v_sqlstr;
                DataList1.DataBind();

            }
            else
            {
                Response.Write("No Data Found!");
            }
        }
    }
}