namespace AUTO_SCHE
{
    partial class MTM_datatrans01
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.CB_timerStart = new System.Windows.Forms.CheckBox();
            this.rTB_log_out = new System.Windows.Forms.RichTextBox();
            this.rTB_log_in = new System.Windows.Forms.RichTextBox();
            this.btn_out = new System.Windows.Forms.Button();
            this.btn_in = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "模工具領用轉檔";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "模工具繳回轉檔";
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CB_timerStart
            // 
            this.CB_timerStart.AutoSize = true;
            this.CB_timerStart.Location = new System.Drawing.Point(612, 12);
            this.CB_timerStart.Name = "CB_timerStart";
            this.CB_timerStart.Size = new System.Drawing.Size(72, 16);
            this.CB_timerStart.TabIndex = 2;
            this.CB_timerStart.Text = "啟動排程";
            this.CB_timerStart.UseVisualStyleBackColor = true;
            this.CB_timerStart.CheckedChanged += new System.EventHandler(this.CB_timerStart_CheckedChanged);
            // 
            // rTB_log_out
            // 
            this.rTB_log_out.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.rTB_log_out.Location = new System.Drawing.Point(5, 35);
            this.rTB_log_out.Name = "rTB_log_out";
            this.rTB_log_out.ReadOnly = true;
            this.rTB_log_out.Size = new System.Drawing.Size(686, 155);
            this.rTB_log_out.TabIndex = 3;
            this.rTB_log_out.Text = "";
            // 
            // rTB_log_in
            // 
            this.rTB_log_in.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.rTB_log_in.Location = new System.Drawing.Point(5, 240);
            this.rTB_log_in.Name = "rTB_log_in";
            this.rTB_log_in.ReadOnly = true;
            this.rTB_log_in.Size = new System.Drawing.Size(686, 198);
            this.rTB_log_in.TabIndex = 4;
            this.rTB_log_in.Text = "";
            // 
            // btn_out
            // 
            this.btn_out.Location = new System.Drawing.Point(599, 196);
            this.btn_out.Name = "btn_out";
            this.btn_out.Size = new System.Drawing.Size(92, 23);
            this.btn_out.TabIndex = 5;
            this.btn_out.Text = "匯出領用紀錄";
            this.btn_out.UseVisualStyleBackColor = true;
            this.btn_out.Click += new System.EventHandler(this.btn_out_Click);
            // 
            // btn_in
            // 
            this.btn_in.Location = new System.Drawing.Point(599, 444);
            this.btn_in.Name = "btn_in";
            this.btn_in.Size = new System.Drawing.Size(92, 23);
            this.btn_in.TabIndex = 6;
            this.btn_in.Text = "匯出繳回紀錄";
            this.btn_in.UseVisualStyleBackColor = true;
            this.btn_in.Click += new System.EventHandler(this.btn_in_Click);
            // 
            // MTM_datatrans01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 465);
            this.Controls.Add(this.btn_in);
            this.Controls.Add(this.btn_out);
            this.Controls.Add(this.rTB_log_in);
            this.Controls.Add(this.rTB_log_out);
            this.Controls.Add(this.CB_timerStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MTM_datatrans01";
            this.Text = "物料資料轉入";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox CB_timerStart;
        private System.Windows.Forms.RichTextBox rTB_log_out;
        private System.Windows.Forms.RichTextBox rTB_log_in;
        private System.Windows.Forms.Button btn_out;
        private System.Windows.Forms.Button btn_in;
    }
}