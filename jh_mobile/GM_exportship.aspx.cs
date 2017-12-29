using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;

public partial class GM_exportship : System.Web.UI.Page
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
        QueryCurry();

        //將其他GridView清空
        DS_cust.SelectCommand = "";
        GridView2.DataBind();

        DS_ordsum.SelectCommand = "";
        GridView3.DataBind();

        L_amt.Text = "";
        L_wt.Text = "";

        Panel1.Visible = false;
        TB_coname.Visible = false;

        GridView1.SelectedIndex = -1;

    }

    //幣別查詢
    protected void QueryCurry()
    {
        if ((TB_sdate.Text != "") & (TB_edate.Text != ""))
        {
            Sql_str = "SELECT currency, ";
            Sql_str += "      SUM(amt) sum_amt, ";
            Sql_str += "      sum(nw) sum_nw ";
            Sql_str += " FROM v_shp_sum ";
            Sql_str += "WHERE to_char(closing,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "'  ";
            Sql_str += "GROUP BY CURRENCY";


            DS_curr.SelectCommand = Sql_str;

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
        Sql_str += "ORDER BY sum_amt  DESC";

        DS_cust.SelectCommand = Sql_str;
        //GridView2.DataBind();
    }

    //總金額
    protected void QueryCustAmt(string v_currency)
    {
        //Sql_str = " SELECT   SUM(sk_get_po.amount(o.po_no,o.unit)/1000)     CO_AMT  ";
        //Sql_str += "        ,ROUND(SUM(the_total_order_weight_for(o.po_no)/1000),0)  CUS_WT  ";
        //Sql_str += "   FROM ORDERS o  ";
        //Sql_str += "  WHERE to_char(o.bk_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "'  ";
        //Sql_str += "    AND o.inorext ='E'  ";
        //Sql_str += "    AND o.eorl ='E'  ";
        //Sql_str += "    AND o.currency = '" + v_currency + "'  ";

        

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

    //訂單小計
    protected void QryOrdsum(string v_co_id)
    {
        Sql_str = "SELECT pac_no  ,  ";
        Sql_str += "          closing ,  ";
        Sql_str += "          cus_id co_id,  ";
        Sql_str += "          currency,  ";
        Sql_str += "          SUM(amt) sum_amt,  ";
        Sql_str += "          sum(nw) sum_nw  ";
        Sql_str += "     FROM v_shp_sum  ";
        Sql_str += "    WHERE to_char(closing,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "'   ";
        Sql_str += "    AND CUS_ID = '" + v_co_id + "'  ";
        Sql_str += "    AND CURRENCY = '" + v_currency + "'  ";
        Sql_str += "    GROUP BY pac_no,closing , cus_id, CURRENCY  ";
        Sql_str += "    ORDER BY closing,sum_amt ";
        DS_ordsum.SelectCommand = Sql_str;
    }

    //查詢客戶名稱
    protected void QueryCustName(string v_co_id)
    {
        TB_coname.Visible = true;

        Sql_str = "SELECT co_name  ";
        Sql_str += " FROM companies ";
        Sql_str += "WHERE co_id= '" + v_co_id + "' ";
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
                TB_coname.Text = dr[0].ToString();  //公司名稱
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



    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView1.SelectedIndex = e.NewSelectedIndex;
        GridView2.SelectedIndex = -1;
        GridView3.SelectedIndex = -1;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label L_currency = (Label)GridView1.Rows[index].Cells[0].FindControl("L_currency");
        v_currency = L_currency.Text;

        if (e.CommandName == "Select")
        {
            QueryCust(L_currency.Text);
            //QueryCustAmt(L_currency.Text);
            //Panel1.Visible = true;

            DS_ordsum.SelectCommand = "";
            GridView3.DataBind();
        }

        TB_coname.Text = "";
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
            QueryCustName(L_coid.Text);

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