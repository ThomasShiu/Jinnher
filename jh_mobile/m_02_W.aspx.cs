using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class m_02_W : System.Web.UI.Page
{
    string v_sql, pv_kind, v_process,v_year;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Request.Params["kind"] != null))
        {
            pv_kind = Request.Params["kind"].ToString();
            switch (pv_kind)
            {
                case "1":
                    v_process = "球化";
                    break;
                case "2":
                    v_process = "酸洗";
                    break;
                case "3":
                    v_process = "抽線";
                    break;
                default:
                    break;
            }
            v_year = System.DateTime.Now.ToString("yyyy");

            v_sql = " SELECT '" + v_process + "' PROC_NO,TO_CHAR(WRK_DATE,'YYYY/MM')  WRK_DATE,SUM(COIL_WT) TTLWT,KIND ";
            v_sql += "  FROM V_APC_JOB_FNSH ";
            v_sql += "    WHERE   TO_CHAR(WRK_DATE,'YYYY') ='" + v_year + "' ";
            v_sql += "    AND   KIND = '" + pv_kind + "' ";
            v_sql += "    GROUP BY KIND,TO_CHAR(WRK_DATE,'YYYY/MM') ";
            v_sql += "    ORDER BY 2";

            SDS_wire.SelectCommand = v_sql;
        }

    }
}