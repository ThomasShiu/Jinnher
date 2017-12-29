using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;

public partial class GM_demo : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                CreateAnno();

                //string[] v_weather = tsfun.getWaeDataSet("高雄");
                //Weather_icon.ImageUrl = @"images\weather\" + v_weather[8].ToString();
                //string v_weastr = v_weather[7].ToString() + "," + v_weather[5].ToString();
                //L_weather.Text = Strings.StrConv(v_weastr, VbStrConv.TraditionalChinese, 2052);

                DDL_year.Items.Clear();
                DDL_month.Items.Clear();
                //產生年份下拉選單
                for (int i = 1990; i <= (int)(DateTime.Now.Year); i++)
                {
                    DDL_year.Items.Add(i.ToString());

                }
                //產生月份下拉選單
                for (int i = 1; i <= 12; i++)
                {
                    DDL_month.Items.Add((i < 10 ? "0" + i.ToString() : i.ToString()));
                    DDL_monthE.Items.Add((i < 10 ? "0" + i.ToString() : i.ToString()));
                }
            }
            catch (Exception err)
            {
                string v_str = err.ToString();
            }
        }
    }

    protected void CreateAnno()
    {
        DS_anno.SelectCommand = "SELECT A.SN, A.POST_DATE, A.POST_TITTLE, A.CREATE_EMP, A.CREATE_DATE, A.BUSI_TYPE,B.DEP_NAME,NVL(C.EMP_NAME,'ADMIN') AS EMP_NAME " +
                                    "FROM ODS_JH_MESSAGE A,EAGLEHRM.HRS_DEPT B,EMP_MAST C  " +
                                    "WHERE A.BUSI_TYPE = B.DEP_NO(+)   " +
                                    "AND A.CREATE_EMP = C.EMP_NO(+) " +
                                    "ORDER BY A.SN DESC";
    }
    protected void B_qryincome_Click(object sender, EventArgs e)
    {
        string v_yearmonS = DDL_year.SelectedItem.ToString() + DDL_month.SelectedItem.ToString();
        string v_yearmonE = DDL_year.SelectedItem.ToString() + DDL_monthE.SelectedItem.ToString();
        DS_income.SelectCommand = "SELECT  YEARMONTH,INCOME, CREATE_DATE, CREATE_EMP " +
                                  "FROM ODS_JH_INCOME   " +
                                  "WHERE YEARMONTH BETWEEN '" + v_yearmonS + "' AND '" + v_yearmonE + "' " +
                                  "ORDER BY YEARMONTH ";
        DS_income.DataBind();
    }
}