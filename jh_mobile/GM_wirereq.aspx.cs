using System;
using Microsoft.Reporting.WebForms;
using System.Web.UI;

public partial class GM_wirereq : System.Web.UI.Page
{
    string Sql_str, sScript;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //GenPDF();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //    Microsoft.Reporting.WebForms.LocalReport lr =
        //new Microsoft.Reporting.WebForms.LocalReport();

        //    string deviceInfo =
        //      "<DeviceInfo>" +
        //      "<SimplePageHeaders>True</SimplePageHeaders>" +
        //      "</DeviceInfo>";

        //    lr.ReportPath = @"~\Reports\Report.rdlc";

        //    lr.DataSources.Add(new ReportDataSource("V_GM_WIREREQ1", GetData()));

        //    byte[] bytes = lr.Render("Excel", deviceInfo, out mimeType,
        //      out encoding, out streamids, out warnings);

        //    using (FileStream fs = new FileStream(@"c:\My Reports\Monthly Sales.xls", FileMode.Create))
        //    {
        //        fs.Write(bytes, 0, bytes.Length);
        //        fs.Close();
        //    }
        Sql_str = "SELECT x.mtrl , x.dia,x.deliym, (x.stwt/1000) as  schdwt, (y.inwt/1000) as stockwt ";
        Sql_str += "FROM ( ";
        Sql_str += "SELECT     ";
        Sql_str += "    rawmtrl_id_m  mtrl, ";
        Sql_str += "    rawmtrl_dia_m dia, ";
        Sql_str += "    deliym, ";
        Sql_str += "  SUM(wt) stwt ";
        Sql_str += "FROM ( ";
        Sql_str += "        SELECT     r.former2 former_m ";
        Sql_str += "            ,p.rawmtrl_dia   rawmtrl_dia_m ";
        Sql_str += "            ,p.rawmtrl_rawmtrl_id rawmtrl_id_m ";
        Sql_str += "          ,p.wire_dia  wire_dia_m ";
        Sql_str += "          ,sf_get_short_weight2('',r.lot_no)  as wt ";
        Sql_str += "                    ,to_char(r.MINDELIVERY ,'yyyy/mm') as deliym ";
        Sql_str += "           FROM items  i, ";
        Sql_str += "                prd_runs r, ";
        Sql_str += "                products p ";
        Sql_str += "          WHERE    i.lot_no     =       r.lot_no ";
        Sql_str += "            AND i.cat        =       p.cat_cat ";
        Sql_str += "            AND i.dia        =       p.dia ";
        Sql_str += "            AND i.len        =       p.length ";
        Sql_str += "            AND i.th         =       p.thread ";
        Sql_str += "            AND former2 LIKE 'B%'     ";
        Sql_str += "            AND r.enddate  IS NULL ";
        Sql_str += "            AND schedule IS NOT NULL ";
        Sql_str += "          GROUP BY r.former2,p.rawmtrl_dia,p.rawmtrl_rawmtrl_id,p.wire_dia,r.lot_no,to_char(r.MINDELIVERY ,'yyyy/mm') ";
        Sql_str += "    ) ";
        Sql_str += "  GROUP BY     rawmtrl_id_m,    rawmtrl_dia_m,deliym ";
        Sql_str += ") x ,  ";
        Sql_str += "( ";
        Sql_str += "  SELECT rawmtrl_id mtrl , diameter dia, SUM( NVL(weight,0)) inwt ";
        Sql_str += "    FROM wires ";
        Sql_str += "   WHERE location NOT IN ('O','P', 'SCRAP','SOLD') ";
        Sql_str += "     GROUP BY  rawmtrl_id, diameter ";
        Sql_str += ") y ";
        Sql_str += "WHERE x.mtrl= y.mtrl(+) ";
        Sql_str += "AND x.dia= y.dia(+) ";

        SqlDataSource1.SelectCommand = Sql_str;
        SqlDataSource1.DataBind();
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("WIREREQ", SqlDataSource1));
        ReportViewer1.LocalReport.Refresh();

        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;

        byte[] bytes = ReportViewer1.LocalReport.Render(
           "Excel", null, out mimeType, out encoding, out extension,
           out streamids, out warnings);

        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment; filename=wirerequment.xls");
        Response.AddHeader("Content-Length", bytes.Length.ToString());
        Response.ContentType = "application/octet-stream";
        Response.OutputStream.Write(bytes, 0, bytes.Length);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //sScript = " $.blockUI({message: $('<h5 style='text-align:center'><img src='images/loading.gif' /> <br/>loading...</h5>')";
        //ScriptManager.RegisterOnSubmitStatement(this, this.GetType(), "blockUI", sScript);
        
        GenPDF();

        //sScript = "$.unblockUI();";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "unblockUI", sScript, true);
    }

    protected void GenPDF()
    {
        Sql_str = "SELECT x.mtrl , x.dia,x.deliym, (x.stwt/1000) as  schdwt, (y.inwt/1000) as stockwt ";
        Sql_str += "FROM ( ";
        Sql_str += "SELECT     ";
        Sql_str += "    rawmtrl_id_m  mtrl, ";
        Sql_str += "    rawmtrl_dia_m dia, ";
        Sql_str += "    deliym, ";
        Sql_str += "  SUM(wt) stwt ";
        Sql_str += "FROM ( ";
        Sql_str += "        SELECT     r.former2 former_m ";
        Sql_str += "            ,p.rawmtrl_dia   rawmtrl_dia_m ";
        Sql_str += "            ,p.rawmtrl_rawmtrl_id rawmtrl_id_m ";
        Sql_str += "          ,p.wire_dia  wire_dia_m ";
        Sql_str += "          ,sf_get_short_weight2('',r.lot_no)  as wt ";
        Sql_str += "                    ,to_char(r.MINDELIVERY ,'yyyy/mm') as deliym ";
        Sql_str += "           FROM items  i, ";
        Sql_str += "                prd_runs r, ";
        Sql_str += "                products p ";
        Sql_str += "          WHERE    i.lot_no     =       r.lot_no ";
        Sql_str += "            AND i.cat        =       p.cat_cat ";
        Sql_str += "            AND i.dia        =       p.dia ";
        Sql_str += "            AND i.len        =       p.length ";
        Sql_str += "            AND i.th         =       p.thread ";
        Sql_str += "            AND former2 LIKE 'B%'     ";
        Sql_str += "            AND r.enddate  IS NULL ";
        Sql_str += "            AND schedule IS NOT NULL ";
        Sql_str += "          GROUP BY r.former2,p.rawmtrl_dia,p.rawmtrl_rawmtrl_id,p.wire_dia,r.lot_no,to_char(r.MINDELIVERY ,'yyyy/mm') ";
        Sql_str += "    ) ";
        Sql_str += "  GROUP BY     rawmtrl_id_m,    rawmtrl_dia_m,deliym ";
        Sql_str += ") x ,  ";
        Sql_str += "( ";
        Sql_str += "  SELECT rawmtrl_id mtrl , diameter dia, SUM( NVL(weight,0)) inwt ";
        Sql_str += "    FROM wires ";
        Sql_str += "   WHERE location NOT IN ('O','P', 'SCRAP','SOLD') ";
        Sql_str += "     GROUP BY  rawmtrl_id, diameter ";
        Sql_str += ") y ";
        Sql_str += "WHERE x.mtrl= y.mtrl(+) ";
        Sql_str += "AND x.dia= y.dia(+) ";

        SqlDataSource1.SelectCommand = Sql_str;
        SqlDataSource1.DataBind();
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("WIREREQ", SqlDataSource1));
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