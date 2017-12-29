using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class m_08_W : System.Web.UI.Page
{
    string v_sql;
    protected void Page_Load(object sender, EventArgs e)
    {
        v_sql = " SELECT DECODE(LOCATE,'0','未加工-本廠','14','未加工-岡山廠') LOCATE,COUNT(*) COIL_CNT,SUM(COIL_WT) COIL_WT ";
        v_sql += " FROM V_ASK3412Q ";
        v_sql += " WHERE END_CODE = 'N' AND WIR_TYPE = 'W' AND STK_VEN_NO IN ('JH') AND COIL_WT <> 0 AND ASSM_NO IN ('0000') ";
        v_sql += " GROUP BY LOCATE ";
        v_sql += "   UNION ALL ";
        v_sql += " SELECT DECODE(ASSM_NO,'0010B','待球','0110A','球化','') LOCATE,COUNT(*) COIL_CNT,SUM(COIL_WT) COIL_WT ";
        v_sql += " FROM V_ASK3412Q ";
        v_sql += " WHERE END_CODE = 'N' AND WIR_TYPE = 'W' AND STK_VEN_NO IN ('JH') AND COIL_WT <> 0 AND ASSM_NO IN ('0010B','0110A') ";
        v_sql += " GROUP BY ASSM_NO ";
        v_sql += "   UNION ALL ";
        v_sql += " SELECT NVL(LOCATE,STK_VEN_NO) LOCATE,COUNT(*) COIL_CNT,SUM(COIL_WT) COIL_WT ";
        v_sql += " FROM V_ASK3412Q ";
        v_sql += " WHERE END_CODE = 'N' ";
        v_sql += " AND WIR_TYPE = 'W' AND COIL_WT <> 0 AND STK_VEN_NO NOT IN ('JH') ";
        v_sql += " GROUP BY NVL(LOCATE,STK_VEN_NO) ";

        SDS_wire.SelectCommand = v_sql;
    }
}