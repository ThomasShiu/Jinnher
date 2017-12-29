using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Configuration;

public partial class GM_salesKPI_5 : System.Web.UI.Page
{
    OracleConnection conn;
    OracleCommand cmd, cmd2;
    OracleDataReader dr, dr2;
    thomas_Conn ts_conn = new thomas_Conn();
    thomas_function ts_Fun = new thomas_function();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ts_Fun.chk_verify())
        {
            //Server.Transfer("error.htm");
            Response.Redirect("error2.aspx", false);
            return;
        }
        if (!IsPostBack)
        {

            SDS_year.SelectCommand = "SELECT DISTINCT TO_CHAR(WORK_DATE,'YYYY') YY FROM WORKDATE WHERE TO_CHAR(WORK_DATE,'YYYY') <= TO_CHAR(SYSDATE,'YYYY') ORDER BY 1 DESC";
            DDL_year.DataBind();

            SDS_year2.SelectCommand = "SELECT DISTINCT TO_CHAR(WORK_DATE,'YYYY') YY FROM WORKDATE WHERE TO_CHAR(WORK_DATE,'YYYY') <= TO_CHAR(SYSDATE,'YYYY') ORDER BY 1 DESC";
            DDL_year2.DataBind();

            //紀錄LOG
            string strClientIP = Context.Request.ServerVariables["REMOTE_ADDR"];
            ts_conn.save_log("Guest", "GM_salesKPI_5", strClientIP, "業務外銷出貨統計");
        }
    }

    protected void IBtn_refresh1_Click(object sender, ImageClickEventArgs e)
    {
        string v_sql_1, v_sql_2, v_this_year, v_prv_year;

        v_this_year = DDL_year.SelectedValue;
        v_sql_1 = "SELECT C.EMP_NO EMPLOY_NO,C.EMP_NAME, ";
        v_sql_1 += "      SUM (TTLAMT) TTLAMT, ";
        v_sql_1 += "      SUM (TTLWT) TTLWT ";
        v_sql_1 += "   FROM SAL_PACKING_SUM P,SAL_CUST_MAP C ";
        v_sql_1 += "   WHERE TO_CHAR (P.CLOSING, 'YYYY') = '" + v_this_year + "' ";
        v_sql_1 += "   AND  P.CUS_ID = C.CO_CO_ID(+) ";
        v_sql_1 += "   GROUP BY C.EMP_NO,C.EMP_NAME ";
        v_sql_1 += "   ORDER BY 4 DESC ";

        DS_sales.SelectCommand = v_sql_1;

        GridView1.DataBind();
        GridView2.SelectedIndex = -1;
        GridView3.SelectedIndex = -1;
        GridView3.DataBind();

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label L_EMPLOY_NO = (Label)GridView2.Rows[index].Cells[0].FindControl("L_EMPLOY_NO");

        if (e.CommandName == "Select")
        {
            QueryCust(L_EMPLOY_NO.Text);

        }
    }
    //客戶查詢
    protected void QueryCust(string V_EMPLOY_NO)
    {
        string v_sql_1, v_sql_2, v_this_year, v_prv_year;
        v_this_year = DDL_year.SelectedValue;

        v_sql_1 = " SELECT C.EMP_NO EMPLOY_NO,P.CUS_ID CO_CO_ID, ";
        v_sql_1 += "        SUM(TTLAMT) TTLAMT, ";
        v_sql_1 += "        SUM(TTLWT) TTLWT ";
        v_sql_1 += "   FROM SAL_PACKING_SUM P,SAL_CUST_MAP C ";
        v_sql_1 += "   WHERE TO_CHAR (CLOSING, 'YYYY') = '" + v_this_year + "' ";
        v_sql_1 += "   AND NVL(C.EMP_NO,1)  = NVL('" + V_EMPLOY_NO + "',1) ";
        v_sql_1 += "   AND P.CUS_ID = C.CO_CO_ID(+) ";
        v_sql_1 += "   GROUP BY CUS_ID,C.EMP_NO ";
        v_sql_1 += "   ORDER BY 4 DESC";

        DS_cust.SelectCommand = v_sql_1;
        //GridView2.DataBind();
    }
    protected void RBL_switch1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView2.SelectedIndex = e.NewSelectedIndex;
        GridView3.SelectedIndex = -1;

        SDS_orders1.SelectCommand = "";
        GridView1.DataBind();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label L_CO_CO_ID = (Label)GridView3.Rows[index].Cells[0].FindControl("L_CO_CO_ID");

        string v_coid = L_CO_CO_ID.Text;

        if (e.CommandName == "Select")
        {
            GenChart(L_CO_CO_ID.Text);
            //GenGrid(L_CO_CO_ID.Text);
            //QueryCustName(L_coid.Text);
            //QueryTTLWT(L_coid.Text);
            //DS_order.SelectCommand = "";
            //ListView1.DataBind();
        }
    }

    protected void GenChart(string v_co_id)
    {
        string v_sql_1, v_sql_2, v_this_year, v_prv_year;
        v_this_year = DDL_year.SelectedValue;
        v_prv_year = (Convert.ToInt16(DDL_year.SelectedValue) - 1).ToString();

        v_sql_1 = "SELECT P.CO_ID,DD.YM, NVL(TTLAMT,0) TTLAMT, NVL(TTLWT,0)  TTLWT ";
        v_sql_1 += "FROM ";
        v_sql_1 += "(   SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
        v_sql_1 += "          FROM  WORKDATE  ";
        v_sql_1 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_this_year + "' ";
        v_sql_1 += "          GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
        v_sql_1 += "          ORDER BY YM   )  DD, ";
        v_sql_1 += "   ( SELECT CUS_ID CO_ID,TO_CHAR (CLOSING, 'MM')  CLOSING, ";
        v_sql_1 += "             SUM (TTLAMT) TTLAMT, ";
        v_sql_1 += "            SUM (TTLWT) TTLWT ";
        v_sql_1 += "       FROM SAL_PACKING_SUM  ";
        v_sql_1 += "       WHERE TO_CHAR (CLOSING, 'YYYY') = '" + v_this_year + "' AND CUS_ID = '" + v_co_id + "' ";
        v_sql_1 += "       GROUP BY CUS_ID,TO_CHAR (CLOSING, 'MM') ";
        v_sql_1 += "       ) P ";
        v_sql_1 += "        WHERE  1=1 ";
        v_sql_1 += "        AND DD.YM = P.CLOSING(+) ";

        v_sql_2 = "SELECT P.CO_ID,DD.YM, NVL(TTLAMT,0) TTLAMT, NVL(TTLWT,0)  TTLWT ";
        v_sql_2 += "FROM ";
        v_sql_2 += "(   SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
        v_sql_2 += "          FROM  WORKDATE  ";
        v_sql_2 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_prv_year + "' ";
        v_sql_2 += "          GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
        v_sql_2 += "          ORDER BY YM   )  DD, ";
        v_sql_2 += "   ( SELECT CUS_ID CO_ID,TO_CHAR (CLOSING, 'MM')  CLOSING, ";
        v_sql_2 += "             SUM (TTLAMT) TTLAMT, ";
        v_sql_2 += "            SUM (TTLWT) TTLWT ";
        v_sql_2 += "       FROM SAL_PACKING_SUM  ";
        v_sql_2 += "       WHERE TO_CHAR (CLOSING, 'YYYY') = '" + v_prv_year + "' AND CUS_ID = '" + v_co_id + "' ";
        v_sql_2 += "       GROUP BY CUS_ID,TO_CHAR (CLOSING, 'MM') ";
        v_sql_2 += "       ) P ";
        v_sql_2 += "        WHERE  1=1 ";
        v_sql_2 += "        AND DD.YM = P.CLOSING(+) ";

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
            v_chart.Series["今年"].Points.DataBindXY(dr, "YM", dr, "TTLWT");
            v_chart.Series["今年"].ToolTip = "今年 \n時間 \t= #VALX \n數據 \t= #VALY{N2}  ";

            cmd = new OracleCommand(v_sql2, conn);
            dr = cmd.ExecuteReader();
            //第二數列
            v_chart.Series["去年"].Points.DataBindXY(dr, "YM", dr, "TTLWT");
            v_chart.Series["去年"].ToolTip = "去年 \n時間 \t= #VALX \n數據 \t= #VALY{N2}  ";

            Title title = new Title();
            title.Text = DDL_year.SelectedValue + "-外銷業務出貨統計";
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
        catch
        {

        }

    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void DDL_year2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void RBL_switch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //年度每月接單彙總
        Gen_chartALLorder();
        //前十大客戶
        Gen_chartTOP10();
    }
    protected void IBtn_refresh2_Click(object sender, ImageClickEventArgs e)
    {
        //年度每月接單彙總
        Gen_chartALLorder();

        //前十大客戶
        Gen_chartTOP10();
    }

    protected void Gen_chartTOP10()
    {
        string v_sql_1, v_sql_2, v_this_year, v_prv_year;
        v_this_year = DDL_year2.SelectedValue;
        v_prv_year = (Convert.ToInt16(DDL_year.SelectedValue) - 1).ToString();

        //取得前十大客戶代號
        v_sql_1 = "  SELECT CO_CO_ID FROM (  ";
        v_sql_1 += "          SELECT   CUS_ID CO_CO_ID ,TO_CHAR(CLOSING,'YYYY') YM,SUM(TTLAMT) TTLAMT , SUM(TTLWT) TTLWT  ";
        v_sql_1 += "          FROM  SAL_PACKING_SUM   ";
        v_sql_1 += "          WHERE TO_CHAR(CLOSING,'YYYY') = '" + v_this_year + "'   ";
        v_sql_1 += "          GROUP BY CUS_ID ,TO_CHAR(CLOSING,'YYYY')   ";
        v_sql_1 += "          ORDER BY 4 DESC )  ";
        v_sql_1 += " WHERE ROWNUM <= 5 ";

        try
        {
            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
            conn.Open();

            //第一層迴圈，前十大客戶
            cmd = new OracleCommand(v_sql_1, conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //組出前十名客戶SQL
                v_sql_2 = " SELECT DD.YM, NVL(P.TTLAMT,0) TTLAMT,NVL(P.TTLWT,0) TTLWT ";
                v_sql_2 += "FROM ";
                v_sql_2 += "(   SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
                v_sql_2 += "          FROM  WORKDATE  ";
                v_sql_2 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_this_year + "' ";
                v_sql_2 += "          GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
                v_sql_2 += "          ORDER BY YM ";
                v_sql_2 += "   )  DD, ";
                v_sql_2 += "   ( SELECT  TO_CHAR (CLOSING, 'MM')  CLOSING, ";
                v_sql_2 += "             SUM (TTLAMT) TTLAMT, ";
                v_sql_2 += "            SUM (TTLWT) TTLWT ";
                v_sql_2 += "       FROM SAL_PACKING_SUM  ";
                v_sql_2 += "       WHERE TO_CHAR (CLOSING, 'YYYY') = '" + v_this_year + "' ";
                v_sql_2 += "       AND CUS_ID = '" + dr[0].ToString() + "' ";
                v_sql_2 += "       GROUP BY  TO_CHAR (CLOSING, 'MM') ";
                v_sql_2 += "       ) P ";
                v_sql_2 += "WHERE  1=1 ";
                v_sql_2 += "AND DD.YM = P.CLOSING(+) ";

                cmd2 = new OracleCommand(v_sql_2, conn);
                dr2 = cmd2.ExecuteReader();

                //Chart2.Series.Clear();

                //數據序列集合
                Chart2.Series.Add(dr[0].ToString());
                Chart2.Series[dr[0].ToString()].Points.DataBindXY(dr2, "YM", dr2, "TTLWT");
                Chart2.Series[dr[0].ToString()].ToolTip = dr[0].ToString() + " \n時間 \t= #VALX \n數據 \t= #VALY{N2}  ";

                switch (RBL_switch2.SelectedValue)
                {
                    case "spline":
                        {
                            Chart2.Series[dr[0].ToString()].ChartType = SeriesChartType.Spline; //直條圖
                            Chart2.Series[dr[0].ToString()].BorderWidth = 2;
                            break;
                        }
                    case "column":
                        {
                            Chart2.Series[dr[0].ToString()].ChartType = SeriesChartType.Column; //直條圖
                            Chart2.Series[dr[0].ToString()].BorderWidth = 2;
                            break;
                        }
                }
                //Chart1.Series["Series1"].ChartType = SeriesChartType.Bar; //橫條圖

                //字體設定
                //Chart2.Series[dr[0].ToString()].Font = new System.Drawing.Font("Trebuchet MS", 10, System.Drawing.FontStyle.Bold);
                //Label 背景色
                //Chart2.Series[dr[0].ToString()].LabelBackColor = Color.FromArgb(150, 255, 255, 255);
                //Chart2.Series[dr[0].ToString()].Color = Color.FromArgb(240, 65, 140, 240); //背景色
                //Chart2.Series[dr[0].ToString()].IsValueShownAsLabel = true; // Show data points labels

                cmd2.Dispose();
            }

            Title title = new Title();
            title.Text = v_this_year + "-外銷業務接單統計(前五大)";
            title.Alignment = ContentAlignment.MiddleCenter;
            title.Font = new System.Drawing.Font("Trebuchet MS", 10F, FontStyle.Bold);
            Chart2.Titles.Add(title);

            //Chart2.ChartAreas["ChartArea1"].AxisY.Maximum = 160;
            Chart2.ChartAreas["ChartArea1"].AxisY.Interval = 1000;

            // Set Back Color
            Chart2.BackColor = Color.SkyBlue;
            // Set Back Gradient End Color
            Chart2.BackSecondaryColor = Color.AliceBlue;
            // Set Hatch Style
            //Chart1.BackHatchStyle = ChartHatchStyle.DashedHorizontal;
            // Set Gradient Type
            Chart2.BackGradientStyle = GradientStyle.TopBottom;
            // Set Border Color
            Chart2.BorderColor = Color.Black;
            // Set Border Width
            //Chart2.BorderWidth = 10;
            cmd.Dispose();

            conn.Close();
        }
        catch (Exception err)
        {
            //message4.Text += err.ToString(); 
            cmd.Dispose();
            conn.Close();
        }
    }

    protected void Gen_chartALLorder()
    {
        string v_sql_1, v_sql_2, v_sql_3, v_this_year, v_prv_year, v_prv_year2;
        v_this_year = DDL_year2.SelectedValue;
        v_prv_year = (Convert.ToInt16(DDL_year2.SelectedValue) - 1).ToString();
        v_prv_year2 = (Convert.ToInt16(DDL_year2.SelectedValue) - 2).ToString();

        try
        {

            //組出每個月總接單
            v_sql_1 = "SELECT DD.YM, NVL(P.TTLAMT,0) TTLAMT,NVL(P.TTLWT,0) TTLWT ";
            v_sql_1 += "FROM ";
            v_sql_1 += "(   SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
            v_sql_1 += "          FROM  WORKDATE  ";
            v_sql_1 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_this_year + "' ";
            v_sql_1 += "          GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
            v_sql_1 += "          ORDER BY YM ";
            v_sql_1 += "   )  DD, ";
            v_sql_1 += "   ( SELECT  TO_CHAR (CLOSING, 'MM')  CLOSING, ";
            v_sql_1 += "             SUM (TTLAMT) TTLAMT, ";
            v_sql_1 += "            SUM (TTLWT) TTLWT ";
            v_sql_1 += "       FROM SAL_PACKING_SUM  ";
            v_sql_1 += "       WHERE TO_CHAR (CLOSING, 'YYYY') = '" + v_this_year + "' ";
            v_sql_1 += "       GROUP BY  TO_CHAR (CLOSING, 'MM') ";
            v_sql_1 += "       ) P ";
            v_sql_1 += "WHERE  1=1 ";
            v_sql_1 += "AND DD.YM = P.CLOSING(+) ";

            //組出每個月總接單-去年
            v_sql_2 = "SELECT DD.YM, NVL(P.TTLAMT,0) TTLAMT,NVL(P.TTLWT,0) TTLWT ";
            v_sql_2 += "FROM ";
            v_sql_2 += "(   SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
            v_sql_2 += "          FROM  WORKDATE  ";
            v_sql_2 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_prv_year + "' ";
            v_sql_2 += "          GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
            v_sql_2 += "          ORDER BY YM ";
            v_sql_2 += "   )  DD, ";
            v_sql_2 += "   ( SELECT  TO_CHAR (CLOSING, 'MM')  CLOSING, ";
            v_sql_2 += "             SUM (TTLAMT) TTLAMT, ";
            v_sql_2 += "            SUM (TTLWT) TTLWT ";
            v_sql_2 += "       FROM SAL_PACKING_SUM  ";
            v_sql_2 += "       WHERE TO_CHAR (CLOSING, 'YYYY') = '" + v_prv_year + "' ";
            v_sql_2 += "       GROUP BY  TO_CHAR (CLOSING, 'MM') ";
            v_sql_2 += "       ) P ";
            v_sql_2 += "WHERE  1=1 ";
            v_sql_2 += "AND DD.YM = P.CLOSING(+) ";

            //標準值，前兩年加總除以2
            v_sql_3 = "SELECT  TO_CHAR (CLOSING, 'MM')  YM, ";
            v_sql_3 += "       ROUND(SUM(TTLAMT)/2) TTLAMT, ";
            v_sql_3 += "       ROUND(SUM(TTLWT)/2)  TTLWT  ";
            v_sql_3 += "       FROM SAL_PACKING_SUM  ";
            v_sql_3 += "       WHERE TO_CHAR (CLOSING, 'YYYY') IN ('" + v_prv_year2 + "','" + v_prv_year + "') ";
            v_sql_3 += "       GROUP BY  TO_CHAR (CLOSING, 'MM') ";



            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
            conn.Open();


            cmd = new OracleCommand(v_sql_1, conn);
            dr = cmd.ExecuteReader();
            //今年數據序列集合
            Chart3.Series["今年"].Points.DataBindXY(dr, "YM", dr, "TTLWT");
            Chart3.Series["今年"].ToolTip = v_this_year + " \n時間 \t= #VALX \n數據 \t= #VALY{N2}  ";
            Chart3.Series["今年"].IsValueShownAsLabel = true;
            Chart3.Series["今年"].LegendText = v_this_year;
            cmd.Dispose();
            dr.Close();

            cmd = new OracleCommand(v_sql_2, conn);
            dr = cmd.ExecuteReader();
            //去年數據序列集合
            Chart3.Series["去年"].Points.DataBindXY(dr, "YM", dr, "TTLWT");
            Chart3.Series["去年"].ToolTip = v_prv_year + " \n時間 \t= #VALX \n數據 \t= #VALY{N2}  ";
            Chart3.Series["去年"].IsValueShownAsLabel = true;
            Chart3.Series["去年"].LegendText = v_prv_year;
            cmd.Dispose();
            dr.Close(); ;

            cmd = new OracleCommand(v_sql_3, conn);
            dr = cmd.ExecuteReader();
            //去年數據序列集合
            Chart3.Series["預測值"].Points.DataBindXY(dr, "YM", dr, "TTLWT");
            Chart3.Series["預測值"].ToolTip = "預測值 \n時間 \t= #VALX \n數據 \t= #VALY{N2}  ";
            Chart3.Series["預測值"].IsValueShownAsLabel = false;
            Chart3.Series["預測值"].LegendText = "預測值";
            cmd.Dispose();
            dr.Close(); ;

            switch (RBL_switch2.SelectedValue)
            {
                case "spline":
                    {
                        Chart3.Series["今年"].ChartType = SeriesChartType.Spline; //曲線圖
                        Chart3.Series["今年"].BorderWidth = 2;

                        Chart3.Series["去年"].ChartType = SeriesChartType.Spline; //曲線圖
                        Chart3.Series["去年"].BorderWidth = 2;

                        //Chart3.Series["預測值"].ChartType = SeriesChartType.Spline; //曲線圖
                        //Chart3.Series["預測值"].BorderWidth = 2;
                        break;
                    }
                case "column":
                    {
                        Chart3.Series["今年"].ChartType = SeriesChartType.Column; //直條圖
                        Chart3.Series["今年"].BorderWidth = 2;

                        Chart3.Series["去年"].ChartType = SeriesChartType.Column; //直條圖
                        Chart3.Series["去年"].BorderWidth = 2;

                        //Chart3.Series["預測值"].ChartType = SeriesChartType.Column; //直條圖
                        //Chart3.Series["預測值"].BorderWidth = 2;
                        break;
                    }
            }
            //Chart1.Series["Series1"].ChartType = SeriesChartType.Bar; //橫條圖

            //字體設定
            //Chart2.Series[dr[0].ToString()].Font = new System.Drawing.Font("Trebuchet MS", 10, System.Drawing.FontStyle.Bold);
            //Label 背景色
            //Chart2.Series[dr[0].ToString()].LabelBackColor = Color.FromArgb(150, 255, 255, 255);
            //Chart2.Series[dr[0].ToString()].Color = Color.FromArgb(240, 65, 140, 240); //背景色
            //Chart2.Series[dr[0].ToString()].IsValueShownAsLabel = true; // Show data points labels




            Title title = new Title();
            title.Text = v_this_year + "-外銷業務出貨彙總統計";
            title.Alignment = ContentAlignment.MiddleCenter;
            title.Font = new System.Drawing.Font("Trebuchet MS", 10F, FontStyle.Bold);
            Chart3.Titles.Add(title);

            //Chart2.ChartAreas["ChartArea1"].AxisY.Maximum = 160;
            Chart3.ChartAreas["ChartArea1"].AxisY.Interval = 1000;

            // Set Back Color
            Chart3.BackColor = Color.SkyBlue;
            // Set Back Gradient End Color
            Chart3.BackSecondaryColor = Color.AliceBlue;
            // Set Hatch Style
            //Chart1.BackHatchStyle = ChartHatchStyle.DashedHorizontal;
            // Set Gradient Type
            Chart3.BackGradientStyle = GradientStyle.TopBottom;
            // Set Border Color
            Chart3.BorderColor = Color.Black;
            // Set Border Width
            //Chart2.BorderWidth = 10;

            //Chart3.Series[v_this_year].Dispose();
            //Chart3.Series[v_prv_year].Dispose();

            conn.Close();
        }
        catch (Exception err)
        {
            message4.Text += err.ToString();
            cmd.Dispose();
            //cmd2.Dispose();
            conn.Close();
        }
    }
}