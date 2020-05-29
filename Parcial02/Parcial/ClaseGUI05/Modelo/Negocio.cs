namespace ClaseGUI05.Modelo
{
    public class Negocio
    {
        public int idBusiness { get; set; }
        public string name { get; set; }
        public string description { get; set; }


        public Negocio()
        {
        }

        public Negocio(int pIdBusiness, string pName, string pDescription)
        {
            idBusiness = pIdBusiness;
            name = pName;
            description = pDescription;

        }
    }
}