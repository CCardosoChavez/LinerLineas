using LinerLineas.Entities.Complementarias;
using LinerLineas.Entities.PagoReferenciado;
using LinerLineas.Http.Utilidades;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using LinerLineasAPI.Utilities;
using System;

namespace LinerLineas.Http
{
    public class InicioSessionPagoReferenciadoHttp
    {
        private readonly string apiURL = "";
        RegistroLog log = new RegistroLog();

        public InicioSessionPagoReferenciadoHttp()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            apiURL = builder.GetSection("APIs:LinerLineasAPI").Value;
        }

        public async Task<Result> GetDatosUsuario(AspNetUsers usuario)
        {

            Result result = new Result();
            try
            {
                string url = string.Format($"{apiURL}/InicioSessionPagoReferenciado/GetDatosUsuario?sIdUsuario={usuario.sId}");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                Result resultRespuesta = JsonConvert.DeserializeObject<Result>(json);

                AspNetUsers user = JsonConvert.DeserializeObject<AspNetUsers>(resultRespuesta.Object.ToString());
                result.Object = user;
                result.Correct = true;
                
                log.LogProceso($"InicioSessionPagoReferenciadoHttp - GetDatosUsuario()=> Result API: {result.Correct}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"InicioSessionPagoReferenciadoHttp - GetDatosUsuario() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "InicioSessionPagoReferenciadoHttp", "GetDatosUsuario()");
                result.Correct = false;
                return result;
            }
        }
    }
}
