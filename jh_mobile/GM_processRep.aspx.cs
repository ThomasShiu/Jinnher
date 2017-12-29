using System;
using Microsoft.Reporting.WebForms;
using System.IO;


public partial class GM_processRep : System.Web.UI.Page
{
    string Sql_str;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Query_btn_Click(object sender, EventArgs e)
    {
        Sql_str = "SELECT process,seq,lot_no,weight,d,pdate,NEXT,process_c,mtrl_id ";
        Sql_str += "  FROM wip_data_marts_month ";
        Sql_str += "WHERE YM = '2011/07' ";

        SqlDataSource1.SelectCommand = Sql_str;
        SqlDataSource1.DataBind();
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_process", SqlDataSource1));
        ReportViewer1.LocalReport.Refresh();
    }
    protected void Export_btn_Click(object sender, EventArgs e)
    {
        Sql_str = "SELECT process,seq,lot_no,weight,d,pdate,NEXT,process_c,mtrl_id ";
        Sql_str += "  FROM wip_data_marts_month ";
        Sql_str += "WHERE YM = '2011/07' ";

        SqlDataSource1.SelectCommand = Sql_str;
        SqlDataSource1.DataBind();
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_process", SqlDataSource1));
        ReportViewer1.LocalReport.Refresh();
        //ReportViewer1.DataBind();

        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;

        byte[] bytes = ReportViewer1.LocalReport.Render(
             "Pdf", null, out mimeType, out encoding, out extension,
              out streamids, out warnings);

        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment; filename=wirerequment.pdf");
        Response.AddHeader("Content-Length", bytes.Length.ToString());
        Response.ContentType = "application/octet-stream";
        Response.OutputStream.Write(bytes, 0, bytes.Length);
    }
}