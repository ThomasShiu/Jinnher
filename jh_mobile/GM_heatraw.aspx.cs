using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;


public partial class GM_heatraw : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();
    OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
    OracleDataReader dr;

    string Sql_str,Sql_str2 ,v_ym;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    //產生報表
    protected void Report_btn_Click(object sender, EventArgs e)
    {
        Sql_str = "SELECT   WIP_RAW,QDATE,LOT_NO,CTRLOT_NO, ";
        Sql_str += "     HEAT_NO, FORM_KEGS,Q_KEGS,WT,PDATE,HEAT_ITEMS, ";
        Sql_str += "     H_KEGS,ASRS_KEGS, CAT,DIA,LEN,TH,PL,TEM_SEC1_TEM, ";
        Sql_str += "     PRD_RUNS_STATUS,SEQ,MINDELIVERY,CHKIN_DATE, ";
        Sql_str += "     PDTSIZE,  DIAMETER,MDIA,MLEN, ";
        Sql_str += "     decode( SUBSTR(LOT_NO,8,1),'P','HPL','電') CHK_HPL,asrs_gw, ";
        Sql_str += "     CASE WHEN to_char(MINDELIVERY,'yyyymm')<to_char(SYSDATE,'yyyymm')  THEN WT ELSE 0 END AS T_KEGS  ";
        Sql_str += " FROM HEAT_PRD_RUNS ";

        Sql_str2 = "SELECT  '尚有油桶庫位 '||count(*)||' 格, 約可繳 '||count(*)*0.7||' 噸重 (每桶以700公斤計)' as NW ";
        Sql_str2 += " FROM   asrs.sim_loc_mas M ";
        Sql_str2 += "WHERE   m.loc_sts NOT IN ('F','S','D','X','x') ";
        Sql_str2 += "  AND   tank_type = '1' ";

        try
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            SqlDataSource1.SelectCommand = Sql_str;
            SqlDataSource2.SelectCommand = Sql_str2;
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_heat_prd_runs", SqlDataSource1));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_nw", SqlDataSource2));
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
            Response.AddHeader("Content-Disposition", "attachment; filename=heatraw.pdf");
            Response.AddHeader("Content-Length", bytes.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.OutputStream.Write(bytes, 0, bytes.Length);
        }
        catch (Exception err)
        {
            message.Text = err.ToString();
        }
    }
    protected void Quy_btn_Click(object sender, EventArgs e)
    {
        Sql_str = "SELECT   WIP_RAW,QDATE,LOT_NO,CTRLOT_NO, ";
        Sql_str += "     HEAT_NO, FORM_KEGS,Q_KEGS,WT,PDATE,HEAT_ITEMS, ";
        Sql_str += "     H_KEGS,ASRS_KEGS, CAT,DIA,LEN,TH,PL,TEM_SEC1_TEM, ";
        Sql_str += "     PRD_RUNS_STATUS,SEQ,MINDELIVERY,CHKIN_DATE, ";
        Sql_str += "     PDTSIZE,  DIAMETER,MDIA,MLEN, ";
        Sql_str += "     decode( SUBSTR(LOT_NO,8,1),'P','HPL','電') CHK_HPL,asrs_gw, ";
        Sql_str += "     CASE WHEN to_char(MINDELIVERY,'yyyymm')<to_char(SYSDATE,'yyyymm')  THEN WT ELSE 0 END AS T_KEGS  ";
        Sql_str += " FROM HEAT_PRD_RUNS ";

        Sql_str2 = "SELECT  '尚有油桶庫位 '||count(*)||' 格, 約可繳 '||count(*)*0.7||' 噸重 (每桶以700公斤計)' as NW ";
        Sql_str2 += " FROM   asrs.sim_loc_mas M ";
        Sql_str2 += "WHERE   m.loc_sts NOT IN ('F','S','D','X','x') ";
        Sql_str2 += "  AND   tank_type = '1' ";

        try
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            SqlDataSource1.SelectCommand = Sql_str;
            SqlDataSource2.SelectCommand = Sql_str2;
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_heat_prd_runs", SqlDataSource1));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_nw", SqlDataSource2));
            ReportViewer1.LocalReport.Refresh();
            //ReportViewer1.DataBind();

            //Warning[] warnings;
            //string[] streamids;
            //string mimeType;
            //string encoding;
            //string extension;

            //byte[] bytes = ReportViewer1.LocalReport.Render(
            //     "Pdf", null, out mimeType, out encoding, out extension,
            //      out streamids, out warnings);

            //Response.Clear();
            //Response.AddHeader("Content-Disposition", "attachment; filename=heatraw.pdf");
            //Response.AddHeader("Content-Length", bytes.Length.ToString());
            //Response.ContentType = "application/octet-stream";
            //Response.OutputStream.Write(bytes, 0, bytes.Length);
        }
        catch (Exception err)
        {
            message.Text = err.ToString();
        }
    }
}