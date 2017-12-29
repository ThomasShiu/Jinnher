using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Configuration;

public partial class m_03_F : System.Web.UI.Page
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

        if (!IsPostBack){
            //v_sql = "SELECT  MACHINEID FROM MACHINES WHERE MACHINEID LIKE 'B0%' ORDER BY 1";
            //SDS_machine.SelectCommand = v_sql;
            //DDL_machine2.DataBind();

            //紀錄LOG
            string strClientIP = Context.Request.ServerVariables["REMOTE_ADDR"];
            tsConn.save_log("Guest", "m_03_F", strClientIP, "成型達成率");
        }

    }

}