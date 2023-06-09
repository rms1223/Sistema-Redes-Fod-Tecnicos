using FOD_Meraki.Fomularios.FodApi;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace FOD_Meraki.Fomularios.Meraki
{
    public partial class DevicesTool : Form
    {

        bool isready;
        private string _path = string.Empty;
        private readonly string _PathMerakiQuery = @"https://n389.meraki.com/login/dashboard_login?email=randy.montoya%40fod.ac.cr&go=";
        private readonly string _PathMerakiTextLogin = @"&password_login=true";
        List<string> ClassnameToDelete = new List<string>() {
            "DetailsNav",
            "NodeAddress InfoListItem--separated",
            "InfoListItem InfoListItem--separated SwitchProfile",
            "InfoListItemEditor",
            "InfoListItem InfoListItem--separated IPSettingsItem",
            "alert alert-warning config_template_alert no_margin ssid-admin-hidden strict-network-admin-hidden"
        };

        public DevicesTool(string path)
        {
            try
            {
                InitializeComponent();
                _path = path;
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
                ProcessInit();
                timer1.Start();
            }
            catch (Exception)
            {

                
            }

        }
        private void Login()
        {
            try
            {
                bool estado = true;
                var elements = webBrowser1.Document.GetElementsByTagName("input");
                foreach (HtmlElement element in elements)
                {

                    if (element.Name == "password")
                    {
                        element.SetAttribute("value", "26262326Rsm@#");
                        foreach (HtmlElement form in webBrowser1.Document.Forms)
                        {
                            form.InvokeMember("submit");
                            estado = false;
                            break;
                        }
                    }
                    
                }
                if (estado)
                {
                    
                    webBrowser1.Navigate(_path);
                    
                }
                
            }
            catch (Exception)
            {
            }
        }

        private void DeleteElementsInMerakiDashboard()
        {
            try
            {
                var elements = webBrowser1.Document.All;
                foreach (HtmlElement element in elements)
                {
                    if (ClassnameToDelete.Contains(element.GetAttribute("classname")))
                    {
                        element.OuterHtml = "";
                    }
                }
            }
            catch (Exception)
            {

                
            }
        }
        private void DeleteElementsByClassname()
        {
            try
            {
                
                webBrowser1.Document.GetElementById("main-nav").Style = "display:none;";
                webBrowser1.Document.GetElementById("masthead-react").Style = "display:none;";
                webBrowser1.Document.GetElementById("trailer_react").Style = "display:none;";
                
                
                DeleteElementsInMerakiDashboard();
            }
            catch (Exception)
            {
                DeleteElementsInMerakiDashboard();

            }

        }

        private void ProcessInit()
        {

            isready = false;
            webBrowser1.Navigate(_PathMerakiQuery + _path + _PathMerakiTextLogin);
            do
            {
                Thread.Sleep(10);
                Application.DoEvents();
            } while (!isready);
            Login();
            
        }

        private void XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Dispose();
            timer1.Stop();
            this.Dispose();
            this.Close();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            DeleteElementsByClassname();
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            isready = true;
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(_path);
        }

        private void VerZonasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoneApForm zonasaps = new ZoneApForm();
            zonasaps.Show();
        }
    }
}
