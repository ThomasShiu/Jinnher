using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

public partial class m_04_SAL : System.Web.UI.Page
{
    //外銷接單，圓餅圖
    string Sql_str, v_ym;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TB_year.Text = DateTime.Today.Year.ToString();
           
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
    protected void Chart1_Click(object sender, ImageMapEventArgs e)
    {
        Quy_btn_Click(sender, e);
        int pointIndex = Int32.Parse(e.PostBackValue);
        Series series = Chart1.Series["Series1"];

        if (pointIndex >= 0 && pointIndex < Chart1.Series["Series1"].Points.Count)
        {
            //BindData();
            series.Points[pointIndex].CustomProperties += "Exploded=true";
        }

    }

    protected void Chart2_Click(object sender, ImageMapEventArgs e)
    {
        Quy_btn_Click(sender, e);
        int pointIndex = Int32.Parse(e.PostBackValue);
        Series series2 = Chart2.Series["Series1"];

        if (pointIndex >= 0 && pointIndex < Chart2.Series["Series1"].Points.Count)
        {
            //BindData();
            series2.Points[pointIndex].CustomProperties += "Exploded=true";
        }
    }

    protected void BindData()
    {
        v_ym = TB_year.Text;// +"/" + DDL_month.SelectedValue;

        Sql_str = "SELECT NVL(CONTINENT,'其他') CONTINENT, ";
        Sql_str += "            ROUND(SUM(AMT)/1000) TTLAMT,ROUND(SUM(NW)/1000) TTLNW ";
        Sql_str += "FROM  SAL_ORDER_SUM2 ";
        Sql_str += "WHERE YM = '" + TB_year.Text + "' ";
        Sql_str += "GROUP BY YM,CONTINENT ORDER BY TTLAMT DESC";

        DS_SALORDER.SelectCommand = Sql_str;
        Chart1.DataBind();
        Chart2.DataBind();
        GridView1.DataBind();

    }


}