using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
//世宇斑馬，盤元盤點實盤使用的服務
public partial class WiresdatatTrans3 : System.Web.UI.Page
{
    string Sql_str, v_getValue;
    static string connStr = "jheip";
    //static string connStr = "JHERPDB";
    thomas_Conn tsconn = new thomas_Conn();
    thomas_function ts_Fun = new thomas_function();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ts_Fun.chk_verify())
        {
            //Server.Transfer("error.htm");
            Response.Redirect("error2.aspx", false);
        }
        //if (!IsPostBack)
        //{
        try
        {
            string pv_locate = "";     //儲位
            string pv_coil_no = "";    //卷號
            string pd_proc_date = "";  //盤點日期
            string pv_proc_emp = "";   //盤點人員

            if ((Request.Params["pv_locate"] != null) & (Request.Params["pv_coil_no"] != null) &
                (Request.Params["pd_proc_date"] != null) & (Request.Params["pv_proc_emp"] != null) )
            {
                pv_locate = Request.Params["pv_locate"].ToString();
                pv_coil_no = Request.Params["pv_coil_no"].ToString();
                pd_proc_date = Request.Params["pd_proc_date"].ToString();
                pv_proc_emp = Request.Params["pv_proc_emp"].ToString();

                //呼叫程序，寫入ASK2210M實盤輸入
                Call_SP_INV(pv_locate, pv_coil_no, pd_proc_date, pv_proc_emp);

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
        //}
    }


    //呼叫STORE PROCEDURE，盤元實盤
    protected void Call_SP_INV(string pv_locate, string pv_coil_no, string pd_proc_date, string pv_proc_emp)
    {

        using (OracleConnection objConn = new OracleConnection(
               ConfigurationManager.ConnectionStrings[connStr].ConnectionString))
        {

            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = objConn;
            objCmd.CommandText = "SP_INSERT_ASK2210M";
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                objCmd.Parameters.Add("pv_locate", OracleType.VarChar).Value = pv_locate;        //儲位
                objCmd.Parameters.Add("pv_coil_no", OracleType.VarChar).Value = pv_coil_no;      //卷號
                objCmd.Parameters.Add("pd_proc_date", OracleType.DateTime).Value = DateTime.ParseExact(pd_proc_date, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces); //盤點日期
                objCmd.Parameters.Add("pv_proc_emp", OracleType.VarChar).Value = pv_proc_emp;   //盤點人員

                objCmd.Parameters.Add("pv_err_msg", OracleType.VarChar, 512).Direction = ParameterDirection.Output;

                objConn.Open();
                objCmd.ExecuteNonQuery();

                //v_getValue = (string)objCmd.Parameters["pv_err_msg"].Value;
                //Response.Write("Success," + pv_job_no + "," + pv_coil_no + "," + pd_proc_date + "," + pv_proc_emp );
                //string v_WireDate = GetWireDate(pv_coil_no);

                if (objCmd.Parameters["pv_err_msg"].Value is DBNull)
                {
                    Response.Write("Success");
                }
                else
                {
                    Response.Write(objCmd.Parameters["pv_err_msg"].Value);
                }
            }
            catch (Exception ex)
            {
                objCmd.Dispose();
                objConn.Dispose();
                objConn.Close();
                //err_mess4.Text = "發生異常：" + ex.ToString();
                Response.Write(ex.ToString());
            }
            finally
            {
                objCmd.Dispose();
                objConn.Dispose();
                objConn.Close();
            }
        }
    }

    //取得盤元基本資料
    private string GetWireDate(string v_coil_no)
    {
        Sql_str = "SELECT COIL_NO,WIR_KIND,DRAW_DIA,LUO_NO,COIL_WT FROM ASK_DRW_STOK WHERE COIL_NO = :COIL_NO";


        //建立連線
        //使用web.config conn string
        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jheip"].ConnectionString);
        conn.Open();


        OracleCommand cmd = new OracleCommand(Sql_str, conn);
        cmd.Parameters.Add(new OracleParameter("COIL_NO", OracleType.VarChar, 100));
        cmd.Parameters["COIL_NO"].Value = v_coil_no;

        OracleDataReader dr = cmd.ExecuteReader();

        try
        {
            //while (dr.Read())
            //{
            //組成JSON字串
            if (dr.HasRows)
            {
                dr.Read();
                string COIL_NO = dr["COIL_NO"].ToString();
                string WIR_KIND = dr["WIR_KIND"].ToString();
                string LUO_NO = dr["LUO_NO"].ToString();
                string DRAW_DIA = dr["DRAW_DIA"].ToString();
                string COIL_WT = dr["COIL_WT"].ToString();
                //材質、爐號、線徑、重量
                return WIR_KIND + "," + LUO_NO + "," + DRAW_DIA + "," + COIL_WT;

            }
            else
            {
                return "";
            }

        }
        catch (Exception ex)
        {
            //tsconn.save_log("guest", "QueryWires_01", "192.168.0.19", ex.ToString());
            //Response.Write(ex.ToString());
        }
        finally
        {
            cmd.Dispose();
            dr.Close();
            dr.Dispose();
            conn.Close();
            conn.Dispose();
        }

        return "";
    }
}