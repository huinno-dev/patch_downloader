﻿namespace Huinno_Downloader
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
            this.BT_ConvEcgTest = new System.Windows.Forms.Button();
            this.BT_ExtractUmTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BT_makeHeaderTest = new System.Windows.Forms.Button();
            this.BT_SelSaveDir = new System.Windows.Forms.Button();
            this.BT_ClearLog = new System.Windows.Forms.Button();
            this.BT_LogOut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BT_StartDown
            // 

            this.BT_StartDown.Location = new System.Drawing.Point(492, 137);

            this.BT_StartDown.Name = "BT_StartDown";
            this.BT_StartDown.Size = new System.Drawing.Size(188, 90);
            this.BT_StartDown.TabIndex = 3;
            this.BT_StartDown.Text = "Download";
            this.BT_StartDown.UseVisualStyleBackColor = true;
            this.BT_StartDown.Click += new System.EventHandler(this.BT_StartDown_Click);
            // 
            // progressBar1
            // 

            this.progressBar1.Location = new System.Drawing.Point(32, 257);

            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(606, 38);
            this.progressBar1.TabIndex = 1;
            // 
            // TB_Serial1
            // 
            this.TB_Serial1.Enabled = false;

            this.TB_Serial1.Location = new System.Drawing.Point(34, 206);

            this.TB_Serial1.Name = "TB_Serial1";
            this.TB_Serial1.ReadOnly = true;
            this.TB_Serial1.Size = new System.Drawing.Size(36, 28);
            this.TB_Serial1.TabIndex = 2;
            // 
            // TB_Serial2
            // 
            this.TB_Serial2.Enabled = false;

            this.TB_Serial2.Location = new System.Drawing.Point(78, 206);

            this.TB_Serial2.Name = "TB_Serial2";
            this.TB_Serial2.ReadOnly = true;
            this.TB_Serial2.Size = new System.Drawing.Size(59, 28);
            this.TB_Serial2.TabIndex = 2;
            // 
            // TB_Serial3
            // 
            this.TB_Serial3.Enabled = false;

            this.TB_Serial3.Location = new System.Drawing.Point(160, 206);

            this.TB_Serial3.Name = "TB_Serial3";
            this.TB_Serial3.ReadOnly = true;
            this.TB_Serial3.Size = new System.Drawing.Size(36, 28);
            this.TB_Serial3.TabIndex = 3;
            // 
            // TB_Serial4
            // 
            this.TB_Serial4.Enabled = false;

            this.TB_Serial4.Location = new System.Drawing.Point(204, 206);

            this.TB_Serial4.Name = "TB_Serial4";
            this.TB_Serial4.ReadOnly = true;
            this.TB_Serial4.Size = new System.Drawing.Size(36, 28);
            this.TB_Serial4.TabIndex = 4;
            // 
            // TB_Serial5
            // 
            this.TB_Serial5.Enabled = false;

            this.TB_Serial5.Location = new System.Drawing.Point(248, 206);

            this.TB_Serial5.Name = "TB_Serial5";
            this.TB_Serial5.ReadOnly = true;
            this.TB_Serial5.Size = new System.Drawing.Size(36, 28);
            this.TB_Serial5.TabIndex = 5;
            // 
            // TB_Serial6
            // 
            this.TB_Serial6.Enabled = false;

            this.TB_Serial6.Location = new System.Drawing.Point(292, 206);

            this.TB_Serial6.Name = "TB_Serial6";
            this.TB_Serial6.ReadOnly = true;
            this.TB_Serial6.Size = new System.Drawing.Size(59, 28);
            this.TB_Serial6.TabIndex = 6;
            // 
            // TB_Serial7
            // 
            this.TB_Serial7.Enabled = false;

            this.TB_Serial7.Location = new System.Drawing.Point(362, 206);

            this.TB_Serial7.Name = "TB_Serial7";
            this.TB_Serial7.ReadOnly = true;
            this.TB_Serial7.Size = new System.Drawing.Size(111, 28);
            this.TB_Serial7.TabIndex = 7;
            // 
            // CB_ComPortNameList
            // 
            this.CB_ComPortNameList.FormattingEnabled = true;

            this.CB_ComPortNameList.Location = new System.Drawing.Point(32, 137);

            this.CB_ComPortNameList.Name = "CB_ComPortNameList";
            this.CB_ComPortNameList.Size = new System.Drawing.Size(133, 26);
            this.CB_ComPortNameList.TabIndex = 1;
            this.CB_ComPortNameList.Click += new System.EventHandler(this.CB_ComPortNameList_Click);
            // 
            // BT_ConnPort
            // 

            this.BT_ConnPort.Location = new System.Drawing.Point(184, 137);

            this.BT_ConnPort.Name = "BT_ConnPort";
            this.BT_ConnPort.Size = new System.Drawing.Size(138, 32);
            this.BT_ConnPort.TabIndex = 2;
            this.BT_ConnPort.Text = "Connect";
            this.BT_ConnPort.UseVisualStyleBackColor = true;
            this.BT_ConnPort.Click += new System.EventHandler(this.BT_ConnPort_Click);
            // 
            // BT_OpenSavePath
            // 

            this.BT_OpenSavePath.Location = new System.Drawing.Point(601, 53);

            this.BT_OpenSavePath.Name = "BT_OpenSavePath";
            this.BT_OpenSavePath.Size = new System.Drawing.Size(89, 50);
            this.BT_OpenSavePath.TabIndex = 11;
            this.BT_OpenSavePath.Text = "Open Folder";
            this.BT_OpenSavePath.UseVisualStyleBackColor = true;
            this.BT_OpenSavePath.Click += new System.EventHandler(this.BT_OpenSavePath_Click);
            // 
            // TB_SavePath
            // 

            this.TB_SavePath.Location = new System.Drawing.Point(32, 59);

            this.TB_SavePath.Name = "TB_SavePath";
            this.TB_SavePath.Size = new System.Drawing.Size(411, 28);
            this.TB_SavePath.TabIndex = 0;
            // 
            // TB_LogMsg
            // 

            this.TB_LogMsg.Location = new System.Drawing.Point(32, 321);

            this.TB_LogMsg.Multiline = true;
            this.TB_LogMsg.Name = "TB_LogMsg";
            this.TB_LogMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TB_LogMsg.Size = new System.Drawing.Size(607, 214);
            this.TB_LogMsg.TabIndex = 13;
            // 
            // CB_ComPortBaudList
            // 
            this.CB_ComPortBaudList.FormattingEnabled = true;
            this.CB_ComPortBaudList.Items.AddRange(new object[] {
            "3000000"});
            this.CB_ComPortBaudList.Location = new System.Drawing.Point(719, 56);
            this.CB_ComPortBaudList.Name = "CB_ComPortBaudList";
            this.CB_ComPortBaudList.Size = new System.Drawing.Size(124, 26);
            this.CB_ComPortBaudList.TabIndex = 15;
            // 
            // LB_ProgVal
            // 
            this.LB_ProgVal.AutoSize = true;

            this.LB_ProgVal.Location = new System.Drawing.Point(653, 279);

            this.LB_ProgVal.Name = "LB_ProgVal";
            this.LB_ProgVal.Size = new System.Drawing.Size(0, 18);
            this.LB_ProgVal.TabIndex = 16;
            // 
            // BT_ConvEcgTest
            // 
            this.BT_ConvEcgTest.Location = new System.Drawing.Point(719, 104);
            this.BT_ConvEcgTest.Name = "BT_ConvEcgTest";
            this.BT_ConvEcgTest.Size = new System.Drawing.Size(114, 23);
            this.BT_ConvEcgTest.TabIndex = 17;
            this.BT_ConvEcgTest.Text = "conv ecg";
            this.BT_ConvEcgTest.UseVisualStyleBackColor = true;
            this.BT_ConvEcgTest.Click += new System.EventHandler(this.BT_ConvEcgTest_Click);
            // 
            // BT_ExtractUmTest
            // 
            this.BT_ExtractUmTest.Location = new System.Drawing.Point(719, 153);
            this.BT_ExtractUmTest.Name = "BT_ExtractUmTest";
            this.BT_ExtractUmTest.Size = new System.Drawing.Size(195, 23);
            this.BT_ExtractUmTest.TabIndex = 18;
            this.BT_ExtractUmTest.Text = "Extract user mark test";
            this.BT_ExtractUmTest.UseVisualStyleBackColor = true;
            this.BT_ExtractUmTest.Click += new System.EventHandler(this.BT_ExtractUmTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;

            this.label1.Location = new System.Drawing.Point(30, 112);

            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "COM Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;

            this.label2.Location = new System.Drawing.Point(30, 182);

            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 18);
            this.label2.TabIndex = 20;
            this.label2.Text = "Patch Info.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;

            this.label3.Location = new System.Drawing.Point(28, 34);

            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 18);
            this.label3.TabIndex = 21;
            this.label3.Text = "Path to save files";
            // 
            // BT_makeHeaderTest
            // 
            this.BT_makeHeaderTest.Location = new System.Drawing.Point(873, 104);
            this.BT_makeHeaderTest.Name = "BT_makeHeaderTest";
            this.BT_makeHeaderTest.Size = new System.Drawing.Size(246, 23);
            this.BT_makeHeaderTest.TabIndex = 23;
            this.BT_makeHeaderTest.Text = "usermark header test";
            this.BT_makeHeaderTest.UseVisualStyleBackColor = true;
            this.BT_makeHeaderTest.Click += new System.EventHandler(this.BT_makeHeaderTest_Click);
            // 
            // BT_SelSaveDir
            // 

            this.BT_SelSaveDir.Location = new System.Drawing.Point(492, 53);

            this.BT_SelSaveDir.Name = "BT_SelSaveDir";
            this.BT_SelSaveDir.Size = new System.Drawing.Size(89, 50);
            this.BT_SelSaveDir.TabIndex = 24;
            this.BT_SelSaveDir.Text = "Select Folder";
            this.BT_SelSaveDir.UseVisualStyleBackColor = true;
            this.BT_SelSaveDir.Click += new System.EventHandler(this.BT_SelSaveDir_Click);
            // 
            // BT_ClearLog
            // 

            this.BT_ClearLog.Location = new System.Drawing.Point(590, 535);

            this.BT_ClearLog.Name = "BT_ClearLog";
            this.BT_ClearLog.Size = new System.Drawing.Size(70, 30);
            this.BT_ClearLog.TabIndex = 25;
            this.BT_ClearLog.Text = "Clear";
            this.BT_ClearLog.UseVisualStyleBackColor = true;
            this.BT_ClearLog.Click += new System.EventHandler(this.BT_ClearLog_Click);
            // 
            // BT_LogOut
            // 

            this.BT_LogOut.Location = new System.Drawing.Point(601, 0);
            this.BT_LogOut.Name = "BT_LogOut";
            this.BT_LogOut.Size = new System.Drawing.Size(98, 47);

            this.BT_LogOut.TabIndex = 26;
            this.BT_LogOut.Text = "Log out";
            this.BT_LogOut.UseVisualStyleBackColor = true;
            this.BT_LogOut.Click += new System.EventHandler(this.BT_LogOut_Click);
            // 
            // main_window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize = new System.Drawing.Size(729, 599);

            this.Controls.Add(this.BT_LogOut);
            this.Controls.Add(this.BT_ClearLog);
            this.Controls.Add(this.BT_SelSaveDir);
            this.Controls.Add(this.TB_Serial7);
            this.Controls.Add(this.TB_Serial6);
            this.Controls.Add(this.TB_Serial5);
            this.Controls.Add(this.TB_Serial4);
            this.Controls.Add(this.TB_Serial3);
            this.Controls.Add(this.TB_Serial2);
            this.Controls.Add(this.TB_Serial1);
            this.Controls.Add(this.TB_SavePath);
            this.Controls.Add(this.BT_makeHeaderTest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BT_ExtractUmTest);
            this.Controls.Add(this.BT_ConvEcgTest);
            this.Controls.Add(this.LB_ProgVal);
            this.Controls.Add(this.CB_ComPortBaudList);
            this.Controls.Add(this.TB_LogMsg);
            this.Controls.Add(this.BT_OpenSavePath);
            this.Controls.Add(this.BT_ConnPort);
            this.Controls.Add(this.CB_ComPortNameList);
            this.Controls.Add(this.BT_StartDown);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MinimumSize = new System.Drawing.Size(753, 642);

            this.Name = "main_window";
            this.Text = "[Huinno] Patch Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_window_FormClosing);
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
        private System.Windows.Forms.Button BT_ConvEcgTest;
        private System.Windows.Forms.Button BT_ExtractUmTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BT_makeHeaderTest;
        private System.Windows.Forms.Button BT_SelSaveDir;
        private System.Windows.Forms.Button BT_ClearLog;
        private System.Windows.Forms.Button BT_LogOut;
    }
}

