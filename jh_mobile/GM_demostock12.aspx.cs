﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;

public partial class GM_demostock12 : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();
    OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
    OracleDataReader dr;

    string Sql_str;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //TB_year.Text = DateTime.Today.Year.ToString();
            QueryCust();
        }
    }

    protected void Quy_btn_Click(object sender, EventArgs e)
    {

        QueryCust();

        //將其他GridView清空
        DS_cust.SelectCommand = "";
        //GridView2.DataBind();

        DS_plt.SelectCommand = "";
        GridView3.DataBind();

        DS_order.SelectCommand = "";
        GridView4.DataBind();

    }


    //種類查詢
    protected void QueryCust()
    {
        Sql_str = " SELECT A.CAT,A.TTLPLT,A.TTLWT, B.PATH,B.DESCRPT_C ";
        Sql_str += "FROM    V_INV_NOCO_CAT A,CATEGORIES B ";
        Sql_str += "WHERE  A.CAT = B.CAT ";
        DS_cust.SelectCommand = Sql_str;
        //GridView2.DataBind();
    }

    //庫存小計
    protected void QryOrdsum(string v_cat)
    {
        Sql_str = "SELECT  CAT,DIA,LEN,TH,PL,TTLPLT,TTLWT,DELIYEAR ";
        Sql_str += "FROM   V_INV_NOCO_PDT ";
        Sql_str += "WHERE  CAT = '" + v_cat + "' ";
        Sql_str += "ORDER  BY   CAT,DIA,LEN,TH,PL ";

        DS_plt.SelectCommand = Sql_str;
    }



    //庫存查詢
    protected void QryOrder(string v_cat, string v_dia, string v_len, string v_th, string v_pl, string v_deliyear)
    {
        //QryOrder(L_cat.Text, L_dia.Text, L_len.Text, L_th.Text, L_pl.Text, L_deliyear.Text);
        Sql_str = "SELECT A.DELIYEAR,A.STOCK_DATE,A.CAT,A.DIA,A.LEN,A.TTLWT,A.PACKKEG ,A.PCSQTY,A.STOCK_ID,A.LOCATION,A.WEIGHTPERK,B.DESCRPT_C  ";
        Sql_str += "          , A.TH , A.PL ,A.PACK_CODE,A.QTY_PER_CTN ";
        Sql_str += "FROM  V_INV_NOCO_PDT2 A,CATEGORIES B ";
        Sql_str += "WHERE     A. DELIYEAR = '" + v_deliyear + "' ";
        Sql_str += "    AND     A.CAT = '" + v_cat + "' ";
        Sql_str += "    AND     A.DIA = '" + v_dia + "' ";
        Sql_str += "    AND     A.LEN = '" + v_len + "' ";
        Sql_str += "    AND     A.TH='" + v_th + "' ";
        Sql_str += "    AND     A.PL = '" + v_pl + "' ";
        Sql_str += "    AND    A.CAT = B.CAT ";
        Sql_str += "ORDER BY A.STOCK_ID,A.CAT,A.DIA,A.LEN,A.TH,A.PL,A.PACKKEG ";

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



    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //GridView2.SelectedIndex = e.NewSelectedIndex;
        GridView3.SelectedIndex = -1;
        GridView4.SelectedIndex = -1;

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        //Label L_cat = (Label)GridView2.Rows[index].Cells[0].FindControl("L_cat");

        if (e.CommandName == "Select")
        {
            //QryOrdsum(L_cat.Text);

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
        Label L_cat = (Label)GridView3.Rows[index].Cells[0].FindControl("L_cat");
        Label L_dia = (Label)GridView3.Rows[index].Cells[0].FindControl("L_dia");
        Label L_len = (Label)GridView3.Rows[index].Cells[0].FindControl("L_len");
        Label L_th = (Label)GridView3.Rows[index].Cells[0].FindControl("L_th");
        Label L_pl = (Label)GridView3.Rows[index].Cells[0].FindControl("L_pl");
        Label L_deliyear = (Label)GridView3.Rows[index].Cells[0].FindControl("L_deliyear");

        if (e.CommandName == "Select")
        {
            QryOrder(L_cat.Text, L_dia.Text, L_len.Text, L_th.Text, L_pl.Text, L_deliyear.Text);

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
        Label v_L_cat = (Label)GridView4.Rows[index].Cells[0].FindControl("L_cat");

        if (e.CommandName == "Select")
        {
            QryDescript(v_L_cat.Text);
        }
    }

    protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ListView1_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        ListView1.SelectedIndex = e.NewSelectedIndex;
        QueryCust();
    }
    protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }

    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        //這樣就可以讀到RowIndex
        //int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
        //這樣就可以取得Keys值了
        //string v_actno = GridView1.DataKeys[index].Value.ToString();
        Label L_cat = (Label)e.Item.FindControl("L_cat");

        if (e.CommandName == "Select")
        {
            QryOrdsum(L_cat.Text);
        }
        else if (e.CommandName == "Disable")
        {
            ListView1.SelectedIndex = -1;
            QueryCust();

            DS_plt.SelectCommand = "";
            GridView3.DataBind();
        }

        DS_order.SelectCommand = "";
        GridView4.DataBind();
    }
}