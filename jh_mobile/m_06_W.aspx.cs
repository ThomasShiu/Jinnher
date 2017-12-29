using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class m_06_W : System.Web.UI.Page
{
    string v_sql, pv_kind, v_process, v_year;

    protected void Page_Load(object sender, EventArgs e)
    {

        v_year = System.DateTime.Now.ToString("yyyy");

        v_sql = " SELECT S.MAKER,S.WIR_KIND,S.DRAW_DIA, ";
        v_sql += "  SUM(S.COIL_WT) TTLWT,COUNT(*) CNT ";
        v_sql += "FROM V_ASK3412Q S ";
        v_sql += "WHERE COIL_WT <> 0 ";
        v_sql += "AND END_CODE = 'N' ";
        v_sql += "AND STK_VEN_NO = 'JH' ";
        v_sql += "AND ASSM_NO = '0000' ";
        v_sql += "GROUP BY S.MAKER,S.WIR_KIND,S.DRAW_DIA ";
        v_sql += "ORDER BY 2,3,1 ";

        SDS_wire.SelectCommand = v_sql;
    }
}