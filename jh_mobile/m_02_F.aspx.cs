using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Configuration;

public partial class m_02_F : System.Web.UI.Page
{
    thomas_Conn tsConn = new thomas_Conn();
    thomas_function tsFun = new thomas_function();
    OracleConnection conn;
    OracleCommand cmd, cmd2, cmd3;
    OracleDataReader dr, dr2, dr3;
    string v_sql1, v_sql2, v_sql3;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (tsFun.chk_verify())
        {
            //Server.Transfer("error.htm");
            Response.Redirect("error2.aspx", false);
            return;
        }


        //圖表chart - 成型產量
        v_sql1 = "SELECT SUBSTR(W1.WORK_DATE,6,2) WORK_DATE,X1.NETWT ";
        v_sql1 += "FROM (SELECT TO_CHAR(W.WORK_DATE,'YYYY/MM') WORK_DATE  FROM WORKDATE W GROUP BY TO_CHAR(W.WORK_DATE,'YYYY/MM') ) W1 ,  ";
        v_sql1 += "         (SELECT X.YM,ROUND(SUM(B0_NETWT+B1_NETWT+B2_NETWT+B3_NETWT+B4_NETWT+B5_NETWT)/1000) NETWT  FROM XLS_F_WT  X GROUP BY X.YM) X1  ";
        v_sql1 += "WHERE  W1.WORK_DATE LIKE '" + DateTime.Now.AddYears(-2).Year.ToString() + "%'  ";
        //v_sql1 += "AND   W1.WORK_DATE >= '" + DateTime.Now.AddYears(-2).AddMonths(-6).ToString("yyyy/MM") + "%'  ";
        v_sql1 += "AND    W1.WORK_DATE = X1.YM(+)  ";

        v_sql2 = "SELECT SUBSTR(W1.WORK_DATE,6,2) WORK_DATE,X1.NETWT ";
        v_sql2 += "FROM (SELECT TO_CHAR(W.WORK_DATE,'YYYY/MM') WORK_DATE  FROM WORKDATE W GROUP BY TO_CHAR(W.WORK_DATE,'YYYY/MM') ) W1 ,  ";
        v_sql2 += "         (SELECT X.YM,ROUND(SUM(B0_NETWT+B1_NETWT+B2_NETWT+B3_NETWT+B4_NETWT+B5_NETWT)/1000) NETWT  FROM XLS_F_WT  X GROUP BY X.YM) X1  ";
        v_sql2 += "WHERE  W1.WORK_DATE LIKE '" + DateTime.Now.AddYears(-1).Year.ToString() + "%'  ";
        //v_sql2 += "AND   W1.WORK_DATE >= '" + DateTime.Now.AddYears(-2).AddMonths(-6).ToString("yyyy/MM") + "%'  ";
        v_sql2 += "AND    W1.WORK_DATE = X1.YM(+)  ";

        v_sql3 = "SELECT SUBSTR(W1.WORK_DATE,6,2) WORK_DATE,X1.NETWT ";
        v_sql3 += "FROM (SELECT TO_CHAR(W.WORK_DATE,'YYYY/MM') WORK_DATE  FROM WORKDATE W GROUP BY TO_CHAR(W.WORK_DATE,'YYYY/MM') ) W1 ,  ";
        v_sql3 += "         (SELECT X.YM,ROUND(SUM(B0_NETWT+B1_NETWT+B2_NETWT+B3_NETWT+B4_NETWT+B5_NETWT)/1000) NETWT  FROM XLS_F_WT  X GROUP BY X.YM) X1  ";
        v_sql3 += "WHERE  W1.WORK_DATE LIKE '" + DateTime.Now.Year.ToString() + "%'  ";
        //v_sql3 += "AND   W1.WORK_DATE >= '" + DateTime.Now.AddYears(-2).AddMonths(-6).ToString("yyyy/MM") + "%'  ";
        v_sql3 += "AND    W1.WORK_DATE = X1.YM(+)  ";

        Gen_chart(this.Chart1, v_sql1, v_sql2, v_sql3, "Jinnher");

        //圖表chart2
        v_sql1 = "SELECT W.WORK_DATE,SUM(W.NETWT) OVER(ORDER BY W.WORK_DATE) as NETWT FROM ( ";
        v_sql1 += "SELECT SUBSTR(W1.WORK_DATE,6,2) WORK_DATE,X1.NETWT ";
        v_sql1 += "FROM (SELECT TO_CHAR(W.WORK_DATE,'YYYY/MM') WORK_DATE  FROM WORKDATE W GROUP BY TO_CHAR(W.WORK_DATE,'YYYY/MM') ) W1 ,  ";
        v_sql1 += "         (SELECT X.YM,ROUND(SUM(B0_NETWT+B1_NETWT+B2_NETWT+B3_NETWT+B4_NETWT+B5_NETWT)/1000) NETWT  FROM XLS_F_WT  X GROUP BY X.YM) X1  ";
        v_sql1 += "WHERE  W1.WORK_DATE LIKE '" + DateTime.Now.AddYears(-2).Year.ToString() + "%'  ";
        //v_sql1 += "AND   W1.WORK_DATE >= '" + DateTime.Now.AddYears(-2).AddMonths(-6).ToString("yyyy/MM") + "%'  ";
        v_sql1 += "AND    W1.WORK_DATE = X1.YM(+) ) W ";

