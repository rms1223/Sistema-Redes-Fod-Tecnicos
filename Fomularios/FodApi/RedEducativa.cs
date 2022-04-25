using FOD_Meraki.Clases.Api_Fod;
using FOD_Meraki.Clases.Api_Fod.Modelo;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FOD_Meraki.Fomularios.FodApi
{
    public partial class RedEducativa : Form
    {
        private Equipos_Redes_Fod _FodApi;
        private List<CeRedEducativa> redesDispositivos;
        public RedEducativa()
        {
            InitializeComponent();
            _FodApi = new Equipos_Redes_Fod();
        }

        private void XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Cargar_Datos_FRedEducativa();
        }

        private void Cargar_Datos_FRedEducativa()
        {
            try
            {

                datagridview_listarRedes.Rows.Clear();
                redesDispositivos = _FodApi.Get_CE_RedEducativa(codigo.Text);
                if (redesDispositivos.Count != 0)
                {
                    foreach (var item in redesDispositivos)
                    {

                        datagridview_listarRedes.Rows.Add(item.codigo_ce, item.nombre_ce, item.nombre_cate, item.ancho_banda);
     
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

        private void Codigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Cargar_Datos_FRedEducativa();
            }
        }
    }
}
