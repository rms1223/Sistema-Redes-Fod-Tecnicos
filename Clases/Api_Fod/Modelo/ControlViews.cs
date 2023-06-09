using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FOD_Meraki.Clases.Api_Fod.Modelo
{
    class ControlViews
    {
        private DataGridView _dataGridView;
        private OperationsDevices _ApiRequest;
        private string _networkName;
        private int _Contador = 0;
        Image img;
        private List<LocalDevices> redesDispositivos;
        public ControlViews(DataGridView _data)
        {
            _dataGridView = _data;
            _ApiRequest = new OperationsDevices();
        }

        public int GetCountDeviceInDatagridView()
        {
            return _dataGridView.Rows.Count;
        }

        public string GetTemplateName(string codigo)
        {
            try
            {
                return _ApiRequest.GetConfigTemplatesFromCode(codigo);
            }
            catch (Exception)
            {
                return "No Especificado";
            }
        }
        
        public string GetCentroEducativoName(string codigo)
        {
            _networkName = _ApiRequest.GetCentroEducativoName(codigo);
            return _networkName;
        }
        public string GetStatusSystem()
        {
             
            return _ApiRequest.GetStatusSystem();
        }


        public void LoadDevices(string codigo)
        {
            try
            {

                _dataGridView.Rows.Clear();
                redesDispositivos = _ApiRequest.SaveDevicesInNetwork(codigo);
                _Contador = 0;
                if (redesDispositivos.Count != 0)
                {
                    foreach (var item in redesDispositivos)
                    {
                      
                        string valor = item.status.ToUpper();
                        if (valor.Equals(Key.PrimaryStatusDevice) || valor.Equals(Key.SecondStatusDevice))
                        {
                           
                            img = Image.FromFile(Key.PathImageOn);
                            
                        }
                        else
                        {
                            img = Image.FromFile(Key.PathImageOff);
                        }
                        if (item.name.Contains("UTM"))
                        {
                            _dataGridView.Rows.Add(img, item.name, item.serial, Key.DefaultIPRouter, item.mac,item.model,item.tags, item.notes,"Herramientas-PING");
                        }
                        else
                        {
                            _dataGridView.Rows.Add(img, item.name, item.serial, item.lanIp, item.mac, item.model, item.tags, item.notes, "Herramientas-PING");
                        }

                        _Contador++;
                    }
                    

                }
                else
                {
                    _dataGridView.Rows.Clear();
                    _Contador = 0;
                    MessageBox.Show("Centro Educativo no Encontrado ", "No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al procesar los datos verifique su Conexión "+ex, "Error de Consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OnFrameChanged(object o, EventArgs e)
        {

            _dataGridView.Invalidate();
        }


        public string GetPathDevices(string serie)
        {
            string ruta = string.Empty;
            
            foreach (var item in redesDispositivos)
            {
                if (item.serial == serie)
                {
                    ruta = item.url;
                    break;
                }
            }
            
            return ruta;
        }

        public void SearchDevices(string txt_search)
        {

            try
            {
                _dataGridView.Rows.Clear();
                var redesDispositivos = _ApiRequest.SearchDevicesInNetworkFromName(txt_search);
                _Contador = 0;
                
                if (redesDispositivos.Count != 0)
                {
                    foreach (var item in redesDispositivos)
                    {
                        
                        string valor = item.status.ToUpper();
                        if (valor.Equals(Key.PrimaryStatusDevice) || valor.Equals(Key.SecondStatusDevice))
                        {
                            img = Image.FromFile(Key.PathImageOn);
                        }
                        else
                        {
                            img = Image.FromFile(Key.PathImageOff);
                        }
                        if (item.name.Contains("UTM"))
                        {
                            _dataGridView.Rows.Add(img, item.name, item.serial, Key.DefaultIPRouter, item.mac, item.model, item.tags,item.notes,"Herramientas-PING");
                        }
                        else
                        {
                            _dataGridView.Rows.Add(img, item.name, item.serial, item.lanIp, item.mac, item.model, item.tags,item.notes,"Herramientas-PING");
                        }

                        _Contador++;
                    }

                }
                else
                {
                    _dataGridView.Rows.Clear();
                    _Contador = 0;
                    MessageBox.Show("Centro Educativo no Encontrado ", "No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception EX)
            {

                MessageBox.Show("Error al procesar los datos verifique su Conexión\n"+EX, "Error de Consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        
    }
}
