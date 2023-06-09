using System;
using System.IO;
using System.Windows.Forms;

namespace FOD_Meraki.Clases.Api_Fod
{
    class Key
    {
        public static string XApiFodValue { get; set; }

        public static string XApiFodName { get; set; }

        public static string PathApiQuery
        {
            get
            {
                //return "http://api.redesfod.com/";
                return "http://localhost:4000/";
            }
        }
        public static string PathApiStatusSystem
        {
            get
            {
                return "api/estado/sistema/";
            }
        }
        public static string PathApiDevice
        {
            get
            {
                return "api/device/codigo/";
            }
        }
        public static string PathApiRedEducativa
        {
            get
            {
                return "api/ce/rededucativa/";
            }
        }
        public static string PathApiCarteles
        {
            get
            {
                return "api/ce/informacion/";
            }
        }
        public static string PathApiClientsInNetwork
        {
            get
            {
                return "api/clients/codigo/";
            }
        }
        public static string PathApiArubaToolPing
        {
            get
            {
                return "api/tools/aruba/ping/";
            }
        }
        public static string PathApiCentroEducativoInformation
        {
            get
            {
                return "api/institucion/";
            }
        }
        public static string PathApiTemplateInformation
        {
            get
            {
                return "api/red/template/";
            }
        }
        public static string OrganizationName
        {
            get
            {
                return "Fundación Omar Dengo - Conzultek";
            }
        }
        public static string IdOrganization
        {
            get
            {
                return "846353";
            }
        }
        public static string OrganizationsName
        {
            get
            {
                return "organizations";
            }
        }
        public static string NetworksName
        {
            get
            {
                return "networks";
            }
        }
        public static string DevicesName
        {
            get
            {
                return "devices";
            }
        }
        public static string PathDevicesStatus
        {
            get
            {
                return "appliance/uplink/statuses";
            }
        }
        public static string StatusName
        {
            get
            {
                return "uplink";
            }
        }
        public static string CodeNetworkName
        {
            get
            {
                return "CODIGO_DE_RED";
            }
        }
        public static string NameCentroEducativo
        {
            get
            {
                return "NOMBRE_CE";
            }
        }
        public static string TAG1Name
        {
            get
            {
                return "TAG1(CODIGO_CE)";
            }
        }
        public static string TAG2Name
        {
            get
            {
                return "TAG2(CARTEL)";
            }
        }
        public static string TAG3Name
        {
            get
            {
                return "TAG3(MODALIDAD)";
            }
        }
        public static string TAG4Name
        {
            get
            {
                return "TA4(SITIO)";
            }
        }
        public static string TimeZoneName
        {
            get
            {
                return "America/Los_Angeles";
            }
        }
        public static string ApplianceName
        {
            get
            {
                return "appliance";
            }
        }
        public static string WirelessName
        {
            get
            {
                return "wireless";
            }
        }
        public static string SwitchName
        {
            get
            {
                return "switch";
            }
        }
        public static string ConfigTemplatesName
        {
            get
            {
                return "configTemplates";
            }
        }
        public static string PathImageOn
        {
            get
            {
                return Path.Combine(Application.StartupPath, @"Data\on.png");
            }
        }
        public static string PathImageOff
        {
            get
            {
                return Path.Combine(Application.StartupPath, @"Data\off.png");
            }
        }
        public static string DefaultIPRouter
        {
            get
            {
                return "10.10.10.1";
            }
        }
        public static string KeyTelegram
        {
            get
            {
                return "";
            }
        }

        public static string PathFileDatabase
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Redes_Fod\Datos.db");
            }
        }

        public static string PathFilePython
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Redes_Fod\meraki-python.py");
            }
        }
        public static string PathFilePythonSearch
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Redes_Fod\meraki-python-search.py");
            }
        }
        public static string PathRootPython
        {
            get
            {
                return @"C:\Python39\python.exe";
            }
        }
        public static string PrimaryStatusDevice
        {
            get
            {
                return "ONLINE";
            }
        }
        public static string SecondStatusDevice
        {
            get
            {
                return "UP";
            }
        }
    }
}
