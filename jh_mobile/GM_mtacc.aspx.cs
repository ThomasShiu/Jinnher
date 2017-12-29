using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class GM_mtacc : System.Web.UI.Page
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
            save_log("物料領用統計");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        v_ym = TB_year.Text + "/" + DDL_month.SelectedValue;

        //Sql_str = " SELECT A_DATE AS 日期,ACC_ID AS 領料單號,D_CAT AS 代號,DIE_NAME AS 分類大項,DIE_SIZE AS 名稱, ";
        //Sql_str += "            PCS AS 數量,PRICE AS 單價,TTLP AS 總價,CO_NAME AS 廠商,USED_EMP AS 領用人姓名,USED_DEPT AS 所在單位, ";
        //Sql_str += "            USED_MAC_NUM AS 機台號碼,TO_NUMBER(DIE_SN) AS 牙板序號,USED_ID AS 參考  ";
        //Sql_str += "FROM MT_ACC ";
        //Sql_str += "WHERE TO_CHAR( a_date, 'yyyy/mm') ='" + v_ym + "' ";
        //Sql_str += "ORDER BY USED_DEPT,A_DATE,ACC_ID ";

        Sql_str = "SELECT A_DATE AS 日期,ACC_ID AS 領料單號,D_CAT AS 代號,DIE_NAME AS 分類大項,DIE_SIZE AS 名稱, ";
        Sql_str += "            PCS AS 數量,PRICE AS 單價,TTLP AS 總價,CO_NAME AS 廠商,USED_EMP AS 領用人姓名,USED_DEPT AS 所在單位, ";
        Sql_str += "            USED_MAC_NUM AS 機台號碼,DIE_SN AS 牙板序號,USED_ID AS 參考        ";
        Sql_str += "FROM (  ";
        Sql_str += "SELECT b.USED_APP_DATE a_date, b.ACC_ID,  ";
        Sql_str += "          sf_get_value ('dm_die1','dm_die1_name','dm_die1_id',d.DM_DIE1_ID) d_cat,   ";
        Sql_str += "          C.DIE_NAME,  ";
        Sql_str += "          C.DIE_SIZE,   ";
        Sql_str += "          B.USED_COUNT AS PCS,  ";
        Sql_str += "          NVL (b.PRICE, C.DIE_PRICE) price, ";
        Sql_str += "          B.USED_COUNT * NVL (b.PRICE, C.DIE_PRICE) ttlp, ";
        Sql_str += "          sf_get_value ('companies','co_abbr','co_id',e.FACT_NUM) co_name, ";
        Sql_str += "          B.USED_EMP, ";
        Sql_str += "          B.USED_DEPT, ";
        Sql_str += "          B.USED_MAC_NUM, ";
        Sql_str += "          NULL die_sn, ";
        Sql_str += "          b.USED_ID   ";
        Sql_str += "     FROM DIE_USED B, ";
        Sql_str += "          DIE_PABULUM C, ";
        Sql_str += "          DM_DIES d, ";
        Sql_str += "          DIE_STORAGE e ";
        Sql_str += "    WHERE     TO_CHAR( b.USED_APP_DATE, 'yyyy/mm') ='" + v_ym + "'  ";
        Sql_str += "          AND b.DIE_NUM = c.DIE_NUM ";
        Sql_str += "          AND B.USED_STAT = '15' ";
        Sql_str += "          AND c.DIE_ID = d.DM_DIES_CODE ";
        Sql_str += "          AND b.DIE_NUM = e.DIE_STOR_DIE_NUM ";
        Sql_str += "          AND d.DM_DIE1_ID NOT IN (1, 7)  ";
        Sql_str += "   UNION ALL ";
        Sql_str += "   SELECT u.unuse_date a_date,  ";
        Sql_str += "          '' ACC_ID,  ";
        Sql_str += "          '一般模具類' d_cat,  ";
        Sql_str += "          '牙板' DIE_NAME, ";
        Sql_str += "          C.DIE_SIZE, ";
        Sql_str += "          u.UNUSE_COUNT AS PCS, ";
        Sql_str += "          NVL (e.STOR_PRICE, C.DIE_PRICE) price, ";
        Sql_str += "          u.UNUSE_COUNT * NVL (e.STOR_PRICE, C.DIE_PRICE) ttlp, ";
        Sql_str += "          sf_get_value ('companies','co_abbr','co_id',u.FACT_NUM)  co_name, ";
        Sql_str += "          u.UNUSER USED_EMP, ";
        Sql_str += "          u.UNUSE_MAC USED_DEPT,  ";
        Sql_str += "          SF_GET_MTM_USEMAC (u.DIE_NUM,u.DIE_SN, u.UNUSER,u.UNUSE_MAC) USED_MAC_NUM, ";
        Sql_str += "            TO_CHAR(u.DIE_SN) die_sn, ";
        Sql_str += "            '' USED_ID ";
        Sql_str += "     FROM DIE_PABULUM C, DIE_STORAGE e, DIE_UNUSABLE u ";
        Sql_str += "    WHERE     TO_CHAR( u.unuse_date, 'yyyy/mm') ='" + v_ym + "' ";
        Sql_str += "          AND u.DIE_NUM = e.DIE_STOR_DIE_NUM ";
        Sql_str += "          AND u.DIE_SN = e.DIE_STOR_SN ";
        Sql_str += "          AND e.DIE_STOR_USEDSTAT = 'D'    ";
        Sql_str += "          AND u.DIE_NUM = c.DIE_NUM  ) ";
        Sql_str += "ORDER BY USED_DEPT,A_DATE,ACC_ID ";

        try
        {
            message.Text = "";
            SqlDataSource1.SelectCommand = Sql_str;
            //ReportViewer1.LocalReport.ReportPath = "report/R_BBI_TO_SHIP2.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_mtacc1", SqlDataSource1));
            ReportViewer1.LocalReport.Refresh();
            save_log("物料領用統計");
        }
        catch (Exception ex)
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
    
}