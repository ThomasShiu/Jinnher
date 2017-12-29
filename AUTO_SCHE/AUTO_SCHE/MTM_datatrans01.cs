using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AUTO_SCHE.CLASS;
using System.IO;
using System.Web;

namespace AUTO_SCHE
{
    public partial class MTM_datatrans01 : Form
    {
        string v_sql;
        thomas_class ts_conn = new thomas_class();

        public MTM_datatrans01()
        {
            InitializeComponent();
        }
        private void CB_timerStart_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_timerStart.Checked)
            {
                timer1.Start();

            }
            else
            {
                timer1.Stop();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //從SQL SERVER 取回資料集
            //v_sql = "SELECT CREATE_EMP,CONVERT(char(20), CREATE_DATE, 20) CREATE_DATE,SUBSTR(BARCODE,0,LENGTH(BARCODE)-2) DIE_ID,SUBSTR(BARCODE,-2,2) DIE_STOR_SN FROM MTM_OUT WHERE STATUS = 'N' ";
            //v_sql = "SELECT CREATE_EMP, CONVERT(char(20), CREATE_DATE, 20) AS CREATE_DATE, ";
            //v_sql += "SUBSTRING(BARCODE, 0, LEN(BARCODE) - 2) AS DIE_ID, SUBSTRING(BARCODE, LEN(BARCODE) - 3, 2) AS DIE_STOR_SN,BARCODE ";
            //v_sql += "FROM   MTM_OUT ";
            //v_sql += "WHERE   (STATUS = 'N') ";

            v_sql = "SELECT CREATE_EMP, CONVERT(char(20), CREATE_DATE, 20) AS CREATE_DATE, BARCODE,SN ";
            v_sql += "FROM   MTM_OUT ";
            v_sql += "WHERE   (STATUS = 'N') ";
            string sConnection = "Data Source=192.168.0.9,1433;Initial Catalog=db;User ID=sa;Password=jhjolihi";
            //string sConnection = "Data Source=192.168.0.9,1433;Initial Catalog=db;Persist Security Info=True;User ID=sa;Password=jhjolihi";

            SqlCommand comm = new SqlCommand(v_sql, new SqlConnection(sConnection));
            SqlDataReader dr = null;
            try
            {
                comm.Connection.Open();
                dr = comm.ExecuteReader();

                //迴圈
                //寫入Oracle
                //更新Sql資料狀態
                while (dr.Read())
                {
                    try
                    {
                        //dr[0] CREATE_EMP
                        //dr[1] CREATE_DATE
                        //dr[2] DIE_ID
                        //dr[3] DIE_STOR_SN

                        string v_create_date = dr[1].ToString();
                        v_sql = "INSERT INTO DIE_MTM_OUT(SN,CREATE_EMP,CREATE_DATE,DIE_ID,DIE_STOR_SN ) ";
                        v_sql += "VALUES(DIE_MTM_OUT_SEQ.NEXTVAL,TRIM('" + dr[0].ToString() + "'),to_date('" + dr[1].ToString() + "','YYYY-MM-DD HH24:MI:SS'),TRIM(REPLACE(REPLACE('" + dr[2].ToString().Replace("%QP6", "") + "', CHR(13), ''), CHR(10), '')),TRIM(REPLACE(REPLACE('" + dr[3].ToString().Replace("%QP6", "") + "', CHR(13), ''), CHR(10), '')) ) ";
                        //轉入ORACLE
                        ts_conn.trans_oleDb(v_sql, "insert");

                        //更新MS SQL上的資料狀態為'Y'表示已轉入ORACLE
                        v_sql = "UPDATE MTM_OUT SET STATUS = 'Y' WHERE BARCODE = '" + dr[2].ToString() + "' AND SN = '" + dr[3].ToString() + "' ";
                        ts_conn.trans_MSsql(v_sql, "update", rTB_log_out);

                        rTB_log_out.Text += "資料新增成功：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":" + dr[2].ToString().Replace("\r\n", "") + "-" + dr[3].ToString().Replace("\r\n", "") + "\n";
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());
                        rTB_log_out.Text += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":" + ex.ToString() + "\n";
                    }
                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                rTB_log_out.Text += DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ":" + ex.Message + " \n";
                return;
            }
            dr.Close();
            dr.Dispose();
            comm.Connection.Close();
            comm.Connection.Dispose();

            if (DateTime.Now.ToString("hh24:mi:ss") == "23:59:59")
            {
                //備份LOG檔
                ts_conn.ExportLog(rTB_log_out, "MTM模具領用繳回");
                rTB_log_out.Text = "";
            }
        }

        private void btn_out_Click(object sender, EventArgs e)
        {
            //備份LOG檔
            ts_conn.ExportLog(rTB_log_out, "MTM模具領用LOG");
            rTB_log_out.Text = "";
        }

        private void btn_in_Click(object sender, EventArgs e)
        {
            //備份LOG檔
            ts_conn.ExportLog(rTB_log_out, "MTM模具繳回LOG");
            rTB_log_in.Text = "";
        }
 

       
    }
}
