namespace AUTO_SCHE
{
    partial class BBI_datatrans01
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
            this.CB_timerStart = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.message = new System.Windows.Forms.Label();
            this.rTB_log = new System.Windows.Forms.RichTextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dS_bbishipqueue = new AUTO_SCHE.DS_bbishipqueue();
            this.clear_btn = new System.Windows.Forms.Button();
            this.btn_savelog = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.rTB_log2 = new System.Windows.Forms.RichTextBox();
            this.CB_timerStart2 = new System.Windows.Forms.CheckBox();
            this.clear_btn2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dS_bbishipqueue)).BeginInit();
            this.SuspendLayout();
            // 
            // CB_timerStart
            // 
            this.CB_timerStart.AutoSize = true;
            this.CB_timerStart.Location = new System.Drawing.Point(12, 12);
            this.CB_timerStart.Name = "CB_timerStart";
            this.CB_timerStart.Size = new System.Drawing.Size(72, 16);
            this.CB_timerStart.TabIndex = 0;
            this.CB_timerStart.Text = "啟動排程";
            this.CB_timerStart.UseVisualStyleBackColor = true;
            this.CB_timerStart.CheckedChanged += new System.EventHandler(this.CB_timerStart_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 180000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // message
            // 
            this.message.AutoSize = true;
            this.message.Location = new System.Drawing.Point(12, 370);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(0, 12);
            this.message.TabIndex = 3;
            // 
            // rTB_log
            // 
            this.rTB_log.Location = new System.Drawing.Point(12, 34);
            this.rTB_log.Name = "rTB_log";
            this.rTB_log.ReadOnly = true;
            this.rTB_log.Size = new System.Drawing.Size(444, 404);
            this.rTB_log.TabIndex = 4;
            this.rTB_log.Text = "";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = this.dS_bbishipqueue;
            this.bindingSource1.Position = 0;
            // 
            // dS_bbishipqueue
            // 
            this.dS_bbishipqueue.DataSetName = "DS_bbishipqueue";
            this.dS_bbishipqueue.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // clear_btn
            // 
            this.clear_btn.Location = new System.Drawing.Point(296, 4);
            this.clear_btn.Name = "clear_btn";
            this.clear_btn.Size = new System.Drawing.Size(75, 23);
            this.clear_btn.TabIndex = 5;
            this.clear_btn.Text = "清除";
            this.clear_btn.UseVisualStyleBackColor = true;
            this.clear_btn.Click += new System.EventHandler(this.clear_btn_Click);
            // 
            // btn_savelog
            // 
            this.btn_savelog.Location = new System.Drawing.Point(194, 4);
            this.btn_savelog.Name = "btn_savelog";
            this.btn_savelog.Size = new System.Drawing.Size(75, 23);
            this.btn_savelog.TabIndex = 6;
            this.btn_savelog.Text = "紀錄LOG";
            this.btn_savelog.UseVisualStyleBackColor = true;
            this.btn_savelog.Click += new System.EventHandler(this.btn_savelog_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 60000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // rTB_log2
            // 
            this.rTB_log2.Location = new System.Drawing.Point(462, 34);
            this.rTB_log2.Name = "rTB_log2";
            this.rTB_log2.ReadOnly = true;
            this.rTB_log2.Size = new System.Drawing.Size(325, 404);
            this.rTB_log2.TabIndex = 7;
            this.rTB_log2.Text = "";
            // 
            // CB_timerStart2
            // 
            this.CB_timerStart2.AutoSize = true;
            this.CB_timerStart2.Location = new System.Drawing.Point(462, 12);
            this.CB_timerStart2.Name = "CB_timerStart2";
            this.CB_timerStart2.Size = new System.Drawing.Size(72, 16);
            this.CB_timerStart2.TabIndex = 8;
            this.CB_timerStart2.Text = "啟動排程";
            this.CB_timerStart2.UseVisualStyleBackColor = true;
            this.CB_timerStart2.CheckedChanged += new System.EventHandler(this.CB_timerStart2_CheckedChanged);
            // 
            // clear_btn2
            // 
            this.clear_btn2.Location = new System.Drawing.Point(703, 5);
            this.clear_btn2.Name = "clear_btn2";
            this.clear_btn2.Size = new System.Drawing.Size(75, 23);
            this.clear_btn2.TabIndex = 9;
            this.clear_btn2.Text = "清除";
            this.clear_btn2.UseVisualStyleBackColor = true;
            this.clear_btn2.Click += new System.EventHandler(this.clear_btn2_Click);
            // 
            // BBI_datatrans01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 440);
            this.Controls.Add(this.clear_btn2);
            this.Controls.Add(this.CB_timerStart2);
            this.Controls.Add(this.rTB_log2);
            this.Controls.Add(this.btn_savelog);
            this.Controls.Add(this.clear_btn);
            this.Controls.Add(this.rTB_log);
            this.Controls.Add(this.message);
            this.Controls.Add(this.CB_timerStart);
            this.Name = "BBI_datatrans01";
            this.Text = "BBI Ready Mill訂單轉檔";
            this.Load += new System.EventHandler(this.BBI_datatrans01_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dS_bbishipqueue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CB_timerStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DS_bbishipqueue dS_bbishipqueue;
        private System.Windows.Forms.Label message;
        private System.Windows.Forms.RichTextBox rTB_log;
        private System.Windows.Forms.Button clear_btn;
        private System.Windows.Forms.Button btn_savelog;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.RichTextBox rTB_log2;
        private System.Windows.Forms.CheckBox CB_timerStart2;
        private System.Windows.Forms.Button clear_btn2;
    }
}