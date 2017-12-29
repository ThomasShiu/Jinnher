namespace AUTO_SCHE
{
    partial class WIRES_trans
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.sNDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wIREIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tRANSEMPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tRANSDATEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lOCATIONDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sTATUSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wIRESTRANSQUEUEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new AUTO_SCHE.DataSet1();
            this.wIRES_TRANS_QUEUETableAdapter = new AUTO_SCHE.DataSet1TableAdapters.WIRES_TRANS_QUEUETableAdapter();
            this.CB_start = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.rTB_message = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wIRESTRANSQUEUEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 300000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sNDataGridViewTextBoxColumn,
            this.wIREIDDataGridViewTextBoxColumn,
            this.tRANSEMPDataGridViewTextBoxColumn,
            this.tRANSDATEDataGridViewTextBoxColumn,
            this.lOCATIONDataGridViewTextBoxColumn,
            this.sTATUSDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.wIRESTRANSQUEUEBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(977, 221);
            this.dataGridView1.TabIndex = 0;
            // 
            // sNDataGridViewTextBoxColumn
            // 
            this.sNDataGridViewTextBoxColumn.DataPropertyName = "SN";
            this.sNDataGridViewTextBoxColumn.HeaderText = "SN";
            this.sNDataGridViewTextBoxColumn.Name = "sNDataGridViewTextBoxColumn";
            this.sNDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // wIREIDDataGridViewTextBoxColumn
            // 
            this.wIREIDDataGridViewTextBoxColumn.DataPropertyName = "WIRE_ID";
            this.wIREIDDataGridViewTextBoxColumn.HeaderText = "WIRE_ID";
            this.wIREIDDataGridViewTextBoxColumn.Name = "wIREIDDataGridViewTextBoxColumn";
            // 
            // tRANSEMPDataGridViewTextBoxColumn
            // 
            this.tRANSEMPDataGridViewTextBoxColumn.DataPropertyName = "TRANS_EMP";
            this.tRANSEMPDataGridViewTextBoxColumn.HeaderText = "TRANS_EMP";
            this.tRANSEMPDataGridViewTextBoxColumn.Name = "tRANSEMPDataGridViewTextBoxColumn";
            // 
            // tRANSDATEDataGridViewTextBoxColumn
            // 
            this.tRANSDATEDataGridViewTextBoxColumn.DataPropertyName = "TRANS_DATE";
            this.tRANSDATEDataGridViewTextBoxColumn.HeaderText = "TRANS_DATE";
            this.tRANSDATEDataGridViewTextBoxColumn.Name = "tRANSDATEDataGridViewTextBoxColumn";
            // 
            // lOCATIONDataGridViewTextBoxColumn
            // 
            this.lOCATIONDataGridViewTextBoxColumn.DataPropertyName = "LOCATION";
            this.lOCATIONDataGridViewTextBoxColumn.HeaderText = "LOCATION";
            this.lOCATIONDataGridViewTextBoxColumn.Name = "lOCATIONDataGridViewTextBoxColumn";
            // 
            // sTATUSDataGridViewTextBoxColumn
            // 
            this.sTATUSDataGridViewTextBoxColumn.DataPropertyName = "STATUS";
            this.sTATUSDataGridViewTextBoxColumn.HeaderText = "STATUS";
            this.sTATUSDataGridViewTextBoxColumn.Name = "sTATUSDataGridViewTextBoxColumn";
            // 
            // wIRESTRANSQUEUEBindingSource
            // 
            this.wIRESTRANSQUEUEBindingSource.DataMember = "WIRES_TRANS_QUEUE";
            this.wIRESTRANSQUEUEBindingSource.DataSource = this.dataSet1;
            this.wIRESTRANSQUEUEBindingSource.Filter = "STATUS = \'N\'";
            this.wIRESTRANSQUEUEBindingSource.Sort = "SN";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // wIRES_TRANS_QUEUETableAdapter
            // 
            this.wIRES_TRANS_QUEUETableAdapter.ClearBeforeFill = true;
            // 
            // CB_start
            // 
            this.CB_start.AutoSize = true;
            this.CB_start.Location = new System.Drawing.Point(13, 13);
            this.CB_start.Name = "CB_start";
            this.CB_start.Size = new System.Drawing.Size(89, 19);
            this.CB_start.TabIndex = 1;
            this.CB_start.Text = "啟動排程";
            this.CB_start.UseVisualStyleBackColor = true;
            this.CB_start.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(126, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 33);
            this.button1.TabIndex = 2;
            this.button1.Text = "立即執行";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rTB_message
            // 
            this.rTB_message.Location = new System.Drawing.Point(12, 268);
            this.rTB_message.Margin = new System.Windows.Forms.Padding(4);
            this.rTB_message.Name = "rTB_message";
            this.rTB_message.Size = new System.Drawing.Size(977, 199);
            this.rTB_message.TabIndex = 3;
            this.rTB_message.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 475);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1001, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 60000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // WIRES_trans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 497);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.rTB_message);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CB_start);
            this.Controls.Add(this.dataGridView1);
            this.Name = "WIRES_trans";
            this.Text = "盤元轉檔";
            this.Load += new System.EventHandler(this.WIRES_trans_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wIRESTRANSQUEUEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource wIRESTRANSQUEUEBindingSource;
        private DataSet1TableAdapters.WIRES_TRANS_QUEUETableAdapter wIRES_TRANS_QUEUETableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn sNDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wIREIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tRANSEMPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tRANSDATEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lOCATIONDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sTATUSDataGridViewTextBoxColumn;
        private System.Windows.Forms.CheckBox CB_start;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox rTB_message;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer2;
    }
}