using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class m_05_W : System.Web.UI.Page
{
    string v_sql, pv_kind, pv_wrkdate, v_process,v_year;
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Request.Params["workdate"] != null) & (Request.Params["kind"] != null))
        {
            pv_wrkdate = Request.Params["workdate"].ToString();
            pv_kind = Request.Params["kind"].ToString();
            switch (pv_kind)
            {
                case "A":
                    v_process = "球化代工";
                    break;
                case "P":
                    v_process = "酸洗代工";
                    break;
                case "D":
                    v_process = "抽線代工";
                    break;
                default:
                    break;
            }
            v_year = System.DateTime.Now.ToString("yyyy");

            v_sql = " SELECT  '" + v_process + "' PROC_NO,TO_CHAR(PDATE,'YYYY/MM/DD') WRK_DATE,SUM(FINISH_WT) TTLWT,'" + pv_kind + "' KIND ";
            v_sql += "FROM WIRE_OEM_DAILY ";
            v_sql += "WHERE PROCESS LIKE '%" + pv_kind + "' ";
            v_sql += "AND   TO_CHAR(PDATE,'YYYY/MM') = '" + pv_wrkdate + "' ";
            v_sql += "GROUP BY   TO_CHAR(PDATE,'YYYY/MM/DD')  ";
            v_sql += "ORDER BY 2 ";

            SDS_wire.SelectCommand = v_sql;
        }
    }
}