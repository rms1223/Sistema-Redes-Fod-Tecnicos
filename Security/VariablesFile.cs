using System;


namespace Sistema_Fod.Seguridad
{
    public static class FileConstants
    {
        public static string Server { get; set; }
        public static string User { get; set; }
        public static string Pass { get; set; }
        public static string DatabaseName { get; set; }

        public static string Date
        {
            get
            {

                return DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        public static string Keys
        {
            get
            {

                return "infraFOD";
            }
        }
        public static string DeviceType
        {
            get
            {

                return "Tipo_Equipo";
            }
        }
        public static string DeviceCode
        {
            get
            {

                return "Codigo_Equipo";
            }
        }
        public static string Count
        {
            get
            {

                return "Cantidad";
            }
        }
        public static string Model
        {
            get
            {

                return "Modelo";
            }
        }
        public static string Device
        {
            get
            {

                return "Equipo";
            }
        }
        public static string Brand
        {
            get
            {

                return "Marca";
            }
        }
    }
}

