using System;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;

namespace AUTO_SCHE
{
    public partial class HEAT_IMPORT_01 : Form
    {
        string v_datetime, v_sql;
        string v_path1, v_path2, v_path3, v_path4, v_path5, v_path6, v_path8, v_path9;
        static int v_counter;

        public HEAT_IMPORT_01()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'dataSet1.HEAT_BS_TEMP' 資料表。您可以視需要進行移動或移除。
            //this.hEAT_BS_TEMPTableAdapter.Fill(this.dataSet1.HEAT_BS_TEMP);
            if (Convert.ToDecimal(DateTime.Now.ToString("yyyyMMdd")) >= 20201224)
            {
                Application.Exit();
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            //啟動TIMER
            //timer1.Enabled = true;
            timer1.Start();
            richtb_status.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":TIMER啟動\r\n";
            toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":TIMER啟動中，每10秒執行";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MethodInvoker HEAT_mi = new MethodInvoker(Start_Jobs);
            this.BeginInvoke(HEAT_mi);
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            //停止TIMER
            //timer1.Enabled = false;
            timer1.Stop();
            richtb_status.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":TIMER停止\r\n";
            toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":TIMER停止中";
        }
     
        private void timer1_Tick(object sender, EventArgs e)
        {
            MethodInvoker HEAT_mi = new MethodInvoker(Start_Jobs);
            this.BeginInvoke(HEAT_mi);

            //Start_Jobs();
           
            //每天午夜轉出LOG
            if (DateTime.Now.ToString("HHmm") == "2350")
            {
                ExportLog(rTB_hbx1, "HBX1");
                ExportLog(rTB_hbx2, "HBX2");
                ExportLog(rTB_hbx3, "HBX3");
                ExportLog(rTB_hbx4, "HBX4");
                ExportLog(rTB_hbx5, "HBX5");
                ExportLog(rTB_hbx6, "HBX6");
                ExportLog(rTB_hbx8, "HBX8");
                ExportLog(rTB_hbx9, "HBX9");
            }
            //1.建立ParameterizedThreadStart委派
            //ParameterizedThreadStart myPar = new ParameterizedThreadStart(ImportTXT);
            //2.建立Thread 類別
            //Thread myThread01 = new Thread(new ParameterizedThreadStart(ImportTXT));
            //Thread myThread02 = new Thread(myPar);
            //3.啟動執行緒並帶入參數
            //myThread01.Start(tb_hbx1.Text);
           
            //myThread02.Start("我是多執行緒第二號");


        }

        //匯出LOG
        private void ExportLog(RichTextBox tTB,string v_HEAT)
        {
            //string log = @"C:\log.txt";
            string log = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\HEAT_log\\" + v_HEAT +"_"+ DateTime.Now.ToString("yyyyMMdd") + "_LOG.txt";
            using (FileStream fs = new FileStream(log, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(tTB.Text);
                    tTB.Text = "";
                }
            }
            tTB.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":已匯出LOG檔，請至程式根目錄底下查看。\n";
        }

        private void Start_Jobs()
        {
            ImportTXT(tb_hbx1.Text, rTB_hbx1);
            ImportTXT(tb_hbx2.Text, rTB_hbx2);
            ImportTXT(tb_hbx3.Text, rTB_hbx3);
            ImportTXT(tb_hbx4.Text, rTB_hbx4);
            ImportTXT(tb_hbx5.Text, rTB_hbx5);
            ImportTXT(tb_hbx6.Text, rTB_hbx6);
            ImportTXT(tb_hbx8.Text, rTB_hbx8);
            ImportTXT(tb_hbx9.Text, rTB_hbx9);
        }
        private void ImportTXT(string path, RichTextBox v_tb)
        {
            //string path = (string)arg;
            //v_tb.Text = "";

            //new一個目標目錄的DirectoryInfo
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            //防呆，確認目標目錄確實存在
            if (!dirInfo.Exists)
            {
                MessageBox.Show("Diretory not exist!");
                return;
            }

            //dirInfo.GetFiles()取回目錄下檔案清單(FileInfo陣列)
            FileInfo[] files = dirInfo.GetFiles();
            foreach (FileInfo fileInfo in files)
            {
                //foreach檢查每一個FileInfo檔名，搜尋並取代檔名。
                string oldFileName = fileInfo.FullName;
                heat_process(oldFileName, v_tb);
                Application.DoEvents();
                //Thread.Sleep(5000);

                
                //轉檔完就刪除
                
                //fileInfo.Delete();
                
                
                //檔名不一樣才更改檔名
                //if (oldFileName.IndexOf(strSearch) != -1)
                //{
                //    string newFileName = oldFileName.Replace(strSearch, strReplace);

                //    //檔案更名
                //    fileInfo.MoveTo(newFileName);
                //}
            }

        }

