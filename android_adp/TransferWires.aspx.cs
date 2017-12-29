using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TransferWires : System.Web.UI.Page
{
    string Sql_str, Sql_str2;
    thomas_Conn tsconn = new thomas_Conn();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        try
        {
            string v_barcode1 = "";
            string v_barcode2 = "";
            string v_barcode3 = "";
            string v_station = "";
            string v_create_emp = "";
            string v_create_date = "";
            string v_type = "";

            if ((Request.Params["barcode1"] != null) & (Request.Params["station"] != null) &
                (Request.Params["emp"] != null) & (Request.Params["date"] != null) & (Request.Params["type"] != null))
            {
                v_barcode1 = Request.Params["barcode1"].ToString();
                v_barcode3 = Request.Params["barcode3"].ToString();  //feed入料,finishs完工
                v_station = Request.Params["station"].ToString();
                v_create_emp = Request.Params["emp"].ToString();
                v_create_date = Request.Params["date"].ToString();
                v_type = Request.Params["type"].ToString(); //TRANS,PICK

                Sql_str = "INSERT INTO BARCODE_QUEUE(SN,BARCODE_1,STATION,CREATE_EMP,CREATE_DATE ) VALUES(BARCODE_QUEUE_SEQ.NEXTVAL,";
                Sql_str += " '" + v_barcode1 + "','" + v_station + "','" + v_create_emp + "',TO_DATE('" + v_create_date + "','YYYY/MM/DD HH24:MI:SS')  )";
                tsconn.trans_oracle("jh815",Sql_str, "insert");  //jheip
                Response.Write("Success");
            }
            else
            {
                Response.Write("No data insert!");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }

    }
    
}