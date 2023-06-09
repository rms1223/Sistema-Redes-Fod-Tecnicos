using FOD_Meraki.Clases.Api_Fod;
using FOD_Meraki.Clases.Api_Fod.Modelo;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FOD_Meraki.Fomularios.FodApi
{
    public partial class RedEducativaForm : Form
    {
        private OperationsDevices _FodApi;
        private List<CentroEducativoRedEducativa> redesDispositivos;
        public RedEducativaForm()
        {
            InitializeComponent();
            _FodApi = new OperationsDevices();
        }

        private void XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            LoadDataRedEducativa();
        }

        private void LoadDataRedEducativa()
        {
            try
            {

                datagridview_listarRedes.Rows.Clear();
                redesDispositivos = _FodApi.GetInformationCentroEducativoRedEducativa(codigo.Text);
                if (redesDispositivos.Count != 0)
                {
                    foreach (var item in redesDispositivos)
                    {

                        datagridview_listarRedes.Rows.Add(item.Code, item.Name, item.NameCategory, item.Bandwidth);
     
                    }


                }
                else
                {
                    datagridview_listarRedes.Rows.Clear();
                    MessageBox.Show("Centro Educativo No pertenece a Red Educativa", "No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al procesar los datos verifique su Conexión " + ex, "Error de Consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CodeKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDataRedEducativa();
            }
        }

        private void proveedor_Click(object sender, EventArgs e)
        {

        }
    }
}
