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
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // checkedListBoxQuality
            // 
            checkedListBoxQuality.FormattingEnabled = true;
            checkedListBoxQuality.Location = new Point(17, 25);
            checkedListBoxQuality.Name = "checkedListBoxQuality";
            checkedListBoxQuality.Size = new Size(228, 224);
            checkedListBoxQuality.TabIndex = 0;
            // 
            // textBoxUrl
            // 
            textBoxUrl.Location = new Point(17, 444);
            textBoxUrl.Name = "textBoxUrl";
            textBoxUrl.Size = new Size(364, 27);
            textBoxUrl.TabIndex = 1;
            // 
            // buttonDownload
            // 
            buttonDownload.AutoSize = true;
            buttonDownload.Location = new Point(387, 446);
            buttonDownload.Name = "buttonDownload";
            buttonDownload.Size = new Size(99, 30);
            buttonDownload.TabIndex = 2;
            buttonDownload.Text = "ダウンロード";
            buttonDownload.UseVisualStyleBackColor = true;
            buttonDownload.Click += buttonDownload_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 2);
            label1.Name = "label1";
            label1.Size = new Size(127, 20);
            label1.TabIndex = 3;
            label1.Text = "ダウンロードする画質";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(258, 36);
            label2.Name = "label2";
            label2.Size = new Size(146, 20);
            label2.TabIndex = 4;
            label2.Text = "ダウンロード履歴の保存";
            // 
            // toggleCheckBox_DLhistory
            // 
            toggleCheckBox_DLhistory.Appearance = Appearance.Button;
            toggleCheckBox_DLhistory.Location = new Point(426, 31);
            toggleCheckBox_DLhistory.Name = "toggleCheckBox_DLhistory";
            toggleCheckBox_DLhistory.Size = new Size(60, 30);
            toggleCheckBox_DLhistory.TabIndex = 0;
            toggleCheckBox_DLhistory.Text = "NO";
            toggleCheckBox_DLhistory.TextAlign = ContentAlignment.MiddleCenter;
            toggleCheckBox_DLhistory.UseVisualStyleBackColor = true;
            toggleCheckBox_DLhistory.CheckedChanged += toggle_DLhistory;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(258, 187);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(228, 56);
            trackBar1.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(258, 164);
            label3.Name = "label3";
            label3.Size = new Size(119, 20);
            label3.TabIndex = 6;
            label3.Text = "並列ダウンロード数";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(17, 252);
            label4.Name = "label4";
            label4.Size = new Size(401, 20);
            label4.TabIndex = 7;
            label4.Text = "Cookieの取得について (Firefoxでは自動取得することができます。)";
            // 
            // checkedListBoxPlatform
            // 
            checkedListBoxPlatform.FormattingEnabled = true;
            checkedListBoxPlatform.Location = new Point(17, 275);
            checkedListBoxPlatform.Name = "checkedListBoxPlatform";
            checkedListBoxPlatform.Size = new Size(469, 70);
            checkedListBoxPlatform.TabIndex = 0;
            // 
            // referencebtn
            // 
            referencebtn.AutoSize = true;
            referencebtn.Location = new Point(387, 335);
            referencebtn.Name = "referencebtn";
            referencebtn.Size = new Size(99, 30);
            referencebtn.TabIndex = 0;
            referencebtn.Text = "参照";
            referencebtn.UseVisualStyleBackColor = true;
            // 
            // referencetextbox
            // 
            referencetextbox.Location = new Point(17, 337);
            referencetextbox.Name = "referencetextbox";
            referencetextbox.Size = new Size(364, 27);
            referencetextbox.TabIndex = 0;
            // 
            // FileRefeTextbox
            // 
            FileRefeTextbox.Location = new Point(17, 391);
            FileRefeTextbox.Name = "FileRefeTextbox";
            FileRefeTextbox.Size = new Size(364, 27);
            FileRefeTextbox.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 368);
            label5.Name = "label5";
            label5.Size = new Size(458, 20);
            label5.TabIndex = 9;
            label5.Text = "ダウンロード先（クラウドドライブのパスは正常に動作しない可能性があります。";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(17, 421);
            label6.Name = "label6";
            label6.Size = new Size(123, 20);
            label6.TabIndex = 10;
            label6.Text = "ダウンロードするURL";
            // 
            // FileRefeBtn
            // 
            FileRefeBtn.AutoSize = true;
            FileRefeBtn.Location = new Point(387, 391);
            FileRefeBtn.Name = "FileRefeBtn";
            FileRefeBtn.Size = new Size(99, 30);
            FileRefeBtn.TabIndex = 11;
            FileRefeBtn.Text = "参照";
            FileRefeBtn.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(260, 72);
            label7.Name = "label7";
            label7.Size = new Size(144, 20);
            label7.TabIndex = 12;
            label7.Text = "サムネイルのダウンロード";
            // 
            // toggleCheckBox_Thumbnail
            // 
            toggleCheckBox_Thumbnail.Appearance = Appearance.Button;
            toggleCheckBox_Thumbnail.Location = new Point(426, 67);
            toggleCheckBox_Thumbnail.Name = "toggleCheckBox_Thumbnail";
            toggleCheckBox_Thumbnail.Size = new Size(60, 30);
            toggleCheckBox_Thumbnail.TabIndex = 13;
            toggleCheckBox_Thumbnail.Text = "NO";
            toggleCheckBox_Thumbnail.TextAlign = ContentAlignment.MiddleCenter;
            toggleCheckBox_Thumbnail.UseVisualStyleBackColor = true;
            toggleCheckBox_Thumbnail.CheckedChanged += toggle_Thumbnail;
            // 
            // richTextBoxLog
            // 
            richTextBoxLog.AutoSize = true;
            richTextBoxLog.Location = new Point(507, 33);
            richTextBoxLog.Name = "richTextBoxLog";
            richTextBoxLog.ReadOnly = true;
            richTextBoxLog.Size = new Size(600, 438);
            richTextBoxLog.TabIndex = 0;
            richTextBoxLog.Text = "";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(17, 477);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(772, 510);
            flowLayoutPanel1.TabIndex = 15;
            flowLayoutPanel1.WrapContents = false;
            // 
            // setup_update
            // 
            setup_update.AutoSize = true;
            setup_update.ImeMode = ImeMode.NoControl;
            setup_update.Location = new Point(840, -3);
            setup_update.Name = "setup_update";
            setup_update.Size = new Size(267, 30);
            setup_update.TabIndex = 16;
            setup_update.Text = "初期セットアップ、バージョンアップ";
            setup_update.UseVisualStyleBackColor = true;
            setup_update.Click += setup_update_Click;
            // 
            // toggleCheckBox_DRMprotected
            // 
            toggleCheckBox_DRMprotected.Appearance = Appearance.Button;
            toggleCheckBox_DRMprotected.Location = new Point(426, 103);
            toggleCheckBox_DRMprotected.Name = "toggleCheckBox_DRMprotected";
            toggleCheckBox_DRMprotected.Size = new Size(60, 30);
            toggleCheckBox_DRMprotected.TabIndex = 17;
            toggleCheckBox_DRMprotected.Text = "NO";
            toggleCheckBox_DRMprotected.TextAlign = ContentAlignment.MiddleCenter;
            toggleCheckBox_DRMprotected.UseVisualStyleBackColor = true;
            toggleCheckBox_DRMprotected.CheckedChanged += toggle_DRMprotected;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(258, 108);
            label8.Name = "label8";
            label8.Size = new Size(168, 20);
            label8.TabIndex = 18;
            label8.Text = "DRM保護を回避(非推奨)";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1125, 816);
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
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();


        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxQuality;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox toggleCheckBox_DLhistory;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox checkedListBoxPlatform;
        private System.Windows.Forms.Button referencebtn;
        private System.Windows.Forms.TextBox referencetextbox;
        private System.Windows.Forms.TextBox FileRefeTextbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button FileRefeBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox toggleCheckBox_Thumbnail;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button setup_update;
        private System.Windows.Forms.CheckBox toggleCheckBox_DRMprotected;
        private Label label8;
    }
}

