namespace AUTO_SCHE
{
    partial class EMAIL_JOBS_JHERP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CB_start = new System.Windows.Forms.CheckBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.rTB_message = new System.Windows.Forms.RichTextBox();
            this.btn_sendmail = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataSet1 = new AUTO_SCHE.DataSet1();
            this.oDSJHEMAILJHERPBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.oDS_JH_EMAIL_JHERPTableAdapter = new AUTO_SCHE.DataSet1TableAdapters.ODS_JH_EMAIL_JHERPTableAdapter();
            this.sNDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fROMADDRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tOADDRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sUBJECTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cREATEDATEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cREATEEMPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sTATUSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oDSJHEMAILJHERPBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // CB_start
            // 
            this.CB_start.AutoSize = true;
            this.CB_start.Location = new System.Drawing.Point(16, 10);
            this.CB_start.Margin = new System.Windows.Forms.Padding(4);
            this.CB_start.Name = "CB_start";
            this.CB_start.Size = new System.Drawing.Size(89, 19);
            this.CB_start.TabIndex = 5;
            this.CB_start.Text = "啟動排程";
            this.CB_start.UseVisualStyleBackColor = true;
            this.CB_start.CheckedChanged += new System.EventHandler(this.CB_start_CheckedChanged);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 10000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 486);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1011, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // rTB_message
            // 
            this.rTB_message.Location = new System.Drawing.Point(16, 287);
            this.rTB_message.Margin = new System.Windows.Forms.Padding(4);
            this.rTB_message.Name = "rTB_message";
            this.rTB_message.Size = new System.Drawing.Size(977, 199);
            this.rTB_message.TabIndex = 6;
            this.rTB_message.Text = "";
            // 
            // btn_sendmail
            // 
            this.btn_sendmail.Location = new System.Drawing.Point(120, 5);
            this.btn_sendmail.Margin = new System.Windows.Forms.Padding(4);
            this.btn_sendmail.Name = "btn_sendmail";
            this.btn_sendmail.Size = new System.Drawing.Size(104, 35);
            this.btn_sendmail.TabIndex = 7;
            this.btn_sendmail.Text = "立即執行";
            this.btn_sendmail.UseVisualStyleBackColor = true;
            this.btn_sendmail.Click += new System.EventHandler(this.btn_sendmail_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sNDataGridViewTextBoxColumn,
            this.fROMADDRDataGridViewTextBoxColumn,
            this.tOADDRDataGridViewTextBoxColumn,
            this.sUBJECTDataGridViewTextBoxColumn,
            this.cREATEDATEDataGridViewTextBoxColumn,
            this.cREATEEMPDataGridViewTextBoxColumn,
            this.sTATUSDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.oDSJHEMAILJHERPBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(16, 54);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(977, 200);
            this.dataGridView1.TabIndex = 10;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // oDSJHEMAILJHERPBindingSource
            // 
            this.oDSJHEMAILJHERPBindingSource.DataMember = "ODS_JH_EMAIL_JHERP";
            this.oDSJHEMAILJHERPBindingSource.DataSource = this.dataSet1;
            this.oDSJHEMAILJHERPBindingSource.Filter = "STATUS = \'N\'";
            this.oDSJHEMAILJHERPBindingSource.Sort = "SN";
            // 
            // oDS_JH_EMAIL_JHERPTableAdapter
            // 
            this.oDS_JH_EMAIL_JHERPTableAdapter.ClearBeforeFill = true;
            // 
            // sNDataGridViewTextBoxColumn
            // 
            this.sNDataGridViewTextBoxColumn.DataPropertyName = "SN";
            this.sNDataGridViewTextBoxColumn.HeaderText = "SN";
            this.sNDataGridViewTextBoxColumn.Name = "sNDataGridViewTextBoxColumn";
            this.sNDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fROMADDRDataGridViewTextBoxColumn
            // 
            this.fROMADDRDataGridViewTextBoxColumn.DataPropertyName = "FROM_ADDR";
            this.fROMADDRDataGridViewTextBoxColumn.HeaderText = "FROM_ADDR";
            this.fROMADDRDataGridViewTextBoxColumn.Name = "fROMADDRDataGridViewTextBoxColumn";
            this.fROMADDRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tOADDRDataGridViewTextBoxColumn
            // 
            this.tOADDRDataGridViewTextBoxColumn.DataPropertyName = "TO_ADDR";
            this.tOADDRDataGridViewTextBoxColumn.HeaderText = "TO_ADDR";
            this.tOADDRDataGridViewTextBoxColumn.Name = "tOADDRDataGridViewTextBoxColumn";
            this.tOADDRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sUBJECTDataGridViewTextBoxColumn
            // 
            this.sUBJECTDataGridViewTextBoxColumn.DataPropertyName = "SUBJECT";
            this.sUBJECTDataGridViewTextBoxColumn.HeaderText = "SUBJECT";
            this.sUBJECTDataGridViewTextBoxColumn.Name = "sUBJECTDataGridViewTextBoxColumn";
            this.sUBJECTDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cREATEDATEDataGridViewTextBoxColumn
            // 
            this.cREATEDATEDataGridViewTextBoxColumn.DataPropertyName = "CREATE_DATE";
            this.cREATEDATEDataGridViewTextBoxColumn.HeaderText = "CREATE_DATE";
            this.cREATEDATEDataGridViewTextBoxColumn.Name = "cREATEDATEDataGridViewTextBoxColumn";
            this.cREATEDATEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cREATEEMPDataGridViewTextBoxColumn
            // 
            this.cREATEEMPDataGridViewTextBoxColumn.DataPropertyName = "CREATE_EMP";
            this.cREATEEMPDataGridViewTextBoxColumn.HeaderText = "CREATE_EMP";
            this.cREATEEMPDataGridViewTextBoxColumn.Name = "cREATEEMPDataGridViewTextBoxColumn";
            this.cREATEEMPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sTATUSDataGridViewTextBoxColumn
            // 
            this.sTATUSDataGridViewTextBoxColumn.DataPropertyName = "STATUS";
            this.sTATUSDataGridViewTextBoxColumn.HeaderText = "STATUS";
            this.sTATUSDataGridViewTextBoxColumn.Name = "sTATUSDataGridViewTextBoxColumn";
            this.sTATUSDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // EMAIL_JOBS_JHERP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 508);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.CB_start);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.rTB_message);
            this.Controls.Add(this.btn_sendmail);
            this.Name = "EMAIL_JOBS_JHERP";
            this.Text = "自動發送電子郵件(JHERP)";
            this.Load += new System.EventHandler(this.EMAIL_JOBS_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oDSJHEMAILJHERPBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CB_start;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox rTB_message;
        private System.Windows.Forms.Button btn_sendmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn cONTENTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource oDSJHEMAILJHERPBindingSource;
        private DataSet1TableAdapters.ODS_JH_EMAIL_JHERPTableAdapter oDS_JH_EMAIL_JHERPTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn sNDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fROMADDRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tOADDRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sUBJECTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cREATEDATEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cREATEEMPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sTATUSDataGridViewTextBoxColumn;
    }
}