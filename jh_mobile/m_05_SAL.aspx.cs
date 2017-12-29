using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class m_05_SAL : System.Web.UI.Page
{
    string Sql_str, v_ym;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TB_year.Text = DateTime.Today.Year.ToString();
            //string v_str = DateTime.Today.ToString("MM");
            //DDL_month.SelectedIndex = DDL_month.Items.IndexOf(DDL_month.Items.FindByValue(DateTime.Today.ToString("MM")));

            Quy_btn_Click(sender, e);
        }
    }
    protected void Quy_btn_Click(object sender, EventArgs e)
    {
        BindData();

        //Series series = Chart1.Series.Add("My series");
        Chart1.Series["Series1"].ToolTip = "#VALX: #VAL{N0} 千元";
        Chart1.Series["Series1"].LegendToolTip = "#PERCENT";
        Chart1.Series["Series1"].PostBackValue = "#INDEX";
        Chart1.Series["Series1"].LegendPostBackValue = "#INDEX";

        //Series series = Chart1.Series.Add("My series");
        Chart2.Series["Series1"].ToolTip = "#VALX: #VAL{N0} 噸";
        Chart2.Series["Series1"].LegendToolTip = "#PERCENT";
        Chart2.Series["Series1"].PostBackValue = "#INDEX";
        Chart2.Series["Series1"].LegendPostBackValue = "#INDEX";
    }

    protected void BindData()
    {
        v_ym = TB_year.Text;// +"/" + DDL_month.SelectedValue;

        Sql_str = "SELECT EMP_NAME,TTLAMT, TTLNW  ";
        Sql_str += " FROM(    ";
        Sql_str += " SELECT '1' SEQ,O.EMPLOY_NO,O.EMP_NAME    ";
        Sql_str += "             ,ROUND(SUM(TTLAMT),0) AS TTLAMT ,ROUND(SUM(TTLWT),0) TTLNW   ";
        Sql_str += " FROM   SAL_ORDERS_SUM O    ";
        Sql_str += " WHERE TO_CHAR(BK_DATE,'YYYY') = '" + TB_year.Text + "'   ";
        Sql_str += " GROUP BY O.EMPLOY_NO,O.EMP_NAME    ";
        Sql_str += " ORDER BY TTLAMT DESC )  ";

        DS_SALORDER.SelectCommand = Sql_str;
        Chart1.DataBind();
        Chart2.DataBind();
        GridView1.DataBind();

    }
   
}