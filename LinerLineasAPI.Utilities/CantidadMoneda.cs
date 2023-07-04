using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinerLineasAPI.Utilities
{
    public class CantidadMoneda
    {
        public string FormatearCantidad(decimal numero)
        {
            try
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
                return retorno;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public string AgregarComas(string cadena, string decimales)
        {
            try
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
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public  string Reverse(string str)
        {
            try
            {
                char[] chars = str.ToCharArray();
                Array.Reverse(chars);
                return new string(chars);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
