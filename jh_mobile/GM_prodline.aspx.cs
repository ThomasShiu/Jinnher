using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class GM_prodline : System.Web.UI.Page
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
            TB_year.Text = DateTime.Today.Year.ToString();
            //string v_str = DateTime.Today.ToString("MM");
            DDL_month.SelectedIndex = DDL_month.Items.IndexOf(DDL_month.Items.FindByValue(DateTime.Today.ToString("MM")));
            //QueryProcess();
            //ReportViewer1.LocalReport.EnableHyperlinks = true;
        }
    }

    protected void save_log(string v_machine)
    {
        //取得程式名稱
        string filename = System.IO.Path.GetFileName(Request.PhysicalPath).Replace(".aspx", "");

        //紀錄LOG
        string strClientIP = Context.Request.ServerVariables["REMOTE_ADDR"];
        tsconn.save_log("Guest", filename, strClientIP, "查詢機台:" + v_machine  );
    }

    protected void Quy_btn_Click(object sender, EventArgs e)
    {
        Sql_str = "SELECT y,m,d, mg,machineid,SUM(pcs) mpcs,SUM(pcs*stndwt) snw ";
        Sql_str += " FROM ( ";
        Sql_str += "  SELECT  to_char(pdate,'YYYY') AS Y,to_char(pdate,'MM') AS M,  to_char(pdate,'DD') AS D,   ";
        Sql_str += "      SUBSTR(MACHINEID,1,2) AS MG,MACHINEID, ";
        Sql_str += "      NVL(sf_get_forming_LOTDAY_pcs( LOT_NO ,CTRLOT_NO,PDATE,MACHINEID),0) PCS, ";
        Sql_str += "      sf_get_stndrd_wt_by_lot(LOT_NO) stndwt ";
        Sql_str += "    FROM performance ";
        Sql_str += "   WHERE TO_CHAR(pdate,'YYYY/MM') ='2011/08' ";
        Sql_str += "   AND SUBSTR(MACHINEID,1,2) = 'B0' ";
        Sql_str += "   AND counter2 IS NOT NULL  ";
        Sql_str += " ) GROUP BY  y,m,d, mg,machineid ";
        Sql_str += " ORDER BY y,m,d, mg,machineid ";

        DS_prodline.SelectCommand = Sql_str;
        ReportViewer1.LocalReport.ReportPath = "report/R_prodline2.rdlc";
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_PRODLINE", DS_prodline));
        ReportViewer1.LocalReport.Refresh();

    }
    protected void Report_btn_Click(object sender, EventArgs e)
    {

    }
    protected void B0_btn_Click(object sender, EventArgs e)
    {
        CreatRep("B0");
        save_log("B0");
    }
    protected void B1_btn_Click(object sender, EventArgs e)
    {
        CreatRep("B1");
        save_log("B1");
    }
    protected void B2_btn_Click(object sender, EventArgs e)
    {
        CreatRep("B2");
        save_log("B2");
    }
    protected void B3_btn_Click(object sender, EventArgs e)
    {
        CreatRep("B3");
        save_log("B3");
    }
    protected void B4_btn_Click(object sender, EventArgs e)
    {
        CreatRep("B4");
        save_log("B4");
    }
    protected void B5_btn_Click(object sender, EventArgs e)
    {
        CreatRep("B5");
        save_log("B5");
    }
    protected void CreatRep(string machineid)
    {
        v_ym = TB_year.Text + "/" + DDL_month.SelectedValue;


        //Sql_str = "SELECT y,m,d, mg,machineid,NVL(SUM(pcs),0) mpcs,NVL(SUM(pcs*stndwt),0) snw,STATUS ";
        //Sql_str += " FROM ( ";
        //Sql_str += "  SELECT  to_char(pdate,'YYYY') AS y,to_char(A.PDATE,'MM') m,  to_char(A.pdate,'DD') d, ";
        //Sql_str += "      SUBSTR(A.MACHINEID,1,2)  MG,A.MACHINEID, ";
        //Sql_str += "      NVL(sf_get_forming_LOTDAY_pcs(A. LOT_NO ,A.CTRLOT_NO,A.PDATE,A.MACHINEID),0) PCS, ";
        //Sql_str += "    sf_get_stndrd_wt_by_lot(A.LOT_NO) stndwt , ";
        //Sql_str += "    B.STATUS ";
        //Sql_str += "    FROM performance A, ";
        //Sql_str += "            (SELECT STATUS,MACHINEID,PRODDATE  FROM MACHINE_RUNS WHERE PRODTIME_E IS NULL) B ";
        //Sql_str += "   WHERE A.MACHINEID = B.MACHINEID(+) ";
        //Sql_str += "   AND    TO_CHAR( A.PDATE,'YYYY/MM/DD')=B.PRODDATE(+) ";
        //Sql_str += "   AND TO_CHAR(A.pdate,'YYYY/MM') ='" + v_ym + "' ";
        //Sql_str += "   AND SUBSTR(A.MACHINEID,1,2) = '" + machineid + "' ";
        //Sql_str += "   AND A.counter2 IS NOT NULL  ";
        //Sql_str += " ) GROUP BY  y,m,d, mg,machineid,STATUS ";
        //Sql_str += " ORDER BY y,m,d , machineid   ";

        Sql_str = "SELECT TO_CHAR(A.WORK_DATE,'YYYY') AS Y,TO_CHAR(A.WORK_DATE,'MM') AS M,TO_CHAR(A.WORK_DATE,'DD') AS D, ";
        Sql_str += "            SUBSTR(A.MACHINEID,0,2) AS MG,A.MACHINEID,A.CAPACITY,NVL(B.PCS,0) AS MPCS,NVL(B.SNW,0) SNW  ";
        Sql_str += "FROM  ";
        Sql_str += "  ( SELECT W.WORK_DATE,M.MACHINEID,Round(M.CAPACITY*540) AS CAPACITY ";
        Sql_str += "    FROM   WORKDATE W,MACHINES M ";
        Sql_str += "    WHERE TO_CHAR(W.WORK_DATE,'YYYY/MM') = '" + v_ym + "' ";
        Sql_str += "    AND     W.WORK_DATE <= SYSDATE ";
        Sql_str += "    AND     MACHINEID LIKE '" + machineid + "%'  ) A, ";
        Sql_str += "    (SELECT  PDATE,MACHINEID, ";
        Sql_str += "                 SUM(NVL(sf_get_forming_LOTDAY_pcs(LOT_NO ,CTRLOT_NO,PDATE,MACHINEID),0)) PCS, ";
        Sql_str += "                 ROUND(SUM(NVL((sf_get_forming_LOTDAY_pcs(LOT_NO ,CTRLOT_NO,PDATE,MACHINEID)*sf_get_stndrd_wt_by_lot(LOT_NO))/1000,0)),1) SNW ";
        Sql_str += "    FROM  PERFORMANCE ";
        Sql_str += "    WHERE TO_CHAR(PDATE,'YYYY/MM') = '" + v_ym + "' ";
        Sql_str += "    AND     MACHINEID LIKE '" + machineid + "%' ";
        Sql_str += "    GROUP BY PDATE,MACHINEID) B ";
        Sql_str += "WHERE A.WORK_DATE = B.PDATE(+) ";
        Sql_str += "AND     A.MACHINEID = B.MACHINEID(+)   ";
        Sql_str += "ORDER BY  A.WORK_DATE,A.MACHINEID  ";

        DS_prodline.SelectCommand = Sql_str;
        ReportViewer1.LocalReport.ReportPath = "report/R_prodline2.rdlc";
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_PRODLINE", DS_prodline));
        ReportViewer1.LocalReport.Refresh();
    }
}