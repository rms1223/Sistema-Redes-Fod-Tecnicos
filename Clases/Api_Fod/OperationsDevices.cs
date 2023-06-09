using FOD_Meraki.Clases.Api_Fod.Modelo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;

namespace FOD_Meraki.Clases.Api_Fod
{
    class OperationsDevices
    {
        public OperationsDevices()
        {
        }
        public void DeleteRegisterInDatbaseSqlite()
        {
            try
            {
                string data = @"URI=file:" + Key.PathFileDatabase;
                using (SQLiteConnection conn = new SQLiteConnection(data))
                {
                    conn.Open();
                    string stm = "DELETE FROM equipos";
                    var cmd = new SQLiteCommand(stm, conn);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetConfigTemplatesFromCode(string code)
        {
            try
            {
                string reponseQuery = Query.GetApiNameTemplate(code);
                dynamic data = JsonConvert.DeserializeObject(reponseQuery);
                return data;
            }
            catch (Exception)
            {
                return "No Especificado";
            }
        }

        public string GetCentroEducativoName(string code)
        {
            try
            {
                string reponseQuery = Query.GetApiNameCentroEducativo(code);
                dynamic data = JsonConvert.DeserializeObject(reponseQuery);
                return data[0].centro_educativo;
            }
            catch (Exception)
            {
                return "No Especificado";
            }

        }

        public string GetStatusSystem()
        {
            try
            {
                return Query.GetApiStatusSystem; 
            }
            catch (Exception)
            {
                return "No Especificado";
            }

        }

        public List<LocalDevices> SaveDevicesInNetwork(string code)
        {
            DeleteRegisterInDatbaseSqlite();
            try
            {
                string reponseQuery = Query.GetApiDeviceInNetwork(code); 
                var apInformacion = JsonConvert.DeserializeObject<List<NetworkDevice>>(reponseQuery); 
                string data = @"URI=file:" + Key.PathFileDatabase;
               
                using (SQLiteConnection conn = new SQLiteConnection(data))
                {
                    conn.Open();
                    foreach (var item in apInformacion)
                    {
                        var cmd = new SQLiteCommand(conn)
                        {
                            CommandText = Query.InsertIntoDeviceWithNotes
                        };
                        StringBuilder data_tags = new StringBuilder();
                        if (item.tags.Length != 0)
                        {
                            foreach (var tag in item.tags)
                            {
                                data_tags.Append(tag + " ");
                            }
                        }
                        else
                        {
                            data_tags.Append(item.model);
                        }
                        string ip = string.IsNullOrEmpty(item.lanIp) ? Key.DefaultIPRouter : item.lanIp;
                        cmd.Parameters.AddWithValue("@name", item.name);
                        cmd.Parameters.AddWithValue("@serial", item.serial);
                        cmd.Parameters.AddWithValue("@lanIp", ip);
                        cmd.Parameters.AddWithValue("@mac", item.mac);
                        cmd.Parameters.AddWithValue("@model", item.model);
                        cmd.Parameters.AddWithValue("@tags", data_tags.ToString());
                        cmd.Parameters.AddWithValue("@status",item.estado_dispositivos.status);
                        cmd.Parameters.AddWithValue("@url",item.url);
                        cmd.Parameters.AddWithValue("@notes", item.notes);
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                    
                    conn.Close();
                }
                SaveDevicesInformation(code);

                return SearchDevicesInNetworkFromName(code);
            }
            catch (Exception)
            {
                List<LocalDevices> _equipos = new List<LocalDevices>();
                return _equipos;
            }
            
        }
        private void SaveDevicesInformation(string code)
        {
            try
            {
                string reponseQuery = Query.GetApiClientsInNetwork(code); 
                var apInformacion = JsonConvert.DeserializeObject<List<DeviceInformation>>(reponseQuery);
                if (apInformacion.Count !=0)
                {
                    string data = @"URI=file:" + Key.PathFileDatabase;

                    using (SQLiteConnection conn = new SQLiteConnection(data))
                    {
                        conn.Open();
                        foreach (var item in apInformacion)
                        {
                            var cmd = new SQLiteCommand(conn)
                            {
                                CommandText = Query.InsertIntoDeviceWithCode
                            };
                            cmd.Parameters.AddWithValue("@name", item.Name);
                            cmd.Parameters.AddWithValue("@serial", item.Serial);
                            cmd.Parameters.AddWithValue("@lanIp", item.Ip);
                            cmd.Parameters.AddWithValue("@mac", item.Mac);
                            cmd.Parameters.AddWithValue("@model", item.Model);
                            cmd.Parameters.AddWithValue("@tags", item.Tags);
                            cmd.Parameters.AddWithValue("@status", item.Status);
                            cmd.Parameters.AddWithValue("@url", item.Path);
                            cmd.Parameters.AddWithValue("@codigo", item.Code);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                        }

                        conn.Close();
                    }
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los Dispositivos Contacte con el administrador"+ex,"Error de  Conexion",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
        public ToolPingDevicesAruba SendQueryArubaToolPing(string serie, string ip)
        {
            string responseQuery = Query.GetApiArubaToolPing(serie, ip);
            JObject data = JObject.Parse(responseQuery);
            string val = data.ToString();
            var tool_aruba = JsonConvert.DeserializeObject<ToolPingDevicesAruba>(val);
            return tool_aruba;
        }

        public List<LocalDevices> SearchDevicesInNetworkFromName(string name)
        {
            List<LocalDevices> _equipos = new List<LocalDevices>();
            try
            {
                string nombre_red = string.Empty;
                string data = @"URI=file:" + Key.PathFileDatabase;
                using (SQLiteConnection conn = new SQLiteConnection(data))
                {
                    conn.Open();
                    string stm = Query.SearchDevicesFromCode(name);

                    var cmd = new SQLiteCommand(stm, conn);
                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        _equipos.Add(new LocalDevices(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4), rdr.GetString(5), rdr.GetString(6), rdr.GetString(7), rdr.GetString(8)));
                    }

                    conn.Close();

                }
                return _equipos;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(""+Ex);
                return _equipos;
            }
            
        }

        public List<CentroEducativoRedEducativa> GetInformationCentroEducativoRedEducativa(string code)
        {
            try
            {
                string responseQuery = Query.GetApiInfromationRedEducativa(code); 
                var apInformacion = JsonConvert.DeserializeObject<List<CentroEducativoRedEducativa>>(responseQuery);

                return apInformacion;
            }
            catch (Exception)
            {
                List<CentroEducativoRedEducativa> _equipos = new List<CentroEducativoRedEducativa>();
                return _equipos;
            }

        }

        public List<Cartel> GetCartelCentroEducativo(string code)
        {
            try
            {
                string responseQuery = Query.GetApiCarteles(code);
                var apInformacion = JsonConvert.DeserializeObject<List<Cartel>>(responseQuery);

                return apInformacion;
            }
            catch (Exception)
            {
                List<Cartel> _equipos = new List<Cartel>();
                return _equipos;
            }

        }
    }
}
