using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class m_03_W : System.Web.UI.Page
{
    string v_sql, pv_kind, pv_wrkdate,v_process;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if ((Request.Params["workdate"] != null) & (Request.Params["kind"] != null))
        {
            pv_wrkdate = Request.Params["workdate"].ToString();
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
            v_sql = " SELECT '" + v_process + "' PROC_NO,TO_CHAR(WRK_DATE,'YYYY/MM/DD')  WRK_DATE,SUM(COIL_WT) TTLWT,'" + pv_kind + "' KIND ";
            v_sql += "  FROM V_APC_JOB_FNSH ";
            v_sql += "  WHERE   TO_CHAR(WRK_DATE,'YYYY/MM') ='" + pv_wrkdate + "' ";
            v_sql += "  AND   KIND = '" + pv_kind + "' ";
            v_sql += "  GROUP BY TO_CHAR(WRK_DATE,'YYYY/MM/DD') ";
            v_sql += "  ORDER BY 2";

            SDS_wire.SelectCommand = v_sql;
        }
    }
}