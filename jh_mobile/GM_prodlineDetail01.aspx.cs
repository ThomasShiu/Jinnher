using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GM_prodlineDetail01 : System.Web.UI.Page
{
    string Sql_str;
    static string v_padate, v_machid;
    thomas_Conn tsconn = new thomas_Conn();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                v_padate = Request["pdate"].ToString();
                v_machid = Request["machid"].ToString();
                L_pdate.Text = v_padate;
                L_machineid.Text = v_machid;


                BindGrid1(v_machid);
                BindGrid2(v_machid);
                BindGrid(v_padate, v_machid);

                save_log();
            }
            catch (Exception ex)
            {
                message.Text = ex.ToString();
            }
        }
    }

    protected void save_log()
    {
        //取得程式名稱
        string filename = System.IO.Path.GetFileName(Request.PhysicalPath).Replace(".aspx", "");

        //紀錄LOG
        string strClientIP = Context.Request.ServerVariables["REMOTE_ADDR"];
        tsconn.save_log("Guest", filename, strClientIP, "查詢:" + v_padate + "-" + v_machid);
    }
    protected void BindGrid(string pdate, string machid)
    {

        //Sql_str = "SELECT machineid,lot_no,ctrlot_no,proddate,prodtime_b,sf_get_cat_by_lotno(lot_no) as cat, sf_get_pdtsize_by_lotno(lot_no) as p_size, ";
        //Sql_str += "    CASE WHEN STATUS NOT IN ('G','D') AND  CAUSE_ID IS NOT NULL  ";
        //Sql_str += "    THEN  sf_get_value('CAUSES','CAUSE_NAME','REMARK', CAUSE_ID,'ROWNUM',1) ";
        //Sql_str += "    ELSE  ''   END   AS DOWN_CAUSE ";
        //Sql_str += " FROM machine_runs ";
        //Sql_str += " WHERE MACHINEID = '" + machid + "' ";
        //Sql_str += " AND     PRODDATE= '" + pdate + "' ";
        //Sql_str += " ORDER BY PRODTIME_B ";

        Sql_str = "SELECT   MACHSET AS MACHINEID ,TRIM(LOT_NO) AS LOT_NO, PRODDATE,PRODTIME AS PRODTIME_B, ";
        Sql_str += "             sf_get_cat_by_lotno(TRIM(lot_no)) as cat, sf_get_pdtsize_by_lotno(TRIM(lot_no)) as p_size, ";
        Sql_str += "             sf_get_value_nox('IPQC_REMARK','MARKC','REMARK', PS,'ROWNUM',1)  AS DOWN_CAUSE ";
        Sql_str += "FROM TQC2000.MEAS_4ALLMASTER ";
        Sql_str += "WHERE PRODDATE = '" + pdate + "' ";
        Sql_str += "AND      MACHSET = '" + machid + "' ";
        Sql_str += "ORDER BY LOT_NO,PRODTIME ";

        SqlDataSource1.SelectCommand = Sql_str;

        string strScript = "$(function () {$('#" + GridView1.ClientID + "').toSuperTable({ width: '800px', height: '100px', fixedCols: 1 }).find('tr:even').addClass('GridView_altRow');});";

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "GridView1", strScript, true);

    }
    //排序
    protected void GridView1_Sorted(object sender, EventArgs e)
    {
        BindGrid(v_padate, v_machid);
    }

    protected void BindGrid1(string machid)
    {
        //Sql_str = "SELECT   MACHSET,LOT_NO, PRODDATE,PRODTIME,HEAT_NO, OPERATOR,  WEIGHT,SWEIGHT,PS ";
        //Sql_str += "FROM TQC2000.MEAS_4ALLMASTER ";
        //Sql_str += "WHERE PRODDATE = '" + pdate + "' ";
        //Sql_str += "AND      MACHSET = '" + machid + "' ";
        //Sql_str += "ORDER BY LOT_NO,PRODTIME ";

        Sql_str = "SELECT DISTINCT LOT_NO,TOTALQTY,TOTAL_WT,CAT,PDT_SIZE,STARTDATE,STARTTIME,ENDDATE,MINDELIVERY,DRA_DRAW_ID ";
        Sql_str += "FROM ( ";
        Sql_str += "    SELECT A.LOT_NO,A.TOTALQTY,ROUND(C.STNDRD_WT*A.TOTALQTY/1000) AS TOTAL_WT, ";
        Sql_str += "                 B.CAT,sf_get_pdtsize(B.cat,B.dia,B.len,B.th) AS PDT_SIZE, ";
        Sql_str += "                A.STARTDATE,A.STARTTIME,A.ENDDATE,A.MINDELIVERY, C.DRA_DRAW_ID AS DRA_DRAW_ID ";
        Sql_str += "    FROM PRD_RUNS A, ITEMS  B,PRODUCTS C ";
        Sql_str += "    WHERE A.LOT_NO = B.LOT_NO ";
        Sql_str += "    AND    B.CAT = C.CAT_CAT  ";
        Sql_str += "    AND   B.DIA = C.DIA  ";
        Sql_str += "    AND B.LEN = C.LENGTH  ";
        Sql_str += "    AND B.TH=C.THREAD  ";
        Sql_str += "    AND   A.FORMER2 = '" + machid + "' ";
        Sql_str += "    AND   A.STARTDATE IS NOT NULL ";
        Sql_str += "    AND   TO_CHAR(A.STARTDATE,'YYYY/MM/DD') >= TO_CHAR(ADD_MONTHS(SYSDATE,-1),'YYYY/MM/DD') ";
        Sql_str += " )   ORDER BY ENDDATE DESC ,STARTDATE DESC , STARTTIME DESC ";

        SqlDataSource2.SelectCommand = Sql_str;

        string strScript = "$(function () {$('#" + GridView3.ClientID + "').toSuperTable({ width: '800px', height: '100px', fixedCols: 1 }).find('tr:even').addClass('GridView_altRow');});";

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "GridView3", strScript, true);

    }

    protected void BindGrid2(string machid)
    {
        //Sql_str = "SELECT   MACHSET,LOT_NO, PRODDATE,PRODTIME,HEAT_NO, OPERATOR,  WEIGHT,SWEIGHT,PS ";
        //Sql_str += "FROM TQC2000.MEAS_4ALLMASTER ";
        //Sql_str += "WHERE PRODDATE = '" + pdate + "' ";
        //Sql_str += "AND      MACHSET = '" + machid + "' ";
        //Sql_str += "ORDER BY LOT_NO,PRODTIME ";

        Sql_str = "SELECT LOT_NO, ";
        Sql_str += "            CASE WHEN (STARTDATE IS NULL) THEN TOTALQTY ELSE ";
        Sql_str += "            GREATEST(0,TOTALQTY-sf_get_forming_lot_pcs(LOT_NO))  END  ";
        Sql_str += "             AS C_TOTALQTY,CAT,PDT_SIZE,DRA_DRAW_ID, ";
        Sql_str += "            WIRE_DIA,RAWMTRL_RAWMTRL_ID,SPEED, ";
        Sql_str += "            CASE WHEN STARTDATE IS NULL AND (speed IS NOT NULL OR speed >0)   THEN  ";
        Sql_str += "            ROUND(TOTALQTY*1000 /  speed/60/9,1) ELSE  ";
        Sql_str += "            ROUND(GREATEST(0,TOTALQTY-sf_get_forming_lot_pcs(LOT_NO))*1000 / speed/60/9,1) END AS WDAY, ";
        Sql_str += "            MINDELIVERY,PRIORITY ";
        Sql_str += "FROM V_PRD_RUNS_SPEC   ";
        Sql_str += "WHERE  FORMER2 = '" + machid + "' ";
        Sql_str += "AND ENDDATE IS NULL ";
        Sql_str += "ORDER BY MINDELIVERY ";

        SqlDataSource3.SelectCommand = Sql_str;

        string strScript = "$(function () {$('#" + GridView2.ClientID + "').toSuperTable({ width: '800px', height: '100px', fixedCols: 1 }).find('tr:even').addClass('GridView_altRow');});";

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "GridView2", strScript, true);
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#ffff00'");

            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#ffff00'");

            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#ffff00'");

            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

        }
    }
}