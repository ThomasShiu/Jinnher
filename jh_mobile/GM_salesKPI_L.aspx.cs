using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Configuration;

public partial class GM_salesKPI_L : System.Web.UI.Page
{
    OracleConnection conn;
    OracleCommand cmd, cmd2;
    OracleDataReader dr, dr2; 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SDS_year.SelectCommand = "SELECT DISTINCT TO_CHAR(WORK_DATE,'YYYY') YY FROM WORKDATE WHERE TO_CHAR(WORK_DATE,'YYYY') <= TO_CHAR(SYSDATE,'YYYY') ORDER BY 1 DESC";
            SDS_year.DataBind();
        }
    }

    protected void IBtn_refresh2_Click(object sender, ImageClickEventArgs e)
    {
        
    }
   
    protected void IBtn_refresh1_Click(object sender, ImageClickEventArgs e)
    {
        //年度每月出貨彙總
        GenChart("");
        //前十大客戶
        //Gen_chartTOP10();
    }

    protected void GenChart(string v_co_id)
    {
        string v_sql_1, v_sql_2, v_this_year, v_prv_year;
        v_this_year = DDL_year.SelectedValue;
        v_prv_year = (Convert.ToInt16(DDL_year.SelectedValue) - 1).ToString();

        v_sql_1 = " SELECT DD.YM,ROUND(NVL(SL.SUM_AMT,0)/1000,2) SUM_AMT,ROUND(NVL(SL.SUM_NW,0)/1000,2) SUM_NW  ";
        v_sql_1 += "FROM   ";
        v_sql_1 += "( SELECT  TO_CHAR(WORK_DATE,'YYYY/MM')  YM  ";
        v_sql_1 += "          FROM  WORKDATE   ";
        v_sql_1 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_this_year + "'  ";
        v_sql_1 += "          GROUP BY   TO_CHAR(WORK_DATE,'YYYY/MM')  ";
        v_sql_1 += "          ORDER BY YM )  DD ,(          ";
        v_sql_1 += "    SELECT   TO_CHAR(PAC_DATE,'YYYY/MM') PAC_DATE,   ";
        v_sql_1 += "       SUM(SUM_AMT) SUM_AMT, SUM(SUM_NW) SUM_NW  ";
        v_sql_1 += "    FROM  SAL_ORDERS_SUM_L   ";
        v_sql_1 += "    WHERE TO_CHAR(PAC_DATE,'YYYY') = '" + v_this_year + "'  ";
        v_sql_1 += "    GROUP BY TO_CHAR(PAC_DATE,'YYYY/MM')   ";
        v_sql_1 += "     ) SL  ";
        v_sql_1 += "WHERE DD.YM = SL.PAC_DATE(+)  ";

        v_sql_2 = " SELECT DD.YM,ROUND(NVL(SL.SUM_AMT,0)/1000,2) SUM_AMT,ROUND(NVL(SL.SUM_NW,0)/1000,2) SUM_NW  ";
        v_sql_2 += "FROM   ";
        v_sql_2 += "( SELECT  TO_CHAR(WORK_DATE,'YYYY/MM')  YM  ";
        v_sql_2 += "          FROM  WORKDATE   ";
        v_sql_2 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_prv_year + "'  ";
        v_sql_2 += "          GROUP BY   TO_CHAR(WORK_DATE,'YYYY/MM')  ";
        v_sql_2 += "          ORDER BY YM )  DD ,(          ";
        v_sql_2 += "    SELECT   TO_CHAR(PAC_DATE,'YYYY/MM') PAC_DATE,   ";
        v_sql_2 += "       SUM(SUM_AMT) SUM_AMT, SUM(SUM_NW) SUM_NW  ";
        v_sql_2 += "    FROM  SAL_ORDERS_SUM_L   ";
        v_sql_2 += "    WHERE TO_CHAR(PAC_DATE,'YYYY') = '" + v_prv_year + "'  ";
        v_sql_2 += "    GROUP BY TO_CHAR(PAC_DATE,'YYYY/MM')   ";
        v_sql_2 += "     ) SL  ";
        v_sql_2 += "WHERE DD.YM = SL.PAC_DATE(+)  ";

        Gen_chart1(this.Chart1, v_sql_1, v_sql_2);

        SDS_orders1.SelectCommand = v_sql_1;
        GridView1.DataBind();
    }

    protected void Gen_chart1(Chart v_chart, string v_sql1, string v_sql2)
    {
        try
        {

            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
            conn.Open();

            cmd = new OracleCommand(v_sql1, conn);
            dr = cmd.ExecuteReader();

            //第一數列
            v_chart.Series["今年"].Points.DataBindXY(dr, "YM", dr, "SUM_NW");
            v_chart.Series["今年"].ToolTip = "今年 \n時間 \t= #VALX \n數據 \t= #VALY{N2}  ";

            cmd = new OracleCommand(v_sql2, conn);
            dr = cmd.ExecuteReader();
            //第二數列
            v_chart.Series["去年"].Points.DataBindXY(dr, "YM", dr, "SUM_NW");
            v_chart.Series["去年"].ToolTip = "去年 \n時間 \t= #VALX \n數據 \t= #VALY{N2}  ";

            Title title = new Title();
            title.Text = DDL_year.SelectedValue + "-內銷業務出貨統計";
            title.Alignment = ContentAlignment.MiddleCenter;
            title.Font = new System.Drawing.Font("Trebuchet MS", 10F, FontStyle.Bold);
            v_chart.Titles.Add(title);


            switch (RBL_switch1.SelectedValue)
            {
                case "spline":
                    {
                        v_chart.Series["今年"].ChartType = SeriesChartType.Spline; //直條圖
                        v_chart.Series["今年"].BorderWidth = 2;
                        v_chart.Series["去年"].ChartType = SeriesChartType.Spline; //直條圖
                        v_chart.Series["去年"].BorderWidth = 2;
                        break;
                    }
                case "column":
                    {
                        v_chart.Series["今年"].ChartType = SeriesChartType.Column; //直條圖
                        v_chart.Series["今年"].BorderWidth = 2;
                        v_chart.Series["去年"].ChartType = SeriesChartType.Column; //直條圖
                        v_chart.Series["去年"].BorderWidth = 2;
                        break;
                    }
            }

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
        catch(Exception err)
        {
            message.Text += err.ToString();
        }

    }
    protected void RBL_switch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        IBtn_refresh1_Click(null, null);
    }
}