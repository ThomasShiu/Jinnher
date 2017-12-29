using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class GM_processanaly : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();
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
            QueryProcess();
        }
    }

    //產生報表
    protected void Report_btn_Click(object sender, EventArgs e)
    {
        v_ym = TB_year.Text + "/" + DDL_month.SelectedValue;

        Sql_str = "SELECT seq,lot_no,weight,d,pdate,NEXT,process_c,mtrl_id ";
        Sql_str += "     ,SF_GET_PROCESS_STEP(lot_no,process,next,mtrl_id) AS PROCESS ";
        Sql_str += "     ,SF_GETPROCESS_NAME(process,process_c) AS PROCESS_C ";  
        Sql_str += "  FROM wip_data_marts_month ";
        Sql_str += "WHERE YM = '" + v_ym + "' ";
        Sql_str += "ORDER BY SEQ ";
        try
        {
            SqlDataSource1.SelectCommand = Sql_str;
            //SqlDataSource1.DataBind();
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
        catch (Exception err)
        {
            message.Text = err.ToString();
        }
    }

    protected void Quy_btn_Click(object sender, EventArgs e)
    {
        QueryProcess();

        //將其他GridView清空
        DS_machine.SelectCommand = "";
        GridView2.DataBind();

        GridView1.SelectedIndex = -1;

    }

    //製程
    protected void QueryProcess()
    {
        v_ym = TB_year.Text + "/" + DDL_month.SelectedValue;

        Sql_str = "SELECT  sf_get_value('processes','process_c','process_id', A.process) AS PROCESS_NAME,A.ROWSUM,A.PROCESS,A.YM  ";
        Sql_str += "FROM    WIP_SA4CM A ";
        Sql_str += "WHERE YM = '" + v_ym + "' ";
        Sql_str += "AND     A.DDESC = '小計' ";
        Sql_str += "ORDER BY A.ROWSUM DESC ";

        DS_process.SelectCommand = Sql_str;
    }

    //機台產能查詢
    protected void QueryProcessSum(string v_machid)
    {
        v_ym = TB_year.Text + "/" + DDL_month.SelectedValue;

        Sql_str = " SELECT A.DDESC AS 日,ROUND(A.ROWSUM) AS 小計 ";

        for (int i = 1; i <= 30; i++)
        {
            string v_i;
            if (i < 10) { v_i = "0" + i.ToString(); } else { v_i = i.ToString(); }
            Sql_str += " ,ROUND(C" + v_i + ") AS C" + v_i;
        }
        Sql_str += " FROM   WIP_SA4CM A  ";
        Sql_str += "WHERE A.YM = '" + v_ym + "' ";
        Sql_str += "AND     A.PROCESS = '" + v_machid + "' ";
        Sql_str += "ORDER BY A.DDESC  ";

        DS_machine.SelectCommand = Sql_str;
        //GridView2.DataBind();
    }

    //機台產能查詢B
    protected void QueryProcessSumB(string v_machid)
    {
        v_ym = TB_year.Text + "/" + DDL_month.SelectedValue;

        Sql_str = " SELECT A.DDESC AS 日,ROUND(A.ROWSUM) AS 小計 ";
        Sql_str += "   ,ROUND(C01) AS \"1\" ,ROUND(C02) AS \"2\" ,ROUND(C03) AS \"3\" ,ROUND(C04) AS \"4\" ,ROUND(C05) AS \"5\" ,ROUND(C06) AS \"6\" ,ROUND(C07) AS \"7\" ,ROUND(C08) AS \"8\" ,ROUND(C09) AS \"9\" ,ROUND(C10) AS \"A\"  ";
        Sql_str += "   ,ROUND(C11) AS \"B\" ,ROUND(C12) AS \"C\" ,ROUND(C13) AS \"D\" ,ROUND(C14) AS \"E\" ,ROUND(C15) AS \"F\" ,ROUND(C16) AS \"G\" ,ROUND(C17) AS \"H\" ,ROUND(C18) AS \"I\" ,ROUND(C19) AS \"J\" ,ROUND(C20) AS \"K\"  ";
        Sql_str += "   ,ROUND(C21) AS \"L\" ,ROUND(C22) AS \"M\" ,ROUND(C23) AS \"N\" ,ROUND(C24) AS \"O\" ,ROUND(C25) AS \"P\" ,ROUND(C26) AS \"Q\" ,ROUND(C27) AS \"V\" ,ROUND(C28) AS \"W\" ,ROUND(C29) AS \"Y\" ,ROUND(C30) AS \"Z\"  ";
        Sql_str += " FROM   WIP_SA4CM A  ";
        Sql_str += "WHERE A.YM = '" + v_ym + "' ";
        Sql_str += "AND     A.PROCESS = '" + v_machid + "' ";
        Sql_str += "ORDER BY A.DDESC  ";

        DS_machine.SelectCommand = Sql_str;
        
        //GridView2.DataBind();
    }
    //機台產能查詢F
    protected void QueryProcessSumF(string v_machid)
    {
        v_ym = TB_year.Text + "/" + DDL_month.SelectedValue;

        Sql_str = " SELECT A.DDESC AS 日,ROUND(A.ROWSUM) AS 小計 ";
        Sql_str += "      ,ROUND(C01) AS B0 ,ROUND(C02) AS B1 ,ROUND(C03) AS B2 ,ROUND(C04) AS B3 ,ROUND(C05) AS B4 ,ROUND(C06) AS B5   ";
        Sql_str += " FROM   WIP_SA4CM A  ";
        Sql_str += "WHERE A.YM = '" + v_ym + "' ";
        Sql_str += "AND     A.PROCESS = '" + v_machid + "' ";
        Sql_str += "ORDER BY A.DDESC  ";

        DS_machine.SelectCommand = Sql_str;
        //GridView2.DataBind();
    }
    //機台產能查詢H
    protected void QueryProcessSumH(string v_machid)
    {
        v_ym = TB_year.Text + "/" + DDL_month.SelectedValue;

        Sql_str = " SELECT A.DDESC AS 日,ROUND(A.ROWSUM) AS 小計 ";
        Sql_str += "      ,ROUND(C01) AS \"1\" ,ROUND(C02) AS \"2\" ,ROUND(C03) AS \"3\" ,ROUND(C04) AS \"4\" ,ROUND(C05) AS \"5\" ,ROUND(C06) AS \"6\" ,ROUND(C07) AS \"7\",ROUND(C08) AS \"8\",ROUND(C09) AS \"9\"  ";
        Sql_str += " FROM   WIP_SA4CM A  ";
        Sql_str += "WHERE A.YM = '" + v_ym + "' ";
        Sql_str += "AND     A.PROCESS = '" + v_machid + "' ";
        Sql_str += "ORDER BY A.DDESC  ";

        DS_machine.SelectCommand = Sql_str;
        //GridView2.DataBind();
    }
    //機台產能查詢T
    protected void QueryProcessSumT(string v_machid)
    {
        v_ym = TB_year.Text + "/" + DDL_month.SelectedValue;

        Sql_str = " SELECT A.DDESC AS 日,ROUND(A.ROWSUM) AS 小計 ";
        Sql_str += "      ,ROUND(C01) AS \"A0\" ,ROUND(C02) AS \"A1\" ,ROUND(C03) AS \"A2\" ,ROUND(C04) AS \"A3\" ,ROUND(C05) AS \"A4\"  ";
        Sql_str += " FROM   WIP_SA4CM A  ";
        Sql_str += "WHERE A.YM = '" + v_ym + "' ";
        Sql_str += "AND     A.PROCESS = '" + v_machid + "' ";
        Sql_str += "ORDER BY A.DDESC  ";

        DS_machine.SelectCommand = Sql_str;
        //GridView2.DataBind();
    }
    //機台產能查詢P
    protected void QueryProcessSumP(string v_machid)
    {
        v_ym = TB_year.Text + "/" + DDL_month.SelectedValue;

        Sql_str = " SELECT A.DDESC AS 日,ROUND(A.ROWSUM) AS 小計 ";
        Sql_str += "      ,ROUND(C01) AS \"1\" ,ROUND(C02) AS \"2\" ,ROUND(C03) AS \"3\" ,ROUND(C04) AS \"4\"    ";
        Sql_str += " FROM   WIP_SA4CM A  ";
        Sql_str += "WHERE A.YM = '" + v_ym + "' ";
        Sql_str += "AND     A.PROCESS = '" + v_machid + "' ";
        Sql_str += "ORDER BY A.DDESC  ";

        DS_machine.SelectCommand = Sql_str;
        //GridView2.DataBind();
    }

    //包裝機台
    protected void QueryPackMach()
    {
        Sql_str = " SELECT  ABBR,DESCRPT ";
        Sql_str += "FROM   MACHINE_GROUPS_PACK ";
        Sql_str += "ORDER BY ABBR ";

        DS_packmach.SelectCommand = Sql_str;
        //GridView2.DataBind();
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView1.SelectedIndex = e.NewSelectedIndex;
        GridView2.SelectedIndex = -1;

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label L_process = (Label)GridView1.Rows[index].Cells[0].FindControl("L_process");

        if (e.CommandName == "Select")
        {
            //QueryProcessSum(L_process.Text);
            //包裝機台清空
            DS_packmach.SelectCommand = "";
            GridView3.DataBind();

            switch (L_process.Text)
            {
                case "B":
                    {
                        QueryProcessSumB(L_process.Text);
                        //跳出包裝機視窗
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "window.open('GM_packmach.aspx','','');", true);
                        QueryPackMach();
                        break;
                    }
                case "F":
                    {
                        QueryProcessSumF(L_process.Text);
                        break;
                    }
                case "H":
                    {
                        QueryProcessSumH(L_process.Text);
                        break;
                    }
                case "T":
                    {
                        QueryProcessSumT(L_process.Text);
                        break;
                    }
                case "P":
                    {
                        QueryProcessSumP(L_process.Text);
                        break;
                    }
                 default:
                    {
                        QueryProcessSum(L_process.Text);
                        break;
                    }

            }
        }

    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView2.SelectedIndex = e.NewSelectedIndex;

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label L_coid = (Label)GridView2.Rows[index].Cells[0].FindControl("L_coid");

        if (e.CommandName == "Select")
        {

        }
    }
  


}