
using FOD_Meraki.Clases.Api_Fod;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Sistema_Fod.Seguridad
{
    class ReadFile
    {
        public ReadFile()
        {
            try
            {
                SecurityFile.DecryptFile(Path.Combine(Application.StartupPath, @"Data\Configuration_App.xml.FOD"), FileConstants.Keys);
                XmlTextReader reader = new XmlTextReader(Path.Combine(Application.StartupPath, @"Data\Configuration_App.xml"));
                string nombre_actual = "";
                while (reader.Read())
                {

                    switch (reader.NodeType)
                    {
                        case XmlNodeType.None:
                            break;
                        case XmlNodeType.Element:

                            nombre_actual = reader.Name;
                            break;
                        case XmlNodeType.Text:
                            LoadValues(nombre_actual, reader.Value);
                            break;
                        default:
                            break;
                    }

                }
                reader.Close();
                SecurityFile.EncryptFile(Path.Combine(Application.StartupPath, @"Data\Configuration_App.xml"), FileConstants.Keys);
                

            }
            catch (Exception)
            {

                MessageBox.Show("Error al establecer Conexion", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadValues(string nombre_campo, string valor)
        {
            switch (nombre_campo)
            {
                case "user":
                    FileConstants.User = valor;
                    break;
                case "pass":
                    FileConstants.Pass = valor;
                    break;
                case "server":
                    FileConstants.Server = valor;
                    break;
                case "name_database":
                    FileConstants.DatabaseName = valor;
                    break;
                case "fod_token":
                    Key.XApiFodValue = valor;
                    break;
                case "fod_name":
                    Key.XApiFodName = valor;
                    break;
                default:
                    break;
            }
        }
    }
}