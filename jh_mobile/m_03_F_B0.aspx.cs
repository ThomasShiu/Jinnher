using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class m_03_F_B0 : System.Web.UI.Page
{
    string v_sql;
    thomas_Conn tsConn = new thomas_Conn();
    thomas_function tsFun = new thomas_function();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (tsFun.chk_verify())
        {
            //Server.Transfer("error.htm");
            Response.Redirect("error2.aspx", false);
            return;
        }

        if (!IsPostBack)
        {
            //v_sql = "SELECT  MACHINEID FROM MACHINES WHERE MACHINEID LIKE 'B0%' ORDER BY 1";
            //SDS_machine.SelectCommand = v_sql;
            //DDL_machine2.DataBind();
            TB_sdate.Text = System.DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            TB_edate.Text = System.DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
        }

    }


}