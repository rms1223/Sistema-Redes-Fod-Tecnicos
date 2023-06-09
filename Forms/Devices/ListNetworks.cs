using FOD_Meraki.Clases.Api_Fod;
using FOD_Meraki.Clases.Api_Fod.Modelo;
using FOD_Meraki.Fomularios;
using FOD_Meraki.Fomularios.FodApi;
using FOD_Meraki.Fomularios.Meraki;
using System;
using System.Windows.Forms;

namespace InterfazGrafica.Fomularios.Meraki
{
    public partial class ListNetworks : Form
    {
        
        private ControlViews _ControlViews;
        private static ListNetworks instance = null;

        public static ListNetworks Get_instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ListNetworks();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private ListNetworks()
        {
            InitializeComponent();
            proveedor_name.Text = "Insert Organization";//Key.OrganizationName;
            _ControlViews = new ControlViews(datagridview_listarRedes);
            

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            
            ProcessRequestSearch();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ProcessRequestUser();

        }



        private void Codigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessRequestUser();
            }
        }
        private void ProcessRequestUser()
        {
            SearchLoadForm search = new SearchLoadForm();
            try
            {
                
                search.Show();
                button1.Enabled = false;
                button3.Enabled = false;
                string estado = _ControlViews.GetStatusSystem();

                if (!string.IsNullOrEmpty(estado))
                {
                    estado_sistema.Text = estado;
                    nombre_ce.Text = _ControlViews.GetCentroEducativoName(codigo.Text);
                    template_nombre.Text = _ControlViews.GetTemplateName(codigo.Text);
                    _ControlViews.LoadDevices(codigo.Text);
                    total_registros.Text = $"Total de Registros: {_ControlViews.GetCountDeviceInDatagridView()}";
                }
                else
                {
                    estado_sistema.Text = "Error de Servidor";
                }
                button1.Enabled = true;
                button3.Enabled = true;
                search.Close();
            }
            catch (Exception)
            {
                button1.Enabled = true;
                search.Close();
            }
        }
       

        private void ProcessRequestSearch()
        {
            SearchLoadForm buscar = new SearchLoadForm();
            try
            {
                buscar.Show();
                _ControlViews.SearchDevices(txt_buscar.Text);
                total_registros.Text = $"Total de Registros: {_ControlViews.GetCountDeviceInDatagridView()}";
                buscar.Close();
            }
            catch (Exception)
            {
                buscar.Close();
                MessageBox.Show("Error al procesar la Consulta","Error....",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessRequestSearch();
            }
        }
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                string modelo = datagridview_listarRedes.Rows[e.RowIndex].Cells[5].Value.ToString();
                string serial = datagridview_listarRedes.Rows[e.RowIndex].Cells[2].Value.ToString();
                if (modelo.Equals("A9004"))
                {
                    ToolArubaForm _herramientasAruba = new ToolArubaForm(serial);
                    _herramientasAruba.Show();
                }
                else
                {
                    string _path = _ControlViews.GetPathDevices(serial);
                    DevicesTool herramientas_dispositivos = new DevicesTool(_path);
                    herramientas_dispositivos.Show();
                }
                
            }
        }

        private void DireccionamientoIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DireccionamientoIp direccionamientoIp = new DireccionamientoIp();
            direccionamientoIp.Show();
        }

        private void ModelosDeEquiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DevicesCartelesForm equiposCarteles = new DevicesCartelesForm();
            equiposCarteles.Show();
        }

        private void ExportarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportDataForm exportDatos = new ExportDataForm();
            exportDatos.Show();
            ExportDataToExcel();
            exportDatos.Close();
        }
        private void ExportDataToExcel()
        {
            Microsoft.Office.Interop.Excel.Application exportExcel = new Microsoft.Office.Interop.Excel.Application();
            exportExcel.Application.Workbooks.Add(true);
            int indiceColumna = 0;

            foreach (DataGridViewColumn columna in datagridview_listarRedes.Columns)
            {
                indiceColumna++;
                exportExcel.Cells[1, indiceColumna] = columna.Name;
            }
            int indiceFila = 0;

            foreach (DataGridViewRow filas in datagridview_listarRedes.Rows)
            {
                indiceFila++;
                indiceColumna = 0;
                foreach (DataGridViewColumn columna in datagridview_listarRedes.Columns)
                {
                    indiceColumna++;
                    exportExcel.Cells[indiceFila + 1, indiceColumna] = filas.Cells[columna.Name].Value;
                }
            }
            exportExcel.Visible = true;

        }

        private void EstandarDePuertosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Port_Standard _puertos = new Port_Standard();
            _puertos.Show();
        }
    }
}
