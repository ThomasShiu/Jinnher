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
    public partial class Excel2PDF : Form
    {
        
        public Excel2PDF()
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
            CopyExcel(tB_path.Text, "Heat_ReachRate_temp.xls", "Heat_ReachRate.xls");  //複製excel樣板
            ExportXLS(tB_path.Text);  //匯出資料到Excel
            Thread.Sleep(600);
            ExportXLS3(tB_path.Text);  //匯出資料到Excel
            AutoExportWorkbookToPDF(tB_path.Text); //轉換PDF
            Thread.Sleep(600);
            CopyExcel(tB_path.Text, "Heat_ReachRate.pdf", "熱處理達成率報表.pdf");  //複製excel樣板
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
            string rpt = vPath + "\\Heat_ReachRate.xls";
            string ts_conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + rpt + ";Extended Properties=Excel 8.0;";
            OleDbConnection dbConnDest = new OleDbConnection(ts_conn);
            try
            {
                dbConnDest.Open();
            }
            catch(Exception){
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
                cmdDest.CommandText = "INSERT INTO [reportHeader](reportHeader) VALUES('熱處理達成率')";
                cmdDest.ExecuteNonQuery();
                cmdDest.CommandText = "INSERT INTO [exportDate](exportDate) VALUES('匯出日期:" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "')";
                cmdDest.ExecuteNonQuery();

                //以下填寫EXCEL檔的表頭
                //cmdDest.CommandText = "INSERT INTO [reportHeader2](reportHeader2) VALUES('熱處理預測產量')";
                //cmdDest.ExecuteNonQuery();

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
                        cmdDest.CommandText = "INSERT INTO [rowData]([workDate],[quarter],[week],[weekStr],[process],[machineID],[empCnt],[workTime]";
                        cmdDest.CommandText += " ,[standWt],[realWt],[standRate],[class1],[class2],[class3]) ";
                        cmdDest.CommandText += " VALUES('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "',";
                        cmdDest.CommandText += " '" + dr[5].ToString() + "','" + dr[6].ToString() + "','" + dr[7].ToString() + "','" + dr[8].ToString() + "','" + dr[9].ToString() + "','" + dr[10].ToString() + "',";
                        cmdDest.CommandText += " '" + dr[11].ToString() + "','" + dr[12].ToString() + "','" + dr[13].ToString() + "') ";
                        cmdDest.ExecuteNonQuery();
                    }

                    cmdDest2 = new OleDbCommand(getSqlstr("2"), dbConnDest2);
                    dr = cmdDest2.ExecuteReader();
                    if (dr.Read())
                    {
                        cmdDest.CommandText = "INSERT INTO [ttlRowData]([standTTLWT],[realTTLWT],[finishRate],[TTLClass1],[TTLClass2],[TTLClass3])";
                        cmdDest.CommandText += "  VALUES('" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "','" + dr[5].ToString() + "','" + dr[6].ToString() + "','" + dr[7].ToString() + "') ";
                        cmdDest.ExecuteNonQuery();
                    }

                    cmdDest2 = new OleDbCommand(getSqlstr("3"), dbConnDest2);
                    dr = cmdDest2.ExecuteReader();

                    while (dr.Read())
                    {
                        //以下填寫一筆資料
                        cmdDest.CommandText = "INSERT INTO [rowData2]([workDate2],[quarter2],[week2],[weekStr2],[process2],[machineID2],[empCnt2],[workTime2],[standWt2]) ";
                        cmdDest.CommandText += " VALUES('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "',";
                        cmdDest.CommandText += " '" + dr[5].ToString() + "','" + dr[6].ToString() + "','" + dr[7].ToString() + "','" + dr[8].ToString() + "') ";
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


        private void ExportXLS3(string vPath)
        {
            string rpt = vPath + "\\Heat_ReachRate.xls";
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
                cmdDest.CommandText = "INSERT INTO [reportHeader3](reportHeader) VALUES('熱處理問題與檢討')";
                cmdDest.ExecuteNonQuery();
                cmdDest.CommandText = "INSERT INTO [exportDate3](exportDate) VALUES('匯出日期:" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "')";
                cmdDest.ExecuteNonQuery();

                //建立連線
                dbConnDest2 = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jh815Connection);
                dbConnDest2.Open();


                try
                {
                    cmdDest2 = new OleDbCommand(getSqlstr("5"), dbConnDest2);
                    dr = cmdDest2.ExecuteReader();

                    while (dr.Read())
                    {
                        //以下填寫一筆資料
                        cmdDest.CommandText = "INSERT INTO [rowData3]([workDate],[weekStr],[process],[machineID],[formTime],[toTime],[reason],[review]) ";
                        cmdDest.CommandText += " VALUES('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "',";
                        cmdDest.CommandText += " '" + dr[5].ToString() + "','" + dr[6].ToString() + "','" + dr[7].ToString() + "') ";
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
                //if (fileInfo.Extension.Equals(".xls"))
                if (fileInfo.FullName.Contains("Heat_ReachRate.xls"))
                {
                    ExportWorkbookToPDF(oldFileName, oldFileName.Replace(".xls", ".pdf"));
                }
                //Application.DoEvents();
                Thread.Sleep(2000);


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
                //檔名不一樣才更改檔名
                //if (oldFileName.IndexOf(strSearch) != -1)
                //{
                //    string newFileName = oldFileName.Replace(strSearch, strReplace);

                //    //檔案更名
                //    fileInfo.MoveTo(newFileName);
                //}
            }

        }

        private  void ExportWorkbookToPDF(string workbook, string output)
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
                //richTextBox1.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ": "+e.Message +e.ToString()+ "\r\n";
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
                    v_sql = "SELECT TO_CHAR(PDATE,'YYYY/MM/DD') PDATE, WORK_QUARTER QUARTER, WORK_WEEK WORK_WEEK, ";
                    v_sql += "   WORK_WEEKSTR, PROCESS, MACHINEID ,  ";
                    v_sql += "   EMP_CNT , WORK_TIME ,  to_char(TARGET_WT,'999,999,999,999') TARGET_WT, to_char(REAL_WT,'999,999,999,999') REAL_WT, ";
                    v_sql += "   ROUND((REAL_WT/TARGET_WT)*100) FINISHRATE, ";
                    v_sql += "   to_char(SF_GET_WORKCLASS_WT(PROCESS,MACHINEID,'1',PDATE),'999,999,999,999') CLASS1, ";
                    v_sql += "   to_char(SF_GET_WORKCLASS_WT(PROCESS,MACHINEID,'2',PDATE),'999,999,999,999') CLASS2, ";
                    v_sql += "   to_char(SF_GET_WORKCLASS_WT(PROCESS,MACHINEID,'3',PDATE),'999,999,999,999') CLASS3, ";
                    v_sql += "   REMARK  ";
                    v_sql += "FROM HRP_PROCESS_CAPACITY ";
                    v_sql += "WHERE  TO_CHAR(PDATE,'YYYY/MM/DD') = TO_CHAR(SYSDATE-1,'YYYY/MM/DD') ";
                    v_sql += "AND PROCESS = 'H' ";
                    v_sql += "ORDER BY PDATE,MACHINEID ";
                    break;
                case "2":
                    v_sql = "SELECT SUM(EMP_CNT) EMP_CNT, SUM(WORK_TIME) WORK_TIME,  ";
                    v_sql += "       to_char(SUM(TARGET_WT),'999,999,999,999') TARGET_WT, to_char(SUM(REAL_WT),'999,999,999,999') REAL_WT, ";
                    v_sql += "   ROUND((SUM(REAL_WT)/SUM(TARGET_WT))*100) FINISH_RATE, ";
                    v_sql += "   to_char(SUM(SF_GET_WORKCLASS_WT(PROCESS,MACHINEID,'1',PDATE)),'999,999,999,999') CLASS1, ";
                    v_sql += "   to_char(SUM(SF_GET_WORKCLASS_WT(PROCESS,MACHINEID,'2',PDATE)),'999,999,999,999') CLASS2, ";
                    v_sql += "   to_char(SUM(SF_GET_WORKCLASS_WT(PROCESS,MACHINEID,'3',PDATE)),'999,999,999,999') CLASS3 ";
                    v_sql += "FROM HRP_PROCESS_CAPACITY ";
                    v_sql += "WHERE  TO_CHAR(PDATE,'YYYY/MM/DD') = TO_CHAR(SYSDATE-1,'YYYY/MM/DD') ";
                    v_sql += "AND PROCESS = 'H' ";
                    break;
                case "3":
                    v_sql = "SELECT TO_CHAR(PDATE,'YYYY/MM/DD') PDATE, WORK_QUARTER QUARTER, WORK_WEEK WORK_WEEK, ";
                    v_sql += "   WORK_WEEKSTR, PROCESS, MACHINEID ,  ";
                    v_sql += "   EMP_CNT , WORK_TIME , to_char(TARGET_WT,'999,999,999,999') TARGET_WT ";
                    v_sql += "FROM HRP_PROCESS_CAPACITY ";
                    v_sql += "WHERE  TO_CHAR(PDATE,'YYYY/MM/DD') = TO_CHAR(SYSDATE,'YYYY/MM/DD') ";
                    v_sql += "AND PROCESS = 'H' ";
                    v_sql += "ORDER BY PDATE,MACHINEID ";
                    break;
                case "4":
                    v_sql = "SELECT SUM(EMP_CNT) EMP_CNT, SUM(WORK_TIME) WORK_TIME,  SUM(TARGET_WT) TARGET_WT, SUM(REAL_WT) REAL_WT, ";
                    v_sql += "   ROUND((SUM(REAL_WT)/SUM(TARGET_WT))*100) FINISH_RATE, ";
                    v_sql += "  SUM(SF_GET_WORKCLASS_WT(PROCESS,MACHINEID,'1',PDATE)) CLASS1, ";
                    v_sql += "   SUM(SF_GET_WORKCLASS_WT(PROCESS,MACHINEID,'2',PDATE)) CLASS2, ";
                    v_sql += "   SUM(SF_GET_WORKCLASS_WT(PROCESS,MACHINEID,'3',PDATE)) CLASS3 ";
                    v_sql += "FROM HRP_PROCESS_CAPACITY ";
                    v_sql += "WHERE  TO_CHAR(PDATE,'YYYY/MM/DD') = TO_CHAR(SYSDATE,'YYYY/MM/DD') ";
                    v_sql += "AND PROCESS = 'H' ";
                    break;

                case "5":
                    v_sql = "SELECT  TO_CHAR(PDATE,'YYYY/MM/DD') PDATE, REPLACE(TO_CHAR(PDATE,'DAY'),'星期') WEEK_STR,PROCESS,MACHINEID,   ";
                    v_sql += "   TO_CHAR(FORM_TIME,'MM/DD HH24:MI') FORM_TIME, TO_CHAR(TO_TIME,'MM/DD HH24:MI') TO_TIME, REASON,   REVIEW ";
                    v_sql += "FROM HRP_PROCAPACITY_REVIEW ";
                    v_sql += "WHERE PDATE = TRUNC(SYSDATE-2) ";
                    v_sql += "AND PROCESS = 'H' ";
                    v_sql += "ORDER BY PDATE,MACHINEID,PROCESS,FORM_TIME ";
                    break;

            }
            return v_sql;
        }

    }
}
