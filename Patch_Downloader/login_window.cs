using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Huinno_Dataloader.AlertWindow;

namespace Huinno_Downloader
{
    public partial class login_window : Form
    {
        const char m_PrintPwChar = '*';

        private const bool m_useAdminAccount = true;

        public login_window()
        {
            InitializeComponent();

            // admin id & pw
            //tb_id.Text = "huinno";
            //tb_pw.Text = "huinno1234";
        }

        private bool m_isAuthVer = false;
        public bool AuthVer
        {
            get { return this.m_isAuthVer; }
            set { this.m_isAuthVer = value; }
        }

        private bool m_loginPass = false;
        public bool LoginPass
        {
            get { return this.m_loginPass; }
            set { this.m_loginPass = value; }
        }

        private string m_loginId;
        public string LoginId
        {
            get { return this.m_loginId; }
            set { this.m_loginId = value; }
        }

        private string m_loginPw;
        public string LoginPw
        {
            get { return this.m_loginPw; }
            set { this.m_loginPw = value; }
        }

        private void showDiagText(string result)
        {
            // Get reference to the dialog type.
            var dialogTypeName = "System.Windows.Forms.PropertyGridInternal.GridErrorDlg";
            var dialogType = typeof(Form).Assembly.GetType(dialogTypeName);

            // Create dialog instance.
            var dialog = (Form)Activator.CreateInstance(dialogType, new PropertyGrid());

            // Populate relevant properties on the dialog instance.
            dialog.Text = "Sample Title";
            dialogType.GetProperty("Details").SetValue(dialog, result, null);
            dialogType.GetProperty("Message").SetValue(dialog, "Sample Message", null);

            // Display dialog.
            dialog.ShowDialog();
        }

        private void BT_Exit_Click(object sender, EventArgs e)
        {
            LoginPass = false;
            this.Hide();
            Application.ExitThread();
            Environment.Exit(0);
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string clientID = tb_id.Text;
            string clientSecret = tb_pw.Text;

            if(clientID == "Email" && clientSecret == "Password")
            {
                // failed to login 
                //MessageBox.Show("Failed to login. Check user name or password", "Error", MessageBoxButtons.OK);
                this.Opacity = 0.7;
                login_fail_alert login_fail = new login_fail_alert();
                login_fail.ShowDialog();
                this.Opacity = 1;
                return;
            }

            if ((clientID == "huinno" && clientSecret == "huinno1234") && m_useAdminAccount) //Admin account
            {
                if (m_isAuthVer)
                    MessageBox.Show("Succeed to login(Admin)", "Confirm", MessageBoxButtons.OK);
            }
            else 
            {
                string url = String.Format("http://huinnoapi.koreacentral.cloudapp.azure.com:443/auth/login");
                //string url = String.Format("http://huinnoapi.koreacentral.cloudapp.azure.com");  //For timeout test

                HttpMessageHandler handler = new HttpClientHandler()
                {
                };

                var httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url),
                    //Timeout = new TimeSpan(0, 2, 0)
                    Timeout = TimeSpan.FromMilliseconds(3000) //3 sec
                };

                //var val = System.Text.Encoding.UTF8.GetBytes("doctor@test.com:password");
                var val = System.Text.Encoding.UTF8.GetBytes(clientID + ":" + clientSecret);
                string credential = System.Convert.ToBase64String(val);
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + credential);

                var jsonObject = new object();
                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage response = new HttpResponseMessage();

                try
                {
                    response = httpClient.PostAsync(url, content).Result;
                }
#pragma warning disable 0168
                catch (Exception exception)
#pragma warning restore 0168
                {
                    //MessageBox.Show("There was a problem commnunicating with the server. Please try again later.", "Couldn't connect", MessageBoxButtons.OK);
                    this.Opacity = 0.7;
                    connect_fail_alert connect_fail = new connect_fail_alert();
                    connect_fail.ShowDialog();
                    this.Opacity = 1;
                    return;
                }

                string returnValue = response.Content.ReadAsStringAsync().Result;
                //showDiagText(returnValue);

                if (returnValue.Contains("access_token"))
                {
                    if(m_isAuthVer)
                        MessageBox.Show("Succeed to login", "Confirm", MessageBoxButtons.OK);
                }
                else
                {
                    // failed to login 
                    //MessageBox.Show("Failed to login. Check user name or password", "Error", MessageBoxButtons.OK);
                    this.Opacity = 0.7;
                    login_fail_alert login_fail = new login_fail_alert();
                    login_fail.ShowDialog();
                    this.Opacity = 1;
                    return;
                }
            }

            // if success to login --> pass parameters to main_windows
            LoginPass = true;
            LoginId = clientID;
            LoginPw = clientSecret;

            // close login window
            this.Close();
        }

        private bool m_progRunning;
        public bool ProgRunning
        {
            get { return this.m_progRunning; }
            set { this.m_progRunning = value; }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            m_progRunning = ProgRunning;
        }

        private void tb_id_Enter(object sender, EventArgs e)
        {
            if (tb_id.Text == "Email")
                tb_id.Text = null;

            tb_id.ForeColor = Color.Black;
        }

        private void tb_id_Leave(object sender, EventArgs e)
        {
            if (tb_id.Text == "")
            {
                tb_id.Text = "Email";
                tb_id.ForeColor = Color.DarkGray;
            }

        }

        private void tb_pw_Enter(object sender, EventArgs e)
        {
            if (tb_pw.Text == "Password")
                tb_pw.Text = null;

            tb_pw.ForeColor = Color.Black;
            tb_pw.UseSystemPasswordChar = true;
        }

        private void tb_pw_Leave(object sender, EventArgs e)
        {
            if (tb_pw.Text == "")
            {
                tb_pw.Text = "Password";
                tb_pw.ForeColor = Color.DarkGray;
                tb_pw.UseSystemPasswordChar = false;
            }
        }

        private void btn_minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            //Exit program
            Application.ExitThread();
            Environment.Exit(0);
        }

        private void btn_exit2_Click(object sender, EventArgs e)
        {
            //Exit program
            Application.ExitThread();
            Environment.Exit(0);
        }
    }
}
