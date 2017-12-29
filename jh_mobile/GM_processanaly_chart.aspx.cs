using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

public partial class GM_processanaly_chart : System.Web.UI.Page
{
    string Sql_str, v_ym;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TB_year.Text = DateTime.Today.Year.ToString();
            //string v_str = DateTime.Today.ToString("MM");
            DDL_month.SelectedIndex = DDL_month.Items.IndexOf(DDL_month.Items.FindByValue(DateTime.Today.ToString("MM")));
            
            Quy_btn_Click(  sender,e);

            
        }
    }
    protected void Quy_btn_Click(object sender, EventArgs e)
    {

        BindData();

        //Series series = Chart1.Series.Add("My series");
        Chart1.Series["Series1"].ToolTip = "#VALX: #VAL{N0} 噸";
        Chart1.Series["Series1"].LegendToolTip = "#PERCENT";
        Chart1.Series["Series1"].PostBackValue = "#INDEX";
        Chart1.Series["Series1"].LegendPostBackValue = "#INDEX";

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
    protected void BindData()
    {
        v_ym = TB_year.Text + "/" + DDL_month.SelectedValue;

        Sql_str = "SELECT  sf_get_value('processes','process_c','process_id', A.process) AS PROCESS_NAME,A.ROWSUM,A.PROCESS,A.YM   ";
        Sql_str += "FROM    WIP_SA4CM A ";
        Sql_str += "WHERE YM = '" + v_ym + "' ";
        Sql_str += "AND     A.DDESC = '小計' ";
        Sql_str += "ORDER BY A.ROWSUM DESC  ";

        DS_process.SelectCommand = Sql_str;
        Chart1.DataBind();

    }
}