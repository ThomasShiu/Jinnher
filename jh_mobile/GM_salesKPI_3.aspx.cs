using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Configuration;

public partial class GM_salesKPI_3 : System.Web.UI.Page
{
    //OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
    //OracleDataReader dr;
    string v_sql;
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
            //TB_year.Text = DateTime.Now.ToString("yyyy");

            v_sql = "SELECT DISTINCT TO_CHAR(WORK_DATE,'YYYY') YY FROM WORKDATE WHERE TO_CHAR(WORK_DATE,'YYYY') <= TO_CHAR(SYSDATE,'YYYY') ORDER BY 1 DESC";

            SDS_year.SelectCommand = v_sql;
            DDL_year.DataBind();

            SDS_year2.SelectCommand = v_sql;
            DDL_year2.DataBind();

            SDS_year3.SelectCommand = v_sql;
            DDL_year3.DataBind();

            SDS_yearM.SelectCommand = v_sql;
            DDL_yearM.DataBind();

            DDL_monthM.Text = System.DateTime.Now.ToString("MM");

            //紀錄LOG
            string strClientIP = Context.Request.ServerVariables["REMOTE_ADDR"];
            ts_conn.save_log("Guest", "GM_salesKPI_3", strClientIP, "業務外銷接單統計");

            //SDS_cust1.SelectCommand = "SELECT DISTINCT CO_CO_ID CO_ID FROM SAL_ORDERS_SUM WHERE TO_CHAR (BK_DATE, 'YYYY') = '" + DDL_year.SelectedValue + "' ORDER BY 1";
            //DDL_cust1.Items.Clear();
            //DDL_cust1.DataBind();
        }
    }



    protected void IBtn_refresh1_Click(object sender, ImageClickEventArgs e)
    {
        string v_sql_1, v_sql_2, v_this_year, v_prv_year;

        v_this_year = DDL_year.SelectedValue;
        v_sql_1 = "SELECT O.EMPLOY_NO,O.EMP_NAME  ";
        v_sql_1 += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT  ";
        v_sql_1 += "FROM   SAL_ORDERS_SUM O ";
        v_sql_1 += "WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "' ";
        v_sql_1 += "GROUP BY O.EMPLOY_NO,O.EMP_NAME  ";
        v_sql_1 += "ORDER BY 3 DESC ";

        DS_sales.SelectCommand = v_sql_1;

        GridView1.DataBind();
        GridView2.SelectedIndex = -1;
        GridView3.SelectedIndex = -1;
        GridView3.DataBind();
    }

    protected void Gen_chart1(Chart v_chart, string v_sql1, string v_sql2, string v_sql3)
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
            v_chart.Series["今年"].ToolTip = "今年 \n月份 \t= #VALX \n數據 \t= #VALY{N2}  ";

            cmd = new OracleCommand(v_sql2, conn);
            dr = cmd.ExecuteReader();
            //第二數列
            v_chart.Series["去年"].Points.DataBindXY(dr, "YM", dr, "TTLWT");
            v_chart.Series["去年"].ToolTip = "去年 \n月份 \t= #VALX \n數據 \t= #VALY{N2}  ";

            cmd = new OracleCommand(v_sql3, conn);
            dr = cmd.ExecuteReader();
            //第二數列
            v_chart.Series["成長率"].Points.DataBindXY(dr, "YM", dr, "GRATE");
            v_chart.Series["成長率"].ToolTip = "成長率 \n月份 \t= #VALX \n數據 \t= #VALY{N}  ";

            Title title = new Title();
            title.Text = DDL_year.SelectedValue + "-外銷業務接單統計";
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


    protected void RBL_switch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        IBtn_refresh1_Click(null,null);
        GridView3.SelectedIndex = -1;
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
            GenChart2(L_EMPLOY_NO.Text);
        }

        //TB_coname.Text = "";
        //L_ttlwt.Text = "";
    }

    //客戶查詢
    protected void QueryCust(string V_EMPLOY_NO)
    {
        string v_sql_1, v_sql_2, v_this_year, v_prv_year;
        v_this_year = DDL_year.SelectedValue;

        v_sql_1 = " SELECT EMPLOY_NO,CO_CO_ID ";
        v_sql_1 += "       ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT ";
        v_sql_1 += "       ,ROUND(SUM(TTLAMT)/SUM(TTLWT),2) AS AVG_AMT ";
        v_sql_1 += "FROM   SAL_ORDERS_SUM ";
        v_sql_1 += "WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "'  ";
        v_sql_1 += "AND NVL(EMPLOY_NO,1) =  NVL('" + V_EMPLOY_NO + "',1)  ";
        v_sql_1 += "GROUP BY EMPLOY_NO,CO_CO_ID ";
        v_sql_1 += "ORDER BY 3 DESC ";

        DS_cust.SelectCommand = v_sql_1;
        //GridView2.DataBind();
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView2.SelectedIndex = e.NewSelectedIndex;
        GridView3.SelectedIndex = -1;

        SDS_orders1.SelectCommand = "";
        GridView1.DataBind();
    }

    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView3.SelectedIndex = e.NewSelectedIndex;
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


    // 年度各月份接單 BY 客戶 
    protected void GenChart(string v_co_id)
    {
        string v_sql_1, v_sql_2, v_sql_3, v_this_year, v_prv_year;
        v_this_year = DDL_year.SelectedValue;
        v_prv_year = (Convert.ToInt16(DDL_year.SelectedValue) - 1).ToString();

        v_sql_1 = " SELECT O.CO_ID,O.EMP_NAME,DD.YM ,ROUND(NVL(O.TTLAMT,0)) TTLAMT,NVL(O.TTLWT,0) TTLWT ";
        v_sql_1 += "FROM ";
        v_sql_1 += " (SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
        v_sql_1 += "  FROM  WORKDATE  ";
        v_sql_1 += "  WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_this_year + "' ";
        v_sql_1 += "  GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
        v_sql_1 += "  ORDER BY YM  )  DD , ";
        v_sql_1 += " (SELECT CO_CO_ID CO_ID,EMP_NAME,TO_CHAR(BK_DATE,'MM') BK_DATE ";
        v_sql_1 += "        ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT ";
        v_sql_1 += "  FROM   SAL_ORDERS_SUM ";
        v_sql_1 += "  WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "'  ";
        v_sql_1 += "  AND  CO_CO_ID = '" + v_co_id + "' ";
        v_sql_1 += "  GROUP BY  CO_CO_ID,EMP_NAME,TO_CHAR(BK_DATE,'MM') ";
        v_sql_1 += "  ) O ";
        v_sql_1 += "WHERE 1=1";
        v_sql_1 += "AND DD.YM =O.BK_DATE(+)";

        v_sql_2 = " SELECT O.CO_ID,O.EMP_NAME,DD.YM ,ROUND(NVL(O.TTLAMT,0)) TTLAMT,NVL(O.TTLWT,0) TTLWT ";
        v_sql_2 += "FROM ";
        v_sql_2 += "(SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
        v_sql_2 += "  FROM  WORKDATE  ";
        v_sql_2 += "  WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_prv_year + "' ";
        v_sql_2 += "  GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
        v_sql_2 += "  ORDER BY YM )  DD , ";
        v_sql_2 += " (SELECT CO_CO_ID CO_ID,EMP_NAME,TO_CHAR(BK_DATE,'MM') BK_DATE ";
        v_sql_2 += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT ";
        v_sql_2 += "  FROM   SAL_ORDERS_SUM ";
        v_sql_2 += "  WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_prv_year + "'  ";
        v_sql_2 += "  AND  CO_CO_ID = '" + v_co_id + "' ";
        v_sql_2 += "  GROUP BY  CO_CO_ID,EMP_NAME,TO_CHAR(BK_DATE,'MM') ";
        v_sql_2 += "  ) O ";
        v_sql_2 += "WHERE 1=1 ";
        v_sql_2 += "AND DD.YM =O.BK_DATE(+)";

        //成長率
        v_sql_3 = " SELECT DD.YM,NVL(O.GRATE,0) GRATE ";
        v_sql_3 += "FROM (  ";
        v_sql_3 += "SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
        v_sql_3 += "          FROM  WORKDATE  ";
        v_sql_3 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_this_year + "' ";
        v_sql_3 += "          GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
        v_sql_3 += "          ORDER BY YM    )  DD , ";
        v_sql_3 += " (SELECT BK_DATE,SUM(TTLAMT1) TTLAMT1,SUM(TTLWT1) TTLWT1,SUM(TTLAMT2) TTLAMT2,SUM(TTLWT2) TTLWT2 ";
        v_sql_3 += "             ,ROUND(((SUM(TTLWT2) - SUM(TTLWT1) )/SUM(TTLWT1) )*100)  GRATE ";
        v_sql_3 += " FROM ( ";
        v_sql_3 += " SELECT  TO_CHAR(BK_DATE,'MM') BK_DATE ";
        v_sql_3 += "            ,CASE WHEN TO_CHAR(BK_DATE,'YYYY') = '" + v_prv_year + "' THEN ROUND(SUM(TTLAMT),2) END AS TTLAMT1  ";
        v_sql_3 += "            ,CASE WHEN TO_CHAR(BK_DATE,'YYYY') = '" + v_prv_year + "' THEN ROUND(SUM(TTLWT),2) END AS TTLWT1 ";
        v_sql_3 += "            ,CASE WHEN TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "' THEN ROUND(SUM(TTLAMT),2) END AS TTLAMT2   ";
        v_sql_3 += "            ,CASE WHEN TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "' THEN ROUND(SUM(TTLWT),2) END AS TTLWT2 ";
        v_sql_3 += "  FROM   SAL_ORDERS_SUM ";
        v_sql_3 += "  WHERE TO_CHAR(BK_DATE,'YYYY') IN ( '" + v_prv_year + "' ,'" + v_this_year + "') ";
        v_sql_3 += "  AND CO_CO_ID = '" + v_co_id + "' ";
        v_sql_3 += "  GROUP BY  TO_CHAR(BK_DATE,'YYYY'),TO_CHAR(BK_DATE,'MM') ";
        v_sql_3 += "  ) HAVING SUM(TTLWT2) > 0 GROUP BY BK_DATE ";
        v_sql_3 += "  ) O ";
        v_sql_3 += "WHERE 1=1 ";
        v_sql_3 += "AND DD.YM =O.BK_DATE(+) ";

        Gen_chart1(this.Chart1, v_sql_1, v_sql_2, v_sql_3);

        SDS_orders1.SelectCommand = v_sql_1;
        GridView1.DataBind();
    }

    // 年度各月份接單 BY 業務 
    protected void GenChart2(string v_EMP_NO)
    {
        string v_sql_1, v_sql_2, v_sql_3, v_this_year, v_prv_year;
        v_this_year = DDL_year.SelectedValue;
        v_prv_year = (Convert.ToInt16(DDL_year.SelectedValue) - 1).ToString();

        v_sql_1 = " SELECT DD.YM ,ROUND(NVL(O.TTLAMT,0)) TTLAMT,NVL(O.TTLWT,0) TTLWT ";
        v_sql_1 += "FROM ";
        v_sql_1 += " (SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
        v_sql_1 += "  FROM  WORKDATE  ";
        v_sql_1 += "  WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_this_year + "' ";
        v_sql_1 += "  GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
        v_sql_1 += "  ORDER BY YM  )  DD , ";
        v_sql_1 += " (SELECT EMPLOY_NO,TO_CHAR(BK_DATE,'MM') BK_DATE ";
        v_sql_1 += "        ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT ";
        v_sql_1 += "  FROM   SAL_ORDERS_SUM ";
        v_sql_1 += "  WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "'  ";
        v_sql_1 += "  AND  EMPLOY_NO = '" + v_EMP_NO + "' ";
        v_sql_1 += "  GROUP BY  EMPLOY_NO,TO_CHAR(BK_DATE,'MM') ";
        v_sql_1 += "  ) O ";
        v_sql_1 += "WHERE 1=1";
        v_sql_1 += "AND DD.YM =O.BK_DATE(+)";

        v_sql_2 = " SELECT DD.YM ,ROUND(NVL(O.TTLAMT,0)) TTLAMT,NVL(O.TTLWT,0) TTLWT ";
        v_sql_2 += "FROM ";
        v_sql_2 += "(SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
        v_sql_2 += "  FROM  WORKDATE  ";
        v_sql_2 += "  WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_prv_year + "' ";
        v_sql_2 += "  GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
        v_sql_2 += "  ORDER BY YM )  DD , ";
        v_sql_2 += " (SELECT EMPLOY_NO,TO_CHAR(BK_DATE,'MM') BK_DATE ";
        v_sql_2 += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT ";
        v_sql_2 += "  FROM   SAL_ORDERS_SUM ";
        v_sql_2 += "  WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_prv_year + "'  ";
        v_sql_2 += "  AND  EMPLOY_NO= '" + v_EMP_NO + "' ";
        v_sql_2 += "  GROUP BY  EMPLOY_NO,TO_CHAR(BK_DATE,'MM') ";
        v_sql_2 += "  ) O ";
        v_sql_2 += "WHERE 1=1 ";
        v_sql_2 += "AND DD.YM =O.BK_DATE(+)";

        //成長率
        v_sql_3 = " SELECT DD.YM,NVL(O.GRATE,0) GRATE ";
        v_sql_3 += "FROM (  ";
        v_sql_3 += "SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
        v_sql_3 += "          FROM  WORKDATE  ";
        v_sql_3 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_this_year + "' ";
        v_sql_3 += "          GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
        v_sql_3 += "          ORDER BY YM    )  DD , ";
        v_sql_3 += " (SELECT BK_DATE,SUM(TTLAMT1) TTLAMT1,SUM(TTLWT1) TTLWT1,SUM(TTLAMT2) TTLAMT2,SUM(TTLWT2) TTLWT2 ";
        v_sql_3 += "             ,ROUND(((SUM(TTLWT2) - SUM(TTLWT1) )/SUM(TTLWT1) )*100)  GRATE ";
        v_sql_3 += " FROM ( ";
        v_sql_3 += " SELECT  TO_CHAR(BK_DATE,'MM') BK_DATE ";
        v_sql_3 += "            ,CASE WHEN TO_CHAR(BK_DATE,'YYYY') = '" + v_prv_year + "' THEN ROUND(SUM(TTLAMT),2) END AS TTLAMT1  ";
        v_sql_3 += "            ,CASE WHEN TO_CHAR(BK_DATE,'YYYY') = '" + v_prv_year + "' THEN ROUND(SUM(TTLWT),2) END AS TTLWT1 ";
        v_sql_3 += "            ,CASE WHEN TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "' THEN ROUND(SUM(TTLAMT),2) END AS TTLAMT2   ";
        v_sql_3 += "            ,CASE WHEN TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "' THEN ROUND(SUM(TTLWT),2) END AS TTLWT2 ";
        v_sql_3 += "  FROM   SAL_ORDERS_SUM ";
        v_sql_3 += "  WHERE TO_CHAR(BK_DATE,'YYYY') IN ( '" + v_prv_year + "' ,'" + v_this_year + "') ";
        v_sql_3 += "  AND EMPLOY_NO = '" + v_EMP_NO + "' ";
        v_sql_3 += "  GROUP BY  TO_CHAR(BK_DATE,'YYYY'),TO_CHAR(BK_DATE,'MM') ";
        v_sql_3 += "  ) HAVING SUM(TTLWT2) > 0 GROUP BY BK_DATE ";
        v_sql_3 += "  ) O ";
        v_sql_3 += "WHERE 1=1 ";
        v_sql_3 += "AND DD.YM =O.BK_DATE(+) ";

        Gen_chart1(this.Chart1, v_sql_1, v_sql_2, v_sql_3);

        SDS_orders1.SelectCommand = v_sql_1;
        GridView1.DataBind();
    }

    protected void IBtn_refresh2_Click(object sender, ImageClickEventArgs e)
    {
        //年度每月接單彙總
        Gen_chartALLorder();

        //年度每月接單彙總-累計
        Gen_chartSUMorder();

        //前十大客戶
        Gen_chartTOP10();
    }
    protected void DDL_year2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void RBL_switch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //年度每月接單彙總
        Gen_chartALLorder();

        //年度每月接單彙總-累計
        Gen_chartSUMorder();

        //前十大客戶
        Gen_chartTOP10();
    }
    protected void Gen_chartTOP10()
    {
        string v_sql_1, v_sql_2, v_this_year, v_prv_year;
        v_this_year = DDL_year2.SelectedValue;
        v_prv_year = (Convert.ToInt16(DDL_year.SelectedValue) - 1).ToString();

        //取得前十大客戶代號
        v_sql_1 = " SELECT CO_CO_ID FROM ( ";
        v_sql_1 += " SELECT   CO_CO_ID ,TO_CHAR(BK_DATE,'YYYY') BK_DATE,SUM(TTLAMT) TTLAMT , SUM(TTLWT) TTLWT ";
        v_sql_1 += "  FROM  SAL_ORDERS_SUM  ";
        v_sql_1 += "  WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "'  ";
        v_sql_1 += "  GROUP BY CO_CO_ID ,TO_CHAR(BK_DATE,'YYYY')  ";
        v_sql_1 += "  ORDER BY 4 DESC ) ";
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
                v_sql_2 = " SELECT DD.CO_ID,DD.EMP_NAME,DD.YM,NVL(TTLAMT,0) TTLAMT,NVL(TTLWT,0) TTLWT ";
                v_sql_2 += " FROM ";
                v_sql_2 += " ( SELECT CO_CO_ID CO_ID,EMP_NAME,YM FROM ";
                v_sql_2 += "    V_SAL_CUST_MAP M , ";
                v_sql_2 += "    (   SELECT  TO_CHAR(WORK_DATE,'YYYY/MM')  YM ";
                v_sql_2 += "           FROM  WORKDATE  ";
                v_sql_2 += "           WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_this_year + "' ";
                v_sql_2 += "           GROUP BY   TO_CHAR(WORK_DATE,'YYYY/MM') ";
                v_sql_2 += "           ORDER BY YM ) B ";
                v_sql_2 += "           WHERE 1=1 ";
                v_sql_2 += "           AND  M.CO_CO_ID = '" + dr[0].ToString() + "' ";
                v_sql_2 += "          ORDER BY CO_CO_ID, YM )  DD , ";
                v_sql_2 += "  (SELECT CO_CO_ID CO_ID,TO_CHAR(BK_DATE,'YYYY/MM') BK_DATE ";
                v_sql_2 += "             ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT ";
                v_sql_2 += "   FROM   SAL_ORDERS_SUM ";
                v_sql_2 += "   WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "'  ";
                v_sql_2 += "   GROUP BY  CO_CO_ID,TO_CHAR(BK_DATE,'YYYY/MM') ";
                v_sql_2 += "   ) O ";
                v_sql_2 += " WHERE DD.CO_ID = O.CO_ID(+) ";
                v_sql_2 += " AND DD.YM =O.BK_DATE(+) ";

                cmd2 = new OracleCommand(v_sql_2, conn);
                dr2 = cmd2.ExecuteReader();

                //Chart2.Series.Clear();
                
                //數據序列集合
                Chart2.Series.Add(dr[0].ToString());
                Chart2.Series[dr[0].ToString()].Points.DataBindXY(dr2, "YM", dr2, "TTLWT");
                Chart2.Series[dr[0].ToString()].ToolTip = dr[0].ToString() + " \n月份 \t= #VALX \n數據 \t= #VALY{N2}  ";

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
        catch(Exception err)
        {
            //message4.Text += err.ToString(); 
            cmd.Dispose();
            conn.Close();
        }
    }
    protected void Gen_chartALLorder()
    {
        string v_sql_1, v_sql_2, v_sql_3,v_sql_4, v_this_year, v_prv_year, v_prv_year2;
        v_this_year = DDL_year2.SelectedValue;
        v_prv_year = (Convert.ToInt16(DDL_year2.SelectedValue) - 1).ToString();
        v_prv_year2 = (Convert.ToInt16(DDL_year2.SelectedValue) - 2).ToString();

        try
        {

            //組出每個月總接單
            v_sql_1 = "SELECT DD.YM,NVL(TTLAMT,0) TTLAMT,NVL(TTLWT,0) TTLWT ";
            v_sql_1 += "FROM ";
            v_sql_1 += "( SELECT  TO_CHAR(WORK_DATE,'MM') YM ";
            v_sql_1 += "          FROM  WORKDATE  ";
            v_sql_1 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_this_year + "' ";
            v_sql_1 += "          GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
            v_sql_1 += "          ORDER BY YM )  DD , ";
            v_sql_1 += " (SELECT  TO_CHAR(BK_DATE,'MM') BK_DATE ";
            v_sql_1 += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT ";
            v_sql_1 += "  FROM   SAL_ORDERS_SUM ";
            v_sql_1 += "  WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "'  ";
            v_sql_1 += "  GROUP BY  TO_CHAR(BK_DATE,'MM') ";
            v_sql_1 += "  ) O ";
            v_sql_1 += "WHERE 1=1 ";
            v_sql_1 += "AND DD.YM =O.BK_DATE(+) ";

            //組出每個月總接單-去年
            v_sql_2 = "SELECT DD.YM,NVL(TTLAMT,0) TTLAMT,NVL(TTLWT,0) TTLWT ";
            v_sql_2 += "FROM ";
            v_sql_2 += "( SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
            v_sql_2 += "          FROM  WORKDATE  ";
            v_sql_2 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_prv_year + "' ";
            v_sql_2 += "          GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
            v_sql_2 += "          ORDER BY YM )  DD , ";
            v_sql_2 += " (SELECT  TO_CHAR(BK_DATE,'MM') BK_DATE ";
            v_sql_2 += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT ";
            v_sql_2 += "  FROM   SAL_ORDERS_SUM ";
            v_sql_2 += "  WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_prv_year + "'  ";
            v_sql_2 += "  GROUP BY  TO_CHAR(BK_DATE,'MM') ";
            v_sql_2 += "  ) O ";
            v_sql_2 += "WHERE 1=1 ";
            v_sql_2 += "AND DD.YM =O.BK_DATE(+) ";

            //標準值，前兩年加總除以2
            v_sql_3 = "SELECT  TO_CHAR(BK_DATE,'MM') YM ";
            v_sql_3 += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT)/2,2) TTLWT ";
            v_sql_3 += "  FROM   SAL_ORDERS_SUM ";
            v_sql_3 += "  WHERE TO_CHAR(BK_DATE,'YYYY') IN ('" + v_prv_year2 + "','" + v_prv_year + "') ";
            v_sql_3 += "  GROUP BY  TO_CHAR(BK_DATE,'MM') ";


            //成長率
        v_sql_4 = " SELECT DD.YM,NVL(O.GRATE,0) GRATE ";
        v_sql_4 += "FROM (  ";
        v_sql_4 += "SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
        v_sql_4 += "          FROM  WORKDATE  ";
        v_sql_4 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_this_year + "' ";
        v_sql_4 += "          GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
        v_sql_4 += "          ORDER BY YM    )  DD , ";
        v_sql_4 += " (SELECT BK_DATE,SUM(TTLAMT1) TTLAMT1,SUM(TTLWT1) TTLWT1,SUM(TTLAMT2) TTLAMT2,SUM(TTLWT2) TTLWT2 ";
        v_sql_4 += "             ,ROUND(((SUM(TTLWT2) - SUM(TTLWT1) )/SUM(TTLWT1) )*100)  GRATE ";
        v_sql_4 += " FROM ( ";
        v_sql_4 += " SELECT  TO_CHAR(BK_DATE,'MM') BK_DATE ";
        v_sql_4 += "            ,CASE WHEN TO_CHAR(BK_DATE,'YYYY') = '" + v_prv_year + "' THEN ROUND(SUM(TTLAMT),2) END AS TTLAMT1  ";
        v_sql_4 += "            ,CASE WHEN TO_CHAR(BK_DATE,'YYYY') = '" + v_prv_year + "' THEN ROUND(SUM(TTLWT),2) END AS TTLWT1 ";
        v_sql_4 += "            ,CASE WHEN TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "' THEN ROUND(SUM(TTLAMT),2) END AS TTLAMT2   ";
        v_sql_4 += "            ,CASE WHEN TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "' THEN ROUND(SUM(TTLWT),2) END AS TTLWT2 ";
        v_sql_4 += "  FROM   SAL_ORDERS_SUM ";
        v_sql_4 += "  WHERE TO_CHAR(BK_DATE,'YYYY') IN ( '" + v_prv_year + "' ,'" + v_this_year + "') ";
        v_sql_4 += "  GROUP BY  TO_CHAR(BK_DATE,'YYYY'),TO_CHAR(BK_DATE,'MM') ";
        v_sql_4 += "  ) HAVING SUM(TTLWT2) > 0 GROUP BY BK_DATE ";
        v_sql_4 += "  ) O ";
        v_sql_4 += "WHERE 1=1 ";
        v_sql_4 += "AND DD.YM =O.BK_DATE(+) ";
            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
            conn.Open();


            cmd = new OracleCommand(v_sql_1, conn);
            dr = cmd.ExecuteReader();
            //今年數據序列集合
            Chart3.Series["今年"].Points.DataBindXY(dr, "YM", dr, "TTLWT");
            Chart3.Series["今年"].ToolTip = v_this_year + " \n月份 \t= #VALX \n數據 \t= #VALY{N2}  ";
            Chart3.Series["今年"].IsValueShownAsLabel = true;
            Chart3.Series["今年"].LegendText = v_this_year;
            cmd.Dispose();
            dr.Close();

            cmd = new OracleCommand(v_sql_2, conn);
            dr = cmd.ExecuteReader();
            //去年數據序列集合
            Chart3.Series["去年"].Points.DataBindXY(dr, "YM", dr, "TTLWT");
            Chart3.Series["去年"].ToolTip = v_prv_year + " \n月份 \t= #VALX \n數據 \t= #VALY{N2}  ";
            Chart3.Series["去年"].IsValueShownAsLabel = true;
            Chart3.Series["去年"].LegendText = v_prv_year;
            cmd.Dispose();
            dr.Close();

            cmd = new OracleCommand(v_sql_3, conn);
            dr = cmd.ExecuteReader();
            //去年數據序列集合
            Chart3.Series["預測值"].Points.DataBindXY(dr, "YM", dr, "TTLWT");
            Chart3.Series["預測值"].ToolTip = "預測值 \n月份 \t= #VALX \n數據 \t= #VALY{N2}  ";
            Chart3.Series["預測值"].IsValueShownAsLabel = false;
            Chart3.Series["預測值"].LegendText = "預測值";
            cmd.Dispose();
            dr.Close();

            cmd = new OracleCommand(v_sql_4, conn);
            dr = cmd.ExecuteReader();
            //去年數據序列集合
            Chart3.Series["成長率"].Points.DataBindXY(dr, "YM", dr, "GRATE");
            Chart3.Series["成長率"].ToolTip = "成長率 \n月份 \t= #VALX \n數據 \t= #VALY{N2}  ";
            Chart3.Series["成長率"].IsValueShownAsLabel = true;
            Chart3.Series["成長率"].LegendText = "成長率";
            cmd.Dispose();
            dr.Close(); 

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
            title.Text = v_this_year + "-外銷業務接單彙總統計";
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

    protected void Gen_chartSUMorder()
    {
        string v_sql_1, v_sql_2, v_sql_3, v_this_year, v_prv_year, v_prv_year2;
        v_this_year = DDL_year2.SelectedValue;
        v_prv_year = (Convert.ToInt16(DDL_year2.SelectedValue) - 1).ToString();
        v_prv_year2 = (Convert.ToInt16(DDL_year2.SelectedValue) - 2).ToString();

        try
        {

            //組出每個月總接單
            v_sql_1 = "SELECT YM,TTLAMT,SUM(TTLWT) OVER (ORDER BY YM) AS TTLWT FROM ( ";
            v_sql_1 += "SELECT DD.YM,ROUND(NVL(TTLAMT,0),0) TTLAMT,ROUND(NVL(TTLWT,0),0) TTLWT ";
            v_sql_1 += "FROM ";
            v_sql_1 += "( SELECT  TO_CHAR(WORK_DATE,'MM') YM ";
            v_sql_1 += "          FROM  WORKDATE  ";
            v_sql_1 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_this_year + "' ";
            v_sql_1 += "          GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
            v_sql_1 += "          ORDER BY YM )  DD , ";
            v_sql_1 += " (SELECT  TO_CHAR(BK_DATE,'MM') BK_DATE ";
            v_sql_1 += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT ";
            v_sql_1 += "  FROM   SAL_ORDERS_SUM ";
            v_sql_1 += "  WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "'  ";
            v_sql_1 += "  GROUP BY  TO_CHAR(BK_DATE,'MM') ";
            v_sql_1 += "  ) O ";
            v_sql_1 += "WHERE 1=1 ";
            v_sql_1 += "AND DD.YM =O.BK_DATE(+) )";

            //組出每個月總接單-去年
            v_sql_2 = "SELECT YM,TTLAMT,SUM(TTLWT) OVER (ORDER BY YM) AS TTLWT FROM ( ";
            v_sql_2 += "SELECT DD.YM,ROUND(NVL(TTLAMT,0),0) TTLAMT,ROUND(NVL(TTLWT,0),0) TTLWT ";
            v_sql_2 += "FROM ";
            v_sql_2 += "( SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
            v_sql_2 += "          FROM  WORKDATE  ";
            v_sql_2 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_prv_year + "' ";
            v_sql_2 += "          GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
            v_sql_2 += "          ORDER BY YM )  DD , ";
            v_sql_2 += " (SELECT  TO_CHAR(BK_DATE,'MM') BK_DATE ";
            v_sql_2 += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT ";
            v_sql_2 += "  FROM   SAL_ORDERS_SUM ";
            v_sql_2 += "  WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_prv_year + "'  ";
            v_sql_2 += "  GROUP BY  TO_CHAR(BK_DATE,'MM') ";
            v_sql_2 += "  ) O ";
            v_sql_2 += "WHERE 1=1 ";
            v_sql_2 += "AND DD.YM =O.BK_DATE(+) )";

            //組出每個月總接單-前年
            v_sql_3 = "SELECT YM,TTLAMT,SUM(TTLWT) OVER (ORDER BY YM) AS TTLWT FROM ( ";
            v_sql_3 += "SELECT DD.YM,ROUND(NVL(TTLAMT,0),0) TTLAMT,ROUND(NVL(TTLWT,0),0) TTLWT ";
            v_sql_3 += "FROM ";
            v_sql_3 += "( SELECT  TO_CHAR(WORK_DATE,'MM')  YM ";
            v_sql_3 += "          FROM  WORKDATE  ";
            v_sql_3 += "          WHERE TO_CHAR(WORK_DATE,'YYYY') = '" + v_prv_year2 + "' ";
            v_sql_3 += "          GROUP BY   TO_CHAR(WORK_DATE,'MM') ";
            v_sql_3 += "          ORDER BY YM )  DD , ";
            v_sql_3 += " (SELECT  TO_CHAR(BK_DATE,'MM') BK_DATE ";
            v_sql_3 += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT ";
            v_sql_3 += "  FROM   SAL_ORDERS_SUM ";
            v_sql_3 += "  WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_prv_year2 + "'  ";
            v_sql_3 += "  GROUP BY  TO_CHAR(BK_DATE,'MM') ";
            v_sql_3 += "  ) O ";
            v_sql_3 += "WHERE 1=1 ";
            v_sql_3 += "AND DD.YM =O.BK_DATE(+) )";

            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
            conn.Open();


            cmd = new OracleCommand(v_sql_1, conn);
            dr = cmd.ExecuteReader();
            //今年數據序列集合
            Chart4.Series["今年"].Points.DataBindXY(dr, "YM", dr, "TTLWT");
            Chart4.Series["今年"].ToolTip = v_this_year + " \n月份 \t= #VALX \n數據 \t= #VALY{N2}  ";
            Chart4.Series["今年"].IsValueShownAsLabel = true;
            Chart4.Series["今年"].LegendText = v_this_year;
            cmd.Dispose();
            dr.Close();

            cmd = new OracleCommand(v_sql_2, conn);
            dr = cmd.ExecuteReader();
            //去年數據序列集合
            Chart4.Series["去年"].Points.DataBindXY(dr, "YM", dr, "TTLWT");
            Chart4.Series["去年"].ToolTip = v_prv_year + " \n月份 \t= #VALX \n數據 \t= #VALY{N2}  ";
            Chart4.Series["去年"].IsValueShownAsLabel = true;
            Chart4.Series["去年"].LegendText = v_prv_year;
            cmd.Dispose();
            dr.Close();

            cmd = new OracleCommand(v_sql_3, conn);
            dr = cmd.ExecuteReader();
            //前年數據序列集合
            Chart4.Series["前年"].Points.DataBindXY(dr, "YM", dr, "TTLWT");
            Chart4.Series["前年"].ToolTip = v_prv_year + " \n月份 \t= #VALX \n數據 \t= #VALY{N2}  ";
            Chart4.Series["前年"].IsValueShownAsLabel = false;
            Chart4.Series["前年"].LegendText = v_prv_year2;
            cmd.Dispose();
            dr.Close();

            Title title = new Title();
            title.Text = v_this_year + "-外銷業務接單彙總統計-累計";
            title.Alignment = ContentAlignment.MiddleCenter;
            title.Font = new System.Drawing.Font("Trebuchet MS", 10F, FontStyle.Bold);
            Chart4.Titles.Add(title);

            //Chart2.ChartAreas["ChartArea1"].AxisY.Maximum = 160;
            //Chart4.ChartAreas["ChartArea1"].AxisY.Interval = 1000;

            // Set Back Color
            Chart4.BackColor = Color.SkyBlue;
            // Set Back Gradient End Color
            Chart4.BackSecondaryColor = Color.AliceBlue;
            // Set Hatch Style
            //Chart1.BackHatchStyle = ChartHatchStyle.DashedHorizontal;
            // Set Gradient Type
            Chart4.BackGradientStyle = GradientStyle.TopBottom;
            // Set Border Color
            Chart4.BorderColor = Color.Black;
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

    protected void RBL_chartM_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void IB_queryM_Click(object sender, ImageClickEventArgs e)
    {
        string v_sql_1, v_sql_2, v_this_year, v_this_month;

        try
        {
            v_this_year = DDL_yearM.SelectedValue;
            v_this_month = DDL_monthM.SelectedValue;
            //v_sql_1 = "SELECT O.EMPLOY_NO,O.EMP_NAME  ";
            //v_sql_1 += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT  ";
            //v_sql_1 += "FROM   SAL_ORDERS_SUM O ";
            //v_sql_1 += "WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "' ";
            //v_sql_1 += "GROUP BY O.EMPLOY_NO,O.EMP_NAME  ";
            //v_sql_1 += "ORDER BY 3 DESC ";

            v_sql_1 = " SELECT SEQ,EMPLOY_NO,EMP_NAME,TTLAMT, TTLWT  ";
            v_sql_1 += "FROM(   ";
            v_sql_1 += "SELECT '1' SEQ,O.EMPLOY_NO,O.EMP_NAME    ";
            v_sql_1 += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT  ";
            v_sql_1 += "FROM   SAL_ORDERS_SUM O   ";
            v_sql_1 += "WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_this_year + "'  ";
            v_sql_1 += "GROUP BY O.EMPLOY_NO,O.EMP_NAME   ";
            v_sql_1 += "ORDER BY TTLAMT DESC )   ";
            v_sql_1 += "UNION  ";
            v_sql_1 += "SELECT '99' SEQ,'TOTAL' EMPLOY_NO,'TOTAL' EMP_NAME    ";
            v_sql_1 += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT    ";
            v_sql_1 += "FROM   SAL_ORDERS_SUM O   ";
            v_sql_1 += "WHERE TO_CHAR(BK_DATE,'YYYY') =  '" + v_this_year + "'  ";
            v_sql_1 += "ORDER BY SEQ,TTLAMT DESC";

            DS_salesM.SelectCommand = v_sql_1;

            GridView4.DataBind();

            //v_sql_2 = "SELECT O.EMPLOY_NO,O.EMP_NAME ,TO_CHAR(BK_DATE,'YYYY/MM') YM ";
            //v_sql_2 += "           ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT   ";
            //v_sql_2 += "FROM SAL_ORDERS_SUM O ";
            //v_sql_2 += "WHERE TO_CHAR(BK_DATE,'YYYYMM') = '" + v_this_year + v_this_month + "'  ";
            //v_sql_2 += "GROUP BY O.EMPLOY_NO,O.EMP_NAME, TO_CHAR(BK_DATE,'YYYY/MM')  ";
            //v_sql_2 += "ORDER BY 3,4 DESC ";

            v_sql_2 = "SELECT SEQ,EMPLOY_NO,EMP_NAME,YM,TTLAMT,TTLWT  ";
            v_sql_2 += "FROM (  ";
            v_sql_2 += "SELECT '1' SEQ,O.EMPLOY_NO,O.EMP_NAME ,TO_CHAR(BK_DATE,'YYYY/MM') YM   ";
            v_sql_2 += "           ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT   ";
            v_sql_2 += "FROM SAL_ORDERS_SUM O   ";
            v_sql_2 += "WHERE TO_CHAR(BK_DATE,'YYYYMM') = '" + v_this_year +v_this_month + "'    ";
            v_sql_2 += "GROUP BY O.EMPLOY_NO,O.EMP_NAME, TO_CHAR(BK_DATE,'YYYY/MM')  ";
            v_sql_2 += "ORDER BY 3,4 DESC  ";
            v_sql_2 += ") UNION  ";
            v_sql_2 += "SELECT '99' SEQ,'TOTAL' EMPLOY_NO,'TOTAL' EMP_NAME ,'" + v_this_year + "/" + v_this_month + "' YM   ";
            v_sql_2 += "           ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT   ";
            v_sql_2 += "FROM SAL_ORDERS_SUM O   ";
            v_sql_2 += "WHERE TO_CHAR(BK_DATE,'YYYYMM') = '" + v_this_year + v_this_month + "'  ";
            v_sql_2 += "ORDER BY SEQ,TTLAMT DESC";
            DS_month.SelectCommand = v_sql_2;

            GridView5.DataBind();
        }
        catch (Exception ex)
        {
            L_messM.Text = ex.ToString();
        }

    }

    protected void Quy_btn_Click(object sender, EventArgs e)
    {

        BindPieData();

        //Series series = Chart1.Series.Add("My series");
        Chart5.Series["Series1"].ToolTip = "#VALX: #VAL{N0} 千元";
        Chart5.Series["Series1"].LegendToolTip = "#PERCENT";
        Chart5.Series["Series1"].PostBackValue = "#INDEX";
        Chart5.Series["Series1"].LegendPostBackValue = "#INDEX";

        //Series series = Chart1.Series.Add("My series");
        Chart6.Series["Series1"].ToolTip = "#VALX: #VAL{N0} 噸";
        Chart6.Series["Series1"].LegendToolTip = "#PERCENT";
        Chart6.Series["Series1"].PostBackValue = "#INDEX";
        Chart6.Series["Series1"].LegendPostBackValue = "#INDEX";

        //v_sql_1 = "SELECT NVL(CONTINENT,'其他') CONTINENT, ";
        //v_sql_1 += "            ROUND(SUM(AMT)/1000) TTLAMT,ROUND(SUM(NW)/1000) TTLNW ";
        //v_sql_1 += "FROM  SAL_ORDER_SUM3 ";
        //v_sql_1 += "WHERE YY = '" + DDL_year3.Text + "' ";
        //v_sql_1 += "AND MM = '" + DDL_month4.Text + "' ";
        //v_sql_1 += "GROUP BY YY,MM,CONTINENT ORDER BY TTLAMT DESC";

        //SqlDataSource1.SelectCommand = v_sql_1;


        ////Series series = Chart1.Series.Add("My series");
        //Chart7.Series["Series1"].ToolTip = "#VALX: #VAL{N0} 噸";
        //Chart7.Series["Series1"].LegendToolTip = "#PERCENT";
        //Chart7.Series["Series1"].PostBackValue = "#INDEX";
        //Chart7.Series["Series1"].LegendPostBackValue = "#INDEX";
    }
    protected void BindPieData()
    {
        string v_sql_1;
        //v_ym = TB_year.Text;// +"/" + DDL_month.SelectedValue;
        if (RBL_ym.SelectedValue == "y")
        {
            v_sql_1 = "SELECT NVL(CONTINENT,'其他') CONTINENT, ";
            v_sql_1 += "            ROUND(SUM(AMT)/1000) TTLAMT,ROUND(SUM(NW)/1000) TTLNW ";
            v_sql_1 += "FROM  SAL_ORDER_SUM2 ";
            v_sql_1 += "WHERE YM = '" + DDL_year3.Text + "' ";
            v_sql_1 += "GROUP BY YM,CONTINENT ORDER BY TTLAMT DESC";
        }
        else
        {
            v_sql_1 = "SELECT NVL(NATION,'其他') CONTINENT, ";
            v_sql_1 += "            ROUND(SUM(AMT)/1000) TTLAMT,ROUND(SUM(NW)/1000) TTLNW ";
            v_sql_1 += "FROM  SAL_ORDER_SUM3 ";
            v_sql_1 += "WHERE YY = '" + DDL_year3.Text + "' ";
            v_sql_1 += "AND MM = '" + DDL_month4.Text + "' ";
            v_sql_1 += "GROUP BY YY,MM,NATION ORDER BY TTLAMT DESC";
        }

        DS_SALORDER.SelectCommand = v_sql_1;

        Chart1.DataBind();
        Chart2.DataBind();
        GridView1.DataBind();
        //GridView2.DataBind();

    }


    protected void RBL_ym_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RBL_ym.SelectedValue == "y")
        {
            DDL_month4.Visible = false;
        }
        else
        {
            DDL_month4.Visible = true;
        }

    }
}