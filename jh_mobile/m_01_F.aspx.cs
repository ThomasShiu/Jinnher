using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Configuration;

public partial class m_01_F : System.Web.UI.Page
{
    thomas_Conn tsConn = new thomas_Conn();
    thomas_function tsFun = new thomas_function();
    OracleConnection conn;
    OracleCommand cmd, cmd2, cmd3;
    OracleDataReader dr, dr2, dr3;
    string v_sql1, v_sql2, v_sql3;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (tsFun.chk_verify())
        {
            //Server.Transfer("error.htm");
            Response.Redirect("error2.aspx", false);
            return;
        }


      
    }

  
}