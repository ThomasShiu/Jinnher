using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class m_02_SAL : System.Web.UI.Page
{
    string v_sql, pv_kind, v_process, v_year;

    protected void Page_Load(object sender, EventArgs e)
    {
        v_year = System.DateTime.Now.ToString("yyyy");

        v_sql = " SELECT DD.YM,ROUND(NVL(TTLAMT,0),0) TTLAMT,ROUND(NVL(TTLWT,0),0) TTLWT  ";
        v_sql += "FROM  ";
        v_sql += "( SELECT  TO_CHAR(WORK_DATE,'YYYY/MM') YM  ";
        v_sql += "          FROM  WORKDATE   ";
        v_sql += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_year + "'  ";
        v_sql += "          GROUP BY   TO_CHAR(WORK_DATE,'YYYY/MM')  ";
        v_sql += "          ORDER BY YM )  DD ,  ";
        v_sql += " (SELECT  TO_CHAR(BK_DATE,'YYYY/MM') BK_DATE  ";
        v_sql += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT  ";
        v_sql += "  FROM   SAL_ORDERS_SUM  ";
        v_sql += "  WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_year + "'   ";
        v_sql += "  GROUP BY  TO_CHAR(BK_DATE,'YYYY/MM')   ) O  ";
        v_sql += "WHERE 1=1  ";
        v_sql += "AND DD.YM =O.BK_DATE(+) ";
        v_sql += "UNION ALL ";
        v_sql += "SELECT  'TOTAL' BK_DATE  ";
        v_sql += "            ,ROUND(SUM(TTLAMT)) AS TTLAMT ,ROUND(SUM(TTLWT)) TTLWT  ";
        v_sql += "  FROM   SAL_ORDERS_SUM  ";
        v_sql += "  WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_year + "'   ";

        SDS_wire.SelectCommand = v_sql;
    }
}