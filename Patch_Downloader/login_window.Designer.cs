namespace Huinno_Downloader
{
    partial class login_window
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
            this.TB_ID = new System.Windows.Forms.TextBox();
            this.TB_PW = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BT_login = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CB_ShowPw = new System.Windows.Forms.CheckBox();
            this.BT_Exit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TB_ID
            // 
            this.TB_ID.Location = new System.Drawing.Point(166, 56);
            this.TB_ID.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TB_ID.Name = "TB_ID";
            this.TB_ID.Size = new System.Drawing.Size(219, 32);
            this.TB_ID.TabIndex = 0;
            // 
            // TB_PW
            // 
            this.TB_PW.Location = new System.Drawing.Point(166, 101);
            this.TB_PW.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TB_PW.Name = "TB_PW";
            this.TB_PW.Size = new System.Drawing.Size(219, 32);
            this.TB_PW.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "User name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 110);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // BT_login
            // 
            this.BT_login.Location = new System.Drawing.Point(402, 56);
            this.BT_login.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BT_login.Name = "BT_login";
            this.BT_login.Size = new System.Drawing.Size(95, 77);
            this.BT_login.TabIndex = 2;
            this.BT_login.Text = "Login";
            this.BT_login.UseVisualStyleBackColor = true;
            this.BT_login.Click += new System.EventHandler(this.BT_login_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CB_ShowPw);
            this.groupBox1.Controls.Add(this.TB_ID);
            this.groupBox1.Controls.Add(this.TB_PW);
            this.groupBox1.Controls.Add(this.BT_login);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(29, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Size = new System.Drawing.Size(556, 183);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log In";
            // 
            // CB_ShowPw
            // 
            this.CB_ShowPw.AutoSize = true;
            this.CB_ShowPw.Location = new System.Drawing.Point(148, 152);
            this.CB_ShowPw.Name = "CB_ShowPw";
            this.CB_ShowPw.Size = new System.Drawing.Size(172, 25);
            this.CB_ShowPw.TabIndex = 4;
            this.CB_ShowPw.Text = "Show password";
            this.CB_ShowPw.UseVisualStyleBackColor = true;
            this.CB_ShowPw.CheckedChanged += new System.EventHandler(this.CB_ShowPw_CheckedChanged);
            // 
            // BT_Exit
            // 
            this.BT_Exit.Location = new System.Drawing.Point(491, 223);
            this.BT_Exit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BT_Exit.Name = "BT_Exit";
            this.BT_Exit.Size = new System.Drawing.Size(95, 44);
            this.BT_Exit.TabIndex = 5;
            this.BT_Exit.Text = "Exit";
            this.BT_Exit.UseVisualStyleBackColor = true;
            this.BT_Exit.Click += new System.EventHandler(this.BT_Exit_Click);
            // 
            // login_window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 327);
            this.ControlBox = false;
            this.Controls.Add(this.BT_Exit);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "login_window";
            this.Text = "[Huinno] Patch Dataloader";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TB_ID;
        private System.Windows.Forms.TextBox TB_PW;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BT_login;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BT_Exit;
        private System.Windows.Forms.CheckBox CB_ShowPw;
    }
}