        v_sql2 = "SELECT W.WORK_DATE,SUM(W.NETWT) OVER(ORDER BY W.WORK_DATE) as NETWT FROM ( ";
        v_sql2 += "SELECT SUBSTR(W1.WORK_DATE,6,2) WORK_DATE,X1.NETWT ";
        v_sql2 += "FROM (SELECT TO_CHAR(W.WORK_DATE,'YYYY/MM') WORK_DATE  FROM WORKDATE W GROUP BY TO_CHAR(W.WORK_DATE,'YYYY/MM') ) W1 ,  ";
        v_sql2 += "         (SELECT X.YM,ROUND(SUM(B0_NETWT+B1_NETWT+B2_NETWT+B3_NETWT+B4_NETWT+B5_NETWT)/1000) NETWT  FROM XLS_F_WT  X GROUP BY X.YM) X1  ";
        v_sql2 += "WHERE  W1.WORK_DATE LIKE '" + DateTime.Now.AddYears(-1).Year.ToString() + "%' ";
        //v_sql2 += "AND   W1.WORK_DATE >= '" + DateTime.Now.AddYears(-2).AddMonths(-6).ToString("yyyy/MM") + "%'  ";
        v_sql2 += "AND    W1.WORK_DATE = X1.YM(+) ) W ";

        v_sql3 = "SELECT W.WORK_DATE,SUM(W.NETWT) OVER(ORDER BY W.WORK_DATE) as NETWT FROM ( ";
        v_sql3 += "SELECT SUBSTR(W1.WORK_DATE,6,2) WORK_DATE,X1.NETWT ";
        v_sql3 += "FROM (SELECT TO_CHAR(W.WORK_DATE,'YYYY/MM') WORK_DATE  FROM WORKDATE W GROUP BY TO_CHAR(W.WORK_DATE,'YYYY/MM') ) W1 ,  ";
        v_sql3 += "         (SELECT X.YM,ROUND(SUM(B0_NETWT+B1_NETWT+B2_NETWT+B3_NETWT+B4_NETWT+B5_NETWT)/1000) NETWT  FROM XLS_F_WT  X GROUP BY X.YM) X1  ";
        v_sql3 += "WHERE  W1.WORK_DATE LIKE '" + DateTime.Now.Year.ToString() + "%'   ";
        //v_sql3 += "AND   W1.WORK_DATE >= '" + DateTime.Now.AddYears(-2).AddMonths(-6).ToString("yyyy/MM") + "%'  ";
        v_sql3 += "AND    W1.WORK_DATE = X1.YM(+) ) W ";

        Gen_chart(this.Chart2, v_sql1, v_sql2, v_sql3, "Jinnher");

        v_sql3 = "SELECT W1.WORK_DATE,X1.NETWT  ";
        v_sql3 += "FROM (SELECT TO_CHAR(W.WORK_DATE,'YYYY/MM') WORK_DATE  FROM WORKDATE W GROUP BY TO_CHAR(W.WORK_DATE,'YYYY/MM') ) W1 ,   ";
        v_sql3 += "         (SELECT X.YM,ROUND(SUM(B0_NETWT+B1_NETWT+B2_NETWT+B3_NETWT+B4_NETWT+B5_NETWT)/1000) NETWT  FROM XLS_F_WT  X GROUP BY X.YM) X1  ";
        v_sql3 += "WHERE  W1.WORK_DATE LIKE '2015%'  ";
        v_sql3 += "AND    W1.WORK_DATE = X1.YM(+)  ";
        v_sql3 += "UNION ALL ";
        v_sql3 += "SELECT 'TOTAL' YM,ROUND(SUM(B0_NETWT+B1_NETWT+B2_NETWT+B3_NETWT+B4_NETWT+B5_NETWT)/1000) NETWT   ";
        v_sql3 += "FROM XLS_F_WT  X WHERE X.YM LIKE '2015%' ";
        v_sql3 += "ORDER BY 1 ";

        SDS_f.SelectCommand = v_sql3;
    }

    protected void Gen_chart(Chart v_chart, string v_sql1, string v_sql2, string v_sql3, string v_Place)
    {
        try
        {
            // OracleDataAdapter adp;
            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
            conn.Open();
            //前年
            cmd = new OracleCommand(v_sql1, conn);
            dr = cmd.ExecuteReader();
            //第一數列
            v_chart.Series["前年"].Points.DataBindXY(dr, "WORK_DATE", dr, "NETWT");
            v_chart.Series["前年"].ToolTip = "去年 \n月份 = #VALX \n公噸 = #VALY  ";
            cmd.Dispose();
            dr.Close();

            //去年
            cmd = new OracleCommand(v_sql2, conn);
            dr = cmd.ExecuteReader();
            //第二數列
            v_chart.Series["去年"].Points.DataBindXY(dr, "WORK_DATE", dr, "NETWT");
            v_chart.Series["去年"].ToolTip = "去年 \n月份 = #VALX \n公噸 = #VALY  ";
            cmd.Dispose();
            dr.Close();

            //今年
            cmd = new OracleCommand(v_sql3, conn);
            dr = cmd.ExecuteReader();
            //第三數列
            v_chart.Series["今年"].Points.DataBindXY(dr, "WORK_DATE", dr, "NETWT");
            v_chart.Series["今年"].ToolTip = "今年 \n月份 = #VALX \n公噸 = #VALY  ";
            cmd.Dispose();
            dr.Close();

            // Set Back Color
            v_chart.BackColor = Color.SkyBlue;
            // Set Back Gradient End Color
            v_chart.BackSecondaryColor = Color.AliceBlue;
            // Set Hatch Style
            //Chart1.BackHatchStyle = ChartHatchStyle.DashedHorizontal;
            // Set Gradient Type
            v_chart.BackGradientStyle = GradientStyle.TopBottom;
            // Set Border Color
            v_chart.BorderColor = Color.Black;
            // Set Border Width
            v_chart.BorderWidth = 10;

            cmd.Dispose();
            conn.Close();
        }
        catch
        {
            cmd.Dispose();
            //cmd2.Dispose();
            conn.Close();
        }

    }
}