        private void StartGetTXT()
        {
            v_datetime = DateTime.Now.AddSeconds(-10).ToString("ddHHmmss"); //慢10秒抓取，以免時間差

            v_path1 = tb_hbx1.Text + v_datetime + ".txt";
            v_path2 = tb_hbx2.Text + v_datetime + ".txt";
            v_path3 = tb_hbx3.Text + v_datetime + ".txt";
            v_path4 = tb_hbx4.Text + v_datetime + ".txt";
            v_path5 = tb_hbx5.Text + v_datetime + ".txt";
            v_path6 = tb_hbx6.Text + v_datetime + ".txt";
            v_path8 = tb_hbx8.Text + v_datetime + ".txt";
            v_path9 = tb_hbx9.Text + v_datetime + ".txt";

            //秒數為0或5才執行，每五秒執行一次
            if (v_datetime.Substring(7, 1) == "5" || v_datetime.Substring(7, 1) == "0")
            {
                heat_process(v_path1, rTB_hbx1);
                heat_process(v_path2, rTB_hbx2);
                heat_process(v_path3, rTB_hbx3);
                heat_process(v_path4, rTB_hbx4);
                heat_process(v_path5, rTB_hbx5);
                heat_process(v_path6, rTB_hbx6);
                heat_process(v_path8, rTB_hbx8);
                heat_process(v_path9, rTB_hbx9);

                v_counter++;
            }

            //清除所有訊息
            if (v_counter > 5000)
            {
                ExportLog(rTB_hbx1, "HBX1");
                ExportLog(rTB_hbx2, "HBX2");
                ExportLog(rTB_hbx3, "HBX3");
                ExportLog(rTB_hbx4, "HBX4");
                ExportLog(rTB_hbx5, "HBX5");
                ExportLog(rTB_hbx6, "HBX6");
                ExportLog(rTB_hbx8, "HBX8");
                ExportLog(rTB_hbx9, "HBX9");
            }
        }

