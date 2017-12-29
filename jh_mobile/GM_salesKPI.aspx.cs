using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Configuration;

public partial class GM_salesKPI : System.Web.UI.Page
{
    string v_sql1, v_sql2, v_sql3, v_sql4;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //圖表chart
            v_sql1 = "SELECT S.CUS_ID,S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
            v_sql1 += "            SUM(S.AMT) AMT,SUM(S.NW) NW  ";
            v_sql1 += "FROM SHP_SUM_SALES S,SYS_USER U ";
            v_sql1 += "WHERE S.REP_ID = U.EMPLOY_NO ";
            v_sql1 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2011' ";
            v_sql1 += "AND S.REP_ID = 'B0006' ";
            v_sql1 += "AND S.CUS_ID = 'EUSIIP' ";
            v_sql1 += "GROUP BY S.CUS_ID,S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') ";
            v_sql1 += "ORDER BY S.CUS_ID,S.CURRENCY,TO_CHAR(S.CLOSING,'MM') ";

            v_sql2 = "SELECT S.CUS_ID,S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
            v_sql2 += "            SUM(S.AMT) AMT,SUM(S.NW) NW  ";
            v_sql2 += "FROM SHP_SUM_SALES S,SYS_USER U ";
            v_sql2 += "WHERE S.REP_ID = U.EMPLOY_NO ";
            v_sql2 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2012' ";
            v_sql2 += "AND S.REP_ID = 'B0006' ";
            v_sql2 += "AND S.CUS_ID = 'EUSIIP' ";
            v_sql2 += "GROUP BY S.CUS_ID,S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') ";
            v_sql2 += "ORDER BY S.CUS_ID,S.CURRENCY,TO_CHAR(S.CLOSING,'MM')  ";

            v_sql3 = "SELECT S.CUS_ID,S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
            v_sql3 += "            SUM(S.AMT) AMT,SUM(S.NW) NW  ";
            v_sql3 += "FROM SHP_SUM_SALES S,SYS_USER U ";
            v_sql3 += "WHERE S.REP_ID = U.EMPLOY_NO ";
            v_sql3 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2011' ";
            v_sql3 += "AND S.REP_ID = 'B0006' ";
            v_sql3 += "AND S.CUS_ID = 'EGMWUI' ";
            v_sql3 += "GROUP BY S.CUS_ID,S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') ";
            v_sql3 += "ORDER BY S.CUS_ID,S.CURRENCY,TO_CHAR(S.CLOSING,'MM') ";

            v_sql4 = "SELECT S.CUS_ID,S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
            v_sql4 += "            SUM(S.AMT) AMT,SUM(S.NW) NW  ";
            v_sql4 += "FROM SHP_SUM_SALES S,SYS_USER U ";
            v_sql4 += "WHERE S.REP_ID = U.EMPLOY_NO ";
            v_sql4 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2012' ";
            v_sql4 += "AND S.REP_ID = 'B0006' ";
            v_sql4 += "AND S.CUS_ID = 'EGMWUI' ";
            v_sql4 += "GROUP BY S.CUS_ID,S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') ";
            v_sql4 += "ORDER BY S.CUS_ID,S.CURRENCY,TO_CHAR(S.CLOSING,'MM')  ";

            Gen_chart2(this.Chart1, v_sql1, v_sql2, v_sql3, v_sql4, "Jinnher");

            //圖表chart
            v_sql1 = "SELECT S.CURRENCY,TO_CHAR(S.CLOSING,'MM') CLOSING,  ";
            v_sql1 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW   ";
            v_sql1 += "FROM SHP_SUM_SALES S,SYS_USER U  ";
            v_sql1 += "WHERE S.REP_ID = U.EMPLOY_NO  ";
            v_sql1 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2009'  ";
            v_sql1 += "AND S.REP_ID = 'B0006'  ";
            v_sql1 += "AND S.CURRENCY = 'US$'  ";
            v_sql1 += "GROUP BY  S.CURRENCY,TO_CHAR(S.CLOSING,'MM')  ";
            v_sql1 += "ORDER BY  TO_CHAR(S.CLOSING,'MM') ";

