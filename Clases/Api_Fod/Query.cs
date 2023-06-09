using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Windows.Forms;

namespace FOD_Meraki.Clases.Api_Fod
{
    internal class Query
    {
        public static string InsertIntoDeviceWithNotes 
        {
            get 
            {
                return "INSERT INTO equipos (nombre,serial,ip,mac,modelo,tags,estado,ruta,notes) VALUES(@name,@serial,@lanIp,@mac,@model,@tags,@status,@url,@notes)";
            }
        }
        public static string InsertIntoDeviceWithCode
        {
            get
            {
                return "INSERT INTO equipos (nombre,serial,ip,mac,modelo,tags,estado,ruta,notes) VALUES(@name,@serial,@lanIp,@mac,@model,@tags,@status,@url,@codigo)";
            }
        }

        public static string SearchDevicesFromCode(string name)
        {
            return "SELECT notes,nombre,serial,ip,mac,modelo,tags,estado,ruta FROM equipos WHERE nombre LIKE '%" + name + "%' OR serial LIKE '%" + name + "%' OR tags LIKE '%" + name + "%'";
        }
        public static string GetApiNameTemplate(string code)
        {
            return ProcessApiQueryAsync(Key.PathApiTemplateInformation + code);
        }
        public static string GetApiNameCentroEducativo(string code)
        {
            return ProcessApiQueryAsync(Key.PathApiCentroEducativoInformation + code);
        }
        public static string GetApiStatusSystem
        {
            get
            {
                return ProcessApiQueryAsync(Key.PathApiStatusSystem);
            }
        }
        public static string GetApiDeviceInNetwork(string code)
        {
            return ProcessApiQueryAsync(Key.PathApiDevice + code);
        }

        public static string GetApiClientsInNetwork(string code)
        {
            return ProcessApiQueryAsync(Key.PathApiClientsInNetwork + code);
        }
        public static string GetApiArubaToolPing(string serie, string ip)
        {
            return ProcessApiQueryAsync(Key.PathApiArubaToolPing + serie + "/" + ip);
        }
        public static string GetApiInfromationRedEducativa(string code)
        {
            return ProcessApiQueryAsync(Key.PathApiRedEducativa + code);
        }
        public static string GetApiCarteles(string code)
        {
            return ProcessApiQueryAsync(Key.PathApiCarteles + code);
        }

        private static string ProcessApiQueryAsync(string _typeQuery)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Key.PathApiQuery);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add(Key.XApiFodName, Key.XApiFodValue);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync(_typeQuery).Result;
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
    }
}
