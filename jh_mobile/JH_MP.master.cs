using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;

public partial class JH_MP : System.Web.UI.MasterPage
{
    thomas_function tsfun = new thomas_function();
    thomas_Conn tsconn = new thomas_Conn();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //取得程式名稱
            string filename = System.IO.Path.GetFileName(Request.PhysicalPath).Replace(".aspx", "");

            //紀錄LOG
            string strClientIP = Context.Request.ServerVariables["REMOTE_ADDR"];
            tsconn.save_log("Guest", filename, strClientIP,"");
        }
    }
}
