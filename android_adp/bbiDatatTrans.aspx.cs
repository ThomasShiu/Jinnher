using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;
using System.Data.OracleClient;

public partial class bbiDatatTrans : System.Web.UI.Page
{
    string Sql_str;
    thomas_Conn tsconn = new thomas_Conn();
    thomas_function ts_Fun = new thomas_function();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ts_Fun.chk_verify())
        {
            //Server.Transfer("error.htm");
            Response.Redirect("error2.aspx", false);
        }

        try
        {

            string v_barcode1 = "";
            string v_barcode2 = "";
            string v_pacno = "";
            string v_create_emp = "";
            string v_create_date = "";

            if ((Request.Params["barcode1"] != null) & (Request.Params["barcode2"] != null) & 
                (Request.Params["emp"] != null) & (Request.Params["date"] != null) & (Request.Params["pacno"] != null))
            {
                v_barcode1 = Request.Params["barcode1"].ToString();
                v_barcode2 = Request.Params["barcode2"].ToString();
                v_pacno = Request.Params["pacno"].ToString();
                v_create_emp = Request.Params["emp"].ToString();
                v_create_date = Request.Params["date"].ToString();
                //BBI條碼長度13字元
                if (v_barcode1.Length >= 13)
                {
                    //檢查STOCK_ID是否正確
                    if (Chk_Stock_id(v_pacno, v_barcode2))
                    {
                        Sql_str = "INSERT INTO BBI_SHIP_QUEUE(SN,BARCODE_1,BARCODE_2,BBI_SO,EMP_NO,CREATE_DATE )  VALUES(BBI_SHIP_QUEUE_SEQ.NEXTVAL,";
                        Sql_str += " '" + v_barcode1 + "','" + v_barcode2 + "','" + v_pacno + "','" + v_create_emp + "',TO_DATE('" + v_create_date + "','YYYY/MM/DD HH24:MI:SS')  )";
                        tsconn.trans_oracle("jh815", Sql_str, "insert");
                        //Response.Write(Sql_str);
                        Response.Write("Success");
                    }
                    else
                    {
                        Response.Write("Data ERROR");
                    }
                }
                else
                {
                    Response.Write("Barcode1 too short");
                }


            }
            else
            {
                Response.Write("No data insert");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    private bool Chk_Stock_id(string v_pac_no, string v_stock_id)
    {
        bool v_true_false = false;

        //建立連線
        //使用web.config conn string
        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
        conn.Open();
        //判斷有無排出貨資料
        Sql_str = "select count(*) as cnt from packings p, listitems i where p.pac_id = I.pac_id and p.pac_no = :pacno and i.stock_id = :stockid ";
        OracleCommand cmd = new OracleCommand(Sql_str, conn);
        cmd.Parameters.Add(new OracleParameter("pacno", OracleType.VarChar, 100));
        cmd.Parameters["pacno"].Value = v_pac_no;

        cmd.Parameters.Add(new OracleParameter("stockid", OracleType.VarChar, 100));
        cmd.Parameters["stockid"].Value = v_stock_id;
        OracleDataReader dr= cmd.ExecuteReader();
        int v_cnt = 0;

        try
        {
            dr.Read();
            v_cnt = Convert.ToInt32(dr[0].ToString());
            if (v_cnt>0 )
                v_true_false = true;
            else
                v_true_false = false;
        }

        catch (Exception err)
        {
            string v_err = err.ToString();
            //MessageBox.Show(String.Format("{0}", err.Message), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            //rTB_log2.Text += "資料比對錯誤：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "," + err.Message + "\n";
            //rTB_log2.ScrollToCaret();
        }
        finally
        {
            cmd.Dispose();
            dr.Close();
            dr.Dispose();
            conn.Close();
            conn.Dispose();
        }
        return v_true_false;
    }
}