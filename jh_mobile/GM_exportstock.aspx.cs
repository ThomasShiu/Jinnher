using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
public partial class GM_exportstock : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();
    OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
    OracleDataReader dr;

    string Sql_str, v_s1, v_s2;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TB_year.Text = DateTime.Today.Year.ToString();
            QueryCust();
        }
    }

    protected void Quy_btn_Click(object sender, EventArgs e)
    {

        QueryCust();

        //將其他GridView清空
        DS_plt.SelectCommand = "";
        GridView3.DataBind();

        DS_order.SelectCommand = "";
        GridView4.DataBind();

    }

   
    //客戶查詢
    protected void QueryCust()
    {
        Sql_str = " SELECT CO_CO_ID,PLTCNT,ROUND(WTTON,4) WTTON,EMP_NAME ";
        Sql_str += "FROM   V_INV_CO ";
        Sql_str += "ORDER BY EMP_NAME,WTTON  DESC ";

        DS_cust.SelectCommand = Sql_str;
        //GridView2.DataBind();
    }

    //訂單小計
    protected void QryOrdsum(string v_co_id)
    {
        Sql_str = "SELECT  DELIYEAR,CO_CO_ID,PLTCNT,WTTON ";
        Sql_str += "FROM  v_inv_orders_yearco ";
        Sql_str += "WHERE DELIYEAR = '" + TB_year.Text + "'   ";
        Sql_str += "AND CO_CO_ID = '" + v_co_id + "' ";

        DS_plt.SelectCommand = Sql_str;
    }

   

    //訂單查詢
    protected void QryOrder(string v_coid, string v_year)
    {
        Sql_str = "SELECT DELIYEAR,PO_NO,DELIVERY,CAT, DIA,LEN,TH,PL,PACKKEG,PCSQTY,STOCK_ID,LOCATION,CO_CO_ID,WEIGHTPERK ";
        Sql_str += "FROM    V_INV_ORDERS_CO ";
        Sql_str += "WHERE DELIYEAR = '" + v_year + "' ";
        Sql_str += "AND     CO_CO_ID = '" + v_coid + "' ";
        Sql_str += "ORDER BY STOCK_ID,CAT,DIA,LEN,TH,PL,PACKKEG ";

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
        Label L_coid2 = (Label)GridView3.Rows[index].Cells[0].FindControl("L_coid2");
        Label L_year = (Label)GridView3.Rows[index].Cells[0].FindControl("L_year");

        if (e.CommandName == "Select")
        {
            QryOrder(L_coid2.Text, L_year.Text);

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
}