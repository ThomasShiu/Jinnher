using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Data.OleDb;
using System.Configuration;

namespace AUTO_SCHE
{
    public partial class Excel2PDF_P : Form
    {
        public Excel2PDF_P()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(RunThread);
            t.IsBackground = true;
            t.Start();
        }

        private delegate void myUICallBack(string myStr, Control ctl);
        private void myUI(string myStr, Control ctl)
        {
            if (this.InvokeRequired)
            {
                myUICallBack myUpdate = new myUICallBack(myUI);
                this.Invoke(myUpdate, myStr, ctl);
            }
            else
            {
                ctl.Text += myStr;
            }
        }

        public void RunThread()
        {
            CopyExcel(tB_path.Text, "PLT_FEED_PERFORMANCE_temp.xls", "PLT_FEED_PERFORMANCE.xls");  //複製excel樣板
            ExportXLS(tB_path.Text);  //匯出資料到Excel
            Thread.Sleep(600);
            //ExportXLS3(tB_path.Text);  //匯出資料到Excel
            AutoExportWorkbookToPDF(tB_path.Text); //轉換PDF
            Thread.Sleep(600);
            CopyExcel(tB_path.Text, "PLT_FEED_PERFORMANCE.pdf", "電鍍倒料績效.pdf");  //複製excel樣板
        }

