namespace ClaseGUI05.Modelo
{
    public class Address
    {
        public int idAdreess { get; set; }
        public int idUser { get; set; }
        public string address { get; set; }


        public Address()
        {
        }

        public Address(int pIdAdreess, int pID, string pAddress)
        {
            idAdreess = pIdAdreess;
            idUser = pID;
            address = pAddress;

        }
    }
}