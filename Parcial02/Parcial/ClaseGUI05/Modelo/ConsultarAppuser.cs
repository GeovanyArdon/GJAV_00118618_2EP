using System;
using System.Collections.Generic;
using System.Data;

namespace ClaseGUI05.Modelo
{
    public static class ConsultarAppuser
    {
        public static List<Appuser> getLista()
        {
            // Consulta (probar primero en pgAdmin 4 si funciona)
            string sql = "select * from \"appuser\"";

            // Ejecutar select y recibir tabla resultado en variable dt
            DataTable dt = Conexion.realizarConsulta(sql);
            
            // Hacer algo con la tabla resultado, en este caso se recorre cada fila
            // (cada fila almacena un cliente) y se van guardando en una lista
            List<Appuser> lista = new List<Appuser>();
            foreach (DataRow fila in dt.Rows)
            {
                Appuser c = new Appuser();
                c.idUser = Convert.ToInt32(fila[0].ToString());
                c.fullname = fila[1].ToString();
                c.username = fila[2].ToString();
                c.password = fila[3].ToString();
                c.userType = Convert.ToBoolean(fila[4].ToString());
                
                lista.Add(c);
            }
            return lista;
        }

        public static void agregarAppuser(Appuser c)
        {
            string sql = String.Format(
                "insert into \"appuser\"" + 
                "(\"fullname\", \"username\", \"password\", \"userType\")" +
                "values ('{0}', '{1}', '{2}', '{3}');",
                c.fullname, c.username, c.password, c.userType);
                
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