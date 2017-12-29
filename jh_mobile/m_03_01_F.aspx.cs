using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class m_03_01_F : System.Web.UI.Page
{
    string v_sql,v_machine,v_sdate,v_edate;
    thomas_Conn tsConn = new thomas_Conn();
    thomas_function tsFun = new thomas_function();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (tsFun.chk_verify())
        {
            //Server.Transfer("error.htm");
            Response.Redirect("error2.aspx", false);
            return;
        }

        L_message.Text = "";

        if ((Request.Params["SC_1"] != null) & (Request.Params["SC_1"] != ""))
        {
            v_machine = Request.Params["SC_1"];
            L_machine.Text = "機台:"+v_machine;
        }
        else
        {
            L_message.Text += "機台空白!";
            return;
        }

        if ((Request.Params["TB_sdate"] != null) & (Request.Params["TB_sdate"] != ""))
        {
            v_sdate = Request.Params["TB_sdate"];
            L_sdate.Text = "起訖日期:" + v_sdate;
        }
        else
        {
            L_message.Text += "開始日期空白!";
            return;
        }

        if ((Request.Params["TB_edate"] != null) & (Request.Params["TB_edate"] != ""))
        {
            v_edate = Request.Params["TB_edate"];
            L_edate.Text = "~" + v_edate;
        }
        else
        {
            L_message.Text += "結束日期空白!";
            return;
        }

        v_sql = " SELECT  SUBSTR(PDATE,6,5) 日期, M_GROUP 機台群組, MACHINEID 機台,  ";
        v_sql += "   PCS 生產支數, STND_PCS 標準支數, MP_RATE 生產效率,  ";
        v_sql += "   LOTS 批數, WORK_TIME 稼動工時, DOWN_TIME 停機時間,  ";
        v_sql += "   DEDUCTIBLE_TIME 可扣時間, STNDRD_RATE 達成率 ";
        v_sql += "FROM HRP_EMP_CAPACITY_DETAIL ";
        //v_sql += "WHERE MACHINEID = NVL('" + v_machine + "',MACHINEID) ";
        v_sql += "WHERE M_GROUP IN (SELECT  M_GROUP FROM HRP_MACHINE_SET WHERE M_MACHINEID LIKE '" + v_machine + "%') ";
        v_sql += "AND PDATE BETWEEN  '" + v_sdate + "' AND '" + v_edate + "' ";
        v_sql += "ORDER BY PDATE,MACHINEID";

        SDS_f.SelectCommand = v_sql;
    }
}