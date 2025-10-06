using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace yt_dlp
{
    public partial class Form1 : Form
    {
        string settingsPath = "settings.txt";
        string urlListPath = "URL_list.txt";
        string versionPath = "Version.txt";

        public Form1()
        {
            InitializeComponent();

            checkedListBoxQuality.Items.AddRange(new string[]
            {
                "4320p（7680 x 4320）",
                "2160p（3840 x 2160）",
                "1440p（2560 x 1440）",
                "1080p（1920 x 1080）",
                "720p （1280 x 720 ）",
                "480p （ 720 x 480 ）",
                "360p （ 640 x 360 ）",
                "240p （ 427 x 240 ）",
                "144p （ 256 x 144 ）"
            });

            checkedListBoxQuality.CheckOnClick = true;
            checkedListBoxQuality.ItemCheck += CheckedListBoxQuality_ItemCheck;

            trackBar1.Minimum = 1;
            trackBar1.Maximum = 32;
            trackBar1.Value = 8; // 初期値（お好みで変更）
            trackBar1.ValueChanged += TrackBar1_ValueChanged;

            checkedListBoxPlatform.Items.AddRange(new string[] { "Firefox (Youtubeにログインしている必要があります。)", "それ以外" });
            checkedListBoxPlatform.CheckOnClick = true;
            checkedListBoxPlatform.ItemCheck += CheckedListBoxPlatform_ItemCheck;

            referencebtn.Visible = false;
            referencetextbox.Visible = false;
            referencebtn.Click += Referencebtn_Click;
            referencetextbox.TextChanged += Referencetextbox_TextChanged;

            FileRefeBtn.Click += FileRefeBtn_Click;
            FileRefeTextbox.TextChanged += FileRefeTextbox_TextChanged;

            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            File.WriteAllText(versionPath, "0.2.9");

            string versionText = "不明";

            if (File.Exists(versionPath))
            {
                versionText = File.ReadAllText(versionPath).Trim();
            }

            // フォームのタイトルを設定
            this.Text = $"Youtubeダウンローダー {versionText}";


            if (!File.Exists(settingsPath)) return;

            string[] lines = File.ReadAllLines(settingsPath);

            // 1行目: download_quality=X
            if (lines.Length >= 1 && lines[0].StartsWith("download_quality="))
            {
                string numberPart = lines[0].Replace("download_quality=", "").Trim();
                if (int.TryParse(numberPart, out int index))
                {
                    index -= 1;
                    if (index >= 0 && index < checkedListBoxQuality.Items.Count)
                    {
                        checkedListBoxQuality.SetItemChecked(index, true);
                    }
                }
            }

            // 2行目: DLhistory=yes/no
            if (lines.Length >= 2 && lines[1].StartsWith("DLhistory="))
            {
                string flag = lines[1].Substring("DLhistory=".Length).Trim().ToLower();
                if (flag == "on")
                {
                    toggleCheckBox_DLhistory.Checked = true;
                }
                else if (flag == "off")
                {
                    toggleCheckBox_DLhistory.Checked = false;
                }
                toggleCheckBox_DLhistory.Text = toggleCheckBox_DLhistory.Checked ? "ON" : "OFF";
            }

            // 3行目: parallelDL=1~32
            if (lines.Length >= 3 && lines[2].StartsWith("parallelDL="))
            {
                string val = lines[2].Substring("parallelDL=".Length).Trim();
                if (int.TryParse(val, out int n) && n >= trackBar1.Minimum && n <= trackBar1.Maximum)
                {
                    trackBar1.Value = n;
                }
            }

            // label3の設定
            if (label3 != null) { label3.Text = $"並列ダウンロード数      並列数: {trackBar1.Value}"; }

            // 4行目: platform=firefox/other、cookieのパス設定入力
            if (lines.Length >= 4 && lines[3].StartsWith("Platform="))
            {
                string platform = lines[3].Substring("Platform=".Length).Trim().ToLower();
                if (platform == "firefox")
                {
                    checkedListBoxPlatform.SetItemChecked(0, true);
                    referencebtn.Visible = false;
                    referencetextbox.Visible = false;
                }
                else if (platform == "other")
                {
                    checkedListBoxPlatform.SetItemChecked(1, true);
                    referencebtn.Visible = true;
                    referencetextbox.Visible = true;
                }
            }

            // settings.txt の5行目を referencetextbox に表示
            if (lines.Length >= 5) { referencetextbox.Text = lines[4]; }

            // 6行目: 保存先のパス設定入力
            if (lines.Length >= 6) { FileRefeTextbox.Text = lines[5].Trim(); }

            // 7行目: Thumbnail=yes/no
            if (lines.Length >= 7 && lines[6].StartsWith("Thumbnail="))
            {
                string flag = lines[6].Substring("Thumbnail=".Length).Trim().ToLower();
                if (flag == "on")
                {
                    toggleCheckBox_Thumbnail.Checked = true;
                }
                else if (flag == "off")
                {
                    toggleCheckBox_Thumbnail.Checked = false;
                }
                toggleCheckBox_Thumbnail.Text = toggleCheckBox_Thumbnail.Checked ? "ON" : "OFF";
            }

            // 8行目: DRMprotected=yes/no
            if (lines.Length >= 8 && lines[7].StartsWith("DRMprotected="))
            {
                string flag = lines[7].Substring("DRMprotected=".Length).Trim().ToLower();
                if (flag == "on")
                {
                    toggleCheckBox_DRMprotected.Checked = true;
                }
                else if (flag == "off")
                {
                    toggleCheckBox_DRMprotected.Checked = false;
                }
                toggleCheckBox_DRMprotected.Text = toggleCheckBox_DRMprotected.Checked ? "ON" : "OFF";
            }

            // 9行目: BATrun=yes/no
            if (lines.Length >= 9 && lines[8].StartsWith("BATrun="))
            {
                string flag = lines[8].Substring("BATrun=".Length).Trim().ToLower();
                if (flag == "on")
                {
                    toggleCheckBox_BATchange.Checked = true;
                }
                else if (flag == "off")
                {
                    toggleCheckBox_BATchange.Checked = false;
                }
                toggleCheckBox_BATchange.Text = toggleCheckBox_BATchange.Checked ? "ON" : "OFF";
            }
        }

        private void CheckedListBoxQuality_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < checkedListBoxQuality.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        checkedListBoxQuality.SetItemChecked(i, false);
                    }
                }

                int selectedIndex = e.Index + 1;

                string[] lines = File.Exists(settingsPath) ? File.ReadAllLines(settingsPath) : new string[2];
                lines = SettingsLines(lines);

                lines[0] = $"download_quality={selectedIndex}";
                File.WriteAllLines(settingsPath, lines);

            }
        }

        private void toggle_DLhistory(object sender, EventArgs e)
        {
            toggleCheckBox_DLhistory.Text = toggleCheckBox_DLhistory.Checked ? "ON" : "OFF";

            string selected = toggleCheckBox_DLhistory.Checked ? "on" : "off";

            string[] lines = File.Exists(settingsPath) ? File.ReadAllLines(settingsPath) : new string[2];
            lines = SettingsLines(lines);

            lines[1] = $"DLhistory={selected}";
            File.WriteAllLines(settingsPath, lines);
        }

        private void toggle_Thumbnail(object sender, EventArgs e)
        {
            toggleCheckBox_Thumbnail.Text = toggleCheckBox_Thumbnail.Checked ? "ON" : "OFF";

            string selected = toggleCheckBox_Thumbnail.Checked ? "on" : "off";

            string[] lines = File.Exists(settingsPath) ? File.ReadAllLines(settingsPath) : new string[7];
            lines = SettingsLines(lines);

            lines[6] = $"Thumbnail={selected}";
            File.WriteAllLines(settingsPath, lines);
        }

        private void toggle_DRMprotected(object sender, EventArgs e)
        {
            toggleCheckBox_DRMprotected.Text = toggleCheckBox_DRMprotected.Checked ? "ON" : "OFF";
            string selected = toggleCheckBox_DRMprotected.Checked ? "on" : "off";
            string[] lines = File.Exists(settingsPath) ? File.ReadAllLines(settingsPath) : new string[8]; // ここを8に変更
            lines = SettingsLines(lines);
            lines[7] = $"DRMprotected={selected}"; // 8行目（インデックス7）に書き込み
            File.WriteAllLines(settingsPath, lines);
        }

        private void toggle_BATchange(object sender, EventArgs e)
        {
            toggleCheckBox_BATchange.Text = toggleCheckBox_BATchange.Checked ? "ON" : "OFF";
            string selected = toggleCheckBox_BATchange.Checked ? "on" : "off";
            string[] lines = File.Exists(settingsPath) ? File.ReadAllLines(settingsPath) : new string[9];
            lines = SettingsLines(lines);
            lines[8] = $"BATrun={selected}";
            File.WriteAllLines(settingsPath, lines);
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            // ✅ 画質設定の保存
            var selected = checkedListBoxQuality.CheckedIndices;
            if (selected.Count != 1)
            {
                MessageBox.Show("画質は1つだけ選択してください。", "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = selected[0]; // 0ベース
            int selectedNumber = selectedIndex + 1; // 1〜9 に変換

            string[] lines = File.Exists(settingsPath) ? File.ReadAllLines(settingsPath) : new string[2];
            lines = SettingsLines(lines);
            lines[0] = $"download_quality={selectedNumber}";
            File.WriteAllLines(settingsPath, lines);

            string url = textBoxUrl.Text.Trim();

            // settings.txt から Platform を読み取り
            string platformLine = lines.Length >= 4 ? lines[3].Trim() : "";
            bool isFirefox = platformLine.Equals("Platform=Firefox", StringComparison.OrdinalIgnoreCase);

            if (isFirefox)
            {
                try
                {
                    ProcessStartInfo cookieProcInfo = new ProcessStartInfo
                    {
                        FileName = "yt-dlp",
                        Arguments = "--cookies-from-browser firefox --cookies cookies.txt",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using (Process cookieProc = Process.Start(cookieProcInfo))
                    {
                        cookieProc.WaitForExit(); // cookie 抽出完了まで待機（非同期にしてもOK）
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Firefox cookie 抽出に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(url))
            {
                MessageBox.Show("URLを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isPlaylist =
                url.Contains("youtube.com/playlist?list=") ||
                url.Contains("youtube.com/@") ||
                url.Contains("youtube.com/channel") ||
                url.Contains("abema.tv/video/title");

            bool isSingleVideo =
                url.Contains("youtube.com/watch?v=") ||
                url.Contains("youtu.be/") ||
                url.Contains("youtube.com/shorts/") ||
                url.Contains("abema.tv/video/episode");

            if (isPlaylist)
            {
                try
                {
                    // yt-dlp でプレイリストURL群を取得し、URL_list.txt に出力
                    string args;
                    args = $"\"{url}\" --flat-playlist --skip-download --get-url --quiet --no-warnings";

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "yt-dlp",
                        Arguments = args,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using (Process proc = Process.Start(psi))
                    using (StreamReader reader = proc.StandardOutput)
                    {
                        string output = reader.ReadToEnd();
                        File.WriteAllText(urlListPath, output);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"yt-dlp の実行中にエラーが発生しました:\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (isSingleVideo)
            {
                int listIndex = url.IndexOf("&list=");
                if (listIndex >= 0)
                {
                    url = url.Substring(0, listIndex);
                }
                int siIndex = url.IndexOf("?si=");
                if (siIndex >= 0)
                {
                    url = url.Substring(0, siIndex);
                }
                File.WriteAllText(urlListPath, url + Environment.NewLine);
            }
            else
            {
                MessageBox.Show("入力されたURLは対応する形式ではありません。", "不正なURL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RunYtDlpFromSettings();
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            string[] lines = File.Exists(settingsPath) ? File.ReadAllLines(settingsPath) : new string[3];
            lines = SettingsLines(lines);
            lines[2] = $"parallelDL={trackBar1.Value}";
            File.WriteAllLines(settingsPath, lines);

            //label3の設定
            if (label3 != null)
            {
                label3.Text = $"並列ダウンロード数      並列数: {trackBar1.Value}";
            }
        }

        private void CheckedListBoxPlatform_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // 他の項目のチェックを外す（排他選択）
            for (int i = 0; i < checkedListBoxPlatform.Items.Count; i++)
            {
                if (i != e.Index)
                {
                    checkedListBoxPlatform.SetItemChecked(i, false);
                }
            }

            string selected = (e.Index == 0) ? "Firefox" : "other";
            referencebtn.Visible = (selected == "other");
            referencetextbox.Visible = (selected == "other");

            string[] lines = File.Exists(settingsPath) ? File.ReadAllLines(settingsPath) : new string[5];
            lines = SettingsLines(lines);
            lines[3] = $"Platform={selected}";

            File.WriteAllLines(settingsPath, lines);


        }

        private void Referencebtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "ファイルを選択してください";
                dialog.Filter = "テキストファイル (*.txt)|*.txt";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = dialog.FileName;

                    string[] lines = File.Exists(settingsPath) ? File.ReadAllLines(settingsPath) : new string[5];
                    lines = SettingsLines(lines);
                    lines[4] = path;
                    File.WriteAllLines(settingsPath, lines);
                    referencetextbox.Text = path; // ← textbox にも反映
                }
            }
        }

        private string[] SettingsLines(string[] lines)
        {
            string[] result = new string[9];
            result[0] = (lines.Length > 0) ? lines[0] : "download_quality=4";
            result[1] = (lines.Length > 1) ? lines[1] : "DLhistory=no";
            result[2] = (lines.Length > 2) ? lines[2] : "parallelDL=1";
            result[3] = (lines.Length > 3) ? lines[3] : "Platform=other";
            result[4] = (lines.Length > 4) ? lines[4] : "path";
            result[5] = (lines.Length > 5) ? lines[5] : "SaveDir";
            result[6] = (lines.Length > 6) ? lines[6] : "Thumbnail=no";
            result[7] = (lines.Length > 7) ? lines[7] : "DRMprotected=no";
            result[8] = (lines.Length > 8) ? lines[8] : "BATrun=no";
            return result;
        }

        private void Referencetextbox_TextChanged(object sender, EventArgs e)
        {
            if (checkedListBoxPlatform.CheckedIndices.Count > 0 && checkedListBoxPlatform.CheckedIndices[0] == 0)
                return;
            string path = referencetextbox.Text;
            string[] lines = File.Exists(settingsPath) ? File.ReadAllLines(settingsPath) : new string[5];
            lines = SettingsLines(lines);
            lines[4] = path;
            File.WriteAllLines(settingsPath, lines);
        }

        private void FileRefeBtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "保存先フォルダを選択してください";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;

                    string[] lines = File.Exists(settingsPath) ? File.ReadAllLines(settingsPath) : new string[6];
                    lines = SettingsLines(lines);
                    lines[5] = selectedPath;
                    File.WriteAllLines(settingsPath, lines);

                    FileRefeTextbox.Text = selectedPath; // テキストボックスにも反映
                }
            }

        }

        private void FileRefeTextbox_TextChanged(object sender, EventArgs e)
        {
            string path = FileRefeTextbox.Text;
            string[] lines = File.Exists(settingsPath) ? File.ReadAllLines(settingsPath) : new string[6];
            lines = SettingsLines(lines);
            lines[5] = path;
            File.WriteAllLines(settingsPath, lines);
        }

        private void RunYtDlpFromSettings()
        {
            if (!File.Exists(settingsPath) || !File.Exists(urlListPath))
            {
                MessageBox.Show("必要な設定ファイルまたはURLファイルが見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string[] settings = File.ReadAllLines(settingsPath);
            string[] urls = File.ReadAllLines(urlListPath);

            if (urls.Length == 0)
            {
                MessageBox.Show("URL_list.txt にURLが存在しません。", "URLなし", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. 画質 → -f
            string[] formats =
            {
            "bv[height=4320][ext=mp4]+ba[ext=m4a]/bv[height=2160][ext=mp4]+ba[ext=m4a]/bv[height=1440][ext=mp4]+ba[ext=m4a]/bv[height=1080][ext=mp4]+ba[ext=m4a]/bv[height=720][ext=mp4]+ba[ext=m4a]/bv[height=480][ext=mp4]+ba[ext=m4a]/bv[height=360][ext=mp4]+ba[ext=m4a]/bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]",
            "bv[height=2160][ext=mp4]+ba[ext=m4a]/bv[height=1440][ext=mp4]+ba[ext=m4a]/bv[height=1080][ext=mp4]+ba[ext=m4a]/bv[height=720][ext=mp4]+ba[ext=m4a]/bv[height=480][ext=mp4]+ba[ext=m4a]/bv[height=360][ext=mp4]+ba[ext=m4a]/bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]",
            "bv[height=1440][ext=mp4]+ba[ext=m4a]/bv[height=1080][ext=mp4]+ba[ext=m4a]/bv[height=720][ext=mp4]+ba[ext=m4a]/bv[height=480][ext=mp4]+ba[ext=m4a]/bv[height=360][ext=mp4]+ba[ext=m4a]/bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]",
            "bv[height=1080][ext=mp4]+ba[ext=m4a]/bv[height=720][ext=mp4]+ba[ext=m4a]/bv[height=480][ext=mp4]+ba[ext=m4a]/bv[height=360][ext=mp4]+ba[ext=m4a]/bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]",
            "bv[height=720][ext=mp4]+ba[ext=m4a]/bv[height=480][ext=mp4]+ba[ext=m4a]/bv[height=360][ext=mp4]+ba[ext=m4a]/bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]",
            "bv[height=480][ext=mp4]+ba[ext=m4a]/bv[height=360][ext=mp4]+ba[ext=m4a]/bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]",
            "bv[height=360][ext=mp4]+ba[ext=m4a]/bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]",
            "bv[height=240][ext=mp4]+ba[ext=m4a]/bv[height=144][ext=mp4]+ba[ext=m4a]",
            "bv[height=144][ext=mp4]+ba[ext=m4a]"
            };

            string format = formats[3]; // default
            if (settings.Length >= 1 && settings[0].StartsWith("download_quality="))
            {
                if (int.TryParse(settings[0].Split('=')[1], out int q) && q >= 1 && q <= formats.Length)
                    format = formats[q - 1];
            }

            // 2. DLhistory
            bool useHistory = settings.Length >= 2 && settings[1].Trim().ToLower().Contains("on");
            string archiveOption = useHistory ? "--download-archive downloaded.txt" : "";

            // 3. 並列数
            string parallel = "8";
            if (settings.Length >= 3 && settings[2].StartsWith("parallelDL="))
                parallel = settings[2].Split('=')[1];

            // 4. cookie
            string cookiePath = "cookies.txt";
            if (settings.Length >= 4)
            {
                string platform = settings[3].Split('=')[1].Trim().ToLower();
                if (platform == "other" && settings.Length >= 5)
                {
                    cookiePath = settings[4].Trim();
                }
            }

            // 5. 保存先
            string saveDir = Path.Combine(Application.StartupPath);
            if (settings.Length >= 6 && Directory.Exists(settings[5]))
            {
                saveDir = Path.Combine(settings[5]);
                if (!Directory.Exists(saveDir)) Directory.CreateDirectory(saveDir);
            }

            // 5.1 サムネイル
            bool useThumbnail = settings.Length >= 7 && settings[6].Trim().ToLower().Contains("on");
            string ThumbnailOption = useThumbnail ? "--write-thumbnail" : "";

            // 5.2 DRMprotected
            bool isDRMprotected = settings.Length >= 8 && settings[7].Trim().ToLower().Contains("on");
            string DRMOption = isDRMprotected ? "--extractor-args \"youtube:player-client=default,-tv,web_safari,web_embedded\"" : "";



            // 6. URL ループ
            foreach (string rawUrl in urls)
            {
                string url = rawUrl.Trim();
                if (string.IsNullOrEmpty(url)) continue;

                // ラベルとプログレスバーを含むパネルを作成
                Panel downloadPanel = new Panel
                {
                    Width = flowLayoutPanel1.Width - 25,
                    Height = 28,
                    Margin = new Padding(2)
                };

                // 横並びにするための TableLayoutPanel を使用（推奨）
                TableLayoutPanel layout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 1,
                    Width = downloadPanel.Width,
                    Height = downloadPanel.Height
                };
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // ラベル: 50%
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // プログレスバー: 50%

                Label label = new Label
                {
                    Text = url.Length > 60 ? url.Substring(0, 60) + "..." : url,
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(2)
                };

                ProgressBar progressBar = new ProgressBar
                {
                    Minimum = 0,
                    Maximum = 100,
                    Value = 0,
                    Height = 20,
                    Dock = DockStyle.Fill,
                };

                layout.Controls.Add(label, 0, 0);
                layout.Controls.Add(progressBar, 1, 0);

                downloadPanel.Controls.Add(layout);
                flowLayoutPanel1.Controls.Add(downloadPanel);

                bool isAbemaVideo = url.Contains("abema.tv/video/episode") || url.Contains("abema.tv/video/title");
                string FormatOption = isAbemaVideo ? "" : $"-f \"{format}\"";
                string outputPattern = $"{saveDir}\\%(upload_date)s-%(title)s.%(ext)s";
                string args;
                args = $"\"{url}\" --cookies \"{cookiePath}\" {ThumbnailOption} {DRMOption} --embed-thumbnail --add-metadata {archiveOption} --ignore-errors {FormatOption} --output \"{outputPattern}\" -N {parallel} --fragment-retries 5 --retries infinite";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "yt-dlp",
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                Process proc = new Process
                {
                    StartInfo = psi,
                    EnableRaisingEvents = true
                };

                proc.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        this.Invoke(() =>
                        {
                            richTextBoxLog.AppendText(e.Data + Environment.NewLine);
                            richTextBoxLog.SelectionStart = richTextBoxLog.Text.Length;
                            richTextBoxLog.ScrollToCaret();

                            // 進捗検出
                            if (e.Data.Contains("[download]"))
                            {
                                Match match = Regex.Match(e.Data, @"\[download\]\s+([\d.]+)%");
                                if (match.Success && double.TryParse(match.Groups[1].Value, out double percent))
                                {
                                    progressBar.Value = Math.Min((int)percent, 100);
                                }
                            }

                            // メンバー限定検出
                            if (e.Data.Contains("Join this channel to get access to members-only"))
                            {
                                Label errorLabel = new Label
                                {
                                    Text = "以下の内容をお試しください:\n・cookieファイルのパスは正しいですか？\n・cookieファイルを更新してください。",
                                    ForeColor = Color.Red,
                                    AutoSize = true,
                                    MaximumSize = new Size(flowLayoutPanel1.Width - 20, 0),
                                    Margin = new Padding(5)
                                };
                                flowLayoutPanel1.Controls.Add(errorLabel);
                            }
                        });
                    }
                };

                proc.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        this.Invoke(() =>
                        {
                            richTextBoxLog.AppendText("[ERROR] " + e.Data + Environment.NewLine);
                            richTextBoxLog.SelectionStart = richTextBoxLog.Text.Length;
                            richTextBoxLog.ScrollToCaret();
                        });
                    }

                    if (!string.IsNullOrEmpty(e.Data) && e.Data.Contains("Join this channel to get access to members-only"))
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            Label adviceLabel = new Label
                            {
                                Text = "cookieファイルが取得できませんでした。以下をお試しください。\n・cookieファイルのパスは正しいですか？\n・cookieファイルを更新してください。\nこれらを試しても解決しない場合は、discordサーバーにて質問してください。",
                                AutoSize = true,
                                ForeColor = Color.Red
                            };
                            flowLayoutPanel1.Controls.Add(adviceLabel);
                        }));
                    }

                    if (!string.IsNullOrEmpty(e.Data) && e.Data.Contains("has already been recorded in the archive"))
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            Label adviceLabel = new Label
                            {
                                Text = "入力されたはURLはすでにダウンロードされております。以下をお試しください。\n・入力されたURLはすでにdownloaded.txtに記載されております。\n 該当の箇所を削除するか、downloaded.txtを丸ごと削除してください。\nこれらを試しても解決しない場合は、discordサーバーにて質問してください。",
                                AutoSize = true,
                                ForeColor = Color.Red
                            };
                            flowLayoutPanel1.Controls.Add(adviceLabel);
                        }));
                    }

                    if (!string.IsNullOrEmpty(e.Data) && e.Data.Contains("Requested format is not available."))
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            Label adviceLabel = new Label
                            {
                                Text = "入力されたURLの形式は対応しておりません。\n・short動画は高確率でダウンロードできません。\n short動画以外でダウンロードすることができない場合はdiscordサーバーにてご連絡ください。",
                                AutoSize = true,
                                ForeColor = Color.Red
                            };
                            flowLayoutPanel1.Controls.Add(adviceLabel);
                        }));
                    }

                    if (!string.IsNullOrEmpty(e.Data) && e.Data.Contains("they are DRM protected."))
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            Label adviceLabel = new Label
                            {
                                Text = "DRM保護によりダウンロードがブロックされました。「DRMの保護を回避」をONにし、再度ダウンロードをお試しください。",
                                AutoSize = true,
                                ForeColor = Color.Red
                            };
                            flowLayoutPanel1.Controls.Add(adviceLabel);
                        }));
                    }

                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            richTextBoxLog.AppendText("[ERROR] " + e.Data + Environment.NewLine);
                        }));
                    }
                };

                proc.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data) && e.Data.Contains("Join this channel to get access to members-only"))
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            Label adviceLabel = new Label
                            {
                                Text = "cookieファイルが取得できませんでした。以下をお試しください。\n・cookieファイルのパスは正しいですか？\n・cookieファイルを更新してください。\nこれらを試しても解決しない場合は、discordサーバーにて質問してください。",
                                AutoSize = true,
                                ForeColor = Color.Red
                            };
                            flowLayoutPanel1.Controls.Add(adviceLabel);
                        }));
                    }

                    if (!string.IsNullOrEmpty(e.Data) && e.Data.Contains("has already been recorded in the archive"))
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            Label adviceLabel = new Label
                            {
                                Text = "入力されたはURLはすでにダウンロードされております。以下をお試しください。\n・入力されたURLはすでにdownloaded.txtに記載されております。\n 該当の箇所を削除するか、downloaded.txtを丸ごと削除してください。\nこれらを試しても解決しない場合は、discordサーバーにて質問してください。",
                                AutoSize = true,
                                ForeColor = Color.Red
                            };
                            flowLayoutPanel1.Controls.Add(adviceLabel);
                        }));
                    }

                    if (!string.IsNullOrEmpty(e.Data) && e.Data.Contains("Requested format is not available."))
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            Label adviceLabel = new Label
                            {
                                Text = "入力されたURLの形式は対応しておりません。\n・short動画は高確率でダウンロードできません。\n short動画以外でダウンロードすることができない場合はdiscordサーバーにてご連絡ください。",
                                AutoSize = true,
                                ForeColor = Color.Red
                            };
                            flowLayoutPanel1.Controls.Add(adviceLabel);
                        }));
                    }

                    if (!string.IsNullOrEmpty(e.Data) && e.Data.Contains("they are DRM protected."))
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            Label adviceLabel = new Label
                            {
                                Text = "DRM保護によりダウンロードがブロックされました。「DRMの保護を回避」をONにし、再度ダウンロードをお試しください。",
                                AutoSize = true,
                                ForeColor = Color.Red
                            };
                            flowLayoutPanel1.Controls.Add(adviceLabel);
                        }));
                    }

                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            richTextBoxLog.AppendText(e.Data + Environment.NewLine);
                            richTextBoxLog.SelectionStart = richTextBoxLog.Text.Length;
                            richTextBoxLog.ScrollToCaret();

                            // 進捗の抽出と更新
                            Match match = Regex.Match(e.Data, @"\[download\]\s+([\d.]+)%");
                            if (match.Success && double.TryParse(match.Groups[1].Value, out double percent))
                            {
                                progressBar.Value = Math.Min((int)percent, 100);
                            }
                        }));
                    }
                };


                proc.Start();
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();

                proc.Exited += (s, e) =>
                {
                    // UI スレッドでの処理
                    this.Invoke((MethodInvoker)(() =>
                    {
                        // プログレスバーとラベルを削除
                        if (progressBar.Parent != null)
                        {
                            flowLayoutPanel1.Controls.Remove(progressBar);
                        }
                        if (label.Parent != null)
                        {
                            flowLayoutPanel1.Controls.Remove(label);
                        }
                        // ログに完了を表示
                        richTextBoxLog.AppendText($"完了: {url}{Environment.NewLine}");
                    }));

                    // バッチファイルでサムネイルを移動
                    try
                    {
                        string moveScriptPath = Path.Combine(Path.GetTempPath(), $"move_{Guid.NewGuid()}.bat");
                        string moveTarget = Path.Combine(saveDir, "サムネイル");

                        if (!Directory.Exists(moveTarget))
                        {
                            Directory.CreateDirectory(moveTarget);
                        }

                        string batContent = $@"

                        @echo off
                        move /Y ""{saveDir}\.webp"" ""{moveTarget}""
                        move /Y ""{saveDir}\.jpg"" ""{moveTarget}""
                        timeout /t 1 > nul
                        del ""{moveScriptPath}""
                        ";

                        File.WriteAllText(moveScriptPath, batContent, Encoding.Default);

                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = $"/C \"{moveScriptPath}\"",
                            UseShellExecute = false,
                            CreateNoWindow = true
                        });
                    }
                    catch (Exception ex)
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            richTextBoxLog.AppendText($"[エラー] サムネイル移動中に例外が発生しました: {ex.Message}{Environment.NewLine}");
                        }));
                    }
                };



            }
        }

        private void setup_update_Click(object sender, EventArgs e)
        {
            string saveDir = "";
            try
            {
                string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                saveDir = Path.GetDirectoryName(exePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存先ディレクトリの取得に失敗しました: " + ex.Message);
                this.Activate();
            }

            // ffmpeg チェック & ダウンロード + 展開
            string ffmpegExePath = Path.Combine(saveDir, "ffmpeg.exe");
            if (!File.Exists(ffmpegExePath))
            {
                try
                {
                    string zipUrl = "https://github.com/GyanD/codexffmpeg/releases/download/7.1.1/ffmpeg-7.1.1-full_build.zip";
                    string zipPath = Path.Combine(saveDir, "ffmpeg.zip");

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "curl",
                        Arguments = $"-L -o \"{zipPath}\" \"{zipUrl}\"",

                        UseShellExecute = false,
                        CreateNoWindow = false
                    })?.WaitForExit();

                    if (File.Exists(zipPath))
                    {
                        string extractDir = Path.Combine(saveDir, "ffmpeg_temp");
                        System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractDir);
                        File.Delete(zipPath);

                        // ffmpeg.exe を bin フォルダから移動
                        string sourceFfmpeg = Directory.GetFiles(Path.Combine(extractDir, "ffmpeg-7.1.1-full_build", "bin"), "ffmpeg.exe").FirstOrDefault();
                        if (!string.IsNullOrEmpty(sourceFfmpeg))
                        {
                            File.Copy(sourceFfmpeg, ffmpegExePath, true);
                        }

                        Directory.Delete(extractDir, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ffmpeg のインストールに失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Activate();
                }
            }

            // yt-dlp チェック & ダウンロード
            string ytDlpPath = Path.Combine(saveDir, "yt-dlp.exe");
            if (!File.Exists(ytDlpPath))
            {
                try
                {
                    string ytDlpUrl = "https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp.exe";
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "curl",
                        Arguments = $"-L -o \"{ytDlpPath}\" \"{ytDlpUrl}\"",
                        UseShellExecute = false,
                        CreateNoWindow = false
                    })?.WaitForExit();

                    // ダウンロード直後にアップデートを実行
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = ytDlpPath,
                        Arguments = "--update-to nightly",
                        UseShellExecute = false,
                        CreateNoWindow = false
                    })?.WaitForExit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"yt-dlp のダウンロードに失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Activate();
                }
            }
            else
            {
                // 既に存在する場合はアップデート
                Process.Start(new ProcessStartInfo
                {
                    FileName = ytDlpPath,
                    Arguments = "--update-to nightly",
                    UseShellExecute = false,
                    CreateNoWindow = false
                })?.WaitForExit();
            }

            // AtomicParsley チェック & インストール
            string atomicParsleyExe = Path.Combine(saveDir, "AtomicParsley.exe");
            if (!File.Exists(atomicParsleyExe))
            {
                string zipPath = Path.Combine(saveDir, "AtomicParsley.zip");
                string downloadUrl = "https://github.com/wez/atomicparsley/releases/download/20240608.083822.1ed9031/AtomicParsleyWindows.zip";

                Process.Start(new ProcessStartInfo
                {
                    FileName = "curl",
                    Arguments = $"-L -o \"{zipPath}\" \"{downloadUrl}\"",
                    UseShellExecute = false,
                    CreateNoWindow = false
                })?.WaitForExit();

                if (File.Exists(zipPath))
                {
                    try
                    {
                        System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, saveDir);
                        File.Delete(zipPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"AtomicParsley の展開に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Activate();
                    }
                }
            }

            //downloader自体のアップデート
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string versionUrl = "https://raw.githubusercontent.com/s3114/yt-dlp/refs/heads/master/Version.txt";
                    string latestVersion = client.GetStringAsync(versionUrl).Result.Trim();
                    string currentVersion = File.ReadAllText(versionPath).Trim();

                    if (currentVersion != latestVersion)
                    {
                        DialogResult result = MessageBox.Show(
                            $"新しいバージョン {latestVersion} が見つかりました。\nアップデートしますか？",
                            "アップデート確認",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        );

                        if (result == DialogResult.Yes)
                        {
                            string exeUrl = "https://github.com/s3114/yt-dlp/releases/latest/download/Youtube-downloader.exe";
                            string tempPath = Path.Combine(Path.GetTempPath(), "Youtube-downloader_new.exe");

                            using (HttpResponseMessage response = client.GetAsync(exeUrl).Result)
                            using (FileStream fs = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                response.Content.CopyToAsync(fs).Wait();
                            }

                            string updaterScript = Path.Combine(Path.GetTempPath(), "update.bat");
                            string exePath = Application.ExecutablePath;

                            File.WriteAllText(updaterScript, $@"
                                @echo off
                                timeout /t 1 /nobreak > nul
                                copy /y ""{tempPath}"" ""{exePath}""
                                start """" ""{exePath}""
                                del ""{tempPath}""
                                del ""%~f0""
                                ");
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = updaterScript,
                                UseShellExecute = true,
                                CreateNoWindow = true
                            });

                            Application.Exit(); // 自アプリ終了
                        }
                    }
                    else
                    {
                        MessageBox.Show("最新版を使用しています。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Activate();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"バージョン確認またはアップデートに失敗しました:\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Activate();
            }


            MessageBox.Show("セットアップ又はアップデートが完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Activate();
        }

    }
}



//上げる前に必ず確認すること
//バージョンの更新！！！！！！！

//--extractor-args "youtube:player-client=default,-tv,web_safari,web_embedded" 