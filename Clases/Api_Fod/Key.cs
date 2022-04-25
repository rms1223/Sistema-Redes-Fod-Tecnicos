﻿using System;
using System.IO;
using System.Windows.Forms;

namespace FOD_Meraki.Clases.Api_Fod
{
    class Key
    {
        public static string XApiFodValue { get; set; }

        public static string XApiFodName { get; set; }

        public static string BaseUrl_V0
        {
            get
            {
                //return "http://api.redesfod.com/";
                return "http://localhost:4000/";
            }
        }
       

        public static string Url_Estado_Sistema
        {
            get
            {
                return "api/estado/sistema/";
            }
        }
        public static string Url_Device_Codigo
        {
            get
            {
                return "api/device/codigo/";
            }
        }
        public static string Url_CE_RedEducativa
        {
            get
            {
                return "api/ce/rededucativa/";
            }
        }
        public static string Url_Cartel_CE
        {
            get
            {
                return "api/ce/informacion/";
            }
        }
        public static string Url_Devices_Clients_Codigo
        {
            get
            {
                return "api/clients/codigo/";
            }
        }
        public static string Url_Aruba_ToolPing
        {
            get
            {
                return "api/tools/aruba/ping/";
            }
        }
        public static string Url_CE
        {
            get
            {
                return "api/institucion/";
            }
        }
        public static string Url_Template
        {
            get
            {
                return "api/red/template/";
            }
        }
        public static string Organizacion
        {
            get
            {
                return "Fundación Omar Dengo - Conzultek";
            }
        }
        public static string Id_Organizacion
        {
            get
            {
                return "846353";
            }
        }
        public static string OrganizationsUrl
        {
            get
            {
                return "organizations";
            }
        }
        public static string NetworksUrl
        {
            get
            {
                return "networks";
            }
        }
        public static string DevicesUrl
        {
            get
            {
                return "devices";
            }
        }

        public static string DevicesStatus
        {
            get
            {
                return "appliance/uplink/statuses";
            }
        }
        public static string StatusUrl
        {
            get
            {
                return "uplink";
            }
        }
        public static string Codigo_Red
        {
            get
            {
                return "CODIGO_DE_RED";
            }
        }
        public static string Nombre_CE
        {
            get
            {
                return "NOMBRE_CE";
            }
        }
        public static string TAG1
        {
            get
            {
                return "TAG1(CODIGO_CE)";
            }
        }
        public static string TAG2
        {
            get
            {
                return "TAG2(CARTEL)";
            }
        }
        public static string TAG3
        {
            get
            {
                return "TAG3(MODALIDAD)";
            }
        }
        public static string TAG4
        {
            get
            {
                return "TA4(SITIO)";
            }
        }
        public static string Time_Zone
        {
            get
            {
                return "America/Los_Angeles";
            }
        }
        public static string Api_Appliance
        {
            get
            {
                return "appliance";
            }
        }
        public static string Api_Wireless
        {
            get
            {
                return "wireless";
            }
        }
        public static string Api_Switch
        {
            get
            {
                return "switch";
            }
        }
        public static string Config_templates
        {
            get
            {
                return "configTemplates";
            }
        }
        public static string Image_On
        {
            get
            {
                return Path.Combine(Application.StartupPath, @"Data\on.png");
            }
        }
        public static string Image_Off
        {
            get
            {
                return Path.Combine(Application.StartupPath, @"Data\off.png");
            }
        }
        public static string IP_Router
        {
            get
            {
                return "10.10.10.1";
            }
        }
        public static string Key_Telegram
        {
            get
            {
                return "1436790587:AAGNQje4_mCQDWIh5exjnZvpKDiLgyz3t5w";
            }
        }

        public static string Path_File_Db
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Redes_Fod\Datos.db");
            }
        }

        public static string Path_File_Py
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Redes_Fod\meraki-python.py");
            }
        }
        public static string Path_File_Py_Search
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Redes_Fod\meraki-python-search.py");
            }
        }
        public static string Path_Python_Root
        {
            get
            {
                return @"C:\Python39\python.exe";
            }
        }
    }
}
