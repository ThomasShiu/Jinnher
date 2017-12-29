using System;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text;

namespace AUTO_SCHE
{
    public partial class HEAT_IMPORT_02 : Form
    {
        string v_sql, line;
        //string v_path1, v_path2, v_path3, v_path4, v_path5, v_path6, v_path8, v_path9;
        static int v_counter = 0, v_SetCount=0;
        DateTime v_sTime, v_nowTime ,v_eTime;
        //static Boolean v_folder_1 = false;
        //static string line, v_rTB, v_switch, v_rTB1, v_rTB2, v_rTB3, v_rTB4, v_rTB5, v_rTB6, v_rTB8, v_rTB9, v_rTB_status;
        Thread t;
        public HEAT_IMPORT_02()
        {
            InitializeComponent();
        }

        private void btn_start_Click_1(object sender, EventArgs e)
        {
            L_hbx1.Text = "";
            L_hbx2.Text = "";
            L_hbx3.Text = "";
            L_hbx4.Text = "";
            L_hbx5.Text = "";
            L_hbx6.Text = "";
            L_hbx8.Text = "";
            L_hbx9.Text = "";
            L_stc1.Text = "";

            //啟動TIMER
            //timer1.Enabled = true;
            timer1.Start();
            richtb_status.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":TIMER啟動\r\n";
            toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":TIMER啟動中";
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            //停止TIMER
            //timer1.Enabled = false;
            timer1.Stop();
            richtb_status.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":TIMER停止\r\n";
            toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":TIMER停止中";
        }

