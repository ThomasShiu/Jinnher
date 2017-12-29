using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class GM_platheatraw : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();
    OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
    OracleDataReader dr;

    string Sql_str, v_ym;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    //產生報表
    protected void Report_btn_Click(object sender, EventArgs e)
    {
        Sql_str = "SELECT FIN_TYPE, ";
        Sql_str += "decode(fin_type, 'MG','公制除氫','IG','英制除氫','MZ','公制電鍍','IZ','英制電鍍' ";
        Sql_str += "        ,'Y','五彩','3Y','H3Y','LC','低碳','OC','洗油','其他') fin_desc , ";
        Sql_str += "             THREAD_TYPE,CODE4LOT, LOT_NO, CTRLOT_NO, ";
        Sql_str += "             CAT, DIA, LEN,TH,PL,NEXT,PDTSIZE,RAWMTRL_ID, ";
        Sql_str += "             HEAT_NO,KEGS,GRADE, ";
        Sql_str += "             MPS,PLT,KEG_NO,DELIVERY0,FOLLOWUP,STOCK_STATUS, ";
        Sql_str += "             MOVEON_DATE,ROUND(SYSDATE - moveon_date) AS QDATE, ";
        Sql_str += "             ( ";
        Sql_str += "                   SELECT NVL(SUM(NVL(weight,0)),0) ";
        Sql_str += "                    FROM wip B ";
        Sql_str += "                   WHERE B.next = 'P' ";
        Sql_str += "                     AND B.lot_no = A.LOT_NO ";
        Sql_str += "                     AND B.ctrlot_no = A.CTRLOT_NO ";
        Sql_str += "                     AND B.in_process = 'N' ";
        Sql_str += "                     AND B.weight is not null ";
        Sql_str += "             )  AS WT, ";
        Sql_str += "             sf_get_sim_asrs_gw('P',A.LOT_NO, A.CTRLOT_NO) AS GW_ASRS, ";
        Sql_str += "             CASE WHEN to_char(A.DELIVERY0,'yyyymm')<to_char(SYSDATE,'yyyymm') THEN  ";
        Sql_str += "              ( ";
        Sql_str += "                   SELECT NVL(SUM(NVL(weight,0)),0) ";
        Sql_str += "                    FROM wip B ";
        Sql_str += "                   WHERE B.next = 'P' ";
        Sql_str += "                     AND B.lot_no = A.LOT_NO ";
        Sql_str += "                     AND B.ctrlot_no = A.CTRLOT_NO ";
        Sql_str += "                     AND B.in_process = 'N' ";
        Sql_str += "                     AND B.weight is not null ";
        Sql_str += "             )  ELSE 0 END AS GW_DELAY ";
        Sql_str += "FROM WIP_PLT_TRANSFER  A ";
        Sql_str += "ORDER BY  MOVEON_DATE ";

        try
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            SqlDataSource1.SelectCommand = Sql_str;
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_WIP_PLT", SqlDataSource1));
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
            Response.AddHeader("Content-Disposition", "attachment; filename=Platheatraw.pdf");
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
        Sql_str = "SELECT FIN_TYPE, ";
        Sql_str += "decode(fin_type, 'MG','公制除氫','IG','英制除氫','MZ','公制電鍍','IZ','英制電鍍' ";
        Sql_str += "        ,'Y','五彩','3Y','H3Y','LC','低碳','OC','洗油','其他') fin_desc , ";
        Sql_str += "             THREAD_TYPE,CODE4LOT, LOT_NO, CTRLOT_NO, ";
        Sql_str += "             CAT, DIA, LEN,TH,PL,NEXT,PDTSIZE,RAWMTRL_ID, ";
        Sql_str += "             HEAT_NO,KEGS,GRADE, ";
        Sql_str += "             MPS,PLT,KEG_NO,DELIVERY0,FOLLOWUP,STOCK_STATUS, ";
        Sql_str += "             MOVEON_DATE,SYSDATE - moveon_date qdate, ";
        Sql_str += "             ( ";
        Sql_str += "                   SELECT NVL(SUM(NVL(weight,0)),0) ";
        Sql_str += "                    FROM wip B ";
        Sql_str += "                   WHERE B.next = 'P' ";
        Sql_str += "                     AND B.lot_no = A.LOT_NO ";
        Sql_str += "                     AND B.ctrlot_no = A.CTRLOT_NO ";
        Sql_str += "                     AND B.in_process = 'N' ";
        Sql_str += "                     AND B.weight is not null ";
        Sql_str += "             )  AS WT, ";
        Sql_str += "             sf_get_sim_asrs_gw('P',A.LOT_NO, A.CTRLOT_NO) AS GW_ASRS, ";
        Sql_str += "             CASE WHEN to_char(A.DELIVERY0,'yyyymm')<to_char(SYSDATE,'yyyymm') THEN  ";
        Sql_str += "              ( ";
        Sql_str += "                   SELECT NVL(SUM(NVL(weight,0)),0) ";
        Sql_str += "                    FROM wip B ";
        Sql_str += "                   WHERE B.next = 'P' ";
        Sql_str += "                     AND B.lot_no = A.LOT_NO ";
        Sql_str += "                     AND B.ctrlot_no = A.CTRLOT_NO ";
        Sql_str += "                     AND B.in_process = 'N' ";
        Sql_str += "                     AND B.weight is not null ";
        Sql_str += "             )  ELSE 0 END AS GW_DELAY ";
        Sql_str += "FROM WIP_PLT_TRANSFER  A ";
        Sql_str += "ORDER BY  MOVEON_DATE ";

        try
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            SqlDataSource1.SelectCommand = Sql_str;
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_WIP_PLT", SqlDataSource1)); 
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        Sql_str = "SELECT FIN_TYPE, ";
        Sql_str += "decode(fin_type, 'MG','公制除氫','IG','英制除氫','MZ','公制電鍍','IZ','英制電鍍' ";
        Sql_str += "        ,'Y','五彩','3Y','H3Y','LC','低碳','OC','洗油','其他') fin_desc , ";
        Sql_str += "             THREAD_TYPE,CODE4LOT, LOT_NO, CTRLOT_NO, ";
        Sql_str += "             CAT, DIA, LEN,TH,PL,NEXT,PDTSIZE,RAWMTRL_ID, ";
        Sql_str += "             HEAT_NO,KEGS,GRADE, ";
        Sql_str += "             MPS,PLT,KEG_NO,DELIVERY0,FOLLOWUP,STOCK_STATUS, ";
        Sql_str += "             MOVEON_DATE,ROUND(SYSDATE - moveon_date) AS QDATE, ";
        Sql_str += "             ( ";
        Sql_str += "                   SELECT NVL(SUM(NVL(weight,0)),0) ";
        Sql_str += "                    FROM wip B ";
        Sql_str += "                   WHERE B.next = 'P' ";
        Sql_str += "                     AND B.lot_no = A.LOT_NO ";
        Sql_str += "                     AND B.ctrlot_no = A.CTRLOT_NO ";
        Sql_str += "                     AND B.in_process = 'N' ";
        Sql_str += "                     AND B.weight is not null ";
        Sql_str += "             )  AS WT, ";
        Sql_str += "             sf_get_sim_asrs_gw('P',A.LOT_NO, A.CTRLOT_NO) AS GW_ASRS, ";
        Sql_str += "             CASE WHEN to_char(A.DELIVERY0,'yyyymm')<to_char(SYSDATE,'yyyymm') THEN  ";
        Sql_str += "              ( ";
        Sql_str += "                   SELECT NVL(SUM(NVL(weight,0)),0) ";
        Sql_str += "                    FROM wip B ";
        Sql_str += "                   WHERE B.next = 'P' ";
        Sql_str += "                     AND B.lot_no = A.LOT_NO ";
        Sql_str += "                     AND B.ctrlot_no = A.CTRLOT_NO ";
        Sql_str += "                     AND B.in_process = 'N' ";
        Sql_str += "                     AND B.weight is not null ";
        Sql_str += "             )  ELSE 0 END AS GW_DELAY ";
        Sql_str += "FROM WIP_PLT_TRANSFER  A ";
        Sql_str += "ORDER BY  MOVEON_DATE ";

        try
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            SqlDataSource1.SelectCommand = Sql_str;
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_WIP_PLT", SqlDataSource1));
            ReportViewer1.LocalReport.Refresh();
            //ReportViewer1.DataBind();

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = ReportViewer1.LocalReport.Render(
                 "Excel", null, out mimeType, out encoding, out extension,
                  out streamids, out warnings);

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=Platheatraw.xls");
            Response.AddHeader("Content-Length", bytes.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.OutputStream.Write(bytes, 0, bytes.Length);
        }
        catch (Exception err)
        {
            message.Text = err.ToString();
        }
    }
}