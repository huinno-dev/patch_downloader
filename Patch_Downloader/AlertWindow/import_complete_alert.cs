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
                ControlMainWindow(main_window, true);
                this.Close();
            }
        }

        private void button_WOC2_Click(object sender, EventArgs e)
        {
            if (this.main_window != null)
            {
                ControlMainWindow(main_window, false);
                this.Close();
            }
        }

        delegate void ctrl_Invoke_MainWindow(main_window ctrl, bool enable);
        public void ControlMainWindow(main_window ctr, bool enable)
        {
            if (ctr.InvokeRequired)
            {
                ctrl_Invoke_MainWindow CI = new ctrl_Invoke_MainWindow(ControlMainWindow);
                ctr.Invoke(CI, ctr, enable);
            }
            else
            {
                ctr.isUploadData(enable);
            }
        }
    }
}
