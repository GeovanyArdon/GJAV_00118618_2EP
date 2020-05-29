namespace ClaseGUI05.Modelo
{
    public class Appuser
    {
        public int  idUser { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool userType { get; set; }
        
        public Appuser() { }

        public Appuser(int pID, string pFullname, string pUsername, 
            string pPassword, bool pUsertype)
        {
            idUser = pID;
            fullname = pFullname;
            username = pUsername;
            password = pPassword;
            userType = pUsertype;
        }
    }
}