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
            System.Windows.Forms.Button BT_SelSavePath;
            this.BT_StartDown = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.TB_Serial1 = new System.Windows.Forms.TextBox();
            this.TB_Serial2 = new System.Windows.Forms.TextBox();
            this.TB_Serial3 = new System.Windows.Forms.TextBox();
            this.TB_Serial4 = new System.Windows.Forms.TextBox();
            this.TB_Serial5 = new System.Windows.Forms.TextBox();
            this.TB_Serial6 = new System.Windows.Forms.TextBox();
            this.TB_Serial7 = new System.Windows.Forms.TextBox();
            this.CB_ComPortNameList = new System.Windows.Forms.ComboBox();
            this.BT_ConnPort = new System.Windows.Forms.Button();
            this.BT_OpenSavePath = new System.Windows.Forms.Button();
            this.TB_SavePath = new System.Windows.Forms.TextBox();
            this.TB_LogMsg = new System.Windows.Forms.TextBox();
            this.CB_ComPortBaudList = new System.Windows.Forms.ComboBox();
            this.LB_ProgVal = new System.Windows.Forms.Label();
            this.BT_ConvUserMark = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            BT_SelSavePath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BT_SelSavePath
            // 
            BT_SelSavePath.Location = new System.Drawing.Point(492, 41);
            BT_SelSavePath.Name = "BT_SelSavePath";
            BT_SelSavePath.Size = new System.Drawing.Size(94, 40);
            BT_SelSavePath.TabIndex = 14;
            BT_SelSavePath.Text = "선택";
            BT_SelSavePath.UseVisualStyleBackColor = true;
            BT_SelSavePath.Click += new System.EventHandler(this.BT_SelSavePath_Click);
            // 
            // BT_StartDown
            // 
            this.BT_StartDown.Location = new System.Drawing.Point(492, 126);
            this.BT_StartDown.Name = "BT_StartDown";
            this.BT_StartDown.Size = new System.Drawing.Size(207, 113);
            this.BT_StartDown.TabIndex = 0;
            this.BT_StartDown.Text = "Download";
            this.BT_StartDown.UseVisualStyleBackColor = true;
            this.BT_StartDown.Click += new System.EventHandler(this.BT_StartDown_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(32, 260);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(667, 44);
            this.progressBar1.TabIndex = 1;
            // 
            // TB_Serial1
            // 
            this.TB_Serial1.Location = new System.Drawing.Point(32, 207);
            this.TB_Serial1.Name = "TB_Serial1";
            this.TB_Serial1.ReadOnly = true;
            this.TB_Serial1.Size = new System.Drawing.Size(39, 32);
            this.TB_Serial1.TabIndex = 2;
            // 
            // TB_Serial2
            // 
            this.TB_Serial2.Location = new System.Drawing.Point(76, 207);
            this.TB_Serial2.Name = "TB_Serial2";
            this.TB_Serial2.ReadOnly = true;
            this.TB_Serial2.Size = new System.Drawing.Size(65, 32);
            this.TB_Serial2.TabIndex = 2;
            // 
            // TB_Serial3
            // 
            this.TB_Serial3.Location = new System.Drawing.Point(158, 207);
            this.TB_Serial3.Name = "TB_Serial3";
            this.TB_Serial3.ReadOnly = true;
            this.TB_Serial3.Size = new System.Drawing.Size(39, 32);
            this.TB_Serial3.TabIndex = 3;
            // 
            // TB_Serial4
            // 
            this.TB_Serial4.Location = new System.Drawing.Point(202, 207);
            this.TB_Serial4.Name = "TB_Serial4";
            this.TB_Serial4.ReadOnly = true;
            this.TB_Serial4.Size = new System.Drawing.Size(39, 32);
            this.TB_Serial4.TabIndex = 4;
            // 
            // TB_Serial5
            // 
            this.TB_Serial5.Location = new System.Drawing.Point(246, 207);
            this.TB_Serial5.Name = "TB_Serial5";
            this.TB_Serial5.ReadOnly = true;
            this.TB_Serial5.Size = new System.Drawing.Size(39, 32);
            this.TB_Serial5.TabIndex = 5;
            // 
            // TB_Serial6
            // 
            this.TB_Serial6.Location = new System.Drawing.Point(290, 207);
            this.TB_Serial6.Name = "TB_Serial6";
            this.TB_Serial6.ReadOnly = true;
            this.TB_Serial6.Size = new System.Drawing.Size(65, 32);
            this.TB_Serial6.TabIndex = 6;
            // 
            // TB_Serial7
            // 
            this.TB_Serial7.Location = new System.Drawing.Point(360, 207);
            this.TB_Serial7.Name = "TB_Serial7";
            this.TB_Serial7.ReadOnly = true;
            this.TB_Serial7.Size = new System.Drawing.Size(122, 32);
            this.TB_Serial7.TabIndex = 7;
            // 
            // CB_ComPortNameList
            // 
            this.CB_ComPortNameList.FormattingEnabled = true;
            this.CB_ComPortNameList.Location = new System.Drawing.Point(32, 134);
            this.CB_ComPortNameList.Name = "CB_ComPortNameList";
            this.CB_ComPortNameList.Size = new System.Drawing.Size(146, 29);
            this.CB_ComPortNameList.TabIndex = 9;
            this.CB_ComPortNameList.Click += new System.EventHandler(this.CB_ComPortNameList_Click);
            // 
            // BT_ConnPort
            // 
            this.BT_ConnPort.Location = new System.Drawing.Point(184, 129);
            this.BT_ConnPort.Name = "BT_ConnPort";
            this.BT_ConnPort.Size = new System.Drawing.Size(152, 37);
            this.BT_ConnPort.TabIndex = 10;
            this.BT_ConnPort.Text = "Connect";
            this.BT_ConnPort.UseVisualStyleBackColor = true;
            this.BT_ConnPort.Click += new System.EventHandler(this.BT_ConnPort_Click);
            // 
            // BT_OpenSavePath
            // 
            this.BT_OpenSavePath.Location = new System.Drawing.Point(592, 42);
            this.BT_OpenSavePath.Name = "BT_OpenSavePath";
            this.BT_OpenSavePath.Size = new System.Drawing.Size(107, 39);
            this.BT_OpenSavePath.TabIndex = 11;
            this.BT_OpenSavePath.Text = "바로가기";
            this.BT_OpenSavePath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BT_OpenSavePath.UseVisualStyleBackColor = true;
            this.BT_OpenSavePath.Click += new System.EventHandler(this.BT_OpenSavePath_Click);
            // 
            // TB_SavePath
            // 
            this.TB_SavePath.Location = new System.Drawing.Point(32, 48);
            this.TB_SavePath.Name = "TB_SavePath";
            this.TB_SavePath.Size = new System.Drawing.Size(452, 32);
            this.TB_SavePath.TabIndex = 12;
            // 
            // TB_LogMsg
            // 
            this.TB_LogMsg.Location = new System.Drawing.Point(32, 320);
            this.TB_LogMsg.Multiline = true;
            this.TB_LogMsg.Name = "TB_LogMsg";
            this.TB_LogMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TB_LogMsg.Size = new System.Drawing.Size(667, 249);
            this.TB_LogMsg.TabIndex = 13;
            // 
            // CB_ComPortBaudList
            // 
            this.CB_ComPortBaudList.FormattingEnabled = true;
            this.CB_ComPortBaudList.Items.AddRange(new object[] {
            "3000000"});
            this.CB_ComPortBaudList.Location = new System.Drawing.Point(791, 65);
            this.CB_ComPortBaudList.Name = "CB_ComPortBaudList";
            this.CB_ComPortBaudList.Size = new System.Drawing.Size(136, 29);
            this.CB_ComPortBaudList.TabIndex = 15;
            // 
            // LB_ProgVal
            // 
            this.LB_ProgVal.AutoSize = true;
            this.LB_ProgVal.Location = new System.Drawing.Point(653, 281);
            this.LB_ProgVal.Name = "LB_ProgVal";
            this.LB_ProgVal.Size = new System.Drawing.Size(0, 21);
            this.LB_ProgVal.TabIndex = 16;
            // 
            // BT_ConvUserMark
            // 
            this.BT_ConvUserMark.Location = new System.Drawing.Point(791, 121);
            this.BT_ConvUserMark.Name = "BT_ConvUserMark";
            this.BT_ConvUserMark.Size = new System.Drawing.Size(83, 27);
            this.BT_ConvUserMark.TabIndex = 17;
            this.BT_ConvUserMark.Text = "button1";
            this.BT_ConvUserMark.UseVisualStyleBackColor = true;
            this.BT_ConvUserMark.Click += new System.EventHandler(this.BT_ConvUserMark_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(791, 178);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 27);
            this.button2.TabIndex = 18;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 21);
            this.label1.TabIndex = 19;
            this.label1.Text = "COM Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 21);
            this.label2.TabIndex = 20;
            this.label2.Text = "패치 정보";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 21);
            this.label3.TabIndex = 21;
            this.label3.Text = "저장 경로";
            // 
            // main_window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 599);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.BT_ConvUserMark);
            this.Controls.Add(this.LB_ProgVal);
            this.Controls.Add(this.CB_ComPortBaudList);
            this.Controls.Add(BT_SelSavePath);
            this.Controls.Add(this.TB_LogMsg);
            this.Controls.Add(this.TB_SavePath);
            this.Controls.Add(this.BT_OpenSavePath);
            this.Controls.Add(this.BT_ConnPort);
            this.Controls.Add(this.CB_ComPortNameList);
            this.Controls.Add(this.TB_Serial7);
            this.Controls.Add(this.TB_Serial6);
            this.Controls.Add(this.TB_Serial5);
            this.Controls.Add(this.TB_Serial4);
            this.Controls.Add(this.TB_Serial3);
            this.Controls.Add(this.TB_Serial2);
            this.Controls.Add(this.TB_Serial1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.BT_StartDown);
            this.MinimumSize = new System.Drawing.Size(753, 643);
            this.Name = "main_window";
            this.Text = "[Huinno] Patch Downloader";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.main_window_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BT_StartDown;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox TB_Serial1;
        private System.Windows.Forms.TextBox TB_Serial2;
        private System.Windows.Forms.TextBox TB_Serial3;
        private System.Windows.Forms.TextBox TB_Serial4;
        private System.Windows.Forms.TextBox TB_Serial5;
        private System.Windows.Forms.TextBox TB_Serial6;
        private System.Windows.Forms.TextBox TB_Serial7;
        private System.Windows.Forms.ComboBox CB_ComPortNameList;
        private System.Windows.Forms.Button BT_ConnPort;
        private System.Windows.Forms.Button BT_OpenSavePath;
        private System.Windows.Forms.TextBox TB_SavePath;
        private System.Windows.Forms.TextBox TB_LogMsg;
        private System.Windows.Forms.ComboBox CB_ComPortBaudList;
        private System.Windows.Forms.Label LB_ProgVal;
        private System.Windows.Forms.Button BT_ConvUserMark;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

