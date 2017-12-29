using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;

public partial class WiresdatatTrans : System.Web.UI.Page
{
    string Sql_str,v_getValue;
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
            string pv_job_no = "";     //工令單號
            string pv_coil_no = "";    //卷號
            string pd_proc_date = "";  //領用日期
            string pv_proc_emp = "";   //領用人員
            string pv_class_no = "";   //班別
            string pn_item_no = "";   //項次
            string pv_pole_no = "";   //桿號
            string v_type = "";        //receive領用,finish完工

            if ((Request.Params["pv_job_no"] != null) & (Request.Params["pv_coil_no"] != null) &
                (Request.Params["pd_proc_date"] != null) & (Request.Params["pv_proc_emp"] != null) & 
                (Request.Params["type"] != null))
            {
                pv_job_no = Request.Params["pv_job_no"].ToString();
                pv_coil_no = Request.Params["pv_coil_no"].ToString();
                pd_proc_date = Request.Params["pd_proc_date"].ToString();
                pv_proc_emp = Request.Params["pv_proc_emp"].ToString();
                
                v_type = Request.Params["type"].ToString();

                switch (v_type)
                {
                    //盤元領用
                    case "receive":
                        //呼叫SP，產生單據,領用
                        if (pv_job_no.Substring(0, 1).Equals("D") |
                            pv_job_no.Substring(0, 1).Equals("C"))
                        { 
                            //粗抽、精抽才指定工令項次
                            pn_item_no = Request.Params["pn_item_no"].ToString();
                            pv_proc_emp = "100731";  //蘇星
                        }
                        else
                        {
                            //酸洗製程
                            pn_item_no = "";
                            pv_proc_emp = "100515";  //李憲忠
                        }

                        Call_SP_recevie(pv_job_no, pv_coil_no, pd_proc_date, pv_proc_emp, pn_item_no);

                        //呼叫SP，產生單據,完工；先放上讓酸洗領用時，同時完工2014/03/12
                        //因應績效計算，必須正確作領用與完工 2015/03/01
                        //if (pv_job_no.Substring(0, 1).Equals("W")|
                        //    pv_job_no.Substring(0, 1).Equals("X")) //酸洗工令才同時完工，洗抽，洗球
                        //{
                        //    pv_class_no = "A";  //預設早班
                        //    Call_SP_finish(pv_job_no, pv_coil_no, pd_proc_date, pv_proc_emp, pv_class_no);
                        //}
                        break;

                    //盤元完工
                    case "finish":
                        //呼叫SP，產生單據
                        pv_class_no = Request.Params["pv_class_no"].ToString();
                        try
                        {
                            pv_pole_no = Request.Params["pv_pole_no"].ToString();
                        }
                        catch
                        {
                            pv_pole_no = "";
                        }

                        Call_SP_finish(pv_job_no, pv_coil_no, pd_proc_date, pv_proc_emp, pv_class_no, pv_pole_no);
                        //Response.Write("Success");
                        break;
                }

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


    //呼叫STORE PROCEDURE，工令領用
    protected void Call_SP_recevie(string pv_job_no, string pv_coil_no, string pd_proc_date, string pv_proc_emp, string pn_item_no)
    {

        using (OracleConnection objConn = new OracleConnection(
               ConfigurationManager.ConnectionStrings[connStr].ConnectionString))
        {

            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = objConn;
            objCmd.CommandText = "SP_INSERT_APC2210M";
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                objCmd.Parameters.Add("pv_job_no", OracleType.VarChar).Value = pv_job_no;        //工令單號
                objCmd.Parameters.Add("pv_coil_no", OracleType.VarChar).Value = pv_coil_no;      //卷號
                objCmd.Parameters.Add("pd_proc_date", OracleType.DateTime).Value = DateTime.ParseExact(pd_proc_date, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces); //領用日期
                objCmd.Parameters.Add("pv_proc_emp", OracleType.VarChar).Value = pv_proc_emp;   //領用人員
                
                if (!pn_item_no.Equals(""))
                {
                    objCmd.Parameters.Add("pn_item_no", OracleType.Number).Value = Convert.ToDecimal(pn_item_no);   //工令項次
                }
               
                objCmd.Parameters.Add("pv_err_msg", OracleType.VarChar,255).Direction = ParameterDirection.Output;
 

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

    //呼叫STORE PROCEDURE，工令完工 
    protected void Call_SP_finish(string pv_job_no, string pv_coil_no, string pd_proc_date, string pv_proc_emp, string pv_class_no, string pv_pole_no)
    {
            //pv_job_no     IN     VARCHAR2, -- 工令單號
            //pv_coil_no    IN     VARCHAR2, -- 卷號
            //pd_proc_date  IN     DATE,     -- 完工日期
            //pv_proc_emp   IN     VARCHAR2, -- 完工人員
            //pv_class_no   IN     VARCHAR2  -- 班別
            //pv_pole_no    IN     VARCHAR2  -- 桿號
        using (OracleConnection objConn = new OracleConnection(
                 ConfigurationManager.ConnectionStrings[connStr].ConnectionString))
        {

            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = objConn;
            objCmd.CommandText = "SP_INSERT_APC2330M";
            objCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                //objCmd.Parameters.Add("pv_job_no", OracleType.VarChar).Value = pv_job_no;      //工令單號
                objCmd.Parameters.Add("pv_coil_no", OracleType.VarChar).Value = pv_coil_no;      //卷號
                objCmd.Parameters.Add("pd_proc_date", OracleType.DateTime).Value = DateTime.ParseExact(pd_proc_date, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces); //領用日期
                objCmd.Parameters.Add("pv_proc_emp", OracleType.VarChar).Value = pv_proc_emp;   //領用人員
                objCmd.Parameters.Add("pv_class_no", OracleType.VarChar).Value = pv_class_no;   //班別
                objCmd.Parameters.Add("pv_pole_no", OracleType.VarChar).Value = pv_pole_no;     //桿號

                objCmd.Parameters.Add("pv_err_msg", OracleType.VarChar, 255).Direction = ParameterDirection.Output;

                objConn.Open();
                objCmd.ExecuteNonQuery();

                //v_getValue = (string)objCmd.Parameters["pv_err_msg"].Value;
                //Response.Write("Success," + pv_job_no + "," + pv_coil_no + "," + pd_proc_date + "," + pv_proc_emp );
                string v_WireDate = GetWireDate(pv_coil_no);

                if (objCmd.Parameters["pv_err_msg"].Value is DBNull)
                {
                    //Response.Write("Success");
                    Response.Write("Success," + v_WireDate);
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