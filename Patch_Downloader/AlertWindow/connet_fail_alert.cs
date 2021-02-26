using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Huinno_Dataloader.AlertWindow
{
    public partial class connect_fail_alert : Form
    {
        public connect_fail_alert()
        {
            InitializeComponent();
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
