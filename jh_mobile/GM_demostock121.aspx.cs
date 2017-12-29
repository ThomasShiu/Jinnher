using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
public partial class GM_demostock121 : System.Web.UI.Page
{
    string v_sql,v_sn;
    OracleDataReader dr;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            v_sn = Request.Params.Get("id");
            Literal1.Text = v_sn;
            BindTextBox( v_sn);
        }
        catch
        {
            message.Text = "資料有錯。請聯絡系統管理員。";
        }
    }

    protected void BindTextBox(string v_sn)
    {
        v_sql = " SELECT C.CAT,C.DESCRPT_C,C.DESCRPT_E,C.ABBR,C.PATH ";
        v_sql += " FROM  CATEGORIES C  ";
        v_sql += " WHERE   C.CAT =  '" + v_sn + "' ";
        //string v_return = string.Empty;

        //建立連線
        //使用web.config conn string
        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
        conn.Open();
        OracleCommand cmd = new OracleCommand(v_sql, conn);
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
                message.Text = dr[0].ToString();  //規格
                L_desc_c.Text = dr[1].ToString();
                L_desc_e.Text = dr[2].ToString();
                Img_pic.ImageUrl = dr[4].ToString();

                dr.Close();
                conn.Close();

            }
        }
        catch(Exception ex)
        {
            dr.Close();
            conn.Close();
            message.Text = ex.ToString();
        }
    }
}