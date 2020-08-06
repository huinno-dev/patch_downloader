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

        private void BT_EnterPw_Click(object sender, EventArgs e)
        {
            if( TB_DevPw.Text!="1234")
                MessageBox.Show("Check Password.");

            this.Close();
        }
    }
}
