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

namespace Huinno_Downloader
{
    public partial class login_window : Form
    {
        const char m_PrintPwChar = '*';

        public login_window()
        {
            InitializeComponent();
            
            // check box for selecting to show password or not 
            CB_ShowPw.Visible = false;
            TB_PW.PasswordChar = m_PrintPwChar;

            // test id & pw
            //TB_ID.Text = "huinno";
            //TB_PW.Text = "1234";
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

        private void BT_login_Click(object sender, EventArgs e)
        {
#if true
            string clientID = TB_ID.Text;
            string clientSecret = TB_PW.Text;

            string url = String.Format("http://huinnoapi.koreacentral.cloudapp.azure.com:443/auth/login");

            HttpMessageHandler handler = new HttpClientHandler()
            {
            };

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(url),
                Timeout = new TimeSpan(0, 2, 0)
            };

            //var val = System.Text.Encoding.UTF8.GetBytes("doctor@test.com:password");
            var val = System.Text.Encoding.UTF8.GetBytes(clientID + ":" + clientSecret);
            string credential = System.Convert.ToBase64String(val);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + credential);

            var jsonObject = new object();
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json"); ;
            HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

            string returnValue = response.Content.ReadAsStringAsync().Result;
            //showDiagText(returnValue);

            if (returnValue.Contains("access_token"))
                MessageBox.Show("Succeed to login", "Confirm", MessageBoxButtons.OK);
            else {
                // failed to login 
                MessageBox.Show("Failed to login. Check user name or password", "Error", MessageBoxButtons.OK);
                return;
            }

            // if success to login --> pass parameters to main_windows
            LoginPass = true;
            LoginId = clientID;
            LoginPw = clientSecret;

            // close login window
            this.Close();

#else

            string strId = TB_ID.Text;
            string strPw = TB_PW.Text;

            // process for login through web page
            if (!(strId == "huinno" && strPw == "1234"))
            {
                // test case for already logged in
                if ((strId == "huinno" && strPw == "12345"))
                {
                    MessageBox.Show("Already logged in another computer. Would you like to log out previous connection?", "Login Issue", MessageBoxButtons.YesNo);
                    return;
                }
                // failed to login 
                MessageBox.Show("Failed to login. Check user name or password", "Error", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Succeed to login", "Confirm", MessageBoxButtons.OK);

            // if success to login --> pass parameters to main_windows
            LoginPass = true;
            LoginId = strId;
            LoginPw = strPw;

            // close login window
            this.Close();
#endif
        }

        private void CB_ShowPw_CheckedChanged(object sender, EventArgs e)
        {
            if( CB_ShowPw.Checked == true)
            {
                TB_PW.PasswordChar = default(char);
            }
            else
            {
                TB_PW.PasswordChar = m_PrintPwChar;
            }
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
    }
}
