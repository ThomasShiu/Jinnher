using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
/*
 線材移轉成型
 SP_INSERT_APC2530M( pv_err_msg    OUT    VARCHAR2, -- 錯誤訊息 
                    pv_coil_no    IN     VARCHAR2, -- 卷號 
                    pd_proc_date  IN     DATE,     -- 領用日期 
                    pv_proc_emp   IN     VARCHAR2  -- 領用人員 
                   ) 
 
 */
public partial class Wirestrans : System.Web.UI.Page
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
            string pv_coil_no = "";     //卷號
            string pd_proc_date = "";   //領用日期
            string pv_proc_emp = "";    //領用人員

            if ((Request.Params["pv_coil_no"] != null) & (Request.Params["pd_proc_date"] != null) & (Request.Params["pv_proc_emp"] != null))
            {
                pv_coil_no = Request.Params["pv_coil_no"].ToString();
                pd_proc_date = Request.Params["pd_proc_date"].ToString();
                pv_proc_emp = Request.Params["pv_proc_emp"].ToString();

                //盤元領用
                Call_SP_trans(pv_coil_no,pd_proc_date, pv_proc_emp);
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

    //呼叫STORE PROCEDURE，線材移轉至成型
    protected void Call_SP_trans(string pv_coil_no, string pd_proc_date, string pv_proc_emp)
    {

        using (OracleConnection objConn = new OracleConnection(
               ConfigurationManager.ConnectionStrings[connStr].ConnectionString))
        {

            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = objConn;
            objCmd.CommandText = "SP_INSERT_APC2530M";
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                objCmd.Parameters.Add("pv_coil_no", OracleType.VarChar).Value = pv_coil_no;        //卷號
                objCmd.Parameters.Add("pd_proc_date", OracleType.DateTime).Value = DateTime.ParseExact(pd_proc_date, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces); //領用日期
                objCmd.Parameters.Add("pv_proc_emp", OracleType.VarChar).Value = pv_proc_emp;   //領用人員
                objCmd.Parameters.Add("pv_err_msg", OracleType.VarChar, 255).Direction = ParameterDirection.Output;

                objConn.Open();
                objCmd.ExecuteNonQuery();

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