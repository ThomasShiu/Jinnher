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
 成型廠退料
 SP_INSERT_APC2540M( pv_err_msg       OUT    VARCHAR2, -- 錯誤訊息 
                    pv_coil_no       IN     VARCHAR2, -- 卷號 
                    pv_reason_code   IN     VARCHAR2, -- 退回原因 
                    pd_proc_date     IN     DATE,     -- 領用日期 
                    pv_proc_emp      IN     VARCHAR2  -- 領用人員 
                  ) 
 
 */
public partial class WiresUnload : System.Web.UI.Page
{
    string Sql_str, v_getValue;
    static string connStr = "jheip";
    //static string connStr = "JHERPDB";
    thomas_Conn tsconn = new thomas_Conn();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        try
        {
            string pv_coil_no = "";     //卷號
            string pn_weight = "";      //餘重
            string pv_isrt_type = "";   //良品,不良品
            string pv_reason_code = ""; //退回原因
            string pd_proc_date = "";   //領用日期
            string pv_proc_emp = "";    //領用人員

            //WiresUnload.aspx?pv_coil_no=F15070253&pn_weight=107&pv_isrt_type=Y&pv_reason_code=0099&pd_proc_date=2015/11/23&pv_proc_emp=100515

            if ((Request.Params["pv_coil_no"] != null) & //(Request.Params["pn_weight"] != null) &
                (Request.Params["pv_isrt_type"] != null) & (Request.Params["pv_reason_code"] != null) &
                (Request.Params["pd_proc_date"] != null) & (Request.Params["pv_proc_emp"] != null))
            {
                pv_coil_no = Request.Params["pv_coil_no"].ToString();
                pn_weight = GetWireDate(pv_coil_no);  //餘重，抓V_HEA_DRW_STOK的資料
                pv_isrt_type = Request.Params["pv_isrt_type"].ToString();
                pv_reason_code = Request.Params["pv_reason_code"].ToString();
                pd_proc_date = Request.Params["pd_proc_date"].ToString();
                pv_proc_emp = Request.Params["pv_proc_emp"].ToString();

                //盤元領用
                Call_SP_unload(pv_coil_no,pn_weight,pv_isrt_type, pv_reason_code ,pd_proc_date, pv_proc_emp);
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

    //呼叫STORE PROCEDURE，成型退料
    protected void Call_SP_unload(string pv_coil_no,string pn_weight,string pv_isrt_type,string pv_reason_code, string pd_proc_date, string pv_proc_emp)
    {
        //退料卡是Code39，要去除檢查碼
        if (pv_coil_no.Length > 9)
        {

        }

        using (OracleConnection objConn = new OracleConnection(
               ConfigurationManager.ConnectionStrings[connStr].ConnectionString))
        {

            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = objConn;
            objCmd.CommandText = "SP_INSERT_APC2540M";
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                objCmd.Parameters.Add("pv_coil_no", OracleType.VarChar).Value = pv_coil_no;        //卷號
                objCmd.Parameters.Add("pn_weight", OracleType.Number).Value = pn_weight;          //餘重
                objCmd.Parameters.Add("pv_isrt_type", OracleType.VarChar).Value = pv_isrt_type;      //良品,不良品
                objCmd.Parameters.Add("pv_reason_code", OracleType.VarChar).Value = pv_reason_code;  //退料原因
                objCmd.Parameters.Add("pd_proc_date", OracleType.DateTime).Value = DateTime.ParseExact(pd_proc_date, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces); //領用日期
                objCmd.Parameters.Add("pv_proc_emp", OracleType.VarChar).Value = pv_proc_emp;   //領用人員

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


    //取得盤元基本資料
    private string GetWireDate(string v_coil_no)
    {
        Sql_str = "SELECT COIL_NO,WIR_KIND,DRAW_DIA,LUO_NO,COIL_WT " +
                  " FROM V_HEA_DRW_STOK WHERE COIL_NO = :COIL_NO ";

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
                string COIL_WT2 = "0";
                if (Convert.ToDecimal(DRAW_DIA) < 15)
                {
                    COIL_WT2 = (Convert.ToDecimal(COIL_WT) + 47).ToString();
                }
                else
                {
                    COIL_WT2 = (Convert.ToDecimal(COIL_WT) + 70).ToString();
                }
                //材質、爐號、線徑、重量
                return COIL_WT2;

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