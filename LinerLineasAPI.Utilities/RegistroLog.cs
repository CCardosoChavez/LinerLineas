using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineasAPI.Utilities
{
    public class RegistroLog
    {
        public void LogProceso(string mensaje)
        {
            string nombreArchivo = string.Empty;
            string msg = string.Empty;
            string ruta = string.Empty;

            try
            {
                nombreArchivo = $"LogProceso_{DateTime.Now.ToString("dd-MM-yyyy")}.txt";
                msg = mensaje.Equals("") ? mensaje : string.Format("{0:G}: {1}\r\n", DateTime.Now, mensaje);
                ruta = $"{AppDomain.CurrentDomain.BaseDirectory}Log\\LogProceso\\{nombreArchivo}";

                if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}Log\\LogProceso"))
                    Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}Log\\LogProceso");

                File.AppendAllText(ruta, msg);
            }
            catch (Exception ex)
            {
                LogError(mensaje, "Log", "LogProceso");
            }
        }

        public void LogError(string mensaje, string clase, string metodo)
        {
            string nombreArchivo = string.Empty;
            string msg = string.Empty;
            string ruta = string.Empty;

            try
            {
                nombreArchivo = $"LogError_{DateTime.Now.ToString("dd-MM-yyyy")}.txt";
                msg = mensaje.Equals("") ? mensaje : string.Format("{0:G}: {1}\r\n", DateTime.Now, mensaje);
                ruta = $"{AppDomain.CurrentDomain.BaseDirectory}Log\\LogError\\{nombreArchivo}";

                if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}Log\\LogError"))
                    Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}Log\\LogError");

                File.AppendAllText(ruta, msg);
            }
            catch (Exception ex)
            {
                //LogError(mensaje);
            }
        }
    }
}
