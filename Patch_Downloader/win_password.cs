using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Huinno_Downloader
{
    public partial class win_password : Form
    {
        public win_password()
        {
            InitializeComponent();
        }

        bool m_devMode = false;
        private void BT_EnterPw_Click(object sender, EventArgs e)
        {
            if( TB_DevPw.Text!="1234")
                MessageBox.Show("Check Password.");

            Passvalue = true; // Form1 으로 값을 전달하기 위해
            PassStr = TB_UploadUrl.Text;
            this.Hide();
        }

        private bool Form2_value;
        private String Form2_string;
        public bool Passvalue
        {
            get { return this.Form2_value; } // Form2에서 얻은(get) 값을 다른폼(Form1)으로 전달 목적
            set { this.Form2_value = value; }  // 다른폼(Form1)에서 전달받은 값을 쓰기
        }
        public String PassStr
        {
            get { return this.Form2_string; } // Form2에서 얻은(get) 값을 다른폼(Form1)으로 전달 목적
            set { this.Form2_string = value; }  // 다른폼(Form1)에서 전달받은 값을 쓰기
        }
    }
}
