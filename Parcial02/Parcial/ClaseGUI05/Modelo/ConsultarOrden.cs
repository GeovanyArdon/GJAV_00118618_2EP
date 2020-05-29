using System;
using System.Collections.Generic;
using System.Data;

namespace ClaseGUI05.Modelo
{
    public class ConsultarOrden
    {
        public static List<Orden> getLista()
        {
            // Consulta (probar primero en pgAdmin 4 si funciona)
            string sql = "select * from \"apporder\"";

            // Ejecutar select y recibir tabla resultado en variable dt
            DataTable dt = Conexion.realizarConsulta(sql);
            
            // Hacer algo con la tabla resultado, en este caso se recorre cada fila
            // (cada fila almacena un cliente) y se van guardando en una lista
            List<Orden> lista = new List<Orden>();
            foreach (DataRow fila in dt.Rows)
            {
                Orden c = new Orden();
                c.idOrder = Convert.ToInt32(fila[0].ToString());
                c.date = fila[1].ToString();
                c.idProducto = Convert.ToInt32(fila[2].ToString());
                c.idAddress = Convert.ToInt32(fila[3].ToString());
                
                
                lista.Add(c);
            }
            return lista;
        }

        public static void agregarOrden(Orden c)
        {
            string sql = String.Format(
                "insert into \"apporder\"" + 
                "(\"createDate\", \"idProducto\", \"idAddress\")" +
                "values ('{0}', '{1}', '{2}');",
                c.date, c.idProducto, c.idAddress);
                
            Conexion.realizarAccion(sql);
        }
        
        public static void eliminar(string usuario)
        {
            string sql = String.Format(
                "delete from appuser where username ='{0}'; " ,
                
                usuario);
            
            Conexion.realizarAccion(sql);
        }
    }
}