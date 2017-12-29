using System;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;
using System.Net;
//using WS_getweather;
/// <summary>
/// edaw_function 的摘要描述
/// </summary>
public class thomas_function
{

    public thomas_function()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    //上傳檔案
    public void Upload_file(FileUpload FileUpload1, string saveDir, string savePath, string srvPath,Label message1)
    {
        if (FileUpload1.FileName.ToString() == "")
        {
            return;
        }
        //saveDir = @"doc\";
        string v_filename = DateTime.Now.ToString("yyyyMMddhhmmssff");
        string v_str = FileUpload1.PostedFile.FileName;
        string v_exten ;
        try
        {
            v_exten = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);

            //上傳檔案
            // message1.Text = "";

            if (FileUpload1.HasFile && (FileUpload1.FileBytes.Length < 4096000))
            {
                //savePath = (System.Web.HttpRequest.Request.PhysicalApplicationPath
                //           + (saveDir + Server.HtmlEncode(v_filename + v_exten)));
                //srvPath = (saveDir + Server.HtmlEncode(v_filename + v_exten));
                try
                {
                    FileUpload1.SaveAs(savePath);
                    message1.Text += "上傳檔案完成。";
                }
                catch (Exception ex)
                {
                    message1.Text = ex.Message;
                }
            }
            else
            {
                message1.Text += "檔案上傳錯誤。";
            }

        }
        catch (Exception ex)
        {
            message1.Text = ex.ToString();
        }
    }
    //判斷身分證字號
    public bool CheckIdentificationId(string Input_ID)
    {
        bool IsTrue = false;
        if (Input_ID.Length == 10)
        {
            Input_ID = Input_ID.ToUpper();
            if (Input_ID[0] >= 0x41 && Input_ID[0] <= 0x5A)
            {
                int[] Location_No = new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35, 23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33 };
                int[] Temp = new int[11];
                Temp[1] = Location_No[(Input_ID[0]) - 65] % 10;
                int Sum = Temp[0] = Location_No[(Input_ID[0]) - 65] / 10;
                for (int i = 1; i <= 9; i++)
                {
                    Temp[i + 1] = Input_ID[i] - 48;
                    Sum += Temp[i] * (10 - i);
                }
                if (((Sum % 10) + Temp[10]) % 10 == 0)
                {
                    IsTrue = true;
                }
            }
        }
        return IsTrue;
    }

    //取得首頁位置
    public string get_index_page()
    {
        return "http://www.edaworld.com.tw";
    }

    //送出申請會員
    public bool apply_member(Page p_join_member)
    {
        p_join_member = new Page();
        TextBox L_username=(TextBox)(p_join_member.FindControl("T_username"));

        return true;
    }

    //Email function 
    public void  Send_Mail(string T_email, string v_body,string v_subject)
    {
        // 's_from 發件箱地址
        //'pwd 發件箱密碼
        //' s_to 收件箱地主之誼
        //'m_title 郵件主題
        //'m_body 郵件內容
        //'m_file 附件
        MailAddress from;
        MailAddress mto;
        MailMessage mailobj;


            string[] v_mailto = { T_email, "k0095@edd.e-united.com.tw" };

            //string[] v_mailto = { "skylark@edaskylark.com.tw","k0095@edd.e-united.com.tw" };

            // '构建SmtpClient对象
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("service", "p@ssw0rd");
            smtp.Host = "smtp.edamall.com.tw";
            //SmtpClient smtp = new SmtpClient("msa.hinet.net");
            //smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            for (int i = 0; i < v_mailto.Count(); i++)
            {
                // '构建MailMessage对象

                from = new MailAddress("義大世界-客服中心<service@edaworld.com.tw>");// '發件箱地址
                mto = new MailAddress(v_mailto[i]);// '收件箱地址
                mailobj = new MailMessage(from, mto);

                // '完善MailMessage对象
                mailobj.Subject = v_subject; // " 義大世界-會員資料已確認"; //'主題
                //string v_body = "http://www.edaworld.com.tw/member/init_member.aspx?initcode=" + v_init_code + "&id=" + T_id.Text;
                //mailobj.Body = T_cname.Text + "  您好:<br><br>請進入底下位址，<a href='" + v_body + "'>啟動帳號</a><br><br><font size='2px' color='#FF0000'>此mail由系統發送，請勿直接回覆。如有問題請洽系統管理員</font>";// '郵件內容

                //mailobj.Body = "test";
                mailobj.Body = v_body;
                mailobj.IsBodyHtml = true;//
                mailobj.BodyEncoding = System.Text.Encoding.GetEncoding("BIG5");
                mailobj.Priority = System.Net.Mail.MailPriority.Normal;

                //if (m_file.Trim() != "")// '附件
                //{
                //    mailobj.Attachments.Add(new System.Net.Mail.Attachment(m_file));
                //}

                smtp.Send(mailobj);
            }
            //return "已發送E-mail通知!";

     
    }

    public bool CheckBrithDay(string v_brith)
    {
        DateTime v_brithday = Convert.ToDateTime(v_brith);
        //系統日期
        DateTime v_sysdate = DateTime.Today;
        //比對生日不可大於系統日
        if (v_brithday >= v_sysdate)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    public string GetMD5(string original)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] b = md5.ComputeHash(Encoding.ASCII.GetBytes(original));
        return BitConverter.ToString(b).Replace("-", string.Empty);
    }

    //隨機亂數
    public string GenerateCheckCode(int VcodeNum)
    {
        int number;
        char code;
        string checkCode = String.Empty;

        System.Random random = new Random();

        for (int i = 0; i < VcodeNum; i++)
        {
            number = random.Next();

            if (number % 2 == 0)
                code = (char)('0' + (char)(number % 10));
            else
                code = (char)('A' + (char)(number % 26));

            checkCode += code.ToString();
        }

        return checkCode;
    }

    //過濾特殊符號
    public string Trim_string(string v_string)
    {
        v_string = v_string.Trim().Replace("%","").Replace("'", "").Replace("-", "").Replace("@", "").Replace("/", "").Replace("\\", "");
        return v_string;

    }

    //匯出EXCEL
    public void export_excel(string v_verion, SqlDataSource v_sds, string v_Sqlcmd)
    {
        //匯出excel檔 
        HttpContext.Current.Response.Clear();


        //Response.ContentType = "application/vnd.ms-excel";

        switch (v_verion)
        {
            case "2003":
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + DateTime.Today.ToString("yyyyMMdd") + "_SalesAnalyze.xls");//excel檔名
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                break;
            case "2007":
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + DateTime.Today.ToString("yyyyMMdd") + "_SalesAnalyze.xlsx");//excel檔名
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                break;
            default:
                break;
        }

        HttpContext.Current.Response.Charset = "";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
        DataGrid dg = new DataGrid();

        v_sds.SelectCommand = v_Sqlcmd;
        v_sds.DataBind();

        dg.DataSource = v_sds.Select(DataSourceSelectArguments.Empty);
        dg.DataBind();
        dg.RenderControl(htw);
        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.End();
    }

    public void export_excel2(GridView v_GridView,string Ex_fileName)
    {

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + Ex_fileName + DateTime.Today.ToString("yyyyMMdd")+ ".xls");
        HttpContext.Current.Response.Charset = "big5";

        HttpContext.Current.Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        v_GridView.RenderControl(htmlWrite); //<--非GridView1請更改為要輸出名稱

        HttpContext.Current.Response.Write(stringWrite.ToString());
        HttpContext.Current.Response.End();
    }

    //日期換算星期
    public  string CaculateWeekDay(int y, int m, int d)
    {
        if (m == 1) m = 13;
        if (m == 2) m = 14;
        int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7 + 1;
        string weekstr = "";
        switch (week)
        {
            case 1: weekstr = "星期一"; break;
            case 2: weekstr = "星期二"; break;
            case 3: weekstr = "星期三"; break;
            case 4: weekstr = "星期四"; break;
            case 5: weekstr = "星期五"; break;
            case 6: weekstr = "星期六"; break;
            case 7: weekstr = "星期日"; break;
        }

        return weekstr;
    }

    //GridView 列合併
    public void GVRowSpan(System.Web.UI.Control Parent, string GridVewName, int[] RowIndex, Boolean SelfColor, System.Drawing.Color[] myColors)
    {
        
        string tmpS = "";
        int tmpI = 0;
        GridView Gv = (GridView)(this.FindControlEx(Parent, GridVewName));
        int ColorCnt = myColors.Length;
        int ColorIdx = 0;
        

        if (Gv != null)
        {
            foreach (GridViewRow mySingleRow in Gv.Rows)
            {
                if (mySingleRow.RowIndex == 0)
                {
                    tmpS = mySingleRow.Cells[RowIndex[0]].Text.Trim();
                    for (int y = 0; y <= RowIndex.Length -1 ; y++)
                    {
                        mySingleRow.Cells[RowIndex[y]].RowSpan = 1;
                    }

                    tmpI = 0;

                    if (SelfColor)
                    {
                        mySingleRow.BackColor = myColors[ColorIdx%ColorCnt];
                    }

                }

                if (mySingleRow.RowIndex > 0)
                {
                    //'判斷本筆資料是否與要合併資料一致
                    if (mySingleRow.Cells[RowIndex[0]].Text.Trim() == tmpS)
                    {
                        for (int y = 0; y <= RowIndex.Length - 1; y++)
                        {
                            //合併的Row的RowSpan+1
                            Gv.Rows[tmpI].Cells[RowIndex[y]].RowSpan += 1;
                            //被合併的這一個Row，Visiable=False
                            mySingleRow.Cells[RowIndex[y]].Visible = false;
                            if (SelfColor)
                            {
                                mySingleRow.BackColor = myColors[ColorIdx % ColorCnt];
                            }
                        }
                    }
                    else
                    {
                        tmpS = mySingleRow.Cells[RowIndex[0]].Text.Trim();
                        for (int y = 0; y <= RowIndex.Length - 1; y++)
                        {
                            mySingleRow.Cells[RowIndex[y]].RowSpan = 1;
                        }

                         tmpI = mySingleRow.RowIndex;
                         if (SelfColor)
                         {
                             mySingleRow.BackColor = myColors[ColorIdx % ColorCnt];
                         }

                    }

                }
            }
        }



    }

    public System.Web.UI.Control FindControlEx(System.Web.UI.Control Parent, string ID)
    {
        System.Web.UI.Control oCtrl = null;

        //先使用 FindControl 去尋找指定的子控制項

        oCtrl = Parent.FindControl(ID);

 

        //若尋找不到則往下層遞迴方式去尋找()
        if (oCtrl == null)
        {
            foreach (System.Web.UI.Control oChildCtrl in Parent.Controls)
            {
                oCtrl = FindControlEx(oChildCtrl, ID);
                if (oCtrl != null) { break; }
            }
        }
      

        return oCtrl;
    }

    //取得YAHOO天氣預測
    //public void get_weather(Image v_Img,Label v_Label)
    //{
    //    try
    //    {


    //        WebClient wc = new WebClient();
    //        //byte[] bt = wc.DownloadData(@"http://www.cwb.gov.tw/pda/week/South.htm");
    //         byte[] bt = wc.DownloadData(@"http://www.cwb.gov.tw/pda/forecast/week/South.htm");
    //        string res = Encoding.UTF8.GetString(bt);
    //        //res = Regex.Replace(res, @"<HTML>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
    //        //res = Regex.Replace(res, @"<HEAD>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
    //        //res = Regex.Replace(res, @"<link rel=""govtwsimpledc.xsd"" href=""http://www.gov.tw/schema/dc/"" title=""Dublin Core"" />", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
    //        //res = Regex.Replace(res, @"<meta name=""DC.Creator"" content=""交通部中央氣象局"" />.*? </table>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
    //        //string str_last_index,v_weather;
    //        string strRef2 = @System.DateTime.Now.Date.ToString("MM/dd");  //當前日期
    //        //string v_str = res.Substring(res.IndexOf(strRef2, 2800) + 70, 6).Trim().ToString();
    //        //string v_str = res.Substring(res.IndexOf(strRef2, 2800) - 79, 34).Trim().ToString();

    //        //string v_str = res.Substring(res.IndexOf(strRef2, 2800), 5).Trim().ToString();
    //        Int64 v_int = res.IndexOf(strRef2, 3200);
    //        //上午
    //        if ( res.IndexOf(strRef2, 3200) != -1)
    //        {
    //            if (System.DateTime.Now.DayOfWeek.ToString().Equals("Sunday") | System.DateTime.Now.DayOfWeek.ToString().Equals("Saturday"))
    //            {
    //                //str_last_index = "images/map_icon/" + res.Substring(res.IndexOf(strRef2, 2800) + 97, 8).Trim().ToString() + ".png";
    //                v_Img.ImageUrl = @"http://www.cwb.gov.tw/" + res.Substring(res.IndexOf(strRef2, 3200) - 78, 33).Trim().ToString();
    //                v_Label.Text = res.Substring(res.IndexOf(strRef2, 3200) + 67, 29).Trim().Replace("天氣：", "").Replace("<br />溫度：", "。").ToString();
                    
    //            }
    //            else
    //            {
    //                v_Img.ImageUrl = @"http://www.cwb.gov.tw/" + res.Substring(res.IndexOf(strRef2, 3200) - 78, 33).Trim().ToString();
    //                v_Label.Text = res.Substring(res.IndexOf(strRef2, 3200) + 67, 29).Trim().Replace("天氣：", "").Replace("<br />溫度：", "。").ToString();
                    
    //            }
    //        }
    //        else //下午
    //        {
    //            strRef2 = @System.DateTime.Now.Date.AddDays(1).ToString("MM/dd");
    //            string v_1 = System.DateTime.Now.AddDays(1).DayOfWeek.ToString();

    //            if (System.DateTime.Now.AddDays(1).DayOfWeek.ToString().Equals("Sunday") | System.DateTime.Now.AddDays(1).DayOfWeek.ToString().Equals("Saturday"))
    //            {
    //                v_Img.ImageUrl = @"http://www.cwb.gov.tw/" + res.Substring(res.IndexOf(strRef2, 3200) - 78, 33).Trim().ToString();
    //                v_Label.Text = res.Substring(res.IndexOf(strRef2, 3200) + 40, 29).Trim().Replace("天氣：", "").Replace("<br />溫度：", "。").ToString();
                    
    //            }
    //            else
    //            {
    //                v_Img.ImageUrl = @"http://www.cwb.gov.tw/" + res.Substring(res.IndexOf(strRef2, 3200) - 78, 33).Trim().ToString();
    //                v_Label.Text = res.Substring(res.IndexOf(strRef2, 3200) + 40, 29).Trim().Replace("天氣：", "").Replace("<br />溫度：", "。").ToString();
                    
    //            }
    //        }
    //    }
    //    catch
    //    {
    //       v_Img.ImageUrl= "/images/map_icon/symbol10.png";
    //    }

    //}

    public bool chk_verify()
    {

        String v_this_year = DateTime.Now.ToString("yyyyMMdd");

        if (Convert.ToDecimal(v_this_year) >= 20990808)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //存取對岸的天氣預測
    //public string[] getWaeDataSet(string v_cityname)
    //{
    //    WeatherWebService.WeatherWebServiceSoapClient ws = new WeatherWebService.WeatherWebServiceSoapClient();

    //    string[] v_waether = ws.getWeatherbyCityName(v_cityname);

    //    return v_waether;

    //}

}
