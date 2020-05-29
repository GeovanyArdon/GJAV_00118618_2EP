using System;
using System.Text;
using System.Security.Cryptography;

namespace ClaseGUI05.Controlador
{
    public static class Encriptador
    {
        // Basado de la pagina oficial de microsoft:
        // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5?redirectedfrom=MSDN&view=netcore-3.1
        // Con la ayuda de un aporte en StackOverflow:
        // https://stackoverflow.com/questions/11454004/calculate-a-md5-hash-from-a-string
        // Para los curiosos, mas informacion y ejemplos en:
        // https://www.md5hashgenerator.com
        
        public static string CrearMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static bool CompararMD5(string cadena, string pMD5)
        {
            // Hash the input.
            string hashOfInput = CrearMD5(cadena);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return (0 == comparer.Compare(hashOfInput, pMD5));
        }
    }
}