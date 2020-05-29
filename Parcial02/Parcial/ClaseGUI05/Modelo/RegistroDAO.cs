using System;

namespace ClaseGUI05.Modelo
{
    public static class RegistroDAO
    {
        public static void iniciarSesion(string pUsuario)
        {
            agregarRegistro(pUsuario, true);
        }
        
        public static void cerrarSesion(string pUsuario)
        {
            agregarRegistro(pUsuario, false);
        }
        
        private static void agregarRegistro(string pUsuario, bool entra)
        {
            string sql = String.Format(
                "insert into registro(usuario, entrar) values ('{0}', {1});",
                pUsuario, entra);

            Conexion.realizarAccion(sql);
        }
    }
}