        private void heat_process(string v_HeatFoldFile, RichTextBox v_richTB)
        {
            //FileInfo fi = new FileInfo(v_HeatFoldFile);
            //if (fi.Length != 0)
            //{
            //讀取TXT
            int counter = 0;
            string line, v_pid, v_prodid, v_materid, v_heatid, v_HeatFoldFile_new;
            //string v_backupfold = "E:\\HBX_temp\\backup";
            v_HeatFoldFile_new = v_HeatFoldFile.Replace("E:\\HBX_temp","E:\\HBX_temp\\backup");
            System.IO.StreamReader file;

            // Read the file and display it line by line.
            try
            {
                file = new System.IO.StreamReader(v_HeatFoldFile, System.Text.Encoding.Default);
                //if (file.ReadLine() == null)
                //{
                //    v_richTB.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":空字串" + v_HeatFoldFile + "\n";
                //    //刪除檔案
                //    System.IO.File.Delete(v_HeatFoldFile);
                //    return;
                //}
                v_richTB.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":處理中" + v_HeatFoldFile + "\r\n";
                //v_richTB.ScrollToCaret();
            }
            catch (Exception ex)
            {

                string v_error = ex.ToString();
                return;
            }

            try
            {

                counter = 0;

                while ((file.Peek() > -1))
                {
                    //一個檔案只有一筆資料，多的不轉檔
                    if (counter >= 1)
                    {
                        file.Close();
                        file.Dispose();
                        System.IO.File.Delete(v_HeatFoldFile);
                        return;

                    }

                    //逐行讀取
                    line = file.ReadLine().Trim();

                    //line = file.ReadToEnd();
                    if (line != string.Empty)
                    {

                        //分解字串
                        char[] delimiterChars = { ',' };
                        string[] v_Body = line.Trim().Split(delimiterChars);


                        v_pid = v_Body[0].ToString();
                        v_prodid = v_Body[1].ToString();
                        v_materid = v_Body[2].ToString();
                        v_heatid = v_Body[3].ToString();

                        v_sql = "INSERT INTO HEAT_BS_TEMP(ID, PID, PRODUCT_ID, ";
                        v_sql += "MATERIALID, HEATID, MACHSET,  ";
                        v_sql += "OPERATOR, PRODDATETIME,COL_1, COL_2,  ";
                        v_sql += "COL_3, COL_4, COL_5,  ";
                        v_sql += "COL_6, COL_7, COL_8,  ";
                        v_sql += "COL_9, COL_10, COL_11,  ";
                        v_sql += "COL_12, COL_13, COL_14,  ";
                        v_sql += "COL_15, COL_16, COL_17,  ";
                        v_sql += "COL_18, COL_19, COL_20,  ";
                        v_sql += "COL_21, COL_22, COL_23,  ";
                        v_sql += "COL_24, COL_25, COL_26,  ";
                        v_sql += "COL_27, COL_28, COL_29,  ";
                        v_sql += "COL_30, COL_31, COL_32,  ";
                        v_sql += "COL_33, COL_34, COL_35,  ";
                        v_sql += "COL_36, COL_37, COL_38,  ";
                        v_sql += "COL_39, COL_40, COL_41,  ";
                        v_sql += "COL_42, COL_43, COL_44,  ";
                        v_sql += "COL_45, COL_46, COL_47,  ";
                        v_sql += "COL_48, COL_49, COL_50,  ";
                        v_sql += "COL_51, COL_52, COL_53,  ";
                        v_sql += "COL_54, COL_55, COL_56,  ";
                        v_sql += "COL_57, COL_58, COL_59,  ";
                        v_sql += "COL_60, COL_61, COL_62,  ";
                        v_sql += "COL_63, COL_64, COL_65,  ";
                        v_sql += "COL_66, COL_67, COL_68,  ";
                        v_sql += "COL_69, COL_70, COL_71,  ";
                        v_sql += "COL_72, COL_73, COL_74,  ";
                        v_sql += "COL_75, COL_76, COL_77,  ";
                        v_sql += "COL_78, COL_79, COL_80,  ";
                        v_sql += "COL_81, COL_82,COL_83,COL_84 )";
                        v_sql += "VALUES(HEAT_BSTEMP_SEQ.NEXTVAL ";
                        v_sql += ",'" + v_pid + "'"; //PID
                        v_sql += ",'" + v_prodid + "'"; //PRODUCT_ID
                        v_sql += ",'" + v_materid + "'"; //MATERIALID
                        v_sql += ",'" + v_heatid + "'"; //HEATID
                        v_sql += ",'" + v_Body[4].ToString() + "'"; //MACHSET
                        v_sql += ",'" + v_Body[89].ToString() + "'"; //OPERATOR
                        v_sql += ",TO_DATE('" + v_Body[90].ToString() + "','YYYY/MM/DD HH24:MI:SS')"; //PRODDATETIME

                        for (int i = 5; i <= 88; i++)
                        {
                            v_sql += "," + v_Body[i].ToString() + " ";
                        }

                        v_sql += ") ";

                        //INSERT INTO HEAT_BS_TEMP
                        insertDB(v_sql);

                        //INSERT INTO HEAT_BS200
                        v_sql = "INSERT INTO  HEAT_BS200( ";
                        v_sql += "               ID, PID, PRODUCT_ID,  ";
                        v_sql += "               MATERIALID, HEATID, MACHSET,  ";
                        v_sql += "               OPERATOR, PRODDATETIME,  ";
                        v_sql += "               S0001, S0002, S0003, S0004,  ";
                        v_sql += "               S0005, S0006, S0007, S0008,  ";
                        v_sql += "               S0009, S0010, S0011, S0012,  ";
                        v_sql += "               S0013, S0014, S0015, S0016,  ";
                        v_sql += "               S0017, S0018, S0019, S0020,  ";
                        v_sql += "               S0021, S0022, S0023, S0024,  ";
                        v_sql += "               S0025, S0026, S0027, S0028,  ";
                        v_sql += "               R0001, R0002, R0003, R0004,  ";
                        v_sql += "               R0005, R0006, R0007, R0008,  ";
                        v_sql += "               R0009, R0010, R0011, R0012,  ";
                        v_sql += "               R0013, R0014, R0015, R0016,  ";
                        v_sql += "               R0017, R0018, R0019, R0020,  ";
                        v_sql += "               R0021, R0022, R0023, R0024,  ";
                        v_sql += "               R0025, R0026, R0027, R0028 ";
                        v_sql += "               )    ";
                        v_sql += "                VALUES(  ";
                        v_sql += "                     HEAT_BS200_SEQ.NEXTVAL  ";
                        v_sql += "                     ,'" + v_pid + "'"; //PID
                        v_sql += "                     ,'" + v_prodid + "'"; //PRODUCT_ID
                        v_sql += "                     ,'" + v_materid + "'"; //MATERIALID
                        v_sql += "                     ,'" + v_heatid + "'"; //HEATID
                        v_sql += "                     ,'" + v_Body[4].ToString() + "'"; //MACHSET
                        v_sql += "                     ,'" + v_Body[89].ToString() + "'"; //OPERATOR
                        v_sql += "                     ,TO_DATE('" + v_Body[90].ToString() + "','YYYY/MM/DD HH24:MI:SS'),"; //PRODDATETIME
                        v_sql += "                     " + v_Body[6].ToString() + ",  " + v_Body[9].ToString() + ",  " + v_Body[12].ToString() + ", " + v_Body[15].ToString() + ",  ";
                        v_sql += "                     " + v_Body[18].ToString() + ", " + v_Body[21].ToString() + ", " + v_Body[24].ToString() + ", " + v_Body[27].ToString() + ",  ";
                        v_sql += "                     " + v_Body[30].ToString() + ", " + v_Body[33].ToString() + ", " + v_Body[36].ToString() + ", " + v_Body[39].ToString() + ",  ";
                        v_sql += "                     " + v_Body[42].ToString() + ", " + v_Body[45].ToString() + ", " + v_Body[48].ToString() + ", " + v_Body[51].ToString() + ",  ";
                        v_sql += "                     " + v_Body[54].ToString() + ", " + v_Body[57].ToString() + ", " + v_Body[60].ToString() + ", " + v_Body[63].ToString() + ",  ";
                        v_sql += "                     " + v_Body[66].ToString() + ", " + v_Body[69].ToString() + ", " + v_Body[72].ToString() + ", " + v_Body[75].ToString() + ",  ";
                        v_sql += "                     " + v_Body[78].ToString() + ", " + v_Body[81].ToString() + ", " + v_Body[84].ToString() + ", " + v_Body[87].ToString() + ",  ";
                        v_sql += "                     " + v_Body[7].ToString()  + ", " + v_Body[10].ToString() + ", " + v_Body[13].ToString() + ", " + v_Body[16].ToString() + ",  ";
                        v_sql += "                     " + v_Body[19].ToString() + ", " + v_Body[22].ToString() + ", " + v_Body[25].ToString() + ", " + v_Body[28].ToString() + ",  ";
                        v_sql += "                     " + v_Body[31].ToString() + ", " + v_Body[34].ToString() + ", " + v_Body[37].ToString() + ", " + v_Body[40].ToString() + ", ";
                        v_sql += "                     " + v_Body[43].ToString() + ", " + v_Body[46].ToString() + ", " + v_Body[49].ToString() + ", " + v_Body[52].ToString() + ", ";
                        v_sql += "                     " + v_Body[55].ToString() + ", " + v_Body[58].ToString() + ", " + v_Body[61].ToString() + ", " + v_Body[64].ToString() + ", ";
                        v_sql += "                     " + v_Body[67].ToString() + ", " + v_Body[70].ToString() + ", " + v_Body[73].ToString() + ", " + v_Body[76].ToString() + ", ";
                        v_sql += "                     " + v_Body[79].ToString() + ", " + v_Body[82].ToString() + ", " + v_Body[85].ToString() + ", " + v_Body[88].ToString() + "  ";
                        v_sql += "                )";

                        insertDB(v_sql);
                        counter++;
                    }

                    
                }
                file.Close();
                file.Dispose();
                System.IO.File.Copy(v_HeatFoldFile, v_HeatFoldFile_new, true);
                System.IO.File.Delete(v_HeatFoldFile);
            }
            catch (Exception ex)
            {
                string v_error = ex.ToString();
                v_richTB.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":發生錯誤-" + v_error + "\r\n";
                //v_richTB.ScrollToCaret();
                //會發生空白檔案而造成讀檔錯誤，直接刪除檔案
                //System.IO.File.Delete(v_HeatFoldFile);
                file.Close();
                file.Dispose();
                return;
            }

        }

