using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;

public partial class GM_exportorder2 : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();
    OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["JH815"].ConnectionString);
    OracleDataReader dr;

    string Sql_str, v_s1, v_s2;
    static string v_coid;
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
        //GridView3.DataBind();

        
        DS_order.SelectCommand = "";
        ListView1.DataBind();

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
            Sql_str = "SELECT O.currency currency  ";
            Sql_str += "        ,COUNT(*) PO_COUNT ";
            Sql_str += "        ,SUM(sk_get_po.amount@JH815(O.po_no,o.unit)/1000) CO_AMT  ";
            Sql_str += "        ,COUNT( DISTINCT O.CO_CO_ID )  CUS_COUNT   ";
            Sql_str += "   FROM ORDERS O";
            Sql_str += "  WHERE  to_char(o.bk_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "' ";
            Sql_str += "    AND O.inorext ='E'";
            Sql_str += "    AND O.eorl ='E'     ";
            Sql_str += "  GROUP BY o.currency";


            DS_curr.SelectCommand = Sql_str;

        }
    }
    //客戶查詢
    protected void QueryCust(string v_currency)
    {
        Sql_str = " SELECT o.CO_CO_ID   CO_ID  ";
        Sql_str += "        ,o.currency  currency  ";
        Sql_str += "        ,COUNT(*)    PO_COUNT  ";
        Sql_str += "        ,SUM(sk_get_po.amount@JH815(o.po_no,o.unit)/1000)     CO_AMT  ";
        Sql_str += "        ,SUM(the_total_order_weight_for@JH815(o.po_no)/1000)  CUS_WT  ";
        Sql_str += "   FROM ORDERS o  ";
        Sql_str += "  WHERE to_char(o.bk_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "'  ";
        Sql_str += "    AND o.inorext ='E'  ";
        Sql_str += "    AND o.eorl ='E'  ";
        Sql_str += "    AND o.currency = '" + v_currency + "'  ";
        Sql_str += "  GROUP BY o.co_co_id,o.currency  ";
        Sql_str += " ORDER BY 4  DESC ";
        DS_cust.SelectCommand = Sql_str;
        //GridView2.DataBind();
    }

    protected void QueryCustAmt(string v_currency)
    {
        //Sql_str = " SELECT   SUM(sk_get_po.amount(o.po_no,o.unit)/1000)     CO_AMT  ";
        //Sql_str += "        ,ROUND(SUM(the_total_order_weight_for(o.po_no)/1000),0)  CUS_WT  ";
        //Sql_str += "   FROM ORDERS o  ";
        //Sql_str += "  WHERE to_char(o.bk_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "'  ";
        //Sql_str += "    AND o.inorext ='E'  ";
        //Sql_str += "    AND o.eorl ='E'  ";
        //Sql_str += "    AND o.currency = '" + v_currency + "'  ";

        Sql_str = "SELECT ROUND(SUM(sk_get_po.amount@JH815(o.po_no,o.unit)*O.RATE/1000 ),0) AS TTLAMT,  ";
        Sql_str += "            ROUND(SUM(the_total_order_weight_for@JH815(o.po_no)/1000),0) AS TTLWT  ";
        Sql_str += "  FROM ORDERS o  ";
        Sql_str += " WHERE ( to_char(o.bk_date,'yyyy/mm/dd') BETWEEN  '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "'    )  ";
        Sql_str += "AND o.inorext ='E'  ";
        Sql_str += "AND o.eorl ='E'   ";

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
        Sql_str = "SELECT o.CO_CO_ID  CO_ID ";
        Sql_str += ",O.PO_NO     PO_NO ";
        Sql_str += ",O.TERM      TERM ";
        Sql_str += ",O.TERM1     TERM1 ";
        Sql_str += ",O.RATE      RATE ";
        Sql_str += ",o.currency  currency ";
        Sql_str += ",o.confirmed confirmed ";
        Sql_str += ",o.canceled  canceled ";
        Sql_str += ",o.inorext   inorext ";
        Sql_str += ",the_total_order_weight_for@JH815(o.po_no)  PO_WT ";
        Sql_str += ",sk_get_po.amount@JH815(o.po_no,o.unit)     PO_AMT ";
        Sql_str += "  FROM ORDERS o ";
        Sql_str += "  WHERE to_char(o.bk_date,'yyyy/mm/dd')   BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "'   ";
        Sql_str += "  AND o.inorext ='E' ";
        Sql_str += "  AND o.eorl ='E' ";
        Sql_str += "  AND o.CO_CO_ID ='" + v_co_id + "' ";
        Sql_str += "  ORDER BY PO_NO ";
        DS_ordsum.SelectCommand = Sql_str;
    }
    //查詢客戶名稱
    protected void QueryTTLWT(string v_co_id)
    {
        L_ttlwt.Visible = true;

        Sql_str = " SELECT ROUND(SUM(the_total_order_weight_for@JH815(o.po_no))) AS  PO_WT  ";
        Sql_str += " FROM ORDERS o ";
        Sql_str += "WHERE to_char(o.bk_date,'yyyy/mm/dd')   BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "' ";
        Sql_str += "  AND o.inorext ='E' ";
        Sql_str += "  AND o.eorl ='E' ";
        Sql_str += "  AND o.CO_CO_ID ='" + v_co_id + "' ";


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

                L_ttlwt.Text = String.Format("{0:N0}", dr[0]);   //總重量

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

    //訂單查詢
    protected void QryOrder(string v_pono, string v_rate, string v_term)
    {
        v_s1 = "0";
        v_s2 = "0";

        if ((v_term == "CIF") | (v_term == "C&F"))
        {
            Sql_str = "SELECT NVL(SUM(o.FREIGHT),0)  ";
            Sql_str += "FROM PORT P,ORDERS O ";
            Sql_str += "WHERE P.PORT_ID=o.PORT ";
            Sql_str += "  AND O.PO_NO = '" + v_pono + "' ";
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
                    v_s2 = dr[0].ToString();  //S2
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

        Sql_str = "SELECT   A.PDT_CAT_CAT, ";
        Sql_str += "        SF_GET_PDTSIZE@JH815(A.pdt_cat_cat,A.pdt_dia,A.pdt_length,A.pdt_thread) AS PDT_SIZE , ";
        Sql_str += "        A.PDT_FIN_FIN_ID,A.PACK_CODE,A.PCS, ";
        Sql_str += "        SF_GET_STNDRD_WT@JH815(A.pdt_cat_cat,A.pdt_dia,A.pdt_length,A.pdt_thread) AS STD_WT, ";
        Sql_str += "        SF_GET_STNDRD_WT@JH815(A.pdt_cat_cat,A.pdt_dia,A.pdt_length,A.pdt_thread)*A.PCS AS TTL_WT, ";
        Sql_str += "        A.PRICE, ";
        Sql_str += "        CASE WHEN (sf_get_value@JH815('orders','unit','po_no',A.odr_po_no)='KG') THEN  ";
        Sql_str += "        ROUND((A.PRICE*to_number(" + v_rate + ")-" + v_s1 + "-" + v_s2 + " *SF_GET_VALUE@JH815('O_P_PARAM','VALUE','NAME','US_CURRENCY_RATE','STATUS','ACTIVE')/1000) * 1,2) ";
        Sql_str += "        ELSE ";
        Sql_str += "          ROUND((A.PRICE*to_number(" + v_rate + ")/SF_GET_STNDRD_WT@JH815(A.pdt_cat_cat,A.pdt_dia,A.pdt_length,A.pdt_thread)-" + v_s1 + "-" + v_s2 + "*SF_GET_VALUE@JH815('O_P_PARAM','VALUE','NAME','US_CURRENCY_RATE','STATUS','ACTIVE')/1000) * 1,2) ";
        Sql_str += "        END AS NTKGPRICE ";
        Sql_str += "FROM   orderitems A ";
        Sql_str += "WHERE ODR_PO_NO = '" + v_pono + "' ";
        Sql_str += "ORDER BY A.pdt_cat_cat,A.pdt_dia,A.pdt_length,A.pdt_thread,A.pdt_fin_fin_id ";

        DS_order.SelectCommand = Sql_str;
    }

    //訂單查詢
    protected void QryOrder2(string v_pono, string v_rate, string v_term)
    {
        v_s1 = "0";
        v_s2 = "0";

        if ((v_term == "CIF") | (v_term == "C&F"))
        {
            Sql_str = "SELECT NVL(SUM(o.FREIGHT),0)  ";
            Sql_str += "FROM PORT P,ORDERS O ";
            Sql_str += "WHERE P.PORT_ID=o.PORT ";
            Sql_str += "  AND O.PO_NO = '" + v_pono + "' ";
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
                    v_s2 = dr[0].ToString();  //S2
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

        Sql_str = "SELECT   A.PDT_CAT_CAT, ";
        Sql_str += "        SF_GET_PDTSIZE@JH815(A.pdt_cat_cat,A.pdt_dia,A.pdt_length,A.pdt_thread) AS PDT_SIZE , ";
        Sql_str += "        A.PDT_FIN_FIN_ID,A.PACK_CODE,A.PCS, ";
        Sql_str += "        SF_GET_STNDRD_WT@JH815(A.pdt_cat_cat,A.pdt_dia,A.pdt_length,A.pdt_thread) AS STD_WT, ";
        Sql_str += "        SF_GET_STNDRD_WT@JH815(A.pdt_cat_cat,A.pdt_dia,A.pdt_length,A.pdt_thread)*A.PCS AS TTL_WT, ";
        Sql_str += "        A.PRICE, ";
        Sql_str += "        CASE WHEN (sf_get_value@JH815('orders','unit','po_no',A.odr_po_no)='KG') THEN  ";
        Sql_str += "        ROUND((A.PRICE*to_number(" + v_rate + ")-" + v_s1 + "-" + v_s2 + " *SF_GET_VALUE@JH815('O_P_PARAM','VALUE','NAME','US_CURRENCY_RATE','STATUS','ACTIVE')/1000) * 1,2) ";
        Sql_str += "        ELSE ";
        Sql_str += "          ROUND((A.PRICE*to_number(" + v_rate + ")/SF_GET_STNDRD_WT@JH815(A.pdt_cat_cat,A.pdt_dia,A.pdt_length,A.pdt_thread)-" + v_s1 + "-" + v_s2 + "*SF_GET_VALUE@JH815('O_P_PARAM','VALUE','NAME','US_CURRENCY_RATE','STATUS','ACTIVE')/1000) * 1,2) ";
        Sql_str += "        END AS NTKGPRICE ";
        Sql_str += "FROM   orderitems A ";
        Sql_str += "WHERE ODR_PO_NO = '" + v_pono + "' ";
        Sql_str += "ORDER BY A.pdt_cat_cat,A.pdt_dia,A.pdt_length,A.pdt_thread,A.pdt_fin_fin_id ";

        DS_order.SelectCommand = Sql_str;
    }
    //規格查詢
    protected void QryDescript(string v_cat)
    {

        Sql_str = "SELECT REPLACE(REPLACE(REPLACE(DESCRPT_C, CHR(10)), CHR(13)), CHR(9))  DESCRPT_C  ";
        Sql_str += "  FROM categories ";
        Sql_str += " WHERE CAT = '" + v_cat + "' ";

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
                //TB_desc.Text = dr[0].ToString();  //規格
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "alert('" + dr[0].ToString() + "');", true);
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
        //GridView3.SelectedIndex = -1;
        ListView1.SelectedIndex = -1;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label L_currency = (Label)GridView1.Rows[index].Cells[0].FindControl("L_currency");

        if (e.CommandName == "Select")
        {
            QueryCust(L_currency.Text);
            QueryCustAmt(L_currency.Text);
            Panel1.Visible = true;

            DS_ordsum.SelectCommand = "";
            //GridView3.DataBind();

            DS_order.SelectCommand = "";
            ListView1.DataBind();


        }

        TB_coname.Text = "";
        L_ttlwt.Text = "";
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView2.SelectedIndex = e.NewSelectedIndex;
        //GridView3.SelectedIndex = -1;
        ListView1.SelectedIndex = -1;

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label L_coid = (Label)GridView2.Rows[index].Cells[0].FindControl("L_coid");

        v_coid =  L_coid.Text;

        if (e.CommandName == "Select")
        {
            QryOrdsum(L_coid.Text);
            QueryCustName(L_coid.Text);
            QueryTTLWT(L_coid.Text);
            DS_order.SelectCommand = "";
            //ListView1.DataBind();
        }
    }
    protected void GridView3_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //GridView3.SelectedIndex = e.NewSelectedIndex;
        ListView1.SelectedIndex = -1;

    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        //Label L_pono = (Label)GridView3.Rows[index].Cells[0].FindControl("L_pono");
        //Label L_rate = (Label)GridView3.Rows[index].Cells[1].FindControl("L_rate");
        //Label L_term = (Label)GridView3.Rows[index].Cells[6].FindControl("L_term");

        if (e.CommandName == "Select")
        {
            //QryOrder(L_pono.Text, L_rate.Text, L_term.Text);
        }
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#DDDDDD'");

            //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

            //商品編號
            Label v_L_confirm = (Label)e.Row.Cells[0].FindControl("L_confirm");
            Label v_L_cancel = (Label)e.Row.Cells[0].FindControl("L_cancel");
            Image v_Img_Y = (Image)e.Row.Cells[0].FindControl("Img_Y");
            Image v_Img_N = (Image)e.Row.Cells[0].FindControl("Img_N");

            if (v_L_confirm.Text == "Y")
            {
                v_Img_Y.ImageUrl = "images/accept.png";
            }
            else if (v_L_confirm.Text == "N")
            {
                v_Img_Y.ImageUrl = "images/cross.png";
            }
            else
            {
                v_Img_Y.Visible = false;
            }

            if (v_L_cancel.Text == "Y")
            {
                v_Img_N.ImageUrl = "images/accept.png";
            }
            else if (v_L_cancel.Text == "N")
            {
                v_Img_N.ImageUrl = "images/cross.png";
            }
            else
            {
                v_Img_N.Visible = false;
            }

        }
    }

    protected void GridView5_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView5.SelectedIndex = e.NewSelectedIndex;
    }
    protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label v_L_cat = (Label)GridView5.Rows[index].Cells[0].FindControl("L_cat");

        if (e.CommandName == "Select")
        {
            QryDescript(v_L_cat.Text);
        }
    }

    protected void ListView1_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        ListView1.SelectedIndex = e.NewSelectedIndex;
        QryOrdsum(v_coid);
    }

    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        //int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        int index = ListView1.SelectedIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        //Label L_pono = (Label)ListView1.Items..Rows[index].Cells[0].FindControl("L_pono");
        //Label L_rate = (Label)ListView1.Rows[index].Cells[1].FindControl("L_rate");
        //Label L_term = (Label)ListView1.Rows[index].Cells[6].FindControl("L_term");

        //Label L_pono = (Label)ListView1.Controls[0].FindControl("L_pono");
        //Label L_rate = (Label)ListView1.Controls[0].FindControl("L_rate");
        //Label L_term = (Label)ListView1.Controls[0].FindControl("L_term");
        Label L_pono = (Label)e.Item.FindControl("L_pono");
        Label L_rate = (Label)e.Item.FindControl("L_rate");
        Label L_term = (Label)e.Item.FindControl("L_term");

        if (e.CommandName == "Select")
        {
            QryOrder(L_pono.Text, L_rate.Text, L_term.Text);
        }
        else if (e.CommandName == "Disable")
        {
            ListView1.SelectedIndex =-1;
            QryOrdsum(v_coid);
        }
    }
    protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        QryOrdsum(v_coid);
    }
    protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {

            //e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#DDDDDD'");

            //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

            //商品編號
            Label v_L_confirm = (Label)e.Item.FindControl("L_confirm");
            Label v_L_cancel = (Label)e.Item.FindControl("L_cancel");
            Image v_Img_Y = (Image)e.Item.FindControl("Img_Y");
            Image v_Img_N = (Image)e.Item.FindControl("Img_N");

            if (v_L_confirm.Text == "Y")
            {
                v_Img_Y.ImageUrl = "images/accept.png";
            }
            else if (v_L_confirm.Text == "N")
            {
                v_Img_Y.ImageUrl = "images/cross.png";
            }
            else
            {
                v_Img_Y.Visible = false;
            }

            if (v_L_cancel.Text == "Y")
            {
                v_Img_N.ImageUrl = "images/accept.png";
            }
            else if (v_L_cancel.Text == "N")
            {
                v_Img_N.ImageUrl = "images/cross.png";
            }
            else
            {
                v_Img_N.Visible = false;
            }

        }
    }
}