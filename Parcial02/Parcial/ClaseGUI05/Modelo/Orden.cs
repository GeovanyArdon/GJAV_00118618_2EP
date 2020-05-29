namespace ClaseGUI05.Modelo
{
    public class Orden
    {
              public int  idOrder { get; set; }
                public string date { get; set; }
                public int idProducto { get; set; }
                
                public int idAddress { get; set; }
                
                public Orden() { }
        
                public Orden(int pIdOrder, string pDate, int pidProducto, 
                    int pidAddress)
                {
                    idOrder = pIdOrder;
                    date = pDate;
                    idProducto=pidProducto ;
                    idAddress = pidAddress;
                   
                }
    }
}