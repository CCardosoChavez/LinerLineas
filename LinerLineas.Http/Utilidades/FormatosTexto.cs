using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineas.Http.Utilidades
{
    public class FormatosTexto
    {
        public string StringToBase64(string sTexto)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(sTexto);

            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
