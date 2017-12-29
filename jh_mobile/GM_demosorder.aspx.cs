using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;

public partial class GM_demosorder : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();
    OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
    OracleDataReader dr;

    string Sql_str;
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
        QueryCust();
        QueryCustAmt();
        //將其他GridView清空
        //DS_cust.SelectCommand = "";
        //GridView2.DataBind();

        DS_order.SelectCommand = "";
        GridView3.DataBind();

        DS_ordsum.SelectCommand = "";
        GridView4.DataBind();

        //L_amt.Text = "";
        //L_wt.Text = "";

        Panel1.Visible = true;
        TB_coname.Visible = false;

        GridView1.SelectedIndex = -1;
        GridView2.SelectedIndex = -1;

    }

    //幣別查詢
    protected void QueryCurry()
    {
        if ((TB_sdate.Text != "") & (TB_edate.Text != ""))
        {
            Sql_str = "SELECT SUM(po_count) po_count,SUM(co_amt) co_amt,SUM(cus_count) cus_count ";
            Sql_str += "  FROM (SELECT COUNT(*) PO_COUNT ";
            Sql_str += "               ,SUM(sk_get_local_po.amount(o.po_no,'M')/1000) CO_AMT ";
            Sql_str += "               ,COUNT( DISTINCT o.co_id )  CUS_COUNT       ";                  
            Sql_str += "          FROM local_orders o ";
            Sql_str += "         WHERE to_char(o.bk_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "'  ";
            Sql_str += "        UNION ALL ";
            Sql_str += "        SELECT COUNT(*) PO_COUNT ";
            Sql_str += "               ,SUM(sk_get_local_petition.amount(m.petition_no)/1000) co_amt ";
            Sql_str += "               ,COUNT(DISTINCT m.co_id) CUS_COUNT ";
            Sql_str += "          FROM local_petition_m m ";
            Sql_str += "         WHERE to_char(m.bk_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "'  ";
            Sql_str += "       )";

            DS_curr.SelectCommand = Sql_str;

        }
    }
    //客戶查詢
    protected void QueryCust()
    {
        Sql_str = "  SELECT co_id, SUM(po_count) po_count, SUM(co_amt) co_amt, SUM(cus_wt) cus_wt ";
        Sql_str += "  FROM (SELECT o.co_id   CO_ID ";
        Sql_str += "               ,COUNT(*)    PO_COUNT ";
        Sql_str += "               ,NVL(SUM(sk_get_local_po.amount(o.po_no,'M')),0)    CO_AMT ";
        Sql_str += "               ,SUM(the_ttl_local_order_weight_for(o.po_no))  CUS_WT ";
        Sql_str += "          FROM local_orders o ";
        Sql_str += "         WHERE to_char(o.bk_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "'  ";
        Sql_str += "         GROUP BY o.co_id ";
        Sql_str += "        UNION ALL ";
        Sql_str += "        SELECT m.co_id   CO_ID ";
        Sql_str += "               ,COUNT(*)    PO_COUNT ";
        Sql_str += "               ,NVL(SUM(sk_get_local_petition.amount(m.petition_no)),0)    CO_AMT ";
        Sql_str += "               ,SUM(the_ttl_local_petition_weight(m.petition_no)) CUS_WT ";
        Sql_str += "          FROM local_petition_m m ";
        Sql_str += "         WHERE to_char(m.bk_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "'  ";
        Sql_str += "         GROUP BY m.co_id ";
        Sql_str += "       ) ";
        Sql_str += " GROUP BY co_id ";
        Sql_str += " ORDER BY co_amt  DESC";

        DS_cust.SelectCommand = Sql_str;
        //GridView2.DataBind();
    }

    protected void QueryCustAmt()
    {
        //Sql_str = " SELECT   SUM(sk_get_po.amount(o.po_no,o.unit)/1000)     CO_AMT  ";
        //Sql_str += "        ,ROUND(SUM(the_total_order_weight_for(o.po_no)/1000),0)  CUS_WT  ";
        //Sql_str += "   FROM ORDERS o  ";
        //Sql_str += "  WHERE to_char(o.bk_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "'  ";
        //Sql_str += "    AND o.inorext ='E'  ";
        //Sql_str += "    AND o.eorl ='E'  ";
        //Sql_str += "    AND o.currency = '" + v_currency + "'  ";

        Sql_str = " SELECT  ROUND(SUM(sk_get_local_po.amount(o.po_no,'M')/1000 ),0) AS TTLAMT, ";
        Sql_str += "         ROUND(SUM(the_ttl_local_order_weight_for(o.po_no)/1000)*1000,0) AS TTLWT ";
        Sql_str += "  FROM local_orders o ";
        Sql_str += " WHERE to_char(o.bk_date,'yyyy/mm/dd') BETWEEN   '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "'  ";

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
        Sql_str = "SELECT o.co_id      CO_ID ";
        Sql_str += "       ,O.PO_NO     PO_NO ";
        Sql_str += "       ,O.TERM      TERM ";
        Sql_str += "       ,O.TERM1     TERM1 ";
        Sql_str += "       ,o.confirmed confirmed ";
        Sql_str += "       ,o.canceled  canceled ";
        Sql_str += "       ,the_ttl_local_order_weight_for(o.po_no)  PO_WT ";
        Sql_str += "       ,sk_get_local_po.amount(o.po_no,'M')     PO_AMT ";
        Sql_str += " FROM Local_Orders o ";
        Sql_str += "WHERE to_char(o.bk_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "' ";
        Sql_str += "AND o.co_id ='" + v_co_id + "' ";
        Sql_str += "UNION ALL ";
        Sql_str += "SELECT m.co_id ";
        Sql_str += "       ,m.petition_no   po_no ";
        Sql_str += "       ,m.term ";
        Sql_str += "       ,m.term1 ";
        Sql_str += "       ,m.confirmed ";
        Sql_str += "       ,m.canceled ";
        Sql_str += "       ,the_ttl_local_petition_weight(m.petition_no) po_wt ";
        Sql_str += "       ,sk_get_local_petition.amount(m.petition_no)         po_amt ";
        Sql_str += "  FROM local_petition_m m ";
        Sql_str += " WHERE to_char(m.bk_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "' ";
        Sql_str += " AND m.co_id ='" + v_co_id + "'  ";
        Sql_str += "  ORDER BY PO_NO";

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

    //查詢總重量
    protected void QueryTTLWT(string v_co_id)
    {
        L_ttlwt.Visible = true;
        Sql_str = "SELECT SUM(PO_WT) FROM ( ";
        Sql_str += "     SELECT  SUM(the_ttl_local_order_weight_for(o.po_no))  PO_WT ";
        Sql_str += "      FROM Local_Orders o ";
        Sql_str += "     WHERE to_char(o.bk_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "' ";
        Sql_str += "     AND o.co_id ='" + v_co_id + "' ";
        Sql_str += "    UNION ALL ";
        Sql_str += "    SELECT SUM(the_ttl_local_petition_weight(m.petition_no)) po_wt ";
        Sql_str += "     FROM local_petition_m m ";
        Sql_str += "     WHERE to_char(m.bk_date,'yyyy/mm/dd') BETWEEN '" + TB_sdate.Text + "' AND '" + TB_edate.Text + "' ";
        Sql_str += "     AND m.co_id ='" + v_co_id + "'  ";
        Sql_str += ") ";

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
                L_ttlwt.Text = String.Format("{0:N0}", dr[0]);  //總重量
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
    protected void QryOrder(string v_pono, string v_coid)
    {

        Sql_str = " SELECT PO_NO,CAT,PDT_SIZE,DIA,LENGTH,THREAD,FIN_ID,REQUEST,PACK_CODE,UNIT,PRICE,CONVERT,STAND_WT,ODR_PCS,TTLWT,PCSPRICE,NTKGPRICE  ";
        Sql_str += "             ,STNDRD_WT_PRICE,STNDRD_PCS_PRICE ";
        Sql_str += "             ,CASE WHEN (STNDRD_PCS_PRICE IS NOT NULL AND unit = 'M') THEN ROUND((PCSPRICE -STNDRD_PCS_PRICE)/STNDRD_PCS_PRICE*100,1) ";
        Sql_str += "              WHEN (STNDRD_WT_PRICE IS NOT NULL AND unit IN ('KG','KGS') ) THEN ROUND((NTKGPRICE - STNDRD_WT_PRICE)/STNDRD_WT_PRICE*100,1) ";
        Sql_str += "              END AS PRICE_RATIO ";
        Sql_str += " FROM  ";
        Sql_str += " (  ";
        Sql_str += "     SELECT PO_NO,CAT,PDT_SIZE,DIA,LENGTH,THREAD,FIN_ID,REQUEST,PACK_CODE,UNIT,PRICE,CONVERT,STAND_WT,ODR_PCS,TTLWT,PCSPRICE,NTKGPRICE ";
        Sql_str += "                 ,CASE WHEN (PO_NO LIKE  '1%') THEN NTKGPRICE  ";
        Sql_str += "                  ELSE TO_NUMBER(sf_get_value_nox2('local_cust_qties','wt_price','co_id', '" + v_coid + "','cat', cat,'dia', dia,'length',length,'thread',thread,'fin_id',fin_id,'pack_code',pack_code,'rownum',1)) ";
        Sql_str += "                  END AS STNDRD_WT_PRICE ";
        Sql_str += "                , CASE WHEN (PO_NO LIKE  '1%') THEN PCSPRICE   ";
        Sql_str += "                  ELSE TO_NUMBER(sf_get_value_nox2('local_cust_qties','pcs_price','co_id','" + v_coid + "','cat',cat,'dia',dia,'length',length,'thread',thread,'fin_id',fin_id,'pack_code',pack_code,'rownum',1)) ";
        Sql_str += "                  END AS STNDRD_PCS_PRICE ";
        Sql_str += "     FROM  ";
        Sql_str += "      ( ";
        Sql_str += "             SELECT CAT,sf_get_pdtsize( cat, dia, length, thread) AS PDT_SIZE,DIA,LENGTH,THREAD,FIN_ID,REQUEST,PACK_CODE,UNIT,PRICE,CONVERT,sf_get_stndrd_wt(CAT,DIA,LENGTH,THREAD) AS STAND_WT ";
        Sql_str += "                         ,CASE WHEN (UNIT = 'M') THEN REQUEST    ";
        Sql_str += "                          WHEN   (sf_get_stndrd_wt(CAT,DIA,LENGTH,THREAD) > 0)   THEN (ROUND(REQUEST/sf_get_stndrd_wt(CAT,DIA,LENGTH,THREAD),4))  ";
        Sql_str += "                          ELSE 0  END AS ODR_PCS ";
        Sql_str += "                         ,CASE WHEN (UNIT = 'M') THEN sf_get_stndrd_wt(CAT,DIA,LENGTH,THREAD)*REQUEST    ";
        Sql_str += "                          WHEN   (sf_get_stndrd_wt(CAT,DIA,LENGTH,THREAD) > 0)   THEN REQUEST ";
        Sql_str += "                          ELSE 0  END AS TTLWT ";
        Sql_str += "                          ,CASE WHEN (sf_get_value_nox('products','weight','cat_cat', CAT, 'dia', dia, 'length',length, 'thread', thread ) IS NOT NULL AND UNIT = 'M') THEN ROUND(price * 1,2)  ";
        Sql_str += "                           WHEN (sf_get_value_nox('products','weight','cat_cat', CAT, 'dia', dia, 'length',length, 'thread', thread ) IS NOT NULL AND UNIT <> 'M')  THEN  ";
        Sql_str += "                           ROUND( price*sf_get_stndrd_wt(CAT,DIA,LENGTH,THREAD) ,2)  ELSE 0 END AS PCSPRICE  ";
        Sql_str += "                          ,CASE WHEN (sf_get_value_nox('products','weight','cat_cat', CAT, 'dia', dia, 'length',length, 'thread', thread ) IS NOT NULL AND UNIT = 'M')  ";
        Sql_str += "                           THEN ROUND(price / sf_get_stndrd_wt(CAT,DIA,LENGTH,THREAD),2)  ";
        Sql_str += "                           WHEN (sf_get_value_nox('products','weight','cat_cat', CAT, 'dia', dia, 'length',length, 'thread', thread ) IS NOT NULL AND UNIT <> 'M')  THEN  ";
        Sql_str += "                           ROUND( price*1,2)  ELSE 0 END AS NTKGPRICE  ";
        Sql_str += "                         ,PO_NO  ";
        Sql_str += "            FROM  v_gm_local_odritems  ";
        Sql_str += "            WHERE cat NOT IN ('FREIGHT') ";
        Sql_str += "            AND PO_NO = '" + v_pono + "' ";
        Sql_str += "        ) ";
        Sql_str += " ) ";
        Sql_str += " ORDER BY CAT,DIA,LENGTH,THREAD,FIN_ID ";


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
        GridView3.SelectedIndex = -1;
        GridView4.SelectedIndex = -1;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ////這樣就可以讀到RowIndex
        //int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        ////這樣就可以取得Keys值了
        ////string v_actno = GridView1.DataKeys[index].Value.ToString();
        //Label L_currency = (Label)GridView1.Rows[index].Cells[0].FindControl("L_currency");

        //if (e.CommandName == "Select")
        //{
            
        //    QueryCustAmt(L_currency.Text);
        //    Panel1.Visible = true;

        //    DS_ordsum.SelectCommand = "";
        //    GridView3.DataBind();

        //    DS_order.SelectCommand = "";
        //    GridView4.DataBind();


        //}

        //TB_coname.Text = "";
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView2.SelectedIndex = e.NewSelectedIndex;
        GridView3.SelectedIndex = -1;
        GridView4.SelectedIndex = -1;

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label L_coid = (Label)GridView2.Rows[index].Cells[0].FindControl("L_coid");
        //紀錄公司代號
        v_coid = L_coid.Text;
        L_ttlwt.Text = "";
        if (e.CommandName == "Select")
        {
            QryOrdsum(L_coid.Text);
            QueryCustName(L_coid.Text);
            QueryTTLWT(L_coid.Text);
            DS_order.SelectCommand = "";
            GridView4.DataBind();
        }
    }
    protected void GridView3_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView3.SelectedIndex = e.NewSelectedIndex;
        GridView4.SelectedIndex = -1;

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
            QryOrder(L_pono.Text, v_coid);
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

    protected void GridView4_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridView4.SelectedIndex = e.NewSelectedIndex;
    }
    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label v_L_cat = (Label)GridView4.Rows[index].Cells[0].FindControl("L_cat");

        if (e.CommandName == "Select")
        {
            QryDescript(v_L_cat.Text);
        }
    }
}