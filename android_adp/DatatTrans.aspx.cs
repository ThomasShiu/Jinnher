using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;

public partial class DatatTrans : System.Web.UI.Page
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

            if ((Request.Params["barcode1"] != null) & (Request.Params["barcode2"] != null) & (Request.Params["barcode3"] != null) & (Request.Params["station"] != null) &
                (Request.Params["emp"] != null) & (Request.Params["date"] != null) & (Request.Params["type"] != null))
            {
                v_barcode1 = Request.Params["barcode1"].ToString();
                v_barcode2 = Request.Params["barcode2"].ToString(); //洗抽,洗球
                v_barcode3 = Request.Params["barcode3"].ToString(); //接收,完工
                v_station = Request.Params["station"].ToString();
                v_create_emp = Request.Params["emp"].ToString();
                v_create_date = Request.Params["date"].ToString();
                v_type = Request.Params["type"].ToString(); //TRANS,PICK

                //酸洗

               
                insertPICK(v_barcode1, v_barcode2, v_barcode3, v_station, v_create_emp, v_create_date);

            }
            else
            {
                Response.Write("No data insert!");
            }
        }
        catch (Exception ex)
        {
            Response.Write("Param Error:");
        }
        //}
    }

    private void insertPICK(string v_barcode1, string v_barcode2, string v_barcode3, string v_station, string v_create_emp, string v_create_date)
    {
        string v_wire_id, v_wire_ids, v_wire_rawmtrl, v_wire_diameter, v_wire_heatno, v_wire_coid, v_weight;

        Sql_str2 = " SELECT  W.WIRE_IDS,  ";
        Sql_str2 += "   W.JOBORDER, W.PDATE, W.OPERATOR,  ";
        Sql_str2 += "   W.REMARK, W.EDIT_EMP, W.EDIT_DATE,  ";
        Sql_str2 += "   W.MODIFY_DATE, W.STATUS, W.PROCESS,  ";
        Sql_str2 += "   W.RAWMTRL_ID, W.HEAT_NO, W.DIAMETER,  ";
        Sql_str2 += "   W.WEIGHT, W.CO_ID, W.NEXT ";
        Sql_str2 += "FROM  WIRES_PICKLING W ";
        Sql_str2 += "WHERE (WIRE_IDS = :WIRE_IDS OR WIRE_IDS = SUBSTR(:WIRE_IDS,0,LENGTH(:WIRE_IDS)-1)) ";
        Sql_str2 += "AND PROCESS = :PROCESS ";
        Sql_str2 += "AND NEXT = :NEXT ";
        Sql_str2 += "AND TO_CHAR(PDATE,'YYYY/MM/DD') = :PDATE ";

        Sql_str = "SELECT WIRE_ID,WIRE_IDS,RAWMTRL_ID,CO_ID,NVL(HEAT_NO_NEW,HEAT_NO) HEAT_NO,DIAMETER,WEIGHT,to_char(STORE_DATE,'YYYY/MM/DD') as STORE_DATE,LOCATION ";
        Sql_str += "FROM   WIRES ";
        Sql_str += "WHERE (WIRE_ID = :v_ID OR WIRE_IDS = :v_ID OR WIRE_IDS = SUBSTR(:v_ID,0,LENGTH(:v_ID)-1)) ";

        //建立連線
        //使用web.config conn string
        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
        conn.Open();

        OracleCommand cmd;
        OracleDataReader dr;

        //判斷當天有無此卷號
        try
        {
            cmd = new OracleCommand(Sql_str2, conn);
            cmd.Parameters.Add(new OracleParameter("WIRE_IDS", OracleType.VarChar, 100));
            cmd.Parameters["WIRE_IDS"].Value = v_barcode1;

            cmd.Parameters.Add(new OracleParameter("NEXT", OracleType.VarChar, 100));
            cmd.Parameters["NEXT"].Value = v_barcode2;

            cmd.Parameters.Add(new OracleParameter("PROCESS", OracleType.VarChar, 100));
            cmd.Parameters["PROCESS"].Value = v_barcode3;

            cmd.Parameters.Add(new OracleParameter("PDATE", OracleType.VarChar, 100));
            cmd.Parameters["PDATE"].Value = v_create_date.Substring(0, 10);

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {

                Response.Write("Error!卷號重複 Wire Duplicate!"); 
                conn.Close();
                return;
            }
        }
        catch (Exception ex)
        {
            //Response.Write("Duplicate Error:"+ex.ToString());
            Response.Write("Duplicate Error:" );
            conn.Close();
        }
        finally
        {
        }
 

        try
        {

            v_wire_id = "";
            v_wire_ids = "";
            v_wire_rawmtrl = "";
            v_wire_diameter = "";
            v_wire_heatno = "";
            v_wire_coid = "";
            v_weight = "";

            cmd = new OracleCommand(Sql_str, conn);
            cmd.Parameters.Add(new OracleParameter("v_ID", OracleType.VarChar, 100));
            cmd.Parameters["v_ID"].Value = v_barcode1;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    v_wire_id = dr["WIRE_ID"].ToString();
                    v_wire_ids = dr["WIRE_IDS"].ToString();
                    v_wire_rawmtrl = dr["RAWMTRL_ID"].ToString();
                    v_wire_diameter = dr["DIAMETER"].ToString();
                    v_wire_heatno = dr["HEAT_NO"].ToString();
                    v_wire_coid = dr["CO_ID"].ToString();
                    v_weight = dr["WEIGHT"].ToString();
                }
                Sql_str = "INSERT INTO WIRES_PICKLING(SN,  WIRE_ID, WIRE_IDS,NEXT,PROCESS,OPERATOR,PDATE,  REMARK, EDIT_EMP, EDIT_DATE, STATUS,RAWMTRL_ID,DIAMETER,HEAT_NO,WEIGHT,CO_ID) ";
                Sql_str += " VALUES(WIRES_PICKLING_SEQ.NEXTVAL, ";
                Sql_str += " '" + v_wire_id + "','" + v_wire_ids + "','" + v_barcode2 + "','" + v_barcode3 + "','" + v_create_emp + "',TO_DATE('" + v_create_date + "','YYYY/MM/DD HH24:MI:SS'),";
                Sql_str += "  'PDA上傳','" + v_create_emp + "',TO_DATE('" + v_create_date + "','YYYY/MM/DD HH24:MI:SS'),'N','" + v_wire_rawmtrl + "','" + v_wire_diameter + "','" + v_wire_heatno + "'," + v_weight + ",'" + v_wire_coid + "')";
                tsconn.trans_oracle("jh815",Sql_str, "insert");
                
                Response.Write("Success");
            }
            else
            {
 
                Response.Write("Error!無此卷號 No Wire Number!");
            }
        }
        catch (Exception ex)
        {
            
            Response.Write("Insert Error:");
            conn.Close();
        }
        finally
        {
            
            conn.Close();
        }
    }
}