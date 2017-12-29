using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;

public partial class index : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (tsfun.chk_verify())
        {
            //Server.Transfer("error.htm");
            Response.Redirect("error2.aspx", false);
            return;
        }

        if (!IsPostBack)
        {

            try
            {
                //string[] v_weather = tsfun.getWaeDataSet("高雄");
                //if (v_weather[0].ToString() != "系统维护中！")
                //{
                //    Weather_icon.ImageUrl = @"images\weather\c_" + v_weather[8].Replace(".gif", "").ToString() + ".png";
                //    string v_weastr = v_weather[5].ToString();
                //    L_weather.Text = Strings.StrConv(v_weastr, VbStrConv.TraditionalChinese, 2052);
                //}
                //else
                //{
                //    Weather_icon.Visible = false;
                //    L_weather.Text = "系統維護中";
                //}
            }
            catch
            {
            }
        }
    }
}