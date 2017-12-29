using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;

public partial class GM_orddelivery : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();
    OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
    OracleDataReader dr;

    string Sql_str, v_s1, v_s2;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            QueryCurry();
        }
    }

    protected void Quy_btn_Click(object sender, EventArgs e)
    {
        QueryCurry();

        //將其他GridView清空
        DS_cust.SelectCommand = "";
        GridView2.DataBind();

        DS_order.SelectCommand = "";
        GridView3.DataBind();

        DS_ordsum.SelectCommand = "";
        GridView4.DataBind();

        DS_lot.SelectCommand = "";
        GridView5.DataBind();

        GridView1.SelectedIndex = -1;
       
    }

    //幣別查詢
    protected void QueryCurry()
    {
        Sql_str = "SELECT PO_AMT,WT,PRICE_AMT,CURRENCY ";
        Sql_str += "FROM V_ORDERITEMS_SUM ";
        Sql_str += "ORDER BY PO_AMT DESC ";

        DS_curr.SelectCommand = Sql_str;
    }

    //客戶查詢
    protected void QueryCust(string v_currency)
    {
        Sql_str = " SELECT CO,PO_NO_AMT,WT,CO_PRICE_AMT,CURRENCY ";
        Sql_str += "FROM    v_orderitems_co_amt ";
        Sql_str += "WHERE CURRENCY = '" + v_currency + "' ";
        Sql_str += "ORDER BY po_no_amt DESC ";
        DS_cust.SelectCommand = Sql_str;
        //GridView2.DataBind();
    }

 

    //訂單小計
    protected void QryOrdsum(string v_co_id)
    {
        Sql_str = "SELECT PO_NO,DELIVERY,WT,PO_NO_AMT,PRICE_AMT ,CO,CURRENCY ";
        Sql_str += "FROM v_orderitems_po_amt ";
        Sql_str += "WHERE CO = '" + v_co_id + "' ";
        Sql_str += "ORDER BY delivery ";
        DS_ordsum.SelectCommand = Sql_str;
    }
   
    //訂單查詢
    protected void QryOrder(string v_pono)
    {
        Sql_str = "SELECT PO_NO,CAT,DIA,LEN,TH,PL,KEG,BALANCE,PACKKEG,PCS,WT,O_PRICE,CO,DELIVERY ";
        Sql_str += "FROM   orderitems_data_mart ";
        Sql_str += "WHERE PO_NO = '" + v_pono + "' ";

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

    protected void QueryLot(string v_pono, string v_th, string v_cat, string v_dia, string v_len, string v_pl)
    {

        Sql_str = "SELECT A1.LOT_NO,A1.FM_WT,A1.TH_WT,A1.HEAT_WT,A1.PLATE_WT, ";
        Sql_str += " (SELECT SUM(REPLACE(W.CHOW,'X',''))  FROM wip_matrix W WHERE W.lot_no = A1.LOT_NO) AS CHOW, ";
        Sql_str += "  (SELECT SUM(REPLACE(W.CPOW,'X',''))  FROM wip_matrix W WHERE W.lot_no = A1.LOT_NO)  AS CPOW  ";
        Sql_str += " FROM ( ";
        Sql_str += "    SELECT LOT_NO,SUM(FM_WT) AS FM_WT,SUM(TH_WT) AS TH_WT,SUM(HEAT_WT) AS HEAT_WT,SUM(PLATE_WT) AS PLATE_WT      ";
        Sql_str += "    FROM ( ";
        Sql_str += "        SELECT A.LOT_NO, ";
        Sql_str += "                    CASE WHEN  B.PROCESS = 'F' THEN B.weight ELSE 0 END AS FM_WT, ";
        Sql_str += "                    CASE WHEN  B.PROCESS = 'T' THEN B.weight ELSE 0 END AS TH_WT, ";
        Sql_str += "                    CASE WHEN  B.PROCESS = 'H' THEN B.weight ELSE 0 END AS HEAT_WT, ";
        Sql_str += "                    CASE WHEN  B.PROCESS = 'P' THEN B.weight ELSE 0 END AS PLATE_WT ";
        Sql_str += "        FROM ITEMS  A,WIP B  ";
        Sql_str += "        WHERE A.LOT_NO = B.LOT_NO ";
        Sql_str += "        AND     A. PO_NO = '" + v_pono + "' ";
        Sql_str += "        AND     A.ODRITM_TH = '" + v_th + "' ";
        Sql_str += "        AND     A.ODRITM_CAT = '" + v_cat + "' ";
        Sql_str += "        AND     A.ODRITM_DIA = '" + v_dia + "' ";
        Sql_str += "        AND     A.ODRITM_LEN='" + v_len + "' ";
        Sql_str += "        AND     A.ODRITM_PL = '" + v_pl + "' ";
        Sql_str += "        )  ";
        Sql_str += "    GROUP BY LOT_NO  ";
        Sql_str += "   ) A1 ";

        DS_lot.SelectCommand = Sql_str;
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
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label L_currency = (Label)GridView1.Rows[index].Cells[0].FindControl("L_currency");

        if (e.CommandName == "Select")
        {
            QueryCust(L_currency.Text);

            DS_ordsum.SelectCommand = "";
            GridView3.DataBind();

            DS_order.SelectCommand = "";
            GridView4.DataBind();

            DS_lot.SelectCommand = "";
            GridView5.DataBind();
        }

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

        if (e.CommandName == "Select")
        {
            QryOrdsum(L_coid.Text);

            DS_order.SelectCommand = "";
            GridView4.DataBind();

            DS_lot.SelectCommand = "";
            GridView5.DataBind();
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
            QryOrder(L_pono.Text);

            DS_lot.SelectCommand = "";
            GridView5.DataBind();
        }
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#DDDDDD'");

            //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

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
        Label L_pono = (Label)GridView4.Rows[index].Cells[0].FindControl("L_pono");
        Label L_cat = (Label)GridView4.Rows[index].Cells[0].FindControl("L_cat");
        Label L_dia = (Label)GridView4.Rows[index].Cells[0].FindControl("L_dia");
        Label L_len = (Label)GridView4.Rows[index].Cells[0].FindControl("L_len");
        Label L_th = (Label)GridView4.Rows[index].Cells[0].FindControl("L_th");
        Label L_pl = (Label)GridView4.Rows[index].Cells[0].FindControl("L_pl");

        if (e.CommandName == "Select")
        {
            QueryLot(L_pono.Text, L_th.Text, L_cat.Text, L_dia.Text, L_len.Text, L_pl.Text);
        }
    }
}