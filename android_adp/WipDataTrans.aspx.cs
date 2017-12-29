using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;

public partial class WipDataTrans : System.Web.UI.Page
{
    string Sql_str, v_getValue;
    static string connStr = "jh815";
    //static string connStr = "JHERPDB";
    thomas_Conn tsconn = new thomas_Conn();

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            /*
        pv_err_msg OUT VARCHAR2,
        pv_barcode1 IN VARCHAR2,
        pv_param IN VARCHAR2,
        pv_loc IN VARCHAR2,
        pv_proc_emp  IN VARCHAR2
           */
            string pv_barcode1 = "";   //流程卡號
            string pv_loc = "";      //儲區
            string pv_proc_emp = "";   //領用人員
            string pv_param = "";   //PDA

            if ((Request.Params["pv_barcode1"] != null) & (Request.Params["pv_loc"] != null) &
                (Request.Params["pv_proc_emp"] != null) & (Request.Params["pv_param"] != null))
            {
                pv_barcode1 = Request.Params["pv_barcode1"].ToString();
                pv_loc = Request.Params["pv_loc"].ToString();
                pv_proc_emp = Request.Params["pv_proc_emp"].ToString();
                pv_param = Request.Params["pv_param"].ToString();

                Call_SP_PDAScan(pv_barcode1, pv_param, pv_loc, pv_proc_emp);

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }

    }

    protected void Call_SP_PDAScan(string pv_barcode1, string pv_param, string pv_loc, string pv_proc_emp)
    {

        using (OracleConnection objConn = new OracleConnection(
               ConfigurationManager.ConnectionStrings[connStr].ConnectionString))
        {

            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = objConn;
            objCmd.CommandText = "SP_WIPINV_SCAN";
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                objCmd.Parameters.Add("pv_barcode1", OracleType.VarChar).Value = pv_barcode1;   //條碼
                objCmd.Parameters.Add("pv_param", OracleType.VarChar).Value = pv_param;         //參數
                objCmd.Parameters.Add("pv_loc", OracleType.VarChar).Value = pv_loc;             //儲區
                objCmd.Parameters.Add("pv_proc_emp", OracleType.VarChar).Value = pv_proc_emp;   //作業人員工號

                objCmd.Parameters.Add("pv_err_msg", OracleType.VarChar, 255).Direction = ParameterDirection.Output;


                objConn.Open();
                objCmd.ExecuteNonQuery();

                //v_getValue = (string)objCmd.Parameters["pv_err_msg"].Value;
                //Response.Write("Success," + pv_job_no + "," + pv_coil_no + "," + pd_proc_date + "," + pv_proc_emp );

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
}