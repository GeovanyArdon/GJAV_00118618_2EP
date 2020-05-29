using System;
using System.Collections.Generic;
using System.Data;

namespace ClaseGUI05.Modelo
{
    public class ConsultarProducto
    {
           
        public static List<Producto> getLista()
        {
            // Consulta (probar primero en pgAdmin 4 si funciona)
            string sql = "select * from \"product\"";

            // Ejecutar select y recibir tabla resultado en variable dt
            DataTable dt = Conexion.realizarConsulta(sql);
            
            // Hacer algo con la tabla resultado, en este caso se recorre cada fila
            // (cada fila almacena un cliente) y se van guardando en una lista
            List<Producto> lista = new List<Producto>();
            foreach (DataRow fila in dt.Rows)
            {
                Producto c = new Producto();
                c.idProducto= Convert.ToInt32(fila[0].ToString());
                c.idBusiness= Convert.ToInt32(fila[1].ToString());
                c.name = fila[2].ToString();
                
             
                lista.Add(c);
            }
            return lista;
        }


        public static void agregarProducto(Producto c)
        {
            string sql = String.Format(
                "insert into \"product\"" + 
                "(\"idBusiness\",\"name\")" +
                "values ('{0}','{1}');",
                c.idBusiness,c.name);
                
            Conexion.realizarAccion(sql);
        }
    }
}