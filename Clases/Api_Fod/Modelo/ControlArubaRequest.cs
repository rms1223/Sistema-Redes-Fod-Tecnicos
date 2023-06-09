
namespace FOD_Meraki.Clases.Api_Fod.Modelo
{
    class ControlArubaRequest
    {
        OperationsDevices _ApiRequest;
        public ControlArubaRequest()
        {
            _ApiRequest = new OperationsDevices();
        }

        public ToolPingDevicesAruba Procesar_Ping_Aruba(string serie,string ip_host)
        {
            return _ApiRequest.SendQueryArubaToolPing(serie,ip_host);
        }


    }
}
