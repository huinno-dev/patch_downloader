using Huinno_Downloader;
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
    public partial class import_complete_alert : Form
    {
        internal main_window main_window { get; set; }

        public import_complete_alert()
        {
            InitializeComponent();
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            if(this.main_window != null)
            {
                this.main_window.isUploadData(true);
            }
        }

        private void button_WOC2_Click(object sender, EventArgs e)
        {
            if (this.main_window != null)
            {
                this.main_window.isUploadData(false);
                this.Close();
            }
        }
    }
}
