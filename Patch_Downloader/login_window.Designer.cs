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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login_window));
            this.pb_logo = new System.Windows.Forms.PictureBox();
            this.tb_id = new System.Windows.Forms.TextBox();
            this.tb_pw = new System.Windows.Forms.TextBox();
            this.lb_pw = new RoundBorderLabel.RoundLabel();
            this.btn_login = new ePOSOne.btnProduct.Button_WOC();
            this.lb_id = new RoundBorderLabel.RoundLabel();
            this.btn_minimize = new CustomButton.Border.CustomButton();
            this.btn_exit = new CustomButton.Border.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.pb_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_logo
            // 
            this.pb_logo.Image = ((System.Drawing.Image)(resources.GetObject("pb_logo.Image")));
            this.pb_logo.Location = new System.Drawing.Point(225, 140);
            this.pb_logo.Name = "pb_logo";
            this.pb_logo.Size = new System.Drawing.Size(150, 67);
            this.pb_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_logo.TabIndex = 7;
            this.pb_logo.TabStop = false;
            // 
            // tb_id
            // 
            this.tb_id.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_id.ForeColor = System.Drawing.Color.DarkGray;
            this.tb_id.Location = new System.Drawing.Point(180, 254);
            this.tb_id.Name = "tb_id";
            this.tb_id.Size = new System.Drawing.Size(240, 18);
            this.tb_id.TabIndex = 10;
            this.tb_id.Text = "Email";
            this.tb_id.Enter += new System.EventHandler(this.tb_id_Enter);
            this.tb_id.Leave += new System.EventHandler(this.tb_id_Leave);
            // 
            // tb_pw
            // 
            this.tb_pw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_pw.ForeColor = System.Drawing.Color.DarkGray;
            this.tb_pw.Location = new System.Drawing.Point(180, 306);
            this.tb_pw.Name = "tb_pw";
            this.tb_pw.Size = new System.Drawing.Size(240, 18);
            this.tb_pw.TabIndex = 12;
            this.tb_pw.Text = "Password";
            this.tb_pw.Enter += new System.EventHandler(this.tb_pw_Enter);
            this.tb_pw.Leave += new System.EventHandler(this.tb_pw_Leave);
            // 
            // lb_pw
            // 
            this.lb_pw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.lb_pw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lb_pw.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.lb_pw.Location = new System.Drawing.Point(160, 296);
            this.lb_pw.Name = "lb_pw";
            this.lb_pw.Size = new System.Drawing.Size(280, 40);
            this.lb_pw.TabIndex = 11;
            // 
            // btn_login
            // 
            this.btn_login.BorderColor = System.Drawing.Color.LightGray;
            this.btn_login.ButtonColor = System.Drawing.Color.Blue;
            this.btn_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_login.Font = new System.Drawing.Font("휴먼둥근헤드라인", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.btn_login.Location = new System.Drawing.Point(225, 360);
            this.btn_login.Name = "btn_login";
            this.btn_login.OnHoverBorderColor = System.Drawing.Color.LightGray;
            this.btn_login.OnHoverButtonColor = System.Drawing.Color.Blue;
            this.btn_login.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_login.Size = new System.Drawing.Size(150, 40);
            this.btn_login.TabIndex = 9;
            this.btn_login.Text = "Log In";
            this.btn_login.TextColor = System.Drawing.Color.White;
            this.btn_login.UseVisualStyleBackColor = false;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // lb_id
            // 
            this.lb_id.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.lb_id.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lb_id.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.lb_id.Location = new System.Drawing.Point(160, 244);
            this.lb_id.Name = "lb_id";
            this.lb_id.Size = new System.Drawing.Size(280, 40);
            this.lb_id.TabIndex = 8;
            // 
            // btn_minimize
            // 
            this.btn_minimize.FlatAppearance.BorderSize = 0;
            this.btn_minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_minimize.Font = new System.Drawing.Font("굴림", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_minimize.Location = new System.Drawing.Point(520, 0);
            this.btn_minimize.Name = "btn_minimize";
            this.btn_minimize.Size = new System.Drawing.Size(40, 32);
            this.btn_minimize.TabIndex = 13;
            this.btn_minimize.Text = "-";
            this.btn_minimize.UseVisualStyleBackColor = true;
            this.btn_minimize.Click += new System.EventHandler(this.btn_minimize_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.FlatAppearance.BorderSize = 0;
            this.btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_exit.Font = new System.Drawing.Font("굴림", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_exit.Location = new System.Drawing.Point(560, 0);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(40, 32);
            this.btn_exit.TabIndex = 14;
            this.btn_exit.Text = "x";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // login_window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(600, 540);
            this.ControlBox = false;
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_minimize);
            this.Controls.Add(this.tb_pw);
            this.Controls.Add(this.tb_id);
            this.Controls.Add(this.lb_pw);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.lb_id);
            this.Controls.Add(this.pb_logo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "login_window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pb_logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pb_logo;
        private RoundBorderLabel.RoundLabel lb_id;
        private ePOSOne.btnProduct.Button_WOC btn_login;
        private System.Windows.Forms.TextBox tb_id;
        private RoundBorderLabel.RoundLabel lb_pw;
        private System.Windows.Forms.TextBox tb_pw;
        private CustomButton.Border.CustomButton btn_minimize;
        private CustomButton.Border.CustomButton btn_exit;
    }
}