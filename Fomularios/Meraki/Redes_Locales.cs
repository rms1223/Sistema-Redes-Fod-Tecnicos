using FOD_Meraki.Clases.Api_Fod;
using FOD_Meraki.Clases.Api_Fod.Modelo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InterfazGrafica.Fomularios.Meraki
{
    public partial class Redes_Locales : Form
    {
        private Equipos_Redes_Fod _FodApi;
        private List<CartelCE> redesDispositivos;
        public Redes_Locales()
        {
            InitializeComponent();
            _FodApi = new Equipos_Redes_Fod();
        }

        private void XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Label3_MouseHover(object sender, EventArgs e)
        {
            label3.BackColor = Color.FromArgb(79, 79, 82);
        }

        private void Label3_MouseLeave(object sender, EventArgs e)
        {
            label3.BackColor = Color.FromArgb(61, 64, 65);
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            Cargar_Datos_CartelCE();
        }
        private void Cargar_Datos_CartelCE()
        {
            try
            {

                datagridview_listarRedes.Rows.Clear();
                redesDispositivos = _FodApi.Get_Cartel_CE(codigo.Text);
                if (redesDispositivos.Count != 0)
                {
                    foreach (var item in redesDispositivos)
                    {

                        datagridview_listarRedes.Rows.Add(item.codigo_institucion, item.centro_educativo, item.id_categoria, item.provincia,item.canton,item.distrito);

                    }


                }
                else
                {
                    datagridview_listarRedes.Rows.Clear();
                    MessageBox.Show("Centro Educativo No Encontrado", "No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al procesar los datos verifique su Conexión " + ex, "Error de Consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Codigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Cargar_Datos_CartelCE();
            }
        }
    }
}
