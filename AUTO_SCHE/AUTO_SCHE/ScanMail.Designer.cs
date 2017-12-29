namespace AUTO_SCHE
{
    partial class ScanMail
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
            this.mailListTB = new System.Windows.Forms.RichTextBox();
            this.messageTextBox = new System.Windows.Forms.RichTextBox();
            this.gridHeaders = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.listMessages = new System.Windows.Forms.TreeView();
            this.totalMessagesTextBox = new System.Windows.Forms.TextBox();
            this.useSslCheckBox = new System.Windows.Forms.CheckBox();
            this.popServerTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.connectAndRetrieveButton = new System.Windows.Forms.Button();
            this.listAttachments = new System.Windows.Forms.TreeView();
            this.loginTB = new System.Windows.Forms.TextBox();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeaders)).BeginInit();
            this.SuspendLayout();
            // 
            // mailListTB
            // 
            this.mailListTB.Location = new System.Drawing.Point(14, 524);
            this.mailListTB.Name = "mailListTB";
            this.mailListTB.Size = new System.Drawing.Size(899, 169);
            this.mailListTB.TabIndex = 35;
            this.mailListTB.Text = "";
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(299, 39);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(456, 308);
            this.messageTextBox.TabIndex = 33;
            this.messageTextBox.Text = "";
            // 
            // gridHeaders
            // 
            this.gridHeaders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHeaders.Location = new System.Drawing.Point(299, 353);
            this.gridHeaders.Name = "gridHeaders";
            this.gridHeaders.RowTemplate.Height = 24;
            this.gridHeaders.Size = new System.Drawing.Size(608, 136);
            this.gridHeaders.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(711, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "COUNT";
            // 
            // listMessages
            // 
            this.listMessages.Location = new System.Drawing.Point(12, 39);
            this.listMessages.Name = "listMessages";
            this.listMessages.Size = new System.Drawing.Size(281, 450);
            this.listMessages.TabIndex = 30;
            this.listMessages.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.listMessages_AfterSelect);
            // 
            // totalMessagesTextBox
            // 
            this.totalMessagesTextBox.Location = new System.Drawing.Point(761, 10);
            this.totalMessagesTextBox.Name = "totalMessagesTextBox";
            this.totalMessagesTextBox.Size = new System.Drawing.Size(59, 22);
            this.totalMessagesTextBox.TabIndex = 29;
            // 
            // useSslCheckBox
            // 
            this.useSslCheckBox.AutoSize = true;
            this.useSslCheckBox.Location = new System.Drawing.Point(376, 14);
            this.useSslCheckBox.Name = "useSslCheckBox";
            this.useSslCheckBox.Size = new System.Drawing.Size(43, 16);
            this.useSslCheckBox.TabIndex = 28;
            this.useSslCheckBox.Text = "SSL";
            this.useSslCheckBox.UseVisualStyleBackColor = true;
            // 
            // popServerTextBox
            // 
            this.popServerTextBox.Location = new System.Drawing.Point(99, 11);
            this.popServerTextBox.Name = "popServerTextBox";
            this.popServerTextBox.Size = new System.Drawing.Size(165, 22);
            this.popServerTextBox.TabIndex = 27;
            this.popServerTextBox.Text = "mail.jinnher.com.tw";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "MAIL SERVER";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(270, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "PORT";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(310, 11);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(50, 22);
            this.portTextBox.TabIndex = 24;
            this.portTextBox.Text = "110";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 495);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(901, 23);
            this.progressBar.TabIndex = 23;
            // 
            // connectAndRetrieveButton
            // 
            this.connectAndRetrieveButton.Location = new System.Drawing.Point(826, 10);
            this.connectAndRetrieveButton.Name = "connectAndRetrieveButton";
            this.connectAndRetrieveButton.Size = new System.Drawing.Size(81, 23);
            this.connectAndRetrieveButton.TabIndex = 22;
            this.connectAndRetrieveButton.Text = "Get Mail List";
            this.connectAndRetrieveButton.UseVisualStyleBackColor = true;
            this.connectAndRetrieveButton.Click += new System.EventHandler(this.connectAndRetrieveButton_Click);
            // 
            // listAttachments
            // 
            this.listAttachments.Location = new System.Drawing.Point(764, 39);
            this.listAttachments.Name = "listAttachments";
            this.listAttachments.Size = new System.Drawing.Size(143, 308);
            this.listAttachments.TabIndex = 36;
            // 
            // loginTB
            // 
            this.loginTB.Location = new System.Drawing.Point(488, 11);
            this.loginTB.Name = "loginTB";
            this.loginTB.Size = new System.Drawing.Size(86, 22);
            this.loginTB.TabIndex = 37;
            // 
            // passwordTB
            // 
            this.passwordTB.Location = new System.Drawing.Point(580, 11);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.PasswordChar = '*';
            this.passwordTB.Size = new System.Drawing.Size(100, 22);
            this.passwordTB.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(445, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 12);
            this.label4.TabIndex = 39;
            this.label4.Text = "ID/PW";
            // 
            // ScanMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 708);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.passwordTB);
            this.Controls.Add(this.loginTB);
            this.Controls.Add(this.listAttachments);
            this.Controls.Add(this.mailListTB);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.gridHeaders);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listMessages);
            this.Controls.Add(this.totalMessagesTextBox);
            this.Controls.Add(this.useSslCheckBox);
            this.Controls.Add(this.popServerTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.connectAndRetrieveButton);
            this.Name = "ScanMail";
            this.Text = "ScanMail";
            ((System.ComponentModel.ISupportInitialize)(this.gridHeaders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox mailListTB;
        private System.Windows.Forms.RichTextBox messageTextBox;
        private System.Windows.Forms.DataGridView gridHeaders;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView listMessages;
        private System.Windows.Forms.TextBox totalMessagesTextBox;
        private System.Windows.Forms.CheckBox useSslCheckBox;
        private System.Windows.Forms.TextBox popServerTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button connectAndRetrieveButton;
        private System.Windows.Forms.TreeView listAttachments;
        private System.Windows.Forms.TextBox loginTB;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.Label label4;

    }
}