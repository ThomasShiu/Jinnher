using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using System.Data.Common;
using Newtonsoft.Json;

public partial class SetUploadVer : System.Web.UI.Page
{
    string Sql_str, v_sql;
    thomas_Conn tsconn = new thomas_Conn();
    OracleConnection conn;
    OracleCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //填入欄位
            BindTextDate();

            SDS_notification.SelectCommand = "SELECT  TITTLE, CONTENT,VERSION,DESCRIPT,APK,STATUS,CREATE_DATE FROM ( "+
                                            "SELECT  TITTLE, CONTENT,VERSION,DESCRIPT,APK,STATUS,CREATE_DATE FROM PDA_NOTIFICATION ORDER BY VERSION DESC "+
                                            ") WHERE ROWNUM <= 10 ";
            GridView1.DataBind();
        }
    }

    protected void BindTextDate()
    {
        Sql_str = " SELECT  TITTLE, CONTENT,VERSION,DESCRIPT,STATUS FROM PDA_NOTIFICATION WHERE STATUS = 'Y'  ";

        //建立連線
        //使用web.config conn string
        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jheip"].ConnectionString);
        conn.Open();

        OracleCommand cmd = new OracleCommand(Sql_str, conn);
        OracleDataReader dr = cmd.ExecuteReader();

        try
        {

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //塞資料到各欄位
                    TB_tittle.Text = dr[0].ToString();
                    TB_content.Text = dr[1].ToString();
                    TB_version.Text = dr[2].ToString();
                    //TB_descript.Text = dr[3].ToString().Replace("\n", Environment.NewLine);
                    TB_descript.Text = dr[3].ToString();
                    DDL_status.SelectedValue = dr[4].ToString();

                }
            }
            else { }
        }
        catch (Exception ex)
        {
            message.Text = ex.ToString();
        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        //更新現有版本狀態為停用 STATUS = N
        Sql_str = " UPDATE PDA_NOTIFICATION SET STATUS = 'N' WHERE STATUS = 'Y'  ";
        tsconn.trans_oracle("jheip", Sql_str, "update");

        upload_file();

        BindTextDate();

        SDS_notification.SelectCommand = "SELECT  TITTLE, CONTENT,VERSION,DESCRIPT,APK,STATUS,CREATE_DATE FROM ( " +
                                            "SELECT  TITTLE, CONTENT,VERSION,DESCRIPT,APK,STATUS,CREATE_DATE FROM PDA_NOTIFICATION ORDER BY VERSION DESC " +
                                            ") WHERE ROWNUM <= 10 ";
        GridView1.DataBind();
    }

    protected void upload_file()
    {
        //--註解：網站上的目錄路徑。所以不寫磁碟名稱（不寫 “實體”路徑）。
        //string saveDir = "c:\\上傳的目錄路徑\\";
        string saveDir = "download\\";
        string appPath = Request.PhysicalApplicationPath;
        string tempfileName = "";
        String fileExtension = "";
        System.Text.StringBuilder myLabel = new System.Text.StringBuilder();
        //如果事先宣告 using System.Text;
        //便可改寫成 StringBuilder myLabel = new StringBuilder();
        for (int i = 1; i <= Request.Files.Count; i++)
        {
            FileUpload myFL = new FileUpload();
            myFL = (FileUpload)Page.FindControl("FileUpload" + i);

            TextBox FileRemark = new TextBox();
            FileRemark = (TextBox)Page.FindControl("TB_remark" + i);

            if (myFL.HasFile)
            {
                fileExtension = System.IO.Path.GetExtension(myFL.FileName).ToLower(); //副檔名
                //string fileName = myFL.FileName;
                string fileName = myFL.FileName.Replace(fileExtension,"") + "_" + TB_version.Text.Replace(".","");
                string pathToCheck = appPath + saveDir + fileName + fileExtension;

                //===========================================(Start)
                if (System.IO.File.Exists(pathToCheck))
                {
                    int my_counter = 2;
                    while (System.IO.File.Exists(pathToCheck))
                    {
                        //--檔名相同的話，目前上傳的檔名（改成 tempfileName），
                        //  前面會用數字來代替。
                        tempfileName = fileName + "_" + my_counter.ToString() + fileExtension;
                        pathToCheck = appPath + saveDir + tempfileName;
                        my_counter = my_counter + 1;
                    }
                    fileName = tempfileName;
                    //message.Text += "<br>抱歉，您上傳的檔名發生衝突，檔名修改如下---- " + fileName;
                }
                //===========================================(End)                
                //-- 完成檔案上傳的動作。
                string savePath = appPath + saveDir + fileName + fileExtension;
                try
                {
                    myFL.SaveAs(savePath);
                }
                catch (Exception ex)
                {
                    message.Text += ex.ToString();
                }


                //紀錄至資料庫
                //v_sql = "INSERT INTO MIS_UPLOAD(SN, TYPE, REMARK, PATH, FILE_NAME,CREATE_DATE, CREATE_EMP, IDEN_CODE) ";
                //v_sql += "VALUES(MIS_UPLOAD_SEQ.NEXTVAL, :TYPE, :REMARK, :PATH, :FILE_NAME, SYSDATE, :CREATE_EMP, :IDEN_CODE) ";

                v_sql = "INSERT INTO PDA_NOTIFICATION(SN, TITTLE, CONTENT,VERSION, DESCRIPT,APK,CREATE_EMP,  STATUS) ";
                v_sql += "VALUES(PDA_NOTIFI_SEQ.NEXTVAL,:tittle,:content,:version,:descript,:apk,'100844','Y') ";
                try
                {
                    //建立連線
                    //使用web.config conn string
                    conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jheip"].ConnectionString);
                    conn.Open();

                }
                catch (Exception ex)
                {
                    message.Text += ex.Message;
                    //return v_return;
                }

                finally
                {
                }

                try
                {
                    ////string v_pw2 = get_Encrypt_pwd(v_pw);
                    cmd = new OracleCommand(v_sql, conn);

                    ////標題
                    cmd.Parameters.Add(new OracleParameter("tittle", OracleType.VarChar, 50));
                    cmd.Parameters["tittle"].Value = TB_tittle.Text;

                    ////內容簡介
                    cmd.Parameters.Add(new OracleParameter("content", OracleType.VarChar, 100));
                    cmd.Parameters["content"].Value = TB_content.Text;

                    ////版本
                    cmd.Parameters.Add(new OracleParameter("version", OracleType.VarChar, 10));
                    cmd.Parameters["version"].Value = TB_version.Text;

                    ////描述
                    cmd.Parameters.Add(new OracleParameter("descript", OracleType.VarChar, 1000));
                    cmd.Parameters["descript"].Value = TB_descript.Text;

                    ////APK
                    cmd.Parameters.Add(new OracleParameter("apk", OracleType.VarChar, 50));
                    cmd.Parameters["apk"].Value = fileName + fileExtension;


                    ////執行異動
                    cmd.ExecuteNonQuery();

                    message.Text += "世宇斑馬更新完成!";

                    //紀錄LOG
                    //string strClientIP = Context.Request.ServerVariables["REMOTE_ADDR"];
                    //ts_conn.save_log(v_str1[0].ToString() + v_str1[1].ToString(), "MIS_applyEdit", strClientIP, v_str1[1].ToString() + "修改資訊需求單：" + TB_sn.Text);
                }
                catch (Exception ex)
                {
                    message.Text += ex.ToString();
                }
                finally
                {
                    cmd.Dispose();
                    
                    conn.Dispose();
                    conn.Close();
                }

            }
        }

        
    }
}