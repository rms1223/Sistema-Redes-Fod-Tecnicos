using FOD_Meraki.Clases.Api_Fod.Modelo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace FOD_Meraki.Clases.Api_Fod
{
    class Equipos_Redes_Fod
    {
        public Equipos_Redes_Fod()
        {
            
        }
        public void Eliminar_Registros()
        {
            try
            {
                string data = @"URI=file:" + Key.Path_File_Db;
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

        //Metodo para Procesar api meraki Python --------- new -------------
        public string GetConfigTemplates(string codigo)
        {
            try
            {
                string valor = Procesar_PeticionAsync(Key.Url_Template + codigo);
                dynamic data = JsonConvert.DeserializeObject(valor);
                return data;
            }
            catch (Exception)
            {
                return "No Especificado";
            }
        }
        //--------- new -------------

        //--------- new -------------
        public string Get_NombreCE(string codigo)
        {
            try
            {
                string valor = Procesar_PeticionAsync(Key.Url_CE+codigo);
                dynamic data = JsonConvert.DeserializeObject(valor);
                return data[0].centro_educativo;
            }
            catch (Exception)
            {
                return "No Especificado";
            }

        }

        public string Get_Estado_Sistema()
        {
            try
            {
                string valor = Procesar_PeticionAsync(Key.Url_Estado_Sistema);
                return valor;
            }
            catch (Exception)
            {
                return "No Especificado";
            }

        }
        // Metodos para obtener los dispositivos de una red
        //--------- new -------------
        public List<Local_Devices> Generate_DevicesInRed(string codigo)
        {
            Eliminar_Registros();
            try
            {
                string valor = Procesar_PeticionAsync(Key.Url_Device_Codigo + codigo);
                
                var apInformacion = JsonConvert.DeserializeObject<List<Network_Device>>(valor);

                 
                string data = @"URI=file:" + Key.Path_File_Db;
               
                using (SQLiteConnection conn = new SQLiteConnection(data))
                {
                    conn.Open();
                    foreach (var item in apInformacion)
                    {
                        var cmd = new SQLiteCommand(conn)
                        {
                            CommandText = "INSERT INTO equipos (nombre,serial,ip,mac,modelo,tags,estado,ruta,notes) VALUES(@name,@serial,@lanIp,@mac,@model,@tags,@status,@url,@notes)"
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
                        string ip = string.IsNullOrEmpty(item.lanIp) ? Key.IP_Router : item.lanIp;
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
                Generate_ClientsInDevices(codigo);

                return Buscar_DevicesInRed(codigo);
            }
            catch (Exception)
            {
                List<Local_Devices> _equipos = new List<Local_Devices>();
                return _equipos;
            }
            
        }
        private void Generate_ClientsInDevices(string codigo)
        {
            try
            {
                string valor = Procesar_PeticionAsync(Key.Url_Devices_Clients_Codigo + codigo);
                var apInformacion = JsonConvert.DeserializeObject<List<EquiposInfo>>(valor);
                if (apInformacion.Count !=0)
                {
                    string data = @"URI=file:" + Key.Path_File_Db;

                    using (SQLiteConnection conn = new SQLiteConnection(data))
                    {
                        conn.Open();
                        foreach (var item in apInformacion)
                        {
                            var cmd = new SQLiteCommand(conn)
                            {
                                CommandText = "INSERT INTO equipos (nombre,serial,ip,mac,modelo,tags,estado,ruta,notes) VALUES(@name,@serial,@lanIp,@mac,@model,@tags,@status,@url,@codigo)"
                            };
                            cmd.Parameters.AddWithValue("@name", item.nombre);
                            cmd.Parameters.AddWithValue("@serial", item.serial);
                            cmd.Parameters.AddWithValue("@lanIp", item.ip);
                            cmd.Parameters.AddWithValue("@mac", item.mac);
                            cmd.Parameters.AddWithValue("@model", item.modelo);
                            cmd.Parameters.AddWithValue("@tags", item.tags);
                            cmd.Parameters.AddWithValue("@status", item.estado);
                            cmd.Parameters.AddWithValue("@url", item.ruta);
                            cmd.Parameters.AddWithValue("@codigo", item.codigo);
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
        public PingDevicesAruba Sent_PingAruba(string serie, string ip)
        {
            string valor = Procesar_PeticionAsync(Key.Url_Aruba_ToolPing + serie+"/"+ip);
            JObject data = JObject.Parse(valor);
            string val = data.ToString();
            var tool_aruba = JsonConvert.DeserializeObject<PingDevicesAruba>(val);
            return tool_aruba;
        }


        /// <summary>
        /// Realizar Peticiones al API Fod 
        /// </summary>
        /// <param name="txt_search"></param>
        /// <returns></returns>
        private string Procesar_PeticionAsync(string _tipoConsulta)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Key.BaseUrl_V0);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add(Key.XApiFodName, Key.XApiFodValue);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync(_tipoConsulta).Result;
                    var valor = response.Content.ReadAsStringAsync();
                    return valor.Result;
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Error al realizar la consulta, Verifique su conexión", "Error de Consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }

        }

        public List<Local_Devices> Buscar_DevicesInRed(string txt_search)
        {
            List<Local_Devices> _equipos = new List<Local_Devices>();
            try
            {
                string nombre_red = string.Empty;
                string data = @"URI=file:" + Key.Path_File_Db;
                using (SQLiteConnection conn = new SQLiteConnection(data))
                {
                    conn.Open();
                    string stm = "SELECT notes,nombre,serial,ip,mac,modelo,tags,estado,ruta FROM equipos WHERE nombre LIKE '%" + txt_search + "%' OR serial LIKE '%" + txt_search + "%' OR tags LIKE '%" + txt_search + "%'";

                    var cmd = new SQLiteCommand(stm, conn);
                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        _equipos.Add(new Local_Devices(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4), rdr.GetString(5), rdr.GetString(6), rdr.GetString(7), rdr.GetString(8)));
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

        //----------------------------------------------------------Aqui Agregamos los metodos nuevos  ---------------------------------//
        public List<CeRedEducativa> Get_CE_RedEducativa(string codigo)
        {
            try
            {
                string valor = Procesar_PeticionAsync(Key.Url_CE_RedEducativa + codigo);
                var apInformacion = JsonConvert.DeserializeObject<List<CeRedEducativa>>(valor);

                return apInformacion;
            }
            catch (Exception)
            {
                List<CeRedEducativa> _equipos = new List<CeRedEducativa>();
                return _equipos;
            }

        }

        public List<CartelCE> Get_Cartel_CE(string codigo)
        {
            try
            {
                string valor = Procesar_PeticionAsync(Key.Url_Cartel_CE + codigo);
                var apInformacion = JsonConvert.DeserializeObject<List<CartelCE>>(valor);

                return apInformacion;
            }
            catch (Exception)
            {
                List<CartelCE> _equipos = new List<CartelCE>();
                return _equipos;
            }

        }
    }
}
