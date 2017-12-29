using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class GM_bbitoship2 : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();
    thomas_Conn tsconn = new thomas_Conn();
    OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
    OracleDataReader dr;

    string Sql_str, v_ym;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ReportViewer1.LocalReport.ReportPath = "";
            save_log("BBI READY TO SHIP");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Sql_str = " SELECT  o.port       port ";
        Sql_str += "       , sf_get_ship_note2(I.EVENTUALPO,I.cat, I.dia, I.len,I.th,I.pl , I.stock_id)  MINIPORT ";
        Sql_str += "       , SF_GET_VALUE('BBI_MINIPORT','PDATE','STOCK_ID',S.STOCK_ID,'ROWNUM',1) PDATE ";
        Sql_str += "       , o.delivery0 ";
        Sql_str += "       , I.EVENTUALPO eventualpo ";
        Sql_str += "       , I.CAT  cat ";
        Sql_str += "       , I.DIA  dia ";
        Sql_str += "       , I.LEN  len ";
        Sql_str += "       , I.TH   th ";
        Sql_str += "       , I.PL   pl ";
        Sql_str += "       , i.pack_code 包裝方式 ";
        Sql_str += "       , sf_get_cust_prt_no(O.PO_NO,I.CAT,I.DIA,I.LEN,I.TH,I.PL) PART  ";
        Sql_str += "       , SUM(i.packkeg*i.qty_per_ctn)/sf_get_assemb_no(i.cat,i.dia,i.len,i.th)  pcs ";
        Sql_str += "       , SUM(i.packkeg)/sf_get_assemb_no(i.cat,i.dia,i.len,i.th)  keg ";
        Sql_str += "       , SUM(i.packkeg*p.weight*i.qty_per_ctn)/sf_get_assemb_no(i.cat,i.dia,i.len,i.th) nw ";
        Sql_str += "       , S.stock_id  ";
        Sql_str += "  FROM INVENTORY i, ORDERS o, STOCK s , products p ";
        Sql_str += " WHERE O.PO_NO = I.EVENTUALPO ";
        Sql_str += "   AND I.STOCK_ID = S.STOCK_ID ";
        Sql_str += "   AND (S.SHIPPING = 'N' OR S.SHIPPING IS NULL) ";
        Sql_str += "   AND S.PACKED = 'Y' ";
        Sql_str += "   AND p.cat_cat    = i.cat ";
        Sql_str += "   AND p.dia        = i.dia ";
        Sql_str += "   AND p.length     = i.len ";
        Sql_str += "   AND p.thread       = i.th ";
        Sql_str += "   AND O.CO_CO_ID = 'ETNBBI' ";
        Sql_str += " GROUP BY  o.port       ";
        Sql_str += "       , sf_get_ship_note2(I.EVENTUALPO,I.cat, I.dia, I.len,I.th,I.pl , I.stock_id) ";
        Sql_str += "       , SF_GET_VALUE('BBI_MINIPORT','PDATE','STOCK_ID',S.STOCK_ID,'ROWNUM',1)  ";
        Sql_str += "       , o.delivery0   , I.EVENTUALPO ";
        Sql_str += "       , I.CAT,I.DIA,I.LEN,I.TH,I.PL, i.pack_code ";
        Sql_str += "       , sf_get_cust_prt_no(O.PO_NO,I.CAT,I.DIA,I.LEN,I.TH,I.PL) ";
        Sql_str += "       , S.stock_id ";

        try
        {
            DS_bbitoship2.SelectCommand = Sql_str;
            //ReportViewer1.LocalReport.ReportPath = "report/R_BBI_TO_SHIP2.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_BBI_TO_SHIP2", DS_bbitoship2));
            ReportViewer1.LocalReport.Refresh();
            save_log("BBI轉單後統計");
        }
        catch(Exception ex)
        {
            message.Text = ex.ToString();
            save_log(ex.ToString());
        }
    }

    protected void save_log(string v_mess)
    {
        //取得程式名稱
        string filename = System.IO.Path.GetFileName(Request.PhysicalPath).Replace(".aspx", "");

        //紀錄LOG
        string strClientIP = Context.Request.ServerVariables["REMOTE_ADDR"];
        tsconn.save_log("Guest", filename, strClientIP, v_mess);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Sql_str = "SELECT CPN , cat, dia, len,th,pl , stock_id, kegs, ";
        Sql_str += "      MPCS , sf_get_stndrd_wt(cat, dia, len,th) SWT, EVENTUALPO, ";
        Sql_str += "      sf_get_ship_note2(EVENTUALPO,cat, dia, len,th,pl , stock_id)  MINIPORT,    port ";
        Sql_str += "FROM ( ";
        Sql_str += "    SELECT v.cat, v.dia, v.len,v.th,v.pl  ";
        Sql_str += "        , v.stock_id ";
        Sql_str += "        , sum(v.packkeg)/sf_get_assemb_no(v.cat, v.dia, v.len,v.TH) kegs  ";
        Sql_str += "        , SUM(V.QTY_PER_CTN*v.packkeg)/sf_get_assemb_no(v.cat, v.dia, v.len,v.TH) MPCS ";
        Sql_str += "        , sf_get_value('orderitems','CUST_PRT_NO','ODR_PO_NO',v.EVENTUALPO, 'PDT_CAT_CAT',v.cat,'PDT_DIA',v.dia,'PDT_LENGTH',v.len,'PDT_THREAD',v.th,'PDT_FIN_FIN_ID',v.pl ) CPN ";
        Sql_str += "        , V.EVENTUALPO eventualpo ";
        Sql_str += "        , o.port ";
        Sql_str += "     FROM INVENTORY v , stock s, ORDERS o ";
        Sql_str += "    WHERE v.STOCK_ID =s.STOCK_ID ";
        Sql_str += "    AND ( s.SHIPPING IS NULL OR s.SHIPPING ='N') ";
        Sql_str += "        AND O.PO_NO = v.EVENTUALPO ";
        Sql_str += "    AND O.CO_CO_ID = 'ETNBBI' ";
        Sql_str += "        GROUP BY v.cat, v.dia, v.len,v.th,v.pl , v.stock_id, ";
        Sql_str += "        sf_get_value('orderitems','CUST_PRT_NO','ODR_PO_NO',v.EVENTUALPO, 'PDT_CAT_CAT',v.cat,'PDT_DIA',v.dia,'PDT_LENGTH',v.len,'PDT_THREAD',v.th,'PDT_FIN_FIN_ID',v.pl ) ";
        Sql_str += "        , V.EVENTUALPO, o.port ";
        Sql_str += "        ) v ";
        Sql_str += "ORDER BY CPN , kegs DESC ";
        try
        {
            DS_bbitoship2.SelectCommand = Sql_str;
            ReportViewer1.LocalReport.ReportPath = "report/R_BBI_TO_SHIP1.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_bbitoship1", DS_bbitoship2));
            ReportViewer1.LocalReport.Refresh();
            save_log("BBI出貨明細");
        }
        catch (Exception ex)
        {
            message.Text = ex.ToString();
            save_log(ex.ToString());
        }
    }
}