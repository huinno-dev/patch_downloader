namespace Huinno_Downloader
{
    partial class main_window
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main_window));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_SavePath = new System.Windows.Forms.TextBox();
            this.TB_DeviceSerial = new System.Windows.Forms.TextBox();
            this.LB_Import = new System.Windows.Forms.Label();
            this.LB_Title = new System.Windows.Forms.Label();
            this.PB_Progress = new CustomProgressBar.RoundProgressBar();
            this.CB_ComPortNameList = new CustomComboBox.BorderCombobox();
            this.btn_exit = new CustomButton.BorderButton();
            this.btn_minimize = new CustomButton.BorderButton();
            this.BT_ConnPort = new CustomButton.RoundButton();
            this.BT_SelSaveDir = new CustomButton.RoundButton();
            this.BT_OpenSavePath = new CustomButton.RoundButton();
            this.BT_StartDown = new CustomButton.RoundButton();
            this.LB_SavePath = new CustomLabel.RoundLabel();
            this.LB_DeviceSerial = new CustomLabel.RoundLabel();
            this.LB_ComPortNameList = new CustomLabel.RoundLabel();
            this.TB_LogMsg = new CustomTextBox.RoundTextBox();
            this.LB_Progress = new CustomLabel.RoundLabel();
            this.LB_LogMsg = new CustomLabel.RoundLabel();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Progress)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 147);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "Serial Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(257, 147);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Device Serial No.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 20);
            this.label3.TabIndex = 21;
            this.label3.Text = "Save Files To";
            // 
            // TB_SavePath
            // 
            this.TB_SavePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_SavePath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_SavePath.ForeColor = System.Drawing.Color.Black;
            this.TB_SavePath.Location = new System.Drawing.Point(44, 104);
            this.TB_SavePath.Name = "TB_SavePath";
            this.TB_SavePath.Size = new System.Drawing.Size(383, 20);
            this.TB_SavePath.TabIndex = 26;
            // 
            // TB_DeviceSerial
            // 
            this.TB_DeviceSerial.BackColor = System.Drawing.Color.White;
            this.TB_DeviceSerial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_DeviceSerial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_DeviceSerial.Location = new System.Drawing.Point(273, 184);
            this.TB_DeviceSerial.Name = "TB_DeviceSerial";
            this.TB_DeviceSerial.ReadOnly = true;
            this.TB_DeviceSerial.Size = new System.Drawing.Size(349, 20);
            this.TB_DeviceSerial.TabIndex = 31;
            // 
            // LB_Import
            // 
            this.LB_Import.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_Import.Location = new System.Drawing.Point(25, 228);
            this.LB_Import.Name = "LB_Import";
            this.LB_Import.Size = new System.Drawing.Size(125, 19);
            this.LB_Import.TabIndex = 33;
            this.LB_Import.Text = "Import Data";
            // 
            // LB_Title
            // 
            this.LB_Title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LB_Title.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_Title.Location = new System.Drawing.Point(12, 12);
            this.LB_Title.Name = "LB_Title";
            this.LB_Title.Size = new System.Drawing.Size(537, 33);
            this.LB_Title.TabIndex = 34;
            // 
            // PB_Progress
            // 
            this.PB_Progress.BackColor = System.Drawing.Color.White;
            this.PB_Progress.Image = ((System.Drawing.Image)(resources.GetObject("PB_Progress.Image")));
            this.PB_Progress.Location = new System.Drawing.Point(29, 255);
            this.PB_Progress.Name = "PB_Progress";
            this.PB_Progress.ProgressBackColor = System.Drawing.Color.White;
            this.PB_Progress.ProgressBarColor = System.Drawing.Color.LimeGreen;
            this.PB_Progress.ProgressFont = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold);
            this.PB_Progress.ProgressFontColor = System.Drawing.Color.Black;
            this.PB_Progress.Size = new System.Drawing.Size(511, 36);
            this.PB_Progress.TabIndex = 39;
            this.PB_Progress.TabStop = false;
            this.PB_Progress.Value = 0;
            // 
            // CB_ComPortNameList
            // 
            this.CB_ComPortNameList.BackColor = System.Drawing.Color.White;
            this.CB_ComPortNameList.BorderColor = System.Drawing.Color.White;
            this.CB_ComPortNameList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB_ComPortNameList.ForeColor = System.Drawing.Color.Black;
            this.CB_ComPortNameList.FormattingEnabled = true;
            this.CB_ComPortNameList.Location = new System.Drawing.Point(40, 178);
            this.CB_ComPortNameList.Name = "CB_ComPortNameList";
            this.CB_ComPortNameList.Size = new System.Drawing.Size(86, 28);
            this.CB_ComPortNameList.TabIndex = 38;
            this.CB_ComPortNameList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CB_ComPortNameList_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.FlatAppearance.BorderSize = 0;
            this.btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_exit.Font = new System.Drawing.Font("굴림", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_exit.Location = new System.Drawing.Point(627, 3);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(40, 32);
            this.btn_exit.TabIndex = 36;
            this.btn_exit.Text = "x";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_minimize
            // 
            this.btn_minimize.FlatAppearance.BorderSize = 0;
            this.btn_minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_minimize.Font = new System.Drawing.Font("굴림", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_minimize.Location = new System.Drawing.Point(582, 3);
            this.btn_minimize.Name = "btn_minimize";
            this.btn_minimize.Size = new System.Drawing.Size(40, 32);
            this.btn_minimize.TabIndex = 35;
            this.btn_minimize.Text = "-";
            this.btn_minimize.UseVisualStyleBackColor = true;
            this.btn_minimize.Click += new System.EventHandler(this.btn_minimize_Click);
            // 
            // BT_ConnPort
            // 
            this.BT_ConnPort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.BT_ConnPort.ButtonColor = System.Drawing.Color.Blue;
            this.BT_ConnPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_ConnPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_ConnPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.BT_ConnPort.Location = new System.Drawing.Point(148, 174);
            this.BT_ConnPort.Name = "BT_ConnPort";
            this.BT_ConnPort.Size = new System.Drawing.Size(89, 36);
            this.BT_ConnPort.TabIndex = 30;
            this.BT_ConnPort.Text = "Connect";
            this.BT_ConnPort.TextColor = System.Drawing.Color.White;
            this.BT_ConnPort.UseVisualStyleBackColor = false;
            this.BT_ConnPort.Click += new System.EventHandler(this.BT_ConnPort_Click);
            // 
            // BT_SelSaveDir
            // 
            this.BT_SelSaveDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.BT_SelSaveDir.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.BT_SelSaveDir.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.BT_SelSaveDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_SelSaveDir.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_SelSaveDir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.BT_SelSaveDir.Location = new System.Drawing.Point(460, 96);
            this.BT_SelSaveDir.Name = "BT_SelSaveDir";
            this.BT_SelSaveDir.Size = new System.Drawing.Size(89, 34);
            this.BT_SelSaveDir.TabIndex = 27;
            this.BT_SelSaveDir.Text = "Browse";
            this.BT_SelSaveDir.TextColor = System.Drawing.Color.Black;
            this.BT_SelSaveDir.UseVisualStyleBackColor = false;
            this.BT_SelSaveDir.Click += new System.EventHandler(this.BT_SelSaveDir_Click);
            // 
            // BT_OpenSavePath
            // 
            this.BT_OpenSavePath.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.BT_OpenSavePath.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.BT_OpenSavePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_OpenSavePath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_OpenSavePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.BT_OpenSavePath.Location = new System.Drawing.Point(554, 96);
            this.BT_OpenSavePath.Margin = new System.Windows.Forms.Padding(2);
            this.BT_OpenSavePath.Name = "BT_OpenSavePath";
            this.BT_OpenSavePath.Size = new System.Drawing.Size(89, 34);
            this.BT_OpenSavePath.TabIndex = 29;
            this.BT_OpenSavePath.Text = "Open";
            this.BT_OpenSavePath.TextColor = System.Drawing.Color.Black;
            this.BT_OpenSavePath.UseVisualStyleBackColor = false;
            this.BT_OpenSavePath.Click += new System.EventHandler(this.BT_OpenSavePath_Click);
            // 
            // BT_StartDown
            // 
            this.BT_StartDown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.BT_StartDown.ButtonColor = System.Drawing.Color.Blue;
            this.BT_StartDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_StartDown.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_StartDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.BT_StartDown.Location = new System.Drawing.Point(554, 255);
            this.BT_StartDown.Name = "BT_StartDown";
            this.BT_StartDown.Size = new System.Drawing.Size(89, 36);
            this.BT_StartDown.TabIndex = 32;
            this.BT_StartDown.Text = "Import";
            this.BT_StartDown.TextColor = System.Drawing.Color.White;
            this.BT_StartDown.UseVisualStyleBackColor = false;
            this.BT_StartDown.Click += new System.EventHandler(this.BT_StartDown_Click);
            // 
            // LB_SavePath
            // 
            this.LB_SavePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.LB_SavePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LB_SavePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.LB_SavePath.Location = new System.Drawing.Point(24, 97);
            this.LB_SavePath.Name = "LB_SavePath";
            this.LB_SavePath.Size = new System.Drawing.Size(417, 33);
            this.LB_SavePath.TabIndex = 25;
            // 
            // LB_DeviceSerial
            // 
            this.LB_DeviceSerial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.LB_DeviceSerial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LB_DeviceSerial.ForeColor = System.Drawing.Color.Black;
            this.LB_DeviceSerial.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LB_DeviceSerial.Location = new System.Drawing.Point(257, 174);
            this.LB_DeviceSerial.Name = "LB_DeviceSerial";
            this.LB_DeviceSerial.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LB_DeviceSerial.Size = new System.Drawing.Size(386, 36);
            this.LB_DeviceSerial.TabIndex = 10;
            // 
            // LB_ComPortNameList
            // 
            this.LB_ComPortNameList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.LB_ComPortNameList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LB_ComPortNameList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.LB_ComPortNameList.Location = new System.Drawing.Point(25, 174);
            this.LB_ComPortNameList.Name = "LB_ComPortNameList";
            this.LB_ComPortNameList.Size = new System.Drawing.Size(118, 33);
            this.LB_ComPortNameList.TabIndex = 37;
            // 
            // TB_LogMsg
            // 
            this.TB_LogMsg.BackColor = System.Drawing.Color.White;
            this.TB_LogMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_LogMsg.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_LogMsg.Location = new System.Drawing.Point(27, 319);
            this.TB_LogMsg.Multiline = true;
            this.TB_LogMsg.Name = "TB_LogMsg";
            this.TB_LogMsg.ReadOnly = true;
            this.TB_LogMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TB_LogMsg.Size = new System.Drawing.Size(616, 273);
            this.TB_LogMsg.TabIndex = 41;
            // 
            // LB_Progress
            // 
            this.LB_Progress.Location = new System.Drawing.Point(29, 255);
            this.LB_Progress.Name = "LB_Progress";
            this.LB_Progress.Size = new System.Drawing.Size(511, 36);
            this.LB_Progress.TabIndex = 40;
            // 
            // LB_LogMsg
            // 
            this.LB_LogMsg.Location = new System.Drawing.Point(27, 319);
            this.LB_LogMsg.Name = "LB_LogMsg";
            this.LB_LogMsg.Size = new System.Drawing.Size(617, 274);
            this.LB_LogMsg.TabIndex = 42;
            // 
            // main_window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(671, 627);
            this.ControlBox = false;
            this.Controls.Add(this.PB_Progress);
            this.Controls.Add(this.CB_ComPortNameList);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_minimize);
            this.Controls.Add(this.LB_Title);
            this.Controls.Add(this.LB_Import);
            this.Controls.Add(this.TB_DeviceSerial);
            this.Controls.Add(this.BT_ConnPort);
            this.Controls.Add(this.BT_SelSaveDir);
            this.Controls.Add(this.BT_OpenSavePath);
            this.Controls.Add(this.TB_SavePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BT_StartDown);
            this.Controls.Add(this.LB_SavePath);
            this.Controls.Add(this.LB_DeviceSerial);
            this.Controls.Add(this.LB_ComPortNameList);
            this.Controls.Add(this.TB_LogMsg);
            this.Controls.Add(this.LB_Progress);
            this.Controls.Add(this.LB_LogMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(553, 471);
            this.Name = "main_window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_window_FormClosing);
            this.Load += new System.EventHandler(this.main_window_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PB_Progress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TB_SavePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private CustomLabel.RoundLabel LB_SavePath;
        private CustomButton.RoundButton BT_SelSaveDir;
        private CustomButton.RoundButton BT_OpenSavePath;
        private CustomButton.RoundButton BT_ConnPort;
        private CustomLabel.RoundLabel LB_DeviceSerial;
        private System.Windows.Forms.TextBox TB_DeviceSerial;
        private CustomButton.RoundButton BT_StartDown;
        private System.Windows.Forms.Label LB_Import;
        private System.Windows.Forms.Label LB_Title;
        private CustomButton.BorderButton btn_exit;
        private CustomButton.BorderButton btn_minimize;
        private CustomLabel.RoundLabel LB_ComPortNameList;
        private CustomComboBox.BorderCombobox CB_ComPortNameList;
        private CustomLabel.RoundLabel LB_Progress;
        private CustomTextBox.RoundTextBox TB_LogMsg;
        private CustomLabel.RoundLabel LB_LogMsg;
        private CustomProgressBar.RoundProgressBar PB_Progress;
    }
}

