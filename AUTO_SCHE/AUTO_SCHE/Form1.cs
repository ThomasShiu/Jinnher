using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AUTO_SCHE
{
    public partial class Main_win : Form
    {
        public Main_win()
        {
            InitializeComponent();
        }
        private void Main_win_Load(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(DateTime.Now.ToString("yyyyMMdd")) >= 20201224)
            {
                Application.Exit();
            }
        }
        private void sQL轉ORACLE排程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BBI_datatrans01 bbi = new BBI_datatrans01();
            bbi.MdiParent = this;
            bbi.Show();
        }

        private void 自動溫度收集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HEAT_IMPORT_01 heatimport = new HEAT_IMPORT_01();
            heatimport.MdiParent = this;
            heatimport.Show();
        }

        private void 領用繳回轉檔ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MTM_datatrans01 mtm_01 = new MTM_datatrans01();
            mtm_01.MdiParent = this;
            mtm_01.Show();
        }

        private void 寄送通知ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EMAIL_JOBS email_jobs = new EMAIL_JOBS();
            email_jobs.MdiParent = this;
            email_jobs.Show();
        }

        private void bBI檢驗報告檢查ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BBI_file_check BBI_fchk = new BBI_file_check();
            BBI_fchk.MdiParent = this;
            BBI_fchk.Show();
        }

        private void 盤元掃描轉檔ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WIRES_trans WIRES_trans1 = new WIRES_trans();
            WIRES_trans1.MdiParent = this;
            WIRES_trans1.Show();
        }

        private void xDF資料收集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HEAT_IMPORT_02 heatimport2 = new HEAT_IMPORT_02();
            heatimport2.MdiParent = this;
            heatimport2.Show();
        }

        private void 寄送通知排程JHERPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EMAIL_JOBS_JHERP email_jobs_jherp = new EMAIL_JOBS_JHERP();
            email_jobs_jherp.MdiParent = this;
            email_jobs_jherp.Show();
        }

        private void excelToPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Excel2PDF excel2pdf = new Excel2PDF();
            excel2pdf.MdiParent = this;
            excel2pdf.Show();
        }

        private void excelToPDF電鍍ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Excel2PDF_P excel2pdf_p = new Excel2PDF_P();
            excel2pdf_p.MdiParent = this;
            excel2pdf_p.Show();
        }

        private void excelToPDF成型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Excel2PDF_F excel2pdf_f = new Excel2PDF_F();
            excel2pdf_f.MdiParent = this;
            excel2pdf_f.Show();
        }

        private void sOE3訂單下載ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CSC_SOE3_IMP csc_soe3_imp = new CSC_SOE3_IMP();
            csc_soe3_imp.MdiParent = this;
            csc_soe3_imp.Show();
        }

        private void 掃描郵件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScanMail scanmail = new ScanMail();
            scanmail.MdiParent = this;
            scanmail.Show();
        }

        private void 自動掃描郵件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScanMail_Auto scanmail_auto = new ScanMail_Auto();
            scanmail_auto.MdiParent = this;
            scanmail_auto.Show();
        }

 

        
    }
}
