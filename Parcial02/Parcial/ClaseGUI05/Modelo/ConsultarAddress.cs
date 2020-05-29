using System;
using System.Collections.Generic;
using System.Data;

namespace ClaseGUI05.Modelo
{
    public class ConsultarAddress
    {
        
        public static List<Address> getLista()
        {
            // Consulta (probar primero en pgAdmin 4 si funciona)
            string sql = "select * from \"address\"";

            // Ejecutar select y recibir tabla resultado en variable dt
            DataTable dt = Conexion.realizarConsulta(sql);
            
            // Hacer algo con la tabla resultado, en este caso se recorre cada fila
            // (cada fila almacena un cliente) y se van guardando en una lista
            List<Address> lista = new List<Address>();
            foreach (DataRow fila in dt.Rows)
            {
                Address c = new Address();
                c.idAdreess = Convert.ToInt32(fila[0].ToString());
                c.idUser= Convert.ToInt32(fila[1].ToString());
                c.address = fila[2].ToString();
                
             
                lista.Add(c);
            }
            return lista;
        }


        public static void agregarAddress(Address c)
        {
            string sql = String.Format(
                "insert into \"address\"" + 
                "(\"idUser\",\"address\")" +
                "values ('{0}','{1}');",
                c.idUser,c.address);
                
            Conexion.realizarAccion(sql);
        }

    }
}