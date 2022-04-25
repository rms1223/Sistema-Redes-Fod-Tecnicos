using FOD_Meraki.Fomularios.FodApi;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace FOD_Meraki.Fomularios.Meraki
{
    public partial class Herramientas_dispositivos : Form
    {

        bool isready;
        private string _path = string.Empty;
        private readonly string _pathPrincipal = @"https://n389.meraki.com/login/dashboard_login?email=randy.montoya%40fod.ac.cr&go=";
        private readonly string _textLogin = @"&password_login=true";
        List<string> classname = new List<string>() {
            "DetailsNav",
            "NodeAddress InfoListItem--separated",
            "InfoListItem InfoListItem--separated SwitchProfile",
            "InfoListItemEditor",
            "InfoListItem InfoListItem--separated IPSettingsItem",
            "alert alert-warning config_template_alert no_margin ssid-admin-hidden strict-network-admin-hidden"


        };

        public Herramientas_dispositivos(string ruta)
        {
            try
            {
                InitializeComponent();
                _path = ruta;
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
                IniciarEjecucion();
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

        private void Delete_Elements_dashboard()
        {
            try
            {
                var elements = webBrowser1.Document.All;
                foreach (HtmlElement element in elements)
                {
                    if (classname.Contains(element.GetAttribute("classname")))
                    {
                        element.OuterHtml = "";
                    }
                }
            }
            catch (Exception)
            {

                
            }
        }
        private void Delete_Elements()
        {
            try
            {
                
                webBrowser1.Document.GetElementById("main-nav").Style = "display:none;";
                webBrowser1.Document.GetElementById("masthead-react").Style = "display:none;";
                webBrowser1.Document.GetElementById("trailer_react").Style = "display:none;";
                
                
                Delete_Elements_dashboard();
            }
            catch (Exception)
            {
                Delete_Elements_dashboard();

            }

        }

        private void IniciarEjecucion()
        {

            isready = false;
            webBrowser1.Navigate(_pathPrincipal + _path + _textLogin);
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
            Delete_Elements();
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
            Zonasaps zonasaps = new Zonasaps();
            zonasaps.Show();
        }
    }
}
