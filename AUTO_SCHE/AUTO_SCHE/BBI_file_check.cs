using System;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using AUTO_SCHE.CLASS;
using System.Data;

namespace AUTO_SCHE
{
    public partial class BBI_file_check : Form
    {
        thomas_class ts_conn = new thomas_class();
        string v_body;
        public BBI_file_check()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartProcess();
        }
        private void StartProcess()
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            v_body = "";
            string cpath;
            //string path = @"d:\temp\";
            //根目錄
            string path = TB_path.Text;
            //列出子目錄
            string[] rtn = System.IO.Directory.GetDirectories(path);
            for (int i = 0; i < rtn.Length; i++)
            {
                //子目錄名稱,PAC_ID
                string folderName = System.IO.Path.GetFileName(rtn[i]);

                //richTextBox1.Text += folderName + "\n";

                cpath = path + folderName;
                //new一個目標目錄的DirectoryInfo
                DirectoryInfo dirInfo = new DirectoryInfo(cpath);
                //防呆，確認目標目錄確實存在
                if (!dirInfo.Exists)
                {
                    MessageBox.Show("目錄不存在!");
                    return;
                }

                //dirInfo.GetFiles()取回目錄下檔案清單(FileInfo陣列)
                FileInfo[] files = dirInfo.GetFiles();
                foreach (FileInfo fileInfo in files)
                {
                    //取得完整檔名，去除副檔名,LOT_NO
                    string FileName2 = fileInfo.Name.Replace(fileInfo.Extension, "");

                    //個別檢查批號是否存在
                    if (!CheckDB(folderName, FileName2))
                    {
                        richTextBox1.Text += "[與資料庫不符]" + TB_path.Text + folderName + "\\" + FileName2 + ".pdf\n";
                        richTextBox1.Text += "---------------------------------------------------------------------------\n";

                        v_body += "[與資料庫不符]" + TB_path.Text + folderName + "\\" + FileName2 + ".pdf <br>";
                    }
                    else
                    {
                        richTextBox2.Text += "[與資料庫符合]" + TB_path.Text + folderName + "\\" + FileName2 + ".pdf\n";
                    }
                }
            }

            //發出e-mail通知
            char[] delimiterChars = { ';', ',' };
            string[] v_email_addr = TB_email_addr.Text.Trim().Split(delimiterChars); ;

               
            if (v_body != "")
            {
                
                v_body = "比對根目錄：" + path + "。有異常的批號如下所列-<br>" + v_body;

                for (int i = 0; i <= v_email_addr.Length - 1; i++)
                {
                    //                  收件者,標題,內容
                    ts_conn.Send_Mail2(v_email_addr[i].ToString(), v_body, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "-BBI檢驗報告檢查結果");
                }
            }
            //else
            //{
            //    for (int i = 0; i <= v_email_addr.Length - 1; i++)
            //    {
            //        //                  收件者,標題,內容
            //        ts_conn.Send_Mail2(v_email_addr[i].ToString(), "查無異常批號", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "-BBI檢驗報告檢查：無異常批號");
            //    }
            //}
        }

        private bool CheckDB(string v_Pacno, string v_Lot)
        {
            bool v_flag = false;
            OleDbDataAdapter adp = null ;
            OleDbConnection conn=null;
            OleDbDataReader dr=null;
            OleDbCommand cmd=null;

            try
            {
                adp = new OleDbDataAdapter();
                conn = new OleDbConnection(AUTO_SCHE.Properties.Settings.Default.jh815Connection);
                conn.Open();

                string v_sql = "SELECT DISTINCT NVL( L.ASSEM_LOT_NO||L.ASSEM_CTRL_LOT_NO,L.LOT_NO|| L.CTRL_LOT_NO) as LOT " +
                                " FROM PACKINGS P,LISTITEMS L " +
                                "WHERE P.PAC_ID=L.PAC_ID " +
                                "AND P.PAC_NO = '" + v_Pacno + "' " +
                                "AND NVL( L.ASSEM_LOT_NO||L.ASSEM_CTRL_LOT_NO,L.LOT_NO|| L.CTRL_LOT_NO) = '" + v_Lot + "' ";

                cmd = new OleDbCommand(v_sql, conn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    v_flag = true;         
                }
                else
                {
                    v_flag = false;
                }
            }
            catch
            {
                v_flag = false;
            }
            finally
            {
                cmd.Dispose();
                adp.Dispose();
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return v_flag;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                TB_path.Text = folderBrowserDialog1.SelectedPath + "\\";
            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            StartProcess();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                timer1.Start();

            }
            else
            {
                timer1.Stop();
            }
        }

   
    }
}
