namespace ClaseGUI05.Modelo
{
    public class Producto
    {
          public int idProducto { get; set; }
                public int idBusiness { get; set; }
                public string name { get; set; }
        
        
                public Producto()
                {
                }
        
                public Producto(int pIdProducto, int pIdBusiness, string pName)
                {
                    idProducto = pIdProducto;
                    idBusiness = pIdBusiness;
                    name = pName;
        
                }
    }
}