using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;

public partial class GM_demoship : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();
    OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
    OracleDataReader dr;

    string Sql_str;
    static string v_currency;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TB_sdate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            TB_edate.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }
    }

    protected void Quy_btn_Click(object sender, EventArgs e)
    {
        QueryCust();
        QueryCustAmt();
        //將其他GridView清空
        DS_ordsum.SelectCommand = "";
        GridView3.DataBind();

        //L_amt.Text = "";
        //L_wt.Text = "";

        Panel1.Visible = false;
        TB_coname.Visible = false;

        GridView2.SelectedIndex = -1;

    }

    //客戶統計
    protected void QueryCust()
    {
        if ((TB_sdate.Text != "") & (TB_edate.Text != ""))
        {
            Sql_str = "SELECT cus_id co_id , CST_NAME , round(SUM(request*price),2) sum_amt ";
            Sql_str += "      , round(SUM(DECODE(unit,'M', nvl(v_convert,request*sf_get_stndrd_wt(odr_item_pdt_cat_cat,odr_item_pdt_dia,odr_item_pdt_length,odr_item_pdt_thread)) ,'KGS', request)) ,2) sum_nw ";
            Sql_str += " FROM v_chuhuodan ";
            Sql_str += "WHERE to_char(pac_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "' ";
            Sql_str += "GROUP BY cus_id,CST_NAME";

            DS_cust.SelectCommand = Sql_str;

        }
    }
    //客戶查詢
    protected void QueryCust(string v_currency)
    {
        Sql_str = " SELECT cus_id co_id, ";
        Sql_str += "       currency, ";
        Sql_str += "       SUM(amt) sum_amt, ";
        Sql_str += "       sum(nw) sum_nw ";
        Sql_str += " FROM  v_shp_sum ";
        Sql_str += "WHERE  to_char(closing,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "' ";
        Sql_str += "AND CURRENCY = '" + v_currency + "' ";
        Sql_str += "GROUP BY cus_id, CURRENCY ";
        Sql_str += "ORDER BY sum_amt  DESC ";

        DS_cust.SelectCommand = Sql_str;
        //GridView2.DataBind();
    }

    protected void QryOrdsum(string v_coid)
    {
        Sql_str = " SELECT co_id,pac_no,pac_date,sum_amt,sum_nw,odr_item_pdt_cat_cat,odr_item_pdt_dia ";
        Sql_str += "              ,odr_item_pdt_length ,odr_item_pdt_thread ,odr_item_pdt_fin_fin_id ,price ";
        Sql_str += "              ,ROUND(SUM_AMT/SUM_NW,4)  AS NTKG ";
        Sql_str += "              ,ROUND(SUM_AMT/QTY/1000) AS NTPCS ";
        Sql_str += "              ,keg,qty  , price_cat, cust_job_no,p_cat ";
        Sql_str += "FROM ( ";
        Sql_str += "    SELECT cus_id co_id,pac_no, pac_date ";
        Sql_str += "              , SUM( DECODE(UNIT,'M', PRICE /sf_get_product_weight('STANDARD',odr_item_pdt_cat_cat, ";
        Sql_str += "                odr_item_pdt_dia, odr_item_pdt_length,odr_item_pdt_thread) , 'KGS',PRICE ) ";
        Sql_str += "                    *DECODE(UNIT,'M',request*sf_get_product_weight('STANDARD',odr_item_pdt_cat_cat, ";
        Sql_str += "                odr_item_pdt_dia, odr_item_pdt_length,odr_item_pdt_thread) , 'KGS',request )) sum_amt ";
        Sql_str += "              ,SUM(DECODE(UNIT,'M',request*sf_get_product_weight('STANDARD',odr_item_pdt_cat_cat, ";
        Sql_str += "                odr_item_pdt_dia, odr_item_pdt_length,odr_item_pdt_thread), 'KGS',request )) sum_nw ";
        Sql_str += "              ,odr_item_pdt_cat_cat ";
        Sql_str += "              ,odr_item_pdt_dia ";
        Sql_str += "              ,odr_item_pdt_length ";
        Sql_str += "              ,odr_item_pdt_thread ";
        Sql_str += "              ,odr_item_pdt_fin_fin_id ";
        Sql_str += "              ,price,keg ";
        Sql_str += "              ,round(SUM(DECODE(unit,'KGS', nvl(v_convert,request/sf_get_stndrd_wt(odr_item_pdt_cat_cat,odr_item_pdt_dia,odr_item_pdt_length,odr_item_pdt_thread)) ,'M', request )),2) qty  ";
        Sql_str += "              , price_cat, cust_job_no, DECODE(PRICE_CAT ,'1','牌','2','專','3','議','')  AS P_CAT ";
        Sql_str += "         FROM v_chuhuodan ";
        Sql_str += "        WHERE to_char(pac_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "' ";
        Sql_str += "        AND cus_id = '" + v_coid + "' ";
        Sql_str += "        GROUP BY cus_id,pac_no,pac_date,odr_item_pdt_cat_cat,odr_item_pdt_dia,odr_item_pdt_length,odr_item_pdt_thread,odr_item_pdt_fin_fin_id, price,keg,qty, price_cat, cust_job_no ";
        Sql_str += "   ) ";

        DS_ordsum.SelectCommand = Sql_str;
    }

    //總金額
    protected void QueryCustAmt()
    {
        Sql_str = " SELECT  round(SUM(request*price),2) sum_amt ";
        Sql_str += "      , round(SUM(DECODE(unit,'M', nvl(v_convert,request*sf_get_stndrd_wt(odr_item_pdt_cat_cat,odr_item_pdt_dia,odr_item_pdt_length,odr_item_pdt_thread)),'KGS', request )) ,2) sum_nw ";
        Sql_str += " FROM v_chuhuodan ";
        Sql_str += "WHERE to_char(pac_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "'  ";



        //建立連線
        //使用web.config conn string
        conn.Open();
        OracleCommand cmd = new OracleCommand(Sql_str, conn);
        try
        {
            dr = cmd.ExecuteReader();

            if (!dr.Read())
            {
                dr.Close();
                conn.Close();
            }
            else
            {
                L_amt.Text = dr[0].ToString();  //總金額
                L_wt.Text = dr[1].ToString();   //總重量

                dr.Close();
                conn.Close();
            }
        }
        catch
        {
            dr.Close();
            conn.Close();
        }
    }

    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView2.SelectedIndex = e.NewSelectedIndex;
        GridView3.SelectedIndex = -1;

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
            QryOrdsum(L_coid.Text);
            //QueryCustName(L_coid.Text);

        }
    }
    protected void GridView3_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView3.SelectedIndex = e.NewSelectedIndex;
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label L_pono = (Label)GridView3.Rows[index].Cells[0].FindControl("L_pono");

        if (e.CommandName == "Select")
        {
            //QryOrder(L_pono.Text);
        }
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#DDDDDD'");

            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        }
    }

    
}