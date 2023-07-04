using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerLineasAPI.Utilities
{
    public class GuardarArchivo
    {
        RegistroLog log = new RegistroLog();
        public bool SaveFileToDisk(IFormFile file, string pathPDF, string name)
        {
            try
            {
                string fileName = Path.GetFileName(file.FileName);
                using (FileStream stream = new FileStream(Path.Combine(pathPDF, name), FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                log.LogProceso($"GuardarArchivo - Utilidades ------- SaveFileToDisk()=> Se guardo el archvio {name} en la ruta {pathPDF}");
                return true;
            }
            catch (Exception ex)
            {
                log.LogProceso($"GuardarArchivo - Utilidades ------- SaveFileToDisk()=> : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "GuardarArchivo - Utilidades", "SaveFileToDisk()");
                return false;
            }

        }
    }
}
