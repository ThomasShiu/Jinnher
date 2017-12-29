using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class code128 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Params["wireids"] != null)
        {
            string v_wireids = Request.Params["wireids"].ToString(); 

            Code128 MyCode = new Code128();

            //條碼高度
            MyCode.Height = 100;

            //可見號碼
            MyCode.ValueFont = new Font("細明體", 18, FontStyle.Regular);

            //產生條碼
            System.Drawing.Image img = MyCode.GetCodeImage(v_wireids, Code128.Encode.Code128A);

            //在網頁上輸出
            Response.Clear();
            Response.ContentType = "image/jpeg";
            img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}