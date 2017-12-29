using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Download : System.Web.UI.Page
{
    thomas_function ts_Fun = new thomas_function();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ts_Fun.chk_verify())
        {
            //Server.Transfer("error.htm");
            Response.Redirect("error2.aspx", false);
        }

        if (!IsPostBack)
        {
            SDS_notifi.SelectCommand = "SELECT  SN,TITTLE, CONTENT,VERSION,DESCRIPT,APK,STATUS FROM PDA_NOTIFICATION WHERE STATUS = 'Y' ORDER BY CREATE_DATE DESC";
            DataList1.DataBind();
        }
    }
}