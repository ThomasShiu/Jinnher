namespace AUTO_SCHE
{
    partial class ScanMail_Auto
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
            this.mailListTB = new System.Windows.Forms.RichTextBox();
            this.counterLabel = new System.Windows.Forms.Label();
            this.useSslCheckBox = new System.Windows.Forms.CheckBox();
            this.popServerTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.connectAndRetrieveButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.counter2Label = new System.Windows.Forms.Label();
            this.counter3Label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.IDlabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.startScheCB = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerValTB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.counter4Label = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.errorTB = new System.Windows.Forms.RichTextBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // mailListTB
            // 
            this.mailListTB.Location = new System.Drawing.Point(16, 40);
            this.mailListTB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mailListTB.Name = "mailListTB";
            this.mailListTB.Size = new System.Drawing.Size(1200, 313);
            this.mailListTB.TabIndex = 52;
            this.mailListTB.Text = "";
            this.mailListTB.TextChanged += new System.EventHandler(this.mailListTB_TextChanged);
            // 
            // counterLabel
            // 
            this.counterLabel.AutoSize = true;
            this.counterLabel.ForeColor = System.Drawing.Color.Red;
            this.counterLabel.Location = new System.Drawing.Point(388, 404);
            this.counterLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.counterLabel.Name = "counterLabel";
            this.counterLabel.Size = new System.Drawing.Size(14, 15);
            this.counterLabel.TabIndex = 49;
            this.counterLabel.Text = "0";
            // 
            // useSslCheckBox
            // 
            this.useSslCheckBox.AutoSize = true;
            this.useSslCheckBox.Location = new System.Drawing.Point(501, 9);
            this.useSslCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.useSslCheckBox.Name = "useSslCheckBox";
            this.useSslCheckBox.Size = new System.Drawing.Size(54, 19);
            this.useSslCheckBox.TabIndex = 46;
            this.useSslCheckBox.Text = "SSL";
            this.useSslCheckBox.UseVisualStyleBackColor = true;
            // 
            // popServerTextBox
            // 
            this.popServerTextBox.Location = new System.Drawing.Point(132, 5);
            this.popServerTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.popServerTextBox.Name = "popServerTextBox";
            this.popServerTextBox.Size = new System.Drawing.Size(219, 25);
            this.popServerTextBox.TabIndex = 45;
            this.popServerTextBox.Text = "mail.jinnher.com.tw";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 15);
            this.label2.TabIndex = 44;
            this.label2.Text = "MAIL SERVER";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(360, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 43;
            this.label1.Text = "PORT";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(413, 5);
            this.portTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(65, 25);
            this.portTextBox.TabIndex = 42;
            this.portTextBox.Text = "110";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(19, 361);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1201, 29);
            this.progressBar.TabIndex = 41;
            // 
            // connectAndRetrieveButton
            // 
            this.connectAndRetrieveButton.Location = new System.Drawing.Point(1101, 4);
            this.connectAndRetrieveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.connectAndRetrieveButton.Name = "connectAndRetrieveButton";
            this.connectAndRetrieveButton.Size = new System.Drawing.Size(108, 29);
            this.connectAndRetrieveButton.TabIndex = 40;
            this.connectAndRetrieveButton.Text = "執行掃描";
            this.connectAndRetrieveButton.UseVisualStyleBackColor = true;
            this.connectAndRetrieveButton.Click += new System.EventHandler(this.connectAndRetrieveButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // counter2Label
            // 
            this.counter2Label.AutoSize = true;
            this.counter2Label.ForeColor = System.Drawing.Color.Red;
            this.counter2Label.Location = new System.Drawing.Point(543, 404);
            this.counter2Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.counter2Label.Name = "counter2Label";
            this.counter2Label.Size = new System.Drawing.Size(14, 15);
            this.counter2Label.TabIndex = 53;
            this.counter2Label.Text = "0";
            // 
            // counter3Label
            // 
            this.counter3Label.AutoSize = true;
            this.counter3Label.ForeColor = System.Drawing.Color.Red;
            this.counter3Label.Location = new System.Drawing.Point(716, 404);
            this.counter3Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.counter3Label.Name = "counter3Label";
            this.counter3Label.Size = new System.Drawing.Size(14, 15);
            this.counter3Label.TabIndex = 54;
            this.counter3Label.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 404);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 55;
            this.label3.Text = "目前帳號";
            // 
            // IDlabel
            // 
            this.IDlabel.AutoSize = true;
            this.IDlabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.IDlabel.Location = new System.Drawing.Point(97, 404);
            this.IDlabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.IDlabel.Name = "IDlabel";
            this.IDlabel.Size = new System.Drawing.Size(22, 15);
            this.IDlabel.TabIndex = 56;
            this.IDlabel.Text = "ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(264, 404);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 15);
            this.label4.TabIndex = 57;
            this.label4.Text = "目前帳號總筆數";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(467, 404);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 58;
            this.label5.Text = "處理筆數";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(605, 404);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 59;
            this.label6.Text = "已紀錄總筆數";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.nameLabel.Location = new System.Drawing.Point(160, 404);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(49, 15);
            this.nameLabel.TabIndex = 60;
            this.nameLabel.Text = "NAME";
            // 
            // startScheCB
            // 
            this.startScheCB.AutoSize = true;
            this.startScheCB.Location = new System.Drawing.Point(985, 9);
            this.startScheCB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.startScheCB.Name = "startScheCB";
            this.startScheCB.Size = new System.Drawing.Size(89, 19);
            this.startScheCB.TabIndex = 61;
            this.startScheCB.Text = "啟動排程";
            this.startScheCB.UseVisualStyleBackColor = true;
            this.startScheCB.CheckedChanged += new System.EventHandler(this.startScheCB_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 851);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1237, 24);
            this.statusStrip1.TabIndex = 62;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(123, 19);
            this.toolStripStatusLabel1.Text = "郵件偵測待命中...";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 19);
            // 
            // timerValTB
            // 
            this.timerValTB.Location = new System.Drawing.Point(581, 6);
            this.timerValTB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.timerValTB.Name = "timerValTB";
            this.timerValTB.Size = new System.Drawing.Size(29, 25);
            this.timerValTB.TabIndex = 63;
            this.timerValTB.Text = "120";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(620, 11);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 15);
            this.label7.TabIndex = 64;
            this.label7.Text = "偵測間隔(分鐘)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(775, 404);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 15);
            this.label8.TabIndex = 66;
            this.label8.Text = "錯誤";
            // 
            // counter4Label
            // 
            this.counter4Label.AutoSize = true;
            this.counter4Label.ForeColor = System.Drawing.Color.Red;
            this.counter4Label.Location = new System.Drawing.Point(820, 404);
            this.counter4Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.counter4Label.Name = "counter4Label";
            this.counter4Label.Size = new System.Drawing.Size(14, 15);
            this.counter4Label.TabIndex = 67;
            this.counter4Label.Text = "0";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(21, 422);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1188, 209);
            this.dataGridView1.TabIndex = 68;
            // 
            // errorTB
            // 
            this.errorTB.BackColor = System.Drawing.Color.MistyRose;
            this.errorTB.Location = new System.Drawing.Point(21, 639);
            this.errorTB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.errorTB.Name = "errorTB";
            this.errorTB.Size = new System.Drawing.Size(1187, 204);
            this.errorTB.TabIndex = 69;
            this.errorTB.Text = "";
            this.errorTB.TextChanged += new System.EventHandler(this.errorTB_TextChanged);
            // 
            // ScanMail_Auto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 875);
            this.Controls.Add(this.errorTB);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.counter4Label);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.timerValTB);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.startScheCB);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.IDlabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.counter3Label);
            this.Controls.Add(this.counter2Label);
            this.Controls.Add(this.mailListTB);
            this.Controls.Add(this.counterLabel);
            this.Controls.Add(this.useSslCheckBox);
            this.Controls.Add(this.popServerTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.connectAndRetrieveButton);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ScanMail_Auto";
            this.Text = "自動掃描郵件 BY Thomas";
            this.Load += new System.EventHandler(this.ScanMail_Auto_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox mailListTB;
        private System.Windows.Forms.Label counterLabel;
        private System.Windows.Forms.CheckBox useSslCheckBox;
        private System.Windows.Forms.TextBox popServerTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button connectAndRetrieveButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label counter2Label;
        private System.Windows.Forms.Label counter3Label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label IDlabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.CheckBox startScheCB;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox timerValTB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label counter4Label;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox errorTB;

    }
}