            v_sql2 = "SELECT S.CURRENCY,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
            v_sql2 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW  ";
            v_sql2 += "FROM SHP_SUM_SALES S,SYS_USER U ";
            v_sql2 += "WHERE S.REP_ID = U.EMPLOY_NO ";
            v_sql2 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2010' ";
            v_sql2 += "AND S.REP_ID = 'B0006' ";
            v_sql2 += "AND S.CURRENCY = 'US$' ";
            v_sql2 += "GROUP BY  S.CURRENCY,TO_CHAR(S.CLOSING,'MM') ";
            v_sql2 += "ORDER BY  TO_CHAR(S.CLOSING,'MM')  ";

            v_sql3 = "SELECT S.CURRENCY,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
            v_sql3 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW  ";
            v_sql3 += "FROM SHP_SUM_SALES S,SYS_USER U ";
            v_sql3 += "WHERE S.REP_ID = U.EMPLOY_NO ";
            v_sql3 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2011' ";
            v_sql3 += "AND S.REP_ID = 'B0006' ";
            v_sql3 += "AND S.CURRENCY = 'US$' ";
            v_sql3 += "GROUP BY  S.CURRENCY,TO_CHAR(S.CLOSING,'MM') ";
            v_sql3 += "ORDER BY  TO_CHAR(S.CLOSING,'MM')  ";

            v_sql4 = "SELECT S.CURRENCY,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
            v_sql4 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW  ";
            v_sql4 += "FROM SHP_SUM_SALES S,SYS_USER U ";
            v_sql4 += "WHERE S.REP_ID = U.EMPLOY_NO ";
            v_sql4 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2012' ";
            v_sql4 += "AND S.REP_ID = 'B0006' ";
            v_sql4 += "AND S.CURRENCY = 'US$' ";
            v_sql4 += "GROUP BY  S.CURRENCY,TO_CHAR(S.CLOSING,'MM') ";
            v_sql4 += "ORDER BY  TO_CHAR(S.CLOSING,'MM')  ";

            Gen_chart(this.Chart2, v_sql1, v_sql2, v_sql3, v_sql4, "Jinnher");

            //圖表chart
            v_sql1 = "SELECT S.CURRENCY,TO_CHAR(S.CLOSING,'MM') CLOSING,  ";
            v_sql1 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW   ";
            v_sql1 += "FROM SHP_SUM_SALES S,SYS_USER U  ";
            v_sql1 += "WHERE S.REP_ID = U.EMPLOY_NO  ";
            v_sql1 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2009'  ";
            v_sql1 += "AND S.REP_ID = 'B0006'  ";
            v_sql1 += "AND S.CURRENCY = 'EURO'  ";
            v_sql1 += "GROUP BY  S.CURRENCY,TO_CHAR(S.CLOSING,'MM')  ";
            v_sql1 += "ORDER BY  TO_CHAR(S.CLOSING,'MM') ";

            v_sql2 = "SELECT S.CURRENCY,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
            v_sql2 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW  ";
            v_sql2 += "FROM SHP_SUM_SALES S,SYS_USER U ";
            v_sql2 += "WHERE S.REP_ID = U.EMPLOY_NO ";
            v_sql2 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2010' ";
            v_sql2 += "AND S.REP_ID = 'B0006' ";
            v_sql2 += "AND S.CURRENCY = 'EURO' ";
            v_sql2 += "GROUP BY  S.CURRENCY,TO_CHAR(S.CLOSING,'MM') ";
            v_sql2 += "ORDER BY  TO_CHAR(S.CLOSING,'MM')  ";

            v_sql3 = "SELECT S.CURRENCY,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
            v_sql3 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW  ";
            v_sql3 += "FROM SHP_SUM_SALES S,SYS_USER U ";
            v_sql3 += "WHERE S.REP_ID = U.EMPLOY_NO ";
            v_sql3 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2011' ";
            v_sql3 += "AND S.REP_ID = 'B0006' ";
            v_sql3 += "AND S.CURRENCY = 'EURO' ";
            v_sql3 += "GROUP BY  S.CURRENCY,TO_CHAR(S.CLOSING,'MM') ";
            v_sql3 += "ORDER BY  TO_CHAR(S.CLOSING,'MM')  ";

            v_sql4 = "SELECT S.CURRENCY,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
            v_sql4 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW  ";
            v_sql4 += "FROM SHP_SUM_SALES S,SYS_USER U ";
            v_sql4 += "WHERE S.REP_ID = U.EMPLOY_NO ";
            v_sql4 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2012' ";
            v_sql4 += "AND S.REP_ID = 'B0006' ";
            v_sql4 += "AND S.CURRENCY = 'EURO' ";
            v_sql4 += "GROUP BY  S.CURRENCY,TO_CHAR(S.CLOSING,'MM') ";
            v_sql4 += "ORDER BY  TO_CHAR(S.CLOSING,'MM')  ";

