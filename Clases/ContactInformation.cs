

namespace Sistema_Fod.Classes
{
    class ContactInformation
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Contacts { get; set; }
        public string BandwidthInformation { get; set; }
        public string Category { get; set; }
        public string Modality { get; set; }
        public string DateVisit { get; set; }
        public string Description { get; set; }
        public string Cartel { get; set; }

        public ContactInformation(string codigo, string nombre, string contacto,
            string internet, string categoria, string modalidad, string fecha, string descriipcion, string cartel)
        {
            Code = codigo;
            Name = nombre;
            Contacts = contacto;
            BandwidthInformation = internet;
            Category = categoria;
            Modality = modalidad;
            DateVisit = fecha;
            Description = descriipcion;
            Cartel = cartel;
        }
    }
}