        //LOT NO只有第一次投料會有資料，往後的資料收集需要先抓取上一筆LOT NO
        private string[] GetMaxData(string v_machset)
        {
            OleDbConnection conn;
            OleDbCommand cmd;
            OleDbDataReader dr;
            string v_sqlstr, v_pid, v_prodid, v_materid, v_heatid;

            try
            {


                v_sqlstr = "SELECT  PID, PRODUCT_ID, MATERIALID,HEATID FROM HEAT_BS_TEMP WHERE ID = (SELECT  MAX(ID) FROM HEAT_BS_TEMP WHERE MACHSET = '" + v_machset + "')";
                conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jheipConnection);
                conn.Open();
                cmd = new OleDbCommand(v_sqlstr, conn);
                dr = cmd.ExecuteReader();

                dr.Read();

                v_pid = dr[0].ToString();
                v_prodid = dr[1].ToString();
                v_materid = dr[2].ToString();
                v_heatid = dr[3].ToString();
                dr.Close();
                conn.Close();

                string[] v_date = { v_pid, v_prodid, v_materid, v_heatid };
                return v_date;
            }
            catch (Exception ex)
            {
                string v_error = ex.ToString();
                string[] v_errordate = { "", "", "", "" };
                return v_errordate;
            }
            finally
            {
                //dr.Close();
                //conn.Close();
            }
        }
        private void insertDB(string v_sql)
        {
            OleDbDataAdapter adp = new OleDbDataAdapter();
            //OleDbConnection conn = conn = new OleDbConnection(
            //    System.Configuration.ConfigurationManager.ConnectionStrings["jheipConnection0"].ConnectionString);
            OleDbConnection conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jheipConnection); 
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(v_sql, conn);