        private void CopyExcel(string vPath, string sFile, string tFile)
        {
            //string sFile = "熱處理達成率報表_temp.xls";
            //string tFile = "熱處理達成率報表.xls";
            //string sourcePath = @"D:\Excel2PDF";
            //string targetPath = @"D:\Excel2PDF";

            string sourceFile = System.IO.Path.Combine(vPath, sFile);
            string destFile = System.IO.Path.Combine(vPath, tFile);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(vPath))
            {
                //System.IO.Directory.CreateDirectory(vPath);
                return;
            }

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(sourceFile, destFile, true);
        }

        private void ExportXLS(string vPath)
        {
            string rpt = vPath + "\\PLT_FEED_PERFORMANCE.xls";
            string ts_conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + rpt + ";Extended Properties=Excel 8.0;";
            OleDbConnection dbConnDest = new OleDbConnection(ts_conn);
            try
            {
                dbConnDest.Open();
            }
            catch (Exception)
            {
                myUI(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ": 建立檔案失敗。System.Data.OleDb.OleDbException: 建立檔案失敗。\r\n", richTextBox1);
                return;
            }
            OleDbCommand cmdDest = new OleDbCommand();
            cmdDest.Connection = dbConnDest;
            OleDbConnection dbConnDest2;
            OleDbCommand cmdDest2;
            OleDbDataReader dr;
            try
            {
                //以下填寫EXCEL檔的表頭
                cmdDest.CommandText = "INSERT INTO [exportDate](exportDate) VALUES('匯出日期:" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "')";
                cmdDest.ExecuteNonQuery();
                cmdDest.CommandText = "INSERT INTO [exportDate2](exportDate2) VALUES('匯出日期:" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "')";
                cmdDest.ExecuteNonQuery();
                cmdDest.CommandText = "INSERT INTO [exportDate3](exportDate3) VALUES('匯出日期:" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "')";
                cmdDest.ExecuteNonQuery();
                cmdDest.CommandText = "INSERT INTO [exportDate4](exportDate4) VALUES('匯出日期:" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "')";
                cmdDest.ExecuteNonQuery();
                cmdDest.CommandText = "INSERT INTO [exportDate5](exportDate5) VALUES('匯出日期:" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "')";
                cmdDest.ExecuteNonQuery();

                //建立連線
                dbConnDest2 = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jh815Connection);
                dbConnDest2.Open();

                try
                {
                    cmdDest2 = new OleDbCommand(getSqlstr("1"), dbConnDest2);
                    dr = cmdDest2.ExecuteReader();
                    while (dr.Read())
                    {
                        //以下填寫一筆資料
                        cmdDest.CommandText = "INSERT INTO [rowData]([MACHINEID],[CLA],[PDATE],[DEF_WORK_HOURS],[DEF_CNT],[DEF_ROLL_CNT],[DEF_AROLL_WT],[DEF_PRD_WT],[CNT],[EXECTLY_WORK_HOURS],[EXECTLY_ROLL_CNT],[PERFORMANCE],[NET_WT]) ";
                        cmdDest.CommandText += " VALUES('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "',";
                        cmdDest.CommandText += " '" + dr[5].ToString() + "','" + dr[6].ToString() + "','" + dr[7].ToString() + "','" + dr[8].ToString() + "','" + dr[9].ToString() + "','" + dr[10].ToString() + "','" + dr[11].ToString() + "','" + dr[12].ToString() + "' ) ";
                        cmdDest.ExecuteNonQuery();

                    }

                    cmdDest2 = new OleDbCommand(getSqlstr("2"), dbConnDest2);
                    dr = cmdDest2.ExecuteReader();
                    while (dr.Read())
                    {
                        //以下填寫一筆資料
                        cmdDest.CommandText = "INSERT INTO [rowData2]([pdate],[machineid],[CLA],[def_work_hours],[exectly_work_hours],[def_roll_cnt],[exectly_roll_cnt],[performance],[net_wt]) ";
                        cmdDest.CommandText += " VALUES('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "',";
                        cmdDest.CommandText += " '" + dr[5].ToString() + "','" + dr[6].ToString() + "','" + dr[7].ToString() + "','" + dr[8].ToString() + "' ) ";
                        cmdDest.ExecuteNonQuery();
                        
                    }

                    cmdDest2 = new OleDbCommand(getSqlstr("3"), dbConnDest2);
                    dr = cmdDest2.ExecuteReader();
                    while (dr.Read())
                    {
                        //以下填寫一筆資料
                        cmdDest.CommandText = "INSERT INTO [rowData3]([pdate],[machineid],[CAUSE_NAME],[HMS1],[HMS2],[ttl_dt_mins] ) ";
                        cmdDest.CommandText += " VALUES('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "','" + dr[5].ToString() + "' ) ";
                        cmdDest.ExecuteNonQuery();
                    }

                    cmdDest2 = new OleDbCommand(getSqlstr("4"), dbConnDest2);
                    dr = cmdDest2.ExecuteReader();
                    while (dr.Read())
                    {
                        //以下填寫一筆資料
                        cmdDest.CommandText = "INSERT INTO [rowData4]([PDATE],[CLA],[MACHINEID],[performance],[SOLUTION]) ";
                        cmdDest.CommandText += " VALUES('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "') ";
                        cmdDest.ExecuteNonQuery();
                    }

                    cmdDest2 = new OleDbCommand(getSqlstr("5"), dbConnDest2);
                    dr = cmdDest2.ExecuteReader();
                    while (dr.Read())
                    {
                        //以下填寫一筆資料
                        cmdDest.CommandText = "INSERT INTO [rowData5]([Machineid],[Pdate],[Def_Roll_Cnt],[DEF_AROLL_WT],[CNT],[Real_hour],[Real_keg],[Perform],[Net_Wt]) ";
                        cmdDest.CommandText += " VALUES('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "', ";
                        cmdDest.CommandText += "        '" + dr[5].ToString() + "','" + dr[6].ToString() + "','" + dr[7].ToString() + "','" + dr[8].ToString() + "') ";
                        cmdDest.ExecuteNonQuery();
                    }
                   
                }
                catch (Exception e)
                {
                    //richTextBox1.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ": " + e.Message + e.ToString() + "\r\n";
                    myUI(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ": " + e.Message + e.ToString() + "\r\n", richTextBox1);
                }
                finally
                {
                    dbConnDest2.Close();
                }
                //richTextBox1.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ": 資料已匯出\r\n";
                myUI(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ": 資料已匯出\r\n", richTextBox1);
            }
            catch (Exception e)
            {
                //richTextBox1.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ": " + e.Message + e.ToString() + "\r\n";
                myUI(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ": " + e.Message + e.ToString() + "\r\n", richTextBox1);
                //throw;
            }
            finally
            {
                dbConnDest.Close();
                GC.Collect();
            }

        }

        private void AutoExportWorkbookToPDF(string path)
        {
            //new一個目標目錄的DirectoryInfo
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            //防呆，確認目標目錄確實存在
            if (!dirInfo.Exists)
            {
                //richTextBox1.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":指定目錄不存在!\r\n";
                myUI(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":指定目錄不存在!\r\n", richTextBox1);
                //MessageBox.Show("Diretory not exist!");
                return;
            }
            //dirInfo.GetFiles()取回目錄下檔案清單(FileInfo陣列)
            FileInfo[] files = dirInfo.GetFiles();
            foreach (FileInfo fileInfo in files)
            {
                //foreach檢查每一個FileInfo檔名，搜尋並取代檔名。
                string oldFileName = fileInfo.FullName;
                if (fileInfo.FullName.Contains("PLT_FEED_PERFORMANCE.xls"))
                {
                    ExportWorkbookToPDF(oldFileName, oldFileName.Replace(".xls", ".pdf"));
                }
                //Application.DoEvents();
                Thread.Sleep(500);


                //轉檔完就刪除
                if (fileInfo.Extension.Equals(".xls"))
                {
                    try
                    {
                        //fileInfo.Delete();
                        //Thread.Sleep(2000);
                        //richTextBox1.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":已刪除Excel檔:" + oldFileName + "。\r\n";
                    }
                    catch (Exception)
                    {
                        //richTextBox1.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ": " + e.Message + e.ToString() + "\r\n";
                    }
                }
               
            }

        }

        private void ExportWorkbookToPDF(string workbook, string output)
        {
            XlFixedFormatType targetType = XlFixedFormatType.xlTypePDF;
            object missing = Type.Missing;
            ApplicationClass excelApplication = null;
            Workbook excelWorkbook = null;

            if (string.IsNullOrEmpty(workbook) || string.IsNullOrEmpty(output))
            {
                throw new NullReferenceException("Cannot create PDF copy " +
                    "from empty workbook.");
            }

            try
            {
                excelApplication = new ApplicationClass();
                excelWorkbook = excelApplication.Workbooks.Open(@workbook);

                //excelApplication.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

                // Zoom property must be false, otherwise the "FitToPages" properties 
                // are ignored.
                //excelWorkbook.PageSetup.Zoom = false;

                // these set the number of pages tall or wide the worksheet will be 
                // scaled to when printed.
                //excelWorkbook.PageSetup.FitToPagesTall = 1;
                //excelWorkbook.PageSetup.FitToPagesWide = 1;
                //excelWorkbook.ExportAsFixedFormat(targetType, Path.GetDirectoryName(@"d:\temp\test.pdf"));
                excelWorkbook.ExportAsFixedFormat(targetType, @output);
                //richTextBox1.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":已匯出PDF檔:" + output + "。\r\n";
                myUI(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":已匯出PDF檔:" + output + "。\r\n", richTextBox1);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                //Console.ReadLine();
                excelWorkbook.Close(false, Type.Missing, Type.Missing);
                //richTextBox1.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ": " + e.Message + e.ToString() + "\r\n";
                myUI(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ": " + e.Message + e.ToString() + "\r\n", richTextBox1);
                //throw;
            }
            finally
            {

                //excelWorkbook.Close(true);
                //以下內容全都不能少！！
                excelWorkbook.Close(false, Type.Missing, Type.Missing);
                excelApplication.Workbooks.Close();
                excelApplication.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApplication);

                GC.Collect();

            }


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                timer1.Start();
                //richTextBox1.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":TIMER啟動\r\n";
                myUI(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":TIMER啟動\r\n", richTextBox1);
                //toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":TIMER啟動中，每10秒執行";
            }
            else
            {
                timer1.Stop();
                //richTextBox1.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":TIMER停止\r\n";
                myUI(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":TIMER停止\r\n", richTextBox1);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.ToString("HH:mm") == tB_hhmm.Text)
            {
                //CopyExcel(tB_path.Text, "PLT_FEED_PERFORMANCE_temp.xls", "PLT_FEED_PERFORMANCE.xls");  //複製excel樣板
                //ExportXLS(tB_path.Text);  //匯出資料到Excel
                //Thread.Sleep(600);
                ////ExportXLS3(tB_path.Text);  //匯出資料到Excel
                //AutoExportWorkbookToPDF(tB_path.Text); //轉換PDF
                //Thread.Sleep(600);
                //CopyExcel(tB_path.Text, "PLT_FEED_PERFORMANCE.pdf", "電鍍倒料績效.pdf");  //複製excel樣板
                Thread t = new Thread(RunThread);
                t.IsBackground = true;
                t.Start();
            }
            //每天午夜轉出LOG
            if (DateTime.Now.ToString("HHmm") == "2330")
            {
                richTextBox1.Text = "";

            }
        }


        private string getSqlstr(string v_mode)
        {
            string v_sql = "";

            switch (v_mode)
            {
                case "1":
                    v_sql = "select MACHINEID ,CLA ,PDATE ,nvl(DEF_WORK_HOURS,0)  DEF_WORK_HOURS,nvl(DEF_CNT,0) DEF_CNT , ";
                    v_sql += "      nvl(DEF_ROLL_CNT,0)  DEF_ROLL_CNT,nvl(DEF_AROLL_WT,0)  DEF_AROLL_WT,nvl(DEF_PRD_WT,0)  DEF_PRD_WT,  ";
                    v_sql += "      nvl(CNT,0)  CNT,nvl(EXECTLY_WORK_HOURS,0)  EXECTLY_WORK_HOURS,nvl(EXECTLY_ROLL_CNT,0) EXECTLY_ROLL_CNT , ";
                    v_sql += "      nvl(PERFORMANCE,0)  PERFORMANCE,nvl(NET_WT,0) NET_WT  ";
                    v_sql += "from v_plt_performance_v2 ";
                    v_sql += "where Machineid IN ('P01','P02','P03','P04')   AND substr(pdate,1,7) = to_char(SYSDATE,'YYYY/MM') ";
                    break;
                case "2":
                    v_sql = "select pdate , machineid , CLA ,nvl(def_work_hours,0) def_work_hours,nvl(exectly_work_hours,0)  exectly_work_hours, ";
                    v_sql += "       nvl(def_roll_cnt,0) def_roll_cnt,nvl(exectly_roll_cnt,0) exectly_roll_cnt,nvl(performance,0) performance,nvl(net_wt,0) net_wt ";
                    v_sql += "from v_plt_performance_v2 ";
                    v_sql += "where Machineid IN ('P01','P02','P03','P04')   AND pdate = to_char(SYSDATE-1,'YYYY/MM/DD') ";
                    break;
                case "3":
                    v_sql = "SELECT p.pdate,machineid , c.CAUSE_NAME,HMS1,HMS2,SUM(NVL(pmins,0)) ttl_dt_mins ";
                    v_sql += " FROM plt_runtime p , CAUSES c ";
                    v_sql += "WHERE p.pdate =to_char(SYSDATE-1,'YYYY/MM/DD') ";
                    v_sql += "AND c.CAT='P' ";
                    v_sql += "AND p.DOWNID =c.CAUSE_ID ";
                    v_sql += "GROUP BY p.pdate,machineid ,HMS1,HMS2, c.CAUSE_NAME ";
                    break;
                case "4":
                    v_sql = "select A.PDATE ,A.CLA ,A.MACHINEID ,B.performance , SOLUTION  ";
                    v_sql += "from wip_plt_improvement A,v_plt_performance_v2 B ";
                    v_sql += "where A.pdate=to_char(sysdate-1,'YYYY/MM/DD') ";
                    v_sql += "AND A.Machineid=B.Machineid AND A.Pdate=B.Pdate ";
                    v_sql += "AND A.Cla = B.CLA ";
                    break;
                case "5":
                    v_sql = "SELECT A.Machineid,A.Pdate,H.Def_Roll_Cnt,H.DEF_AROLL_WT,A.CNT ";
                    v_sql += "       ,(SELECT sum(round(floor((A.HMS2-A.HMS1)*24) + mod(floor((A.HMS2-A.HMS1)*24*60),60)/60, 1)) ";
                    v_sql += "         FROM wip_plt_finished A ";
                    v_sql += "         WHERE pdate=to_char(sysdate-1,'YYYY/MM/DD') AND machineid=H.Machineid) Real_hour ";
                    v_sql += "       ,CEIL(A.CNT / (SELECT sum(round(floor((A.HMS2-A.HMS1)*24) + mod(floor((A.HMS2-A.HMS1)*24*60),60)/60, 1)) ";
                    v_sql += "                      FROM wip_plt_finished A ";
                    v_sql += "                      WHERE pdate=to_char(sysdate-1,'YYYY/MM/DD') AND machineid=H.Machineid)) Real_keg  ";
                    v_sql += "       ,round(CEIL(A.CNT / (SELECT sum(round(floor((A.HMS2-A.HMS1)*24) + mod(floor((A.HMS2-A.HMS1)*24*60),60)/60, 1)) ";
                    v_sql += "                            FROM wip_plt_finished A ";
                    v_sql += "                            WHERE pdate=to_char(sysdate-1,'YYYY/MM/DD') AND machineid=H.Machineid))/H.Def_Roll_Cnt,2)*100 Perform      ";
                    v_sql += "       ,A.Net_Wt             ";
                    v_sql += " FROM wip_plt_work_hours A, HRP_PROCESS_CAPACITY H     ";
                    v_sql += " WHERE A.MACHINEID IN  ('S005','S006','S007','S008','S009')   ";
                    v_sql += " AND A.Machineid(+)=H.Machineid AND A.Pdate(+)=to_char(H.Pdate,'YYYY/MM/DD')  ";
                    v_sql += " AND A.pdate=to_char(sysdate-1,'YYYY/MM/DD') ";
                    v_sql += " ORDER BY A.Machineid,A.PDATE    ";
                    break;
            }
            
            return v_sql;
        }
    }
}