            Gen_chart(this.Chart3, v_sql1, v_sql2, v_sql3, v_sql4, "Jinnher");
        }
    }

    protected void Gen_chart(Chart v_chart, string v_sql1, string v_sql2, string v_sql3, string v_sql4, string v_Place)
    {
        try
        {
            // OracleDataAdapter adp;
            OracleConnection conn;
            OracleCommand cmd, cmd2, cmd3, cmd4;
            OracleDataReader dr, dr2, dr3, dr4;


            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jheip"].ConnectionString);
            conn.Open();

            cmd = new OracleCommand(v_sql1, conn);
            dr = cmd.ExecuteReader();


            cmd2 = new OracleCommand(v_sql2, conn);
            dr2 = cmd2.ExecuteReader();

            cmd3 = new OracleCommand(v_sql3, conn);
            dr3 = cmd3.ExecuteReader();

            cmd4 = new OracleCommand(v_sql4, conn);
            dr4 = cmd4.ExecuteReader();

            //第一數列
            v_chart.Series["大前年"].Points.DataBindXY(dr, "CLOSING", dr, "AMT");

            //第二數列
            v_chart.Series["前年"].Points.DataBindXY(dr2, "CLOSING", dr2, "AMT");

            //第三數列
            v_chart.Series["去年"].Points.DataBindXY(dr3, "CLOSING", dr3, "AMT");

            //第四數列
            v_chart.Series["今年"].Points.DataBindXY(dr4, "CLOSING", dr4, "AMT");

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

        }

    }

    protected void Gen_chart2(Chart v_chart, string v_sql1, string v_sql2, string v_sql3, string v_sql4, string v_Place)
    {
        try
        {
            // OracleDataAdapter adp;
            OracleConnection conn;
            OracleCommand cmd, cmd2, cmd3, cmd4;
            OracleDataReader dr, dr2, dr3, dr4;


            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jheip"].ConnectionString);
            conn.Open();

            cmd = new OracleCommand(v_sql1, conn);
            dr = cmd.ExecuteReader();


            cmd2 = new OracleCommand(v_sql2, conn);
            dr2 = cmd2.ExecuteReader();

            cmd3 = new OracleCommand(v_sql3, conn);
            dr3 = cmd3.ExecuteReader();


            cmd4 = new OracleCommand(v_sql4, conn);
            dr4 = cmd4.ExecuteReader();

            //第一數列
            v_chart.Series["去年"].Points.DataBindXY(dr, "CLOSING", dr, "AMT");

            //第二數列
            v_chart.Series["今年"].Points.DataBindXY(dr2, "CLOSING", dr2, "AMT");

            //第三數列
            v_chart.Series["去年2"].Points.DataBindXY(dr3, "CLOSING", dr3, "AMT");

            //第四數列
            v_chart.Series["今年2"].Points.DataBindXY(dr4, "CLOSING", dr4, "AMT");

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
        catch(Exception ex)
        {
            string v_str = ex.ToString();
        }

    }
    protected void DDL_sales_SelectedIndexChanged(object sender, EventArgs e)
    {

        SqlDataSource2.SelectCommand = "SELECT   DISTINCT CUS_ID FROM   SHP_SUM_SALES  WHERE   REP_ID = '" + DDL_sales.SelectedValue + "' ORDER BY CUS_ID";
        DDL_cust.Items.Clear();      //清除內容
        DDL_cust.Items.Add(" ");     //加回第一行空白內容
        DDL_cust.DataBind();


        SqlDataSource3.SelectCommand = "SELECT   DISTINCT CUS_ID FROM   SHP_SUM_SALES  WHERE   REP_ID = '" + DDL_sales.SelectedValue + "' ORDER BY CUS_ID";
        DDL_cust2.Items.Clear();      //清除內容
        DDL_cust2.Items.Add(" ");     //加回第一行空白內容
        DDL_cust2.DataBind();
    }
    protected void DDL_cust_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GenChart(DDL_sales.SelectedValue, DDL_cust.SelectedValue);
    }
    protected void DDL_cust2_SelectedIndexChanged(object sender, EventArgs e)
    {
        GenChart(DDL_sales.SelectedValue, DDL_cust.SelectedValue, DDL_cust2.SelectedValue);
    }
    private void GenChart(string v_sales, string v_cust, string v_cust2)
    {
        //圖表chart
        v_sql1 = "SELECT S.CUS_ID,S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
        v_sql1 += "            SUM(S.AMT) AMT,SUM(S.NW) NW  ";
        v_sql1 += "FROM SHP_SUM_SALES S,SYS_USER U ";
        v_sql1 += "WHERE S.REP_ID = U.EMPLOY_NO ";
        v_sql1 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2011' ";
        v_sql1 += "AND S.REP_ID = '" + v_sales + "' ";
        v_sql1 += "AND S.CUS_ID = '" + v_cust + "' ";
        v_sql1 += "GROUP BY S.CUS_ID,S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') ";
        v_sql1 += "ORDER BY S.CUS_ID,S.CURRENCY,TO_CHAR(S.CLOSING,'MM') ";

        v_sql2 = "SELECT S.CUS_ID,S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
        v_sql2 += "            SUM(S.AMT) AMT,SUM(S.NW) NW  ";
        v_sql2 += "FROM SHP_SUM_SALES S,SYS_USER U ";
        v_sql2 += "WHERE S.REP_ID = U.EMPLOY_NO ";
        v_sql2 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2012' ";
        v_sql2 += "AND S.REP_ID = '" + v_sales + "' ";
        v_sql2 += "AND S.CUS_ID = '" + v_cust + "' ";
        v_sql2 += "GROUP BY S.CUS_ID,S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') ";
        v_sql2 += "ORDER BY S.CUS_ID,S.CURRENCY,TO_CHAR(S.CLOSING,'MM')  ";

        v_sql3 = "SELECT S.CUS_ID,S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
        v_sql3 += "            SUM(S.AMT) AMT,SUM(S.NW) NW  ";
        v_sql3 += "FROM SHP_SUM_SALES S,SYS_USER U ";
        v_sql3 += "WHERE S.REP_ID = U.EMPLOY_NO ";
        v_sql3 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2011' ";
        v_sql3 += "AND S.REP_ID = '" + v_sales + "' ";
        v_sql3 += "AND S.CUS_ID = '" + v_cust2 + "' ";
        v_sql3 += "GROUP BY S.CUS_ID,S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') ";
        v_sql3 += "ORDER BY S.CUS_ID,S.CURRENCY,TO_CHAR(S.CLOSING,'MM') ";

        v_sql4 = "SELECT S.CUS_ID,S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
        v_sql4 += "            SUM(S.AMT) AMT,SUM(S.NW) NW  ";
        v_sql4 += "FROM SHP_SUM_SALES S,SYS_USER U ";
        v_sql4 += "WHERE S.REP_ID = U.EMPLOY_NO ";
        v_sql4 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2012' ";
        v_sql4 += "AND S.REP_ID = '" + v_sales + "' ";
        v_sql4 += "AND S.CUS_ID = '" + v_cust2 + "' ";
        v_sql4 += "GROUP BY S.CUS_ID,S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') ";
        v_sql4 += "ORDER BY S.CUS_ID,S.CURRENCY,TO_CHAR(S.CLOSING,'MM')  ";

       
        Gen_chart2(this.Chart1, v_sql1, v_sql2, v_sql3, v_sql4, "Jinnher");

        //圖表chart
        //v_sql1 = "SELECT S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING,  ";
        //v_sql1 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW   ";
        //v_sql1 += "FROM SHP_SUM_SALES S,SYS_USER U  ";
        //v_sql1 += "WHERE S.REP_ID = U.EMPLOY_NO  ";
        //v_sql1 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2009'  ";
        //v_sql1 += "AND S.REP_ID = '" + v_sales + "' ";  
        //v_sql1 += "AND S.CURRENCY = 'US$'  ";
        //v_sql1 += "GROUP BY  S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM')  ";
        //v_sql1 += "ORDER BY  TO_CHAR(S.CLOSING,'MM') ";

        //v_sql2 = "SELECT S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
        //v_sql2 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW  ";
        //v_sql2 += "FROM SHP_SUM_SALES S,SYS_USER U ";
        //v_sql2 += "WHERE S.REP_ID = U.EMPLOY_NO ";
        //v_sql2 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2010' ";
        //v_sql2 += "AND S.REP_ID = '" + v_sales + "' ";
        //v_sql2 += "AND S.CURRENCY = 'US$' ";
        //v_sql2 += "GROUP BY  S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') ";
        //v_sql2 += "ORDER BY  TO_CHAR(S.CLOSING,'MM')  ";

        //v_sql3 = "SELECT S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
        //v_sql3 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW  ";
        //v_sql3 += "FROM SHP_SUM_SALES S,SYS_USER U ";
        //v_sql3 += "WHERE S.REP_ID = U.EMPLOY_NO ";
        //v_sql3 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2011' ";
        //v_sql3 += "AND S.REP_ID = '" + v_sales + "' ";
        //v_sql3 += "AND S.CURRENCY = 'US$' ";
        //v_sql3 += "GROUP BY  S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') ";
        //v_sql3 += "ORDER BY  TO_CHAR(S.CLOSING,'MM')  ";

        //v_sql4 = "SELECT S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
        //v_sql4 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW  ";
        //v_sql4 += "FROM SHP_SUM_SALES S,SYS_USER U ";
        //v_sql4 += "WHERE S.REP_ID = U.EMPLOY_NO ";
        //v_sql4 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2012' ";
        //v_sql4 += "AND S.REP_ID = '" + v_sales + "' ";
        //v_sql4 += "AND S.CURRENCY = 'US$' ";
        //v_sql4 += "GROUP BY  S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') ";
        //v_sql4 += "ORDER BY  TO_CHAR(S.CLOSING,'MM')  ";

        //Gen_chart2(this.Chart2, v_sql1, v_sql2, v_sql3, v_sql4, "Jinnher");

        //圖表chart
        //v_sql1 = "SELECT S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING,  ";
        //v_sql1 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW   ";
        //v_sql1 += "FROM SHP_SUM_SALES S,SYS_USER U  ";
        //v_sql1 += "WHERE S.REP_ID = U.EMPLOY_NO  ";
        //v_sql1 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2009'  ";
        //v_sql1 += "AND S.REP_ID = '" + v_sales + "' ";
        //v_sql1 += "AND S.CURRENCY = 'EURO'  ";
        //v_sql1 += "GROUP BY  S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM')  ";
        //v_sql1 += "ORDER BY  TO_CHAR(S.CLOSING,'MM') ";

        //v_sql2 = "SELECT S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
        //v_sql2 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW  ";
        //v_sql2 += "FROM SHP_SUM_SALES S,SYS_USER U ";
        //v_sql2 += "WHERE S.REP_ID = U.EMPLOY_NO ";
        //v_sql2 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2010' ";
        //v_sql2 += "AND S.REP_ID = '" + v_sales + "' ";
        //v_sql2 += "AND S.CURRENCY = 'EURO' ";
        //v_sql2 += "GROUP BY  S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') ";
        //v_sql2 += "ORDER BY  TO_CHAR(S.CLOSING,'MM')  ";

        //v_sql3 = "SELECT S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
        //v_sql3 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW  ";
        //v_sql3 += "FROM SHP_SUM_SALES S,SYS_USER U ";
        //v_sql3 += "WHERE S.REP_ID = U.EMPLOY_NO ";
        //v_sql3 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2011' ";
        //v_sql3 += "AND S.REP_ID = '" + v_sales + "' ";
        //v_sql3 += "AND S.CURRENCY = 'EURO' ";
        //v_sql3 += "GROUP BY  S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') ";
        //v_sql3 += "ORDER BY  TO_CHAR(S.CLOSING,'MM')  ";

        //v_sql4 = "SELECT S.CURRENCY,S.REP_ID,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') CLOSING, ";
        //v_sql4 += "            ROUND(SUM(S.AMT)/1000) AMT,ROUND(SUM(S.NW)/1000) NW  ";
        //v_sql4 += "FROM SHP_SUM_SALES S,SYS_USER U ";
        //v_sql4 += "WHERE S.REP_ID = U.EMPLOY_NO ";
        //v_sql4 += "AND  TO_CHAR(S.CLOSING,'YYYY') = '2012' ";
        //v_sql4 += "AND S.REP_ID = '" + v_sales + "' ";
        //v_sql4 += "AND S.CURRENCY = 'EURO' ";
        //v_sql4 += "GROUP BY  S.CURRENCY,S.REP_ID ,U.LOGIN_NAME,TO_CHAR(S.CLOSING,'MM') ";
        //v_sql4 += "ORDER BY  TO_CHAR(S.CLOSING,'MM')  ";

        //Gen_chart2(this.Chart3, v_sql1, v_sql2, v_sql3, v_sql4, "Jinnher");
    }
   
}