            try
            {
                adp.InsertCommand = cmd;
                adp.InsertCommand.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                richtb_status.Text += ex.ToString() + "\r\n";
                cmd.Dispose();
                adp.Dispose();
                conn.Close();
            }
            finally
            {
                cmd.Dispose();
                adp.Dispose();
                conn.Close();
            }
        }
        private void insertBS200(string v_machset)
        {
            string v_insertSql;

            v_insertSql = "INSERT INTO  HEAT_BS200( ";
            v_insertSql += "               ID, PID, PRODUCT_ID,  ";
            v_insertSql += "               MATERIALID, HEATID, MACHSET,  ";
            v_insertSql += "               OPERATOR, PRODDATETIME,  ";
            v_insertSql += "               S0001, S0002, S0003, S0004,  ";
            v_insertSql += "               S0005, S0006, S0007, S0008,  ";
            v_insertSql += "               S0009, S0010, S0011, S0012,  ";
            v_insertSql += "               S0013, S0014, S0015, S0016,  ";
            v_insertSql += "               S0017, S0018, S0019, S0020,  ";
            v_insertSql += "               S0021, S0022, S0023, S0024,  ";
            v_insertSql += "               S0025, S0026, S0027, S0028,  ";
            v_insertSql += "               R0001, R0002, R0003, R0004,  ";
            v_insertSql += "               R0005, R0006, R0007, R0008,  ";
            v_insertSql += "               R0009, R0010, R0011, R0012,  ";
            v_insertSql += "               R0013, R0014, R0015, R0016,  ";
            v_insertSql += "               R0017, R0018, R0019, R0020,  ";
            v_insertSql += "               R0021, R0022, R0023, R0024,  ";
            v_insertSql += "               R0025, R0026, R0027, R0028 ";
            v_insertSql += "               )    ";
            v_insertSql += "                SELECT  ";
            v_insertSql += "                     HEAT_BS200_SEQ.NEXTVAL,  ";
            v_insertSql += "                     PID, PRODUCT_ID,  ";
            v_insertSql += "                     MATERIALID, HEATID, MACHSET,  ";
            v_insertSql += "                     OPERATOR, PRODDATETIME,  ";
            v_insertSql += "                     COL_2,  COL_5,  COL_8,  COL_11,  ";
            v_insertSql += "                     COL_14, COL_17, COL_20, COL_23,  ";
            v_insertSql += "                     COL_26, COL_29, COL_32, COL_35,  ";
            v_insertSql += "                     COL_38, COL_41, COL_44, COL_47,  ";
            v_insertSql += "                     COL_50, COL_53, COL_56, COL_59,  ";
            v_insertSql += "                     COL_62, COL_65, COL_68, COL_71,  ";
            v_insertSql += "                     COL_74, COL_77, COL_80, COL_83,  ";
            v_insertSql += "                     COL_3,  COL_6,  COL_9,  COL_12, ";
            v_insertSql += "                     COL_15, COL_18, COL_21, COL_24, ";
            v_insertSql += "                     COL_27, COL_30, COL_33, COL_36, ";
            v_insertSql += "                     COL_39, COL_42, COL_45, COL_48, ";
            v_insertSql += "                     COL_51, COL_54, COL_57, COL_60, ";
            v_insertSql += "                     COL_63, COL_66, COL_69, COL_72, ";
            v_insertSql += "                     COL_75, COL_78, COL_81, COL_84  ";
            v_insertSql += "                FROM  HEAT_BS_TEMP ";
            v_insertSql += "                WHERE  ID = (SELECT MAX(ID) FROM HEAT_BS_TEMP WHERE MACHSET = '" + v_machset + "') ";

            OleDbDataAdapter adp = new OleDbDataAdapter();
            OleDbConnection conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jheipConnection);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(v_insertSql, conn);

            try
            {
                adp.InsertCommand = cmd;
                adp.InsertCommand.ExecuteNonQuery();
                
            }
            catch
            {
                cmd.Dispose();
                adp.Dispose();
                conn.Close();
            }
            finally
            {
                cmd.Dispose();
                adp.Dispose();
                conn.Close();
            }
        }

        private void HEAT_IMPORT_01_Load(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(DateTime.Now.ToString("yyyyMMdd")) >= 20150828)
            {
                Application.Exit();
            }
        }

        
    }
}
