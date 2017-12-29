using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class m_07_W : System.Web.UI.Page
{
    string v_sql, pv_kind, pv_mark,pv_dia, v_process;

    protected void Page_Load(object sender, EventArgs e)
    {

        
            //pv_mark = Request.Params["mark"].ToString();
            //pv_kind = Request.Params["wirkind"].ToString();
            //pv_dia = Request.Params["dia"].ToString();

            v_sql = " SELECT STK_VEN_NO,COIL_NO,WIR_KIND,ORI_DRAW_DIA,DRAW_DIA,MAKER,LUO_NO, ";
            v_sql += "       SP_ABS_ASSM_NO.GET_ASSM_ABBR(ASSM_NO,0) ASSM_NO,COIL_WT,TO_CHAR(INP_DATE,'YYYY/MM/DD') INP_DATE ";
            v_sql += " FROM V_ASK3412Q ";
            v_sql += " WHERE END_CODE = 'N' ";
            v_sql += " AND COIL_WT <> 0 ";
            v_sql += " AND STK_VEN_NO = 'JHHD' ";
            v_sql += " AND WIR_TYPE = 'D' ";
            v_sql += " ORDER BY WIR_KIND,DRAW_DIA,ASSM_NO,COIL_NO ";

            SDS_wire.SelectCommand = v_sql;
        
    }
}