﻿using System;
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
    public partial class login_confirm_alert : Form
    {
        public login_confirm_alert()
        {
            InitializeComponent();
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}