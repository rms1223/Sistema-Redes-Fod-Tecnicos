using FOD_Meraki.Clases.Api_Fod.Modelo;
using System;
using System.Windows.Forms;

namespace FOD_Meraki.Fomularios.FodApi
{
    public partial class ToolArubaForm : Form
    {
        ControlArubaRequest _ControlArubaRequest;
        public ToolArubaForm(string serie)
        {
            InitializeComponent();
            _ControlArubaRequest = new ControlArubaRequest();
            device_serie.Text = serie;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            SendRequestToolArubaPing();
            button1.Enabled = true;
        }

        private void SendRequestToolArubaPing()
        {
            try
            {
                comando.Text = string.Empty;
                var datos_request = _ControlArubaRequest.Procesar_Ping_Aruba(device_serie.Text,comando_txt.Text);
                comando.Text += "Nombre del CE: "+datos_request.hostname+ "\r\n";
                comando.Text += "Estado de Consulta > "+datos_request.status + "\r\n\r\n";
                comando.Text += datos_request.output;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Aruba Tools", "Erroral enviar Ping\n" + ex, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComandKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Enabled = false;
                SendRequestToolArubaPing();
                button1.Enabled = true;
            }
        }
    }
}