        //立刻執行
        private void button1_Click(object sender, EventArgs e)
        {
            L_hbx1.Text = "";
            L_hbx2.Text = "";
            L_hbx3.Text = "";
            L_hbx4.Text = "";
            L_hbx5.Text = "";
            L_hbx6.Text = "";
            L_hbx8.Text = "";
            L_hbx9.Text = "";
            L_stc1.Text = "";

            Start_Jobs();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string v_datetime2 = DateTime.Now.ToString("ddHHmmss");

            v_SetCount = Convert.ToInt16(TB_count.Text);

            //每30分鐘轉一次
            v_counter++;
            if ((v_SetCount+8) >= v_counter)
            {
                toolStripStatusLabel2.Text = "倒數:" + Convert.ToString((v_SetCount+8) - v_counter);
            }
            if (v_counter == v_SetCount)
            {
                //v_counter = 0;
                //Start_Jobs();
                ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_1), "H001");
            }
            if (v_counter == v_SetCount + 1)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_2), "H002");
            }
            if (v_counter == v_SetCount + 2)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_3), "H003");
            }
            if (v_counter == v_SetCount + 3)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_4), "H004");
            }
            if (v_counter == v_SetCount + 4)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_5), "H005");
            }
            if (v_counter == v_SetCount + 5)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_6), "H006");
            }
            if (v_counter == v_SetCount + 6)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_8), "H008");
            }
            if (v_counter == v_SetCount + 7)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_9), "H009");
            }
            if (v_counter == v_SetCount + 8)
            {
                v_counter = 0;
                ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_A), "STC");
            }

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
                ExportLog(rTB_stc1, "STC");
            }


        }

        //匯出LOG
        private void ExportLog(RichTextBox tTB, string v_HEAT)
        {
            //string log = @"C:\log.txt";
            string log = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\HEAT_xdf_log\\" + v_HEAT + "_" + DateTime.Now.ToString("yyyyMMdd") + "_LOG.txt";
            using (FileStream fs = new FileStream(log, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(tTB.Text);
                    tTB.Text = "";
                }
            }
            tTB.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":已匯出LOG檔，請至程式根目錄底下查看。\n";
        }

        private void Start_Jobs()
        {
            //System.Threading.WaitCallback waitCallback = new WaitCallback(MyThreadWork);

            ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_1), "H001");
            ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_2), "H002");
            ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_3), "H003");
            ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_4), "H004");
            ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_5), "H005");
            ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_6), "H006");
            ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_8), "H008");
            ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_9), "H009");
            ThreadPool.QueueUserWorkItem(new WaitCallback(ImportTXT_A), "STC");

            //Thread t1 = new Thread(ImportTXT_1);
            //t1.IsBackground = true;
            //t1.Start();//Thread.Sleep(5000);
            ////t1.Suspend(500);

            //Thread t2 = new Thread(ImportTXT_2);
            //t2.IsBackground = true;
            //t2.Start(); //Thread.Sleep(5000);

            //Thread t3 = new Thread(ImportTXT_3);
            //t3.IsBackground = true;
            //t3.Start(); //Thread.Sleep(5000);

            //Thread t4 = new Thread(ImportTXT_4);
            //t4.IsBackground = true;
            //t4.Start(); //Thread.Sleep(5000);

            //Thread t5 = new Thread(ImportTXT_5);
            //t5.IsBackground = true;
            //t5.Start(); //Thread.Sleep(5000);

            //Thread t6 = new Thread(ImportTXT_6);
            //t6.IsBackground = true;
            //t6.Start(); //Thread.Sleep(5000);

            //Thread t8 = new Thread(ImportTXT_8);
            //t8.IsBackground = true;
            //t8.Start(); //Thread.Sleep(5000);

            //Thread t9 = new Thread(ImportTXT_9);
            //t9.IsBackground = true;
            //t9.Start(); //Thread.Sleep(5000);

            //Thread tA = new Thread(ImportTXT_A);
            //tA.IsBackground = true;
            //tA.Start(); //Thread.Sleep(5000);

            //t1.Join();t2.Join();t3.Join();t4.Join();t5.Join();t6.Join();t8.Join();t9.Join();tA.Join();

        }
        public void ImportTXT_1(object obj)
        {
            ImportTXT(tb_hbx1.Text, rTB_hbx1, "H001",L_hbx1);
        }
        public void ImportTXT_2(object obj)
        {
            ImportTXT(tb_hbx2.Text, rTB_hbx2, "H002", L_hbx2);
        }
        public void ImportTXT_3(object obj)
        {
            ImportTXT(tb_hbx3.Text, rTB_hbx3, "H003", L_hbx3);
        }
        public void ImportTXT_4(object obj)
        {
            ImportTXT(tb_hbx4.Text, rTB_hbx4, "H004", L_hbx4);
        }
        public void ImportTXT_5(object obj)
        {
            ImportTXT(tb_hbx5.Text, rTB_hbx5, "H005", L_hbx5);
        }
        public void ImportTXT_6(object obj)
        {
            ImportTXT(tb_hbx6.Text, rTB_hbx6, "H006", L_hbx6);
        }
        public void ImportTXT_8(object obj)
        {
            ImportTXT(tb_hbx8.Text, rTB_hbx8, "H008", L_hbx8);
        }
        public void ImportTXT_9(object obj)
        {
            ImportTXT(tb_hbx9.Text, rTB_hbx9, "H009", L_hbx9);
        }
        public void ImportTXT_A(object obj)
        {
            Import_ATXT(tb_STC1.Text, rTB_stc1, "STC", L_stc1);
        }

        public void DeleteFile(string v_Path,Boolean v_switch)
        {
            while (v_switch)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(v_Path);

                //防呆，確認目標目錄確實存在
                if (!dirInfo.Exists)
                {
                    //MessageBox.Show("Diretory not exist!");
                    myUI("Diretory not exist!\r\n", richtb_status);
                    return;
                }
                else
                {

                    //dirInfo.GetFiles()取回目錄下檔案清單(FileInfo陣列)
                    FileInfo[] files = dirInfo.GetFiles();
                    foreach (FileInfo fileInfo in files)
                    {
                        //foreach檢查每一個FileInfo檔名，搜尋並取代檔名。
                        string oldFileName = fileInfo.FullName;
                        System.IO.File.Delete(oldFileName);
                    }
                }
            }
        }
        public void ImportTXT(string path, RichTextBox v_tb, string v_machset,Label v_Label)
        {
            //string path = (string)arg;
            //v_tb.Text = "";

            //new一個目標目錄的DirectoryInfo
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            //防呆，確認目標目錄確實存在
            if (!dirInfo.Exists)
            {
                //MessageBox.Show("Diretory not exist!");
                myUI("Diretory not exist!\r\n", richtb_status);
                return;
            }

            //dirInfo.GetFiles()取回目錄下檔案清單(FileInfo陣列)
            FileInfo[] files = dirInfo.GetFiles();
            foreach (FileInfo fileInfo in files)
            {
                //foreach檢查每一個FileInfo檔名，搜尋並取代檔名。
                string oldFileName = fileInfo.FullName;
                heat_process(oldFileName, v_tb, v_machset, v_Label);
                //Application.DoEvents();
                
            }

        }
        public void Import_ATXT(string path, RichTextBox v_tb, string v_machset,Label v_Label)
        {
            //string path = (string)arg;
            //v_tb.Text = "";

            //new一個目標目錄的DirectoryInfo
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            //防呆，確認目標目錄確實存在
            if (!dirInfo.Exists)
            {
                //MessageBox.Show("Diretory not exist!");
                myUI("Diretory not exist!\r\n", richtb_status);
                return;
            }

            //dirInfo.GetFiles()取回目錄下檔案清單(FileInfo陣列)
            FileInfo[] files = dirInfo.GetFiles();
            foreach (FileInfo fileInfo in files)
            {
                //foreach檢查每一個FileInfo檔名，搜尋並取代檔名。
                string oldFileName = fileInfo.FullName;
                anneal_process(oldFileName, v_tb, v_machset, v_Label);
                //Application.DoEvents();
               
            }

        }
       

        //讀取TXT檔內的文字
        public void heat_process(string v_HeatFoldFile, RichTextBox v_richTB, string v_machset,Label v_Label)
        {
            //FileInfo fi = new FileInfo(v_HeatFoldFile);
            //if (fi.Length != 0)
            //{
            //讀取TXT
            int counter = 0;
            v_sTime = DateTime.Now;
            System.IO.StreamReader file;
            
             string v_HeatFoldFile_new = v_HeatFoldFile.Replace("\\HBX_temp", "\\HBX_temp\\backup");
             v_sql = "";

            // Read the file and display it line by line.
            try
            {
                //將文字檔暫存到記憶體
                file = new System.IO.StreamReader(v_HeatFoldFile, System.Text.Encoding.Default);
                //Thread.Sleep(600);
                //v_richTB.Text += v_machset + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":開始處理" + v_HeatFoldFile + "\r\n";
                myUI(v_machset + " > " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":開始處理" + v_HeatFoldFile + "\r\n", v_richTB);
            }
            catch (Exception ex)
            {
                //string v_error = ex.ToString();
                myUI(v_machset + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":" + ex.ToString() + "\r\n", richtb_status);
                return;
            }

            try
            {
                //日期(日)  E:\HBX_temp\hbx1xdf\temp_20130701.txt
                string v_date = (v_HeatFoldFile.Substring(25, 8)).ToString();  //20130101
                string v_year = v_date.Substring(0, 4);
                string v_month = v_date.Substring(4, 2);
                string v_day = v_date.Substring(6, 2);
                v_date = v_year + "/" + v_month + "/" + v_day ;
                string v_xdfYM = "XDF_" + v_year.Substring(2, 2) + v_month;
                v_sql = " DELETE HEAT_BS200_XDF  PARTITION (" + v_xdfYM + ") ";

                switch (v_machset)
                {
                    case "H001":
                        v_sql += " WHERE MACHSET = 'H001' ";
                        break;
                    case "H002":
                        v_sql += " WHERE MACHSET = 'H002' ";
                        break;
                    case "H003":
                        v_sql += " WHERE MACHSET = 'H003' ";
                        break;
                    case "H004":
                        v_sql += " WHERE MACHSET = 'H004' ";
                        break;
                    case "H005":
                        v_sql += " WHERE MACHSET = 'H005' ";
                        break;
                    case "H006":
                        v_sql += " WHERE MACHSET = 'H006' ";
                        break;
                    case "H008":
                        v_sql += " WHERE MACHSET = 'H008' ";
                        break;
                    case "H009":
                        v_sql += " WHERE MACHSET = 'H009' ";
                        break;
                }

                v_sql += " AND TO_CHAR(proddatetime,'YYYY/MM/DD') = '"+ v_date + "' ";

                //先刪除之前資料
                deleteDB(v_sql, v_machset);
                //Thread.Sleep(300);
               
                string v_date2 = v_date + " 00:01:00";
                string v_date3;
                DateTime dt = Convert.ToDateTime(v_date2);
                //DateTime dt = DateTime.Parse(v_date2);

                while ((file.Peek() > -1))
                {
                    //逐行讀取
                    line = file.ReadLine().Trim();
                    //從第二筆開始讀取
                    if (counter > 0)
                    {
                        if (line != string.Empty)
                        {

                            //分解字串
                            char[] delimiterChars = { ',' };
                            string[] v_Body = line.Trim().Split(delimiterChars);

                            if (v_Body.Length == 51)
                            {

                                v_date3 = dt.ToString("yyyy-MM-dd HH:mm");

                                //v_pid = v_Body[0].ToString();
                                //v_prodid = v_Body[1].ToString();
                                //v_materid = v_Body[2].ToString();
                                //v_heatid = v_Body[3].ToString();

                                //INSERT INTO HEAT_BS200
                                StringBuilder sb1 = new StringBuilder();
                                sb1.Append("INSERT INTO  HEAT_BS200_XDF( ");
                                sb1.Append("  MACHSET,proddatetime,  ");
                                sb1.Append("  S0001,R0001,S0004,R0004, S0005,R0005, S0006,R0006, S0007,R0007, S0008,R0008, S0009,R0009,S0010,R0010, S0011,R0011, S0012,R0012, S0017,R0017, S0018,R0018,   S0019,R0019,S0020,R0020, S0021,R0021, S0022,R0022, S0023,R0023, S0024,R0024, S0025,R0025, S0026,R0026, S0027,R0027,S0013,R0013,S0015,R0015, S0016,R0016, S0014,R0014  ");
                                sb1.Append("  ) VALUES( ");
                                sb1.Append("   '" + v_machset + "' , ");
                                sb1.Append(" TO_DATE('" + v_date3 + "','YYYY/MM/DD HH24:MI:SS'), ");
                                sb1.Append("  " + v_Body[1].ToString() + " , " + v_Body[2].ToString() + " , " + v_Body[3].ToString() + " , " + v_Body[4].ToString() + " ,  " + v_Body[5].ToString() + ",  ");
                                sb1.Append("  " + v_Body[6].ToString() + " , " + v_Body[7].ToString() + " , " + v_Body[8].ToString() + " , " + v_Body[9].ToString() + " ,  ");
                                sb1.Append("  " + v_Body[10].ToString() + ", " + v_Body[11].ToString() + ", " + v_Body[12].ToString() + ", " + v_Body[13].ToString() + ",  ");
                                sb1.Append("  " + v_Body[14].ToString() + ", " + v_Body[15].ToString() + ", " + v_Body[16].ToString() + ", " + v_Body[17].ToString() + ",  ");
                                sb1.Append("  " + v_Body[18].ToString() + ", " + v_Body[19].ToString() + ", " + v_Body[20].ToString() + ", " + v_Body[21].ToString() + ",  ");
                                sb1.Append("  " + v_Body[22].ToString() + ", " + v_Body[23].ToString() + ", " + v_Body[24].ToString() + ", " + v_Body[25].ToString() + ",  ");
                                sb1.Append("  " + v_Body[26].ToString() + ", " + v_Body[27].ToString() + ", " + v_Body[28].ToString() + ", " + v_Body[29].ToString() + ",  ");
                                sb1.Append("  " + v_Body[30].ToString() + ", " + v_Body[31].ToString() + ", " + v_Body[32].ToString() + ", " + v_Body[33].ToString() + ",  ");
                                sb1.Append("  " + v_Body[34].ToString() + ", " + v_Body[35].ToString() + ", " + v_Body[36].ToString() + ", " + v_Body[37].ToString() + ",  ");
                                sb1.Append("  " + v_Body[38].ToString() + ", " + v_Body[39].ToString() + ", " + v_Body[40].ToString() + ", " + v_Body[41].ToString() + ",  ");
                                sb1.Append("  " + v_Body[42].ToString() + ", " + v_Body[43].ToString() + ", " + v_Body[44].ToString() + ", " + v_Body[45].ToString() + ",  ");
                                sb1.Append("  " + v_Body[46].ToString() + ", " + v_Body[47].ToString() + ", " + v_Body[48].ToString() + ", " + v_Body[49].ToString() + ",  ");
                                sb1.Append("  " + v_Body[50].ToString() + " )");

                                //Thread.Sleep(100);
                                //v_sql = "INSERT INTO  HEAT_BS200_XDF( ";
                                //v_sql += "  MACHSET,proddatetime,  ";
                                //v_sql += "  S0003,R0003,S0004,R0004, S0005,R0005, S0006,R0006, S0007,R0007, S0008,R0008, S0009,R0009,S0010,R0010, S0011,R0011, S0012,R0012, S0017,R0017, S0018,R0018, ";
                                //v_sql += "  S0003,R0003,S0004,R0004, S0005,R0005, S0006,R0006, S0007,R0007, S0008,R0008, S0009,R0009,S0010,R0010, S0011,R0011, S0012,R0012, S0017,R0017, S0018,R0018, ";
                                //v_sql += "   '" + v_machset + "' , "; //MACHSET
                                //v_sql += "  TO_DATE('" + v_date3 + "','YYYY/MM/DD HH24:MI:SS'),"; //PRODDATETIME
                                //v_sql += "  " + v_Body[1].ToString() + " , " + v_Body[2].ToString() + " , " + v_Body[3].ToString() + " , " + v_Body[4].ToString() + " ,  " + v_Body[5].ToString() + ",  ";
                                //v_sql += "  " + v_Body[6].ToString() + " , " + v_Body[7].ToString() + " , " + v_Body[8].ToString() + " , " + v_Body[9].ToString() + " ,  ";
                                //v_sql += "  " + v_Body[10].ToString() + ", " + v_Body[11].ToString() + ", " + v_Body[12].ToString() + ", " + v_Body[13].ToString() + ",  ";
                                //v_sql += "  " + v_Body[14].ToString() + ", " + v_Body[15].ToString() + ", " + v_Body[16].ToString() + ", " + v_Body[17].ToString() + ",  ";
                                //v_sql += "  " + v_Body[18].ToString() + ", " + v_Body[19].ToString() + ", " + v_Body[20].ToString() + ", " + v_Body[21].ToString() + ",  ";
                                //v_sql += "  " + v_Body[22].ToString() + ", " + v_Body[23].ToString() + ", " + v_Body[24].ToString() + ", " + v_Body[25].ToString() + ",  ";
                                //v_sql += "  " + v_Body[26].ToString() + ", " + v_Body[27].ToString() + ", " + v_Body[28].ToString() + ", " + v_Body[29].ToString() + ",  ";
                                //v_sql += "  " + v_Body[30].ToString() + ", " + v_Body[31].ToString() + ", " + v_Body[32].ToString() + ", " + v_Body[33].ToString() + ",  ";
                                //v_sql += "  " + v_Body[34].ToString() + ", " + v_Body[35].ToString() + ", " + v_Body[36].ToString() + ", " + v_Body[37].ToString() + ",  ";
                                //v_sql += "  " + v_Body[38].ToString() + ", " + v_Body[39].ToString() + ", " + v_Body[40].ToString() + ", " + v_Body[41].ToString() + ", ";
                                //v_sql += "  " + v_Body[42].ToString() + ", " + v_Body[43].ToString() + ", " + v_Body[44].ToString() + ", " + v_Body[45].ToString() + ", ";
                                //v_sql += "  " + v_Body[46].ToString() + ", " + v_Body[47].ToString() + ", " + v_Body[48].ToString() + ", " + v_Body[49].ToString() + ", ";
                                //v_sql += "  " + v_Body[50].ToString() + " ";
                                //v_sql += "                )";

                                insertDB(sb1.ToString(), v_machset);

                                dt = dt.AddMinutes(1);
                                //Thread.Sleep(300);
                            }
                            else if (v_Body.Length == 53) //新增倒料秒數，重量
                            {
                                v_date3 = dt.ToString("yyyy/MM/dd HH:mm:ss");

                                //v_pid = v_Body[0].ToString();
                                //v_prodid = v_Body[1].ToString();
                                //v_materid = v_Body[2].ToString();
                                //v_heatid = v_Body[3].ToString();

                                //INSERT INTO HEAT_BS200
                                StringBuilder sb1 = new StringBuilder();
                                sb1.Append("INSERT INTO  HEAT_BS200_XDF( ");
                                sb1.Append("  MACHSET,proddatetime,  ");
                                sb1.Append("  S0003,S0002,");  //倒料秒，重量
                                sb1.Append("  S0001,R0001,S0004,R0004, S0005,");
                                sb1.Append("  R0005,S0006,R0006,S0007,");
                                sb1.Append("  R0007,S0008,R0008,S0009,  ");
                                sb1.Append("  R0009,S0010,R0010,S0011,  ");
                                sb1.Append("  R0011,S0012,R0012,S0017,  ");
                                sb1.Append("  R0017,S0018,R0018,S0019,  ");
                                sb1.Append("  R0019,S0020,R0020,S0021,  ");
                                sb1.Append("  R0021,S0022,R0022,S0023,  ");
                                sb1.Append("  R0023,S0024,R0024,S0025,  ");
                                sb1.Append("  R0025,S0026,R0026,S0027,  ");
                                sb1.Append("  R0027,S0013,R0013,S0015,  ");
                                sb1.Append("  R0015,S0016,R0016,S0014,  ");
                                sb1.Append("  R0014  ");
                                sb1.Append("  ) VALUES( ");
                                sb1.Append("   '" + v_machset + "' , ");
                                sb1.Append(" TO_DATE('" + v_date3 + "','YYYY/MM/DD HH24:MI:SS'), ");
                                sb1.Append("  " + v_Body[52].ToString() + " , " + v_Body[51].ToString() + " ,   ");
                                sb1.Append("  " + v_Body[1].ToString() + " , " + v_Body[2].ToString() + " , " + v_Body[3].ToString() + " , " + v_Body[4].ToString() + " ,  " + v_Body[5].ToString() + ",  ");
                                sb1.Append("  " + v_Body[6].ToString() + " , " + v_Body[7].ToString() + " , " + v_Body[8].ToString() + " , " + v_Body[9].ToString() + " ,  ");
                                sb1.Append("  " + v_Body[10].ToString() + ", " + v_Body[11].ToString() + ", " + v_Body[12].ToString() + ", " + v_Body[13].ToString() + ",  ");
                                sb1.Append("  " + v_Body[14].ToString() + ", " + v_Body[15].ToString() + ", " + v_Body[16].ToString() + ", " + v_Body[17].ToString() + ",  ");
                                sb1.Append("  " + v_Body[18].ToString() + ", " + v_Body[19].ToString() + ", " + v_Body[20].ToString() + ", " + v_Body[21].ToString() + ",  ");
                                sb1.Append("  " + v_Body[22].ToString() + ", " + v_Body[23].ToString() + ", " + v_Body[24].ToString() + ", " + v_Body[25].ToString() + ",  ");
                                sb1.Append("  " + v_Body[26].ToString() + ", " + v_Body[27].ToString() + ", " + v_Body[28].ToString() + ", " + v_Body[29].ToString() + ",  ");
                                sb1.Append("  " + v_Body[30].ToString() + ", " + v_Body[31].ToString() + ", " + v_Body[32].ToString() + ", " + v_Body[33].ToString() + ",  ");
                                sb1.Append("  " + v_Body[34].ToString() + ", " + v_Body[35].ToString() + ", " + v_Body[36].ToString() + ", " + v_Body[37].ToString() + ",  ");
                                sb1.Append("  " + v_Body[38].ToString() + ", " + v_Body[39].ToString() + ", " + v_Body[40].ToString() + ", " + v_Body[41].ToString() + ",  ");
                                sb1.Append("  " + v_Body[42].ToString() + ", " + v_Body[43].ToString() + ", " + v_Body[44].ToString() + ", " + v_Body[45].ToString() + ",  ");
                                sb1.Append("  " + v_Body[46].ToString() + ", " + v_Body[47].ToString() + ", " + v_Body[48].ToString() + ", " + v_Body[49].ToString() + ",  ");
                                sb1.Append("  " + v_Body[50].ToString() + " )");

                                insertDB(sb1.ToString(), v_machset);
                                
                                dt = dt.AddMinutes(1);
                                //Thread.Sleep(300);
                            }

                        }
                        else
                        {

                            //v_richTB.Text = v_machset + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":空字串" + v_HeatFoldFile + "\r\n";
                            myUI(v_machset + " > " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":空字串" + v_HeatFoldFile + "\r\n", v_richTB);
                            
                            //刪除檔案
                            file.Close();
                            file.Dispose();
                            //file.DiscardBufferedData();
                            System.IO.File.Copy(v_HeatFoldFile, v_HeatFoldFile_new, true);

                            //Thread.Sleep(500);
                            //System.IO.File.Delete(v_HeatFoldFile);
                            return;
                        }
                    }
                    counter++;
                    myUI2("已處理 "+Convert.ToString(counter-1)+" 筆", v_Label);
                    //v_date3 = dt.ToString("yyyy/MM/dd hh:mm:ss");
                }

                file.Close();
                file.Dispose();
                System.IO.File.Copy(v_HeatFoldFile, v_HeatFoldFile_new, true);
                Thread.Sleep(300);

                //v_richTB.Text += v_machset + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":處理完成" + v_HeatFoldFile + "\r\n";
                v_nowTime = DateTime.Now;
                //string v_estTime = ((TimeSpan)(v_nowTime - v_sTime)).TotalMilliseconds.ToString();
                string v_estTime = ((TimeSpan)(v_nowTime - v_sTime)).TotalSeconds.ToString();

                myUI(v_machset + " > " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":處理完成" + v_HeatFoldFile + " 。歷時: " + v_estTime + "\r\n", v_richTB);
                Thread.Sleep(10000);
                //t.Interrupt();
                System.IO.File.Delete(v_HeatFoldFile);
            }
            catch (IOException err)
            {

                //v_richTB.Text += v_machset + "。" + err.ToString();
                myUI(v_machset + "。" + err.ToString(), v_richTB);
                //Thread.Sleep(100);
                file.Close();
                file.Dispose();
                //file.DiscardBufferedData();
                GC.Collect();
                //System.IO.File.Copy(v_HeatFoldFile, v_HeatFoldFile_new, true);
               
                //會發生空白檔案而造成讀檔錯誤，直接刪除檔案
                //System.IO.File.Delete(v_HeatFoldFile);

            }
            catch (Exception ex)
            {
                string v_error = ex.ToString();

                //v_richTB.Text += v_error + "。\r\n" + v_machset + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":發生錯誤-" + line.ToString() + "\r\n";
                myUI(v_error + "。\r\n" + v_machset + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":發生錯誤-" + line.ToString() + "\r\n", v_richTB);
                file.Close();
                file.Dispose();

            }

        }
        //球化爐讀取TXT檔內的文字
        private void anneal_process(string v_HeatFoldFile, RichTextBox v_richTB, string v_machset,Label v_Label)
        {

            //讀取TXT
            int counter = 0;
            v_sTime = DateTime.Now;
            System.IO.StreamReader file;
            
            v_sql = "";

            // Read the file and display it line by line.
            try
            {
                file = new System.IO.StreamReader(v_HeatFoldFile, System.Text.Encoding.Default);

                //v_richTB.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":開始處理" + v_HeatFoldFile + "\r\n";
                myUI(DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":開始處理" + v_HeatFoldFile + "\r\n", v_richTB);
            }
            catch (Exception ex)
            {

                //string v_error = ex.ToString();
                myUI(v_machset + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":" + ex.ToString() + "\r\n", richtb_status);
                return;
            }

            try
            {
                //日期(日)  E:\HBX_temp\stcxdf\TEMP_20130701.txt
                string v_date = (v_HeatFoldFile.Substring(24, 8)).ToString();  //20130101
                string v_year = v_date.Substring(0, 4);
                string v_month = v_date.Substring(4, 2);
                string v_day = v_date.Substring(6, 2);
                v_date = v_year + "/" + v_month + "/" + v_day;

                StringBuilder sb2 = new StringBuilder();
                sb2.Append(" DELETE ANNEAL_XDF WHERE MACHSET = 'STC' ");
                sb2.Append(" AND TO_CHAR(proddatetime,'YYYY/MM/DD') = '" + v_date + "' ");
                //Thread.Sleep(100);

                //先刪除之前資料
                deleteDB(sb2.ToString(), "STC");


                string v_date2 = v_date + " 00:01:00";
                string v_date3;
                DateTime dt = Convert.ToDateTime(v_date2);

                while ((file.Peek() > -1))
                {
                    //逐行讀取
                    line = file.ReadLine().Trim();
                    //從第二筆開始讀取
                    if (counter > 0)
                    {
                        if (line != string.Empty)
                        {

                            //分解字串
                            char[] delimiterChars = { ',' };
                            string[] v_Body = line.Trim().Split(delimiterChars);

                            if (v_Body.Length == 11)
                            {

                                v_date3 = dt.ToString("yyyy/MM/dd HH:mm:ss");

                                //v_pid = v_Body[0].ToString();
                                //v_prodid = v_Body[1].ToString();
                                //v_materid = v_Body[2].ToString();
                                //v_heatid = v_Body[3].ToString();

                                //INSERT INTO HEAT_BS200
                                StringBuilder sb1 = new StringBuilder();
                                sb1.Append("INSERT INTO  ANNEAL_XDF( ");
                                sb1.Append(" MACHSET,PRODDATETIME, ELAPSE_TIME,  ");
                                sb1.Append(" S0001, R0001,  ");
                                sb1.Append(" S0002, R0002,  ");
                                sb1.Append(" S0003, R0003,  ");
                                sb1.Append(" S0004, R0004, ");
                                sb1.Append(" OPERATOR ) ");
                                sb1.Append("  VALUES(  ");
                                sb1.Append("   '" + v_machset + "' , "); //MACHSET
                                //sb1.Append("  TO_DATE('" + v_date3 + "','YYYY/MM/DD HH24:MI:SS'),"); //PRODDATETIME
                                sb1.Append("  TO_DATE('" + v_Body[0].ToString() + "','MM/DD/YYYY HH24:MI:SS'), "); //PRODDATETIME
                                sb1.Append("  " + v_Body[1].ToString() + " , " + v_Body[5].ToString() + " , " + v_Body[2].ToString() + " , " + v_Body[6].ToString() + " ,  " + v_Body[3].ToString() + ",  ");
                                sb1.Append("  " + v_Body[7].ToString() + " , " + v_Body[4].ToString() + " , " + v_Body[9].ToString() + " , " + v_Body[8].ToString() + " , " + v_Body[10].ToString() + " ");
                                sb1.Append("  )");

                                insertDB(sb1.ToString(), v_machset);

                                dt = dt.AddMinutes(1);
                            }

                        }
                        else
                        {
                            //v_richTB.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":空字串" + v_HeatFoldFile + "\r\n";
                            //刪除檔案
                            //Thread.Sleep(100);
                            file.Close();
                            file.Dispose();
                            System.IO.File.Delete(v_HeatFoldFile);
                            return;
                        }
                    }
                    counter++;
                    myUI2("已處理 " + Convert.ToString(counter - 1) + " 筆", v_Label);
                    //v_date3 = dt.ToString("yyyy/MM/dd hh:mm:ss");
                }


                file.Close();
                file.Dispose();

                v_nowTime = DateTime.Now;
                string v_estTime = ((TimeSpan)(v_nowTime - v_sTime)).TotalSeconds.ToString();
                //string v_estTime = ((TimeSpan)(v_nowTime - v_sTime)).TotalMinutes.ToString();

                myUI(v_machset + " > " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":處理完成" + v_HeatFoldFile + " 。歷時: " + v_estTime + "\r\n", v_richTB);

                Thread.Sleep(5000);
                System.IO.File.Delete(v_HeatFoldFile);

                //Application.DoEvents();
                //Thread.Sleep(1);
            }
            catch (IOException err)
            {
                string v_error = err.ToString();
                //v_richTB.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":發生錯誤-" + line.ToString() + "\r\n";
                //v_richTB.ScrollToCaret();

                //v_richTB.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":發生IO錯誤-" + line.ToString() + "\r\n";
                myUI(DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":發生IO錯誤-" + line.ToString() + "\r\n", v_richTB);
                //Thread.Sleep(100);
                file.Close();
                file.Dispose();
                //會發生空白檔案而造成讀檔錯誤，直接刪除檔案
                System.IO.File.Delete(v_HeatFoldFile);
                throw new System.IO.IOException("File Open Error!");
            }

            catch (Exception ex)
            {
                string v_error = ex.ToString();
                //v_richTB.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":發生錯誤-" + line.ToString() + "\r\n";
                //v_richTB.ScrollToCaret();

                //v_richTB.Text += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":發生錯誤-" + line.ToString() + "\r\n";
                myUI(DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":發生錯誤-" + line.ToString() + "\r\n", v_richTB);
                //Thread.Sleep(100);
                file.Close();
                file.Dispose();
                //會發生空白檔案而造成讀檔錯誤，直接刪除檔案
                System.IO.File.Delete(v_HeatFoldFile);

            }
            
        }

       
        private void insertDB(string v_sql,string v_machset)
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
            catch (Exception ex)
            {
                //richtb_status.Text += "Insert data:" + ex.ToString() + "。" + v_machset + "。" + line.ToString() + "\r\n";
                myUI("Insert data:" + ex.ToString() + "。" + v_machset + "。" + line.ToString() + "\r\n", richtb_status);
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

        private void deleteDB(string v_sql, string v_machset)
        {
            OleDbDataAdapter adp = new OleDbDataAdapter();
            //OleDbConnection conn = conn = new OleDbConnection(
            //    System.Configuration.ConfigurationManager.ConnectionStrings["jheipConnection0"].ConnectionString);
            OleDbConnection conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jheipConnection);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(v_sql, conn);

            try
            {
                adp.DeleteCommand = cmd;
                adp.DeleteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //richtb_status.Text += "Delete data:"+ex.ToString() + "。" + v_machset + "。" + line.ToString() + "\r\n";
                myUI("Delete data:" + ex.ToString() + "。" + v_machset + "。" + line.ToString() + "\r\n", richtb_status);
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


        private void btn_clear1_Click(object sender, EventArgs e)
        {
            rTB_hbx1.Text = "";
            rTB_hbx2.Text = "";
            rTB_hbx3.Text = "";
            rTB_hbx4.Text = "";
            rTB_hbx5.Text = "";
            rTB_hbx6.Text = "";
            rTB_hbx8.Text = "";
            rTB_hbx9.Text = "";
            rTB_stc1.Text = "";
 
        }

        private void btn_clear2_Click(object sender, EventArgs e)
        {
            richtb_status.Text = "";
            
        }

        private void HEAT_IMPORT_02_Load(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(DateTime.Now.ToString("yyyyMMdd")) >= 20201224)
            {
                Application.Exit();
            }
        }
        //宣告委派事件，利用 Invoke 呼叫委派事件執行函式(此函式在 UI Thread)
        delegate void UpdateLableHandler(string value, Control ctl);
        private void printResult(string value, Control ctl)
        {
            ctl.Text += value.ToString();
            //progressBar1.Value = int.Parse(value.ToString());
        }

        //public  void MyThreadWork(object state)
        //{
        //    string v_str = (string)state;
        //    //Console.WriteLine("Begin of {0}", (string)state);
        //    myUI(DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":Begin of " + v_str + "\r\n", richtb_status);

        //    Thread.Sleep(5000);
        //    //Console.WriteLine("End of {0}", (string)state);
        //    myUI(DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":End of " + v_str + "\r\n", richtb_status);
        //}

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
        private delegate void myUICallBack2(string myStr, Control ctl);
        private void myUI2(string myStr, Control ctl)
        {
            if (this.InvokeRequired)
            {
                myUICallBack2 myUpdate2 = new myUICallBack2(myUI2);
                this.Invoke(myUpdate2, myStr, ctl);
            }
            else
            {
                ctl.Text = myStr;
            }
        }

        private void b_hbx1_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(ImportTXT_1);
            t1.IsBackground = true;
            t1.Start();
        }

        private void b_hbx2_Click(object sender, EventArgs e)
        {
            Thread t2 = new Thread(ImportTXT_2);
            t2.IsBackground = true;
            t2.Start();
        }

        private void b_hbx3_Click(object sender, EventArgs e)
        {
            Thread t3 = new Thread(ImportTXT_3);
            t3.IsBackground = true;
            t3.Start();
        }

        private void b_hbx4_Click(object sender, EventArgs e)
        {
            Thread t4 = new Thread(ImportTXT_4);
            t4.IsBackground = true;
            t4.Start();
        }

        private void b_hbx5_Click(object sender, EventArgs e)
        {
            Thread t5 = new Thread(ImportTXT_5);
            t5.IsBackground = true;
            t5.Start();
        }

        private void b_hbx6_Click(object sender, EventArgs e)
        {
            Thread t6 = new Thread(ImportTXT_6);
            t6.IsBackground = true;
            t6.Start();
        }

        private void b_hbx8_Click(object sender, EventArgs e)
        {
            Thread t8 = new Thread(ImportTXT_8);
            t8.IsBackground = true;
            t8.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Thread t9 = new Thread(ImportTXT_9);
            t9.IsBackground = true;
            t9.Start();
        }

        private void b_stc1_Click(object sender, EventArgs e)
        {
            L_stc1.Text = "";
            Thread tA = new Thread(ImportTXT_A);
            tA.IsBackground = true;
            tA.Start();
        }

    }
}
