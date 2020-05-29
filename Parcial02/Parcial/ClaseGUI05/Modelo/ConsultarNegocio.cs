using System;
using System.Collections.Generic;
using System.Data;

namespace ClaseGUI05.Modelo
{
    public class ConsultarNegocio
    {
        public static List<Negocio> getLista()
        {
            // Consulta (probar primero en pgAdmin 4 si funciona)
            string sql = "select * from \"business\"";

            // Ejecutar select y recibir tabla resultado en variable dt
            DataTable dt = Conexion.realizarConsulta(sql);
            
            // Hacer algo con la tabla resultado, en este caso se recorre cada fila
            // (cada fila almacena un cliente) y se van guardando en una lista
            List<Negocio> lista = new List<Negocio>();
            foreach (DataRow fila in dt.Rows)
            {
                Negocio c = new Negocio();
                c.idBusiness = Convert.ToInt32(fila[0].ToString());
                c.name = fila[1].ToString();
                c.description = fila[2].ToString();
                
                
                lista.Add(c);
            }
            return lista;
        }

        public static void agregarNegocio(Negocio r)
        {
            string sql = String.Format(
                "insert into \"business\"" + 
                "(\"name\", \"description\")" +
                "values ('{0}', '{1}');",
                r.name, r.description);
                
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