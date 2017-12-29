using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Configuration;

public partial class GM_salesKPI_2 : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();
    OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
    OracleDataReader dr;

    string Sql_str, v_s1, v_s2;
    static string v_coid;
    static string v_year = DateTime.Now.ToString("yyyy");
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
        }
    }

    //客戶查詢
    protected void QueryCust(string V_EMPLOY_NO)
    {
        Sql_str = " SELECT EMPLOY_NO,CO_CO_ID ";
        Sql_str += "       ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT ";
        Sql_str += "       ,ROUND(SUM(TTLAMT)/SUM(TTLWT),2) AS AVG_AMT ";
        Sql_str += "FROM   SAL_ORDERS_SUM ";
        Sql_str += "WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_year + "'  ";
        Sql_str += "AND NVL(EMPLOY_NO,1) = NVL('" + V_EMPLOY_NO + "',1) ";
        Sql_str += "GROUP BY EMPLOY_NO,CO_CO_ID ";
        Sql_str += "ORDER BY 3 DESC ";

        DS_cust.SelectCommand = Sql_str;
        //GridView2.DataBind();
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView1.SelectedIndex = e.NewSelectedIndex;
        GridView2.SelectedIndex = -1;
        //GridView3.SelectedIndex = -1;
        //ListView1.SelectedIndex = -1;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label L_EMPLOY_NO = (Label)GridView1.Rows[index].Cells[0].FindControl("L_EMPLOY_NO");

        if (e.CommandName == "Select")
        {
            QueryCust(L_EMPLOY_NO.Text);
            CustChart("");
            //QueryCustAmt(L_currency.Text);
            //Panel1.Visible = true;

            //DS_ordsum.SelectCommand = "";
            ////GridView3.DataBind();

            //DS_order.SelectCommand = "";
            //ListView1.DataBind();


        }

        //TB_coname.Text = "";
        //L_ttlwt.Text = "";
    }

   

    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView2.SelectedIndex = e.NewSelectedIndex;
        //GridView3.SelectedIndex = -1;
        //ListView1.SelectedIndex = -1;

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label L_CO_CO_ID = (Label)GridView2.Rows[index].Cells[0].FindControl("L_CO_CO_ID");

        v_coid = L_CO_CO_ID.Text;

        if (e.CommandName == "Select")
        {
            CustChart(L_CO_CO_ID.Text);
            GenGrid(L_CO_CO_ID.Text);
            //QueryCustName(L_coid.Text);
            //QueryTTLWT(L_coid.Text);
            //DS_order.SelectCommand = "";
            //ListView1.DataBind();
        }
    }

    protected void CustChart(string v_cust)
    {
        string v_sql1, v_sql2;
        if (v_cust.Equals(""))
        {
            v_sql1 = "SELECT '' CO_CO_ID,'2012/01'  YYMM,0 TTLAMT,0 TTLWT FROM DUAL";

            v_sql2 = "SELECT '' CO_CO_ID,'2012/01'  YYMM,0 TTLAMT,0 TTLWT FROM DUAL";
        }
        else
        {
            //圖表chart
            v_sql1 = "SELECT CO_CO_ID,TO_CHAR(BK_DATE,'YYYY/MM')  YYMM ";
            v_sql1 += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT ";
            v_sql1 += "FROM   SAL_ORDERS_SUM ";
            v_sql1 += "WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_year + "'   ";
            v_sql1 += "AND   CO_CO_ID = '" + v_cust + "' ";
            v_sql1 += "GROUP BY CO_CO_ID,TO_CHAR(BK_DATE,'YYYY/MM')  ";
            v_sql1 += "ORDER BY 2  ";

            //圖表chart
            v_sql2 = "SELECT CO_CO_ID,TO_CHAR(BK_DATE,'YYYY/MM')  YYMM ";
            v_sql2 += "            ,ROUND(SUM(TTLWT),2) TTLAMT ";
            v_sql2 += "FROM   SAL_ORDERS_SUM ";
            v_sql2 += "WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_year + "'   ";
            v_sql2 += "AND   CO_CO_ID = '" + v_cust + "' ";
            v_sql2 += "GROUP BY CO_CO_ID,TO_CHAR(BK_DATE,'YYYY/MM')  ";
            v_sql2 += "ORDER BY 2  ";
        }
        Gen_chart(this.Chart1, v_sql1, "Jinnher");
        Gen_chart(this.Chart2, v_sql2, "Jinnher");
    }
    protected void Gen_chart(Chart v_chart, string v_sql1, string v_Place)
    {
        try
        {
            // OracleDataAdapter adp;
            OracleConnection conn;
            OracleCommand cmd;
            OracleDataReader dr;


            //建立連線
            //使用web.config conn string
            conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
            conn.Open();

            cmd = new OracleCommand(v_sql1, conn);
            dr = cmd.ExecuteReader();

            //第一數列
            v_chart.Series["今年"].Points.DataBindXY(dr, "YYMM", dr, "TTLAMT");

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

    protected void GenGrid(string v_cust)
    {
        string v_sql1;
        v_sql1 = "SELECT CO_CO_ID, ";
        v_sql1 += "            SUM(TTLAMT_1) TTLAMT_1,SUM(TTLWT_1)  TTLWT_1, ";
        v_sql1 += "            SUM(TTLAMT_2) TTLAMT_2,SUM(TTLWT_2)  TTLWT_2, ";
        v_sql1 += "            SUM(TTLAMT_3) TTLAMT_3,SUM(TTLWT_3)  TTLWT_3, ";
        v_sql1 += "            SUM(TTLAMT_4) TTLAMT_4,SUM(TTLWT_4)  TTLWT_4, ";
        v_sql1 += "            SUM(TTLAMT_5) TTLAMT_5,SUM(TTLWT_5)  TTLWT_5, ";
        v_sql1 += "            SUM(TTLAMT_6) TTLAMT_6,SUM(TTLWT_6)  TTLWT_6, ";
        v_sql1 += "            SUM(TTLAMT_7) TTLAMT_7,SUM(TTLWT_7)  TTLWT_7, ";
        v_sql1 += "            SUM(TTLAMT_8) TTLAMT_8,SUM(TTLWT_8)  TTLWT_8, ";
        v_sql1 += "            SUM(TTLAMT_9) TTLAMT_9,SUM(TTLWT_9)  TTLWT_9, ";
        v_sql1 += "            SUM(TTLAMT_10) TTLAMT_10,SUM(TTLWT_10)  TTLWT_10, ";
        v_sql1 += "            SUM(TTLAMT_11) TTLAMT_11,SUM(TTLWT_11)  TTLWT_11, ";
        v_sql1 += "            SUM(TTLAMT_12) TTLAMT_12,SUM(TTLWT_12)  TTLWT_12 ";
        v_sql1 += "FROM ( ";
        v_sql1 += "SELECT  CO_CO_ID, ";
        v_sql1 += "            CASE WHEN MM = '01' THEN NVL(TTLAMT,0) ELSE 0 END AS TTLAMT_1, ";
        v_sql1 += "            CASE WHEN MM = '01' THEN NVL(TTLWT,0) ELSE 0  END AS TTLWT_1, ";
        v_sql1 += "            CASE WHEN MM = '02' THEN NVL(TTLAMT,0) ELSE 0  END AS TTLAMT_2, ";
        v_sql1 += "            CASE WHEN MM = '02' THEN NVL(TTLWT,0) ELSE 0  END AS TTLWT_2, ";
        v_sql1 += "            CASE WHEN MM = '03' THEN NVL(TTLAMT,0) ELSE 0  END AS TTLAMT_3, ";
        v_sql1 += "            CASE WHEN MM = '03' THEN NVL(TTLWT,0) ELSE 0  END AS TTLWT_3, ";
        v_sql1 += "            CASE WHEN MM = '04' THEN NVL(TTLAMT,0) ELSE 0  END AS TTLAMT_4, ";
        v_sql1 += "            CASE WHEN MM = '04' THEN NVL(TTLWT,0) ELSE 0  END AS TTLWT_4, ";
        v_sql1 += "            CASE WHEN MM = '05' THEN NVL(TTLAMT,0) ELSE 0  END AS TTLAMT_5, ";
        v_sql1 += "            CASE WHEN MM = '05' THEN NVL(TTLWT,0) ELSE 0  END AS TTLWT_5, ";
        v_sql1 += "            CASE WHEN MM = '06' THEN NVL(TTLAMT,0) ELSE 0  END AS TTLAMT_6, ";
        v_sql1 += "            CASE WHEN MM = '06' THEN NVL(TTLWT,0) ELSE 0  END AS TTLWT_6, ";
        v_sql1 += "            CASE WHEN MM = '07' THEN NVL(TTLAMT,0) ELSE 0  END AS TTLAMT_7, ";
        v_sql1 += "            CASE WHEN MM = '07' THEN NVL(TTLWT,0) ELSE 0  END AS TTLWT_7, ";
        v_sql1 += "            CASE WHEN MM = '08' THEN NVL(TTLAMT,0) ELSE 0  END AS TTLAMT_8, ";
        v_sql1 += "            CASE WHEN MM = '08' THEN NVL(TTLWT,0) ELSE 0  END AS TTLWT_8, ";
        v_sql1 += "            CASE WHEN MM = '09' THEN NVL(TTLAMT,0) ELSE 0  END AS TTLAMT_9, ";
        v_sql1 += "            CASE WHEN MM = '09' THEN NVL(TTLWT,0) ELSE 0  END AS TTLWT_9, ";
        v_sql1 += "            CASE WHEN MM = '10' THEN NVL(TTLAMT,0) ELSE 0  END AS TTLAMT_10, ";
        v_sql1 += "            CASE WHEN MM = '10' THEN NVL(TTLWT,0) ELSE 0  END AS TTLWT_10, ";
        v_sql1 += "            CASE WHEN MM = '11' THEN NVL(TTLAMT,0) ELSE 0  END AS TTLAMT_11, ";
        v_sql1 += "            CASE WHEN MM = '11' THEN NVL(TTLWT,0) ELSE 0  END AS TTLWT_11, ";
        v_sql1 += "            CASE WHEN MM = '12' THEN NVL(TTLAMT,0) ELSE 0  END AS TTLAMT_12, ";
        v_sql1 += "            CASE WHEN MM = '12' THEN NVL(TTLWT,0) ELSE 0  END AS TTLWT_12 ";
        v_sql1 += "FROM ( ";
        v_sql1 += "SELECT  O.CO_CO_ID,TO_CHAR(BK_DATE,'MM') MM ";
        v_sql1 += "            ,NVL(TTLAMT,0) AS TTLAMT ,NVL(TTLWT,0) TTLWT ";
        v_sql1 += "FROM    SAL_ORDERS_SUM   O ";
        v_sql1 += "WHERE  TO_CHAR(BK_DATE,'YYYY') = '" + v_year + "' ";
        v_sql1 += "AND   CO_CO_ID LIKE '" + v_cust + "%' ";
        v_sql1 += ") ) GROUP BY CO_CO_ID ";

        SDS_amt.SelectCommand = v_sql1;
        SDS_wt.SelectCommand = v_sql1;
    }
    protected void BTN_query_Click(object sender, EventArgs e)
    {
        v_year = TB_year.Text;
        Sql_str = "SELECT O.EMPLOY_NO,C.EMP_NAME  ";
        Sql_str += "            ,ROUND(SUM(TTLAMT),2) AS TTLAMT ,ROUND(SUM(TTLWT),2) TTLWT  ";
        Sql_str += "FROM   SAL_ORDERS_SUM O,SAL_CUST_MAP C ";
        Sql_str += "WHERE TO_CHAR(BK_DATE,'YYYY') = '" + v_year + "' ";
        Sql_str += "AND  O.CO_CO_ID = C.CO_CO_ID(+)   ";
        Sql_str += "GROUP BY O.EMPLOY_NO,C.EMP_NAME  ";
        Sql_str += "ORDER BY 3 DESC ";

        DS_sales.SelectCommand = Sql_str;

        CustChart("");
    }

}