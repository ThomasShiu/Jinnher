using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ipad : System.Web.UI.Page
{
    string v_str;
    string[] v_str1;
    thomas_function ts_Fun = new thomas_function();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ts_Fun.chk_verify())
        {
            //Server.Transfer("error.htm");
            Response.Redirect("error2.aspx", false);
            return;
        }
        if (!IsPostBack)
        {
            if (Session["admin"] != null)
            {
                v_str = Session["admin"].ToString();
                v_str1 = v_str.Split('.'); // v_str1[0]工號,v_str1[1]姓名
                L_id.Text = v_str1[0].ToString();
            }
        }
    }
}