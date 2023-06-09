

namespace FOD_Meraki.Clases.Api_Fod.Modelo
{
    class DeviceInformation
    {
        public string Status { get; set; }
        public string Serial { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string Mac { get; set; }
        public string Model { get; set; }
        public string Tags { get; set; }
        public string Code { get; set; }
        public string Path { get; set; }
        
        public DeviceInformation(string name, string serial_dev, string ip_dev, string mac_dev, string model, string tags_dev, string status, string path, string codigoCE)
        {
            Name = name;
            Serial = serial_dev;
            Ip = ip_dev;
            Mac = mac_dev;
            Model = model;
            Tags = tags_dev;
            Status = status;
            Path = path;
            Code = codigoCE;
        }
        public DeviceInformation()
        {

        }
    }
}
