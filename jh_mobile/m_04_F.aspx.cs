using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class m_04_F : System.Web.UI.Page
{
    thomas_Conn tsConn = new thomas_Conn();
    thomas_function tsFun = new thomas_function();
    OracleConnection conn;
    OracleCommand cmd, cmd2, cmd3;
    OracleDataReader dr, dr2, dr3;
    string v_sql1, v_sql2, v_sql3,v_ym;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (tsFun.chk_verify())
        {
            //Server.Transfer("error.htm");
            Response.Redirect("error2.aspx", false);
            return;
        }
        if (!IsPostBack)
        {
            TB_year.Text = DateTime.Today.Year.ToString();
            //string v_str = DateTime.Today.ToString("MM");
            DDL_month.SelectedIndex = DDL_month.Items.IndexOf(DDL_month.Items.FindByValue(DateTime.Today.ToString("MM")));

            v_ym = TB_year.Text + "/" + DDL_month.SelectedValue;

            v_sql3 = "SELECT X.YM||'/'||X.D WORK_DATE,ROUND(SUM(B0_NETWT+B1_NETWT+B2_NETWT+B3_NETWT+B4_NETWT+B5_NETWT)/1000) NETWT   ";
            v_sql3 += " FROM XLS_F_WT  X ";
            v_sql3 += " WHERE X.YM LIKE '" + v_ym + "%'  ";
            v_sql3 += " GROUP BY X.YM,X.D ";
            v_sql3 += " UNION ALL ";
            v_sql3 += " SELECT 'TOTAL' WORK_DATE,ROUND(SUM(B0_NETWT+B1_NETWT+B2_NETWT+B3_NETWT+B4_NETWT+B5_NETWT)/1000) NETWT   ";
            v_sql3 += " FROM XLS_F_WT  X  ";
            v_sql3 += " WHERE X.YM LIKE '" + v_ym + "%'  ";

            SDS_f.SelectCommand = v_sql3;
        }
        
    }
    protected void btn_query_Click(object sender, EventArgs e)
    {
        v_ym = TB_year.Text + "/" + DDL_month.SelectedValue;
        v_sql3 = "SELECT X.YM||'/'||X.D WORK_DATE,ROUND(SUM(B0_NETWT+B1_NETWT+B2_NETWT+B3_NETWT+B4_NETWT+B5_NETWT)/1000) NETWT   ";
        v_sql3 += " FROM XLS_F_WT  X ";
        v_sql3 += " WHERE X.YM LIKE '" + v_ym + "%'  ";
        v_sql3 += " GROUP BY X.YM,X.D ";
        v_sql3 += " UNION ALL ";
        v_sql3 += " SELECT 'TOTAL' WORK_DATE,ROUND(SUM(B0_NETWT+B1_NETWT+B2_NETWT+B3_NETWT+B4_NETWT+B5_NETWT)/1000) NETWT   ";
        v_sql3 += " FROM XLS_F_WT  X  ";
        v_sql3 += " WHERE X.YM LIKE '" + v_ym + "%'  ";

        SDS_f.SelectCommand = v_sql3;
    }
}