using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;

public partial class GM_showdescript : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();
    OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
    OracleDataReader dr;
    string v_sn, Sql_str;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                v_sn = Request.Params.Get("sn");
                QryDescript(v_sn);
            }
            catch
            {
                TB_desc.Text = "No Data.";
            }
        }
    }

    //規格查詢
    protected void QryDescript(string v_cat)
    {
        TB_desc.Visible = true;
        TB_desc.Text = "";
        Sql_str = "SELECT DESCRPT_C  ";
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
                TB_desc.Text = dr[0].ToString();  //規格
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
}