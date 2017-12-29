using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class m_03_SAL : System.Web.UI.Page
{
    string v_sql, pv_kind, v_process, v_year;

    protected void Page_Load(object sender, EventArgs e)
    {
        v_year = System.DateTime.Now.ToString("yyyy");

        v_sql = "SELECT DD.YM, ROUND(NVL(P.TTLAMT,0)) TTLAMT,ROUND(NVL(P.TTLWT,0)) TTLWT ";
        v_sql += "FROM ";
        v_sql += "(   SELECT  TO_CHAR(WORK_DATE,'YYYY/MM')  YM ";
        v_sql += "         FROM  WORKDATE  ";
        v_sql += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_year + "' ";
        v_sql += "          GROUP BY   TO_CHAR(WORK_DATE,'YYYY/MM') ";
        v_sql += "          ORDER BY YM ";
        v_sql += "   )  DD, ";
        v_sql += "   ( SELECT  TO_CHAR (CLOSING, 'YYYY/MM')  CLOSING, ";
        v_sql += "             SUM (TTLAMT) TTLAMT, ";
        v_sql += "            SUM (TTLWT) TTLWT ";
        v_sql += "       FROM SAL_PACKING_SUM  ";
        v_sql += "       WHERE TO_CHAR (CLOSING, 'YYYY') = '" + v_year + "' ";
        v_sql += "       GROUP BY  TO_CHAR (CLOSING, 'YYYY/MM') ";
        v_sql += "       ) P ";
        v_sql += "WHERE  1=1 ";
        v_sql += "AND DD.YM = P.CLOSING(+) ";
        v_sql += "UNION ALL ";
        v_sql += "SELECT  'TOTAL'  CLOSING, ";
        v_sql += "    ROUND(SUM (TTLAMT)) TTLAMT, ";
        v_sql += "    ROUND(SUM (TTLWT)) TTLWT ";
        v_sql += "FROM SAL_PACKING_SUM  ";
        v_sql += "WHERE TO_CHAR (CLOSING, 'YYYY') = '" + v_year + "' ";

        SDS_wire.SelectCommand = v_sql;
    }
}