namespace yt_dlp
{
    partial class Form1 : Form
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            checkedListBoxQuality = new CheckedListBox();
            textBoxUrl = new TextBox();
            buttonDownload = new Button();
            label1 = new Label();
            label2 = new Label();
            toggleCheckBox_DLhistory = new CheckBox();
            trackBar1 = new TrackBar();
            label3 = new Label();
            label4 = new Label();
            checkedListBoxPlatform = new CheckedListBox();
            referencebtn = new Button();
            referencetextbox = new TextBox();
            FileRefeTextbox = new TextBox();
            label5 = new Label();
            label6 = new Label();
            FileRefeBtn = new Button();
            label7 = new Label();
            toggleCheckBox_Thumbnail = new CheckBox();
            richTextBoxLog = new RichTextBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            setup_update = new Button();
            toggleCheckBox_DRMprotected = new CheckBox();
            label8 = new Label();
            label9 = new Label();
            toggleCheckBox_BATchange = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // checkedListBoxQuality
            // 
            checkedListBoxQuality.FormattingEnabled = true;
            checkedListBoxQuality.Location = new Point(15, 19);
            checkedListBoxQuality.Margin = new Padding(3, 2, 3, 2);
            checkedListBoxQuality.Name = "checkedListBoxQuality";
            checkedListBoxQuality.Size = new Size(200, 166);
            checkedListBoxQuality.TabIndex = 0;
            // 
            // textBoxUrl
            // 
            textBoxUrl.Location = new Point(15, 333);
            textBoxUrl.Margin = new Padding(3, 2, 3, 2);
            textBoxUrl.Name = "textBoxUrl";
            textBoxUrl.Size = new Size(319, 23);
            textBoxUrl.TabIndex = 1;
            // 
            // buttonDownload
            // 
            buttonDownload.AutoSize = true;
            buttonDownload.Location = new Point(339, 334);
            buttonDownload.Margin = new Padding(3, 2, 3, 2);
            buttonDownload.Name = "buttonDownload";
            buttonDownload.Size = new Size(87, 25);
            buttonDownload.TabIndex = 2;
            buttonDownload.Text = "ダウンロード";
            buttonDownload.UseVisualStyleBackColor = true;
            buttonDownload.Click += buttonDownload_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 2);
            label1.Name = "label1";
            label1.Size = new Size(102, 15);
            label1.TabIndex = 3;
            label1.Text = "ダウンロードする画質";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(226, 27);
            label2.Name = "label2";
            label2.Size = new Size(117, 15);
            label2.TabIndex = 4;
            label2.Text = "ダウンロード履歴の保存";
            // 
            // toggleCheckBox_DLhistory
            // 
            toggleCheckBox_DLhistory.Appearance = Appearance.Button;
            toggleCheckBox_DLhistory.Location = new Point(373, 23);
            toggleCheckBox_DLhistory.Margin = new Padding(3, 2, 3, 2);
            toggleCheckBox_DLhistory.Name = "toggleCheckBox_DLhistory";
            toggleCheckBox_DLhistory.Size = new Size(52, 22);
            toggleCheckBox_DLhistory.TabIndex = 0;
            toggleCheckBox_DLhistory.Text = "OFF";
            toggleCheckBox_DLhistory.TextAlign = ContentAlignment.MiddleCenter;
            toggleCheckBox_DLhistory.UseVisualStyleBackColor = true;
            toggleCheckBox_DLhistory.CheckedChanged += toggle_DLhistory;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(226, 153);
            trackBar1.Margin = new Padding(3, 2, 3, 2);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(200, 45);
            trackBar1.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(226, 133);
            label3.Name = "label3";
            label3.Size = new Size(95, 15);
            label3.TabIndex = 6;
            label3.Text = "並列ダウンロード数";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 189);
            label4.Name = "label4";
            label4.Size = new Size(321, 15);
            label4.TabIndex = 7;
            label4.Text = "Cookieの取得について (Firefoxでは自動取得することができます。)";
            // 
            // checkedListBoxPlatform
            // 
            checkedListBoxPlatform.FormattingEnabled = true;
            checkedListBoxPlatform.Location = new Point(15, 206);
            checkedListBoxPlatform.Margin = new Padding(3, 2, 3, 2);
            checkedListBoxPlatform.Name = "checkedListBoxPlatform";
            checkedListBoxPlatform.Size = new Size(411, 40);
            checkedListBoxPlatform.TabIndex = 0;
            // 
            // referencebtn
            // 
            referencebtn.AutoSize = true;
            referencebtn.Location = new Point(339, 251);
            referencebtn.Margin = new Padding(3, 2, 3, 2);
            referencebtn.Name = "referencebtn";
            referencebtn.Size = new Size(87, 25);
            referencebtn.TabIndex = 0;
            referencebtn.Text = "参照";
            referencebtn.UseVisualStyleBackColor = true;
            // 
            // referencetextbox
            // 
            referencetextbox.Location = new Point(15, 253);
            referencetextbox.Margin = new Padding(3, 2, 3, 2);
            referencetextbox.Name = "referencetextbox";
            referencetextbox.Size = new Size(319, 23);
            referencetextbox.TabIndex = 0;
            // 
            // FileRefeTextbox
            // 
            FileRefeTextbox.Location = new Point(15, 293);
            FileRefeTextbox.Margin = new Padding(3, 2, 3, 2);
            FileRefeTextbox.Name = "FileRefeTextbox";
            FileRefeTextbox.Size = new Size(319, 23);
            FileRefeTextbox.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 276);
            label5.Name = "label5";
            label5.Size = new Size(366, 15);
            label5.TabIndex = 9;
            label5.Text = "ダウンロード先（クラウドドライブのパスは正常に動作しない可能性があります。";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 316);
            label6.Name = "label6";
            label6.Size = new Size(99, 15);
            label6.TabIndex = 10;
            label6.Text = "ダウンロードするURL";
            // 
            // FileRefeBtn
            // 
            FileRefeBtn.AutoSize = true;
            FileRefeBtn.Location = new Point(339, 293);
            FileRefeBtn.Margin = new Padding(3, 2, 3, 2);
            FileRefeBtn.Name = "FileRefeBtn";
            FileRefeBtn.Size = new Size(87, 25);
            FileRefeBtn.TabIndex = 11;
            FileRefeBtn.Text = "参照";
            FileRefeBtn.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(226, 54);
            label7.Name = "label7";
            label7.Size = new Size(116, 15);
            label7.TabIndex = 12;
            label7.Text = "サムネイルのダウンロード";
            // 
            // toggleCheckBox_Thumbnail
            // 
            toggleCheckBox_Thumbnail.Appearance = Appearance.Button;
            toggleCheckBox_Thumbnail.Location = new Point(373, 50);
            toggleCheckBox_Thumbnail.Margin = new Padding(3, 2, 3, 2);
            toggleCheckBox_Thumbnail.Name = "toggleCheckBox_Thumbnail";
            toggleCheckBox_Thumbnail.Size = new Size(52, 22);
            toggleCheckBox_Thumbnail.TabIndex = 13;
            toggleCheckBox_Thumbnail.Text = "OFF";
            toggleCheckBox_Thumbnail.TextAlign = ContentAlignment.MiddleCenter;
            toggleCheckBox_Thumbnail.UseVisualStyleBackColor = true;
            toggleCheckBox_Thumbnail.CheckedChanged += toggle_Thumbnail;
            // 
            // richTextBoxLog
            // 
            richTextBoxLog.AutoSize = true;
            richTextBoxLog.Location = new Point(444, 25);
            richTextBoxLog.Margin = new Padding(3, 2, 3, 2);
            richTextBoxLog.Name = "richTextBoxLog";
            richTextBoxLog.ReadOnly = true;
            richTextBoxLog.Size = new Size(526, 330);
            richTextBoxLog.TabIndex = 0;
            richTextBoxLog.Text = "";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(15, 358);
            flowLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(676, 382);
            flowLayoutPanel1.TabIndex = 15;
            flowLayoutPanel1.WrapContents = false;
            // 
            // setup_update
            // 
            setup_update.AutoSize = true;
            setup_update.ImeMode = ImeMode.NoControl;
            setup_update.Location = new Point(735, -2);
            setup_update.Margin = new Padding(3, 2, 3, 2);
            setup_update.Name = "setup_update";
            setup_update.Size = new Size(234, 25);
            setup_update.TabIndex = 16;
            setup_update.Text = "初期セットアップ、バージョンアップ";
            setup_update.UseVisualStyleBackColor = true;
            setup_update.Click += setup_update_Click;
            // 
            // toggleCheckBox_DRMprotected
            // 
            toggleCheckBox_DRMprotected.Appearance = Appearance.Button;
            toggleCheckBox_DRMprotected.Location = new Point(373, 77);
            toggleCheckBox_DRMprotected.Margin = new Padding(3, 2, 3, 2);
            toggleCheckBox_DRMprotected.Name = "toggleCheckBox_DRMprotected";
            toggleCheckBox_DRMprotected.Size = new Size(52, 22);
            toggleCheckBox_DRMprotected.TabIndex = 17;
            toggleCheckBox_DRMprotected.Text = "OFF";
            toggleCheckBox_DRMprotected.TextAlign = ContentAlignment.MiddleCenter;
            toggleCheckBox_DRMprotected.UseVisualStyleBackColor = true;
            toggleCheckBox_DRMprotected.CheckedChanged += toggle_DRMprotected;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(226, 81);
            label8.Name = "label8";
            label8.Size = new Size(134, 15);
            label8.TabIndex = 18;
            label8.Text = "DRM保護を回避(非推奨)";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(226, 107);
            label9.Name = "label9";
            label9.Size = new Size(145, 15);
            label9.TabIndex = 19;
            label9.Text = "BATに切り替え(大量DL向け)";
            // 
            // toggleCheckBox_BATchange
            // 
            toggleCheckBox_BATchange.Appearance = Appearance.Button;
            toggleCheckBox_BATchange.Location = new Point(373, 103);
            toggleCheckBox_BATchange.Margin = new Padding(3, 2, 3, 2);
            toggleCheckBox_BATchange.Name = "toggleCheckBox_BATchange";
            toggleCheckBox_BATchange.Size = new Size(52, 22);
            toggleCheckBox_BATchange.TabIndex = 17;
            toggleCheckBox_BATchange.Text = "OFF";
            toggleCheckBox_BATchange.TextAlign = ContentAlignment.MiddleCenter;
            toggleCheckBox_BATchange.UseVisualStyleBackColor = true;
            toggleCheckBox_BATchange.CheckedChanged += toggle_BATchange;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 612);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(setup_update);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(richTextBoxLog);
            Controls.Add(toggleCheckBox_Thumbnail);
            Controls.Add(label7);
            Controls.Add(FileRefeBtn);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(FileRefeTextbox);
            Controls.Add(referencetextbox);
            Controls.Add(referencebtn);
            Controls.Add(checkedListBoxPlatform);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(trackBar1);
            Controls.Add(toggleCheckBox_DLhistory);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonDownload);
            Controls.Add(textBoxUrl);
            Controls.Add(checkedListBoxQuality);
            Controls.Add(toggleCheckBox_DRMprotected);
            Controls.Add(toggleCheckBox_BATchange);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();


        }

        #endregion


        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.TextBox referencetextbox;
        private System.Windows.Forms.TextBox FileRefeTextbox;
        private System.Windows.Forms.CheckedListBox checkedListBoxQuality;
        private System.Windows.Forms.CheckedListBox checkedListBoxPlatform;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.Button referencebtn;
        private System.Windows.Forms.Button FileRefeBtn;
        private System.Windows.Forms.Button setup_update;
        private System.Windows.Forms.CheckBox toggleCheckBox_BATchange;
        private System.Windows.Forms.CheckBox toggleCheckBox_DLhistory;
        private System.Windows.Forms.CheckBox toggleCheckBox_Thumbnail;
        private System.Windows.Forms.CheckBox toggleCheckBox_DRMprotected;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;

    }
}

