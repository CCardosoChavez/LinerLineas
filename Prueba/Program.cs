using System;
using System.Text.RegularExpressions;

namespace Prueba
{
    class Program
    {
        static void Main(string[] args)
        {
            FormatearCantidad(512);
        }

        public static void FormatearCantidad(decimal numero)
        {
            string cadenaNumero = numero.ToString();
            string retorno = "";

            if (cadenaNumero.Contains('.'))
            {
                var resultSplit = cadenaNumero.Split(".");
                if (resultSplit[1].Length == 1)
                {
                    resultSplit[1] = resultSplit[1] + "0";

                    retorno = AgregarComas(resultSplit[0], resultSplit[1]);
                }
                else
                {
                    retorno = AgregarComas(resultSplit[0], resultSplit[1]);
                }
            }
            else
            {
                retorno = AgregarComas(cadenaNumero, "00");
            }

        }

        public static string AgregarComas(string cadena, string decimales)
        {
            string retorno = "";
            if (cadena.Length <= 3)
            {
                retorno = cadena + "." + decimales;
            }
            else
            {
                string cadenaInvertida = Reverse(cadena);
                retorno = Regex.Replace(cadenaInvertida, "(.{3})(?!$)", "$0,");
                retorno = Reverse(retorno) + "." + decimales;
            }

            return retorno;
        }

        public static string Reverse(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }
}
