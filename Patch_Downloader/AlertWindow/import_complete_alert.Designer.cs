
namespace Huinno_Dataloader.AlertWindow
{
    partial class import_complete_alert
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_WOC2 = new CustomButton.RoundButton();
            this.button_WOC1 = new CustomButton.RoundButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Import complete";
            // 
            // label2
            // 
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(318, 56);
            this.label2.TabIndex = 1;
            this.label2.Text = "Would you like to upload the imported files\r\nth MEMO web service now?";
            // 
            // button_WOC2
            // 
            this.button_WOC2.BackColor = System.Drawing.Color.White;
            this.button_WOC2.BorderColor = System.Drawing.Color.FromArgb((int)0xd6, (int)0xd6, (int)0xd6);//외곽선 색상
            this.button_WOC2.ButtonColor = System.Drawing.Color.White;
            this.button_WOC2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_WOC2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_WOC2.ForeColor = System.Drawing.Color.White;
            this.button_WOC2.Location = new System.Drawing.Point(177, 132);
            this.button_WOC2.Name = "button_WOC2";
            this.button_WOC2.Size = new System.Drawing.Size(150, 40);
            this.button_WOC2.TabIndex = 2;
            this.button_WOC2.Text = "No";
            this.button_WOC2.TextColor = System.Drawing.Color.Black;
            this.button_WOC2.UseVisualStyleBackColor = false;
            this.button_WOC2.Click += new System.EventHandler(this.button_WOC2_Click);
            // 
            // button_WOC1
            // 
            this.button_WOC1.BackColor = System.Drawing.Color.White;
            this.button_WOC1.BorderColor = System.Drawing.Color.FromArgb((int)0xd6, (int)0xd6, (int)0xd6);//외곽선 색상
            this.button_WOC1.ButtonColor = System.Drawing.Color.Blue;
            this.button_WOC1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_WOC1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_WOC1.ForeColor = System.Drawing.Color.White;
            this.button_WOC1.Location = new System.Drawing.Point(24, 132);
            this.button_WOC1.Name = "button_WOC1";
            this.button_WOC1.Size = new System.Drawing.Size(150, 40);
            this.button_WOC1.TabIndex = 2;
            this.button_WOC1.Text = "Yes";
            this.button_WOC1.TextColor = System.Drawing.Color.White;
            this.button_WOC1.UseVisualStyleBackColor = false;
            this.button_WOC1.Click += new System.EventHandler(this.button_WOC1_Click);
            // 
            // import_complete_alert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(350, 195);
            this.Controls.Add(this.button_WOC2);
            this.Controls.Add(this.button_WOC1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "import_complete_alert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private CustomButton.RoundButton button_WOC1;
        private CustomButton.RoundButton button_WOC2;
    }
}