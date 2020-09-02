namespace Huinno_Downloader
{
    partial class win_password
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
            this.TB_DevPw = new System.Windows.Forms.TextBox();
            this.BT_EnterPw = new System.Windows.Forms.Button();
            this.TB_UploadUrl = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TB_DevPw
            // 
            this.TB_DevPw.Location = new System.Drawing.Point(44, 68);
            this.TB_DevPw.Name = "TB_DevPw";
            this.TB_DevPw.Size = new System.Drawing.Size(84, 32);
            this.TB_DevPw.TabIndex = 0;
            // 
            // BT_EnterPw
            // 
            this.BT_EnterPw.Location = new System.Drawing.Point(338, 68);
            this.BT_EnterPw.Name = "BT_EnterPw";
            this.BT_EnterPw.Size = new System.Drawing.Size(83, 32);
            this.BT_EnterPw.TabIndex = 1;
            this.BT_EnterPw.Text = "Enter";
            this.BT_EnterPw.UseVisualStyleBackColor = true;
            this.BT_EnterPw.Click += new System.EventHandler(this.BT_EnterPw_Click);
            // 
            // TB_UploadUrl
            // 
            this.TB_UploadUrl.Location = new System.Drawing.Point(44, 120);
            this.TB_UploadUrl.Name = "TB_UploadUrl";
            this.TB_UploadUrl.Size = new System.Drawing.Size(244, 32);
            this.TB_UploadUrl.TabIndex = 2;
            // 
            // win_password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 164);
            this.Controls.Add(this.TB_UploadUrl);
            this.Controls.Add(this.BT_EnterPw);
            this.Controls.Add(this.TB_DevPw);
            this.Name = "win_password";
            this.Text = "Dev mode";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_DevPw;
        private System.Windows.Forms.Button BT_EnterPw;
        private System.Windows.Forms.TextBox TB_UploadUrl;
    }
}