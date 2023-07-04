using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinerLineas.Entities;
using LinerLineas.Entities.Tablas;
using LinerLineas.Entities.Catalogos;
using LinerLineas.Entities.Complementarias;
using LinerLineasAPI.Utilities;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LinerLineas.Http
{
    public class DevolucionesAutomaticasHttp
    {
        //private readonly IConfiguration _configuration;
        private readonly string apiURL = "";
        RegistroLog log = new RegistroLog();

        public DevolucionesAutomaticasHttp()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            apiURL = builder.GetSection("APIs:LinerLineasAPI").Value;
        }

        //Metodo Control solicitud de devoluciones
        public async Task<bool> SolicitarDevolucionSaldo(ControlDevolucion controlDevolucion)
        {
            bool resultRespuesta = true;
            try
            {
                string url = $"{apiURL}/DevolucionesAutomaticas/SolicitarDevolucionSaldo";
                HttpClient client = new HttpClient();
                var data = JsonConvert.SerializeObject(controlDevolucion);
                HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    resultRespuesta = JsonConvert.DeserializeObject<bool>(result);
                }
                log.LogProceso($"DevolucionesAutomaticasHttp - SolicitarDevolucionSaldo()=> Result API: {httpResponse.IsSuccessStatusCode}");
                return resultRespuesta;
            }
            catch (Exception ex)
            {
                log.LogProceso($"DevolucionesAutomaticasHttp - SolicitarDevolucionSaldo() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "DevolucionesAutomaticasHttp", "SolicitarDevolucionSaldo()");
                return resultRespuesta;
            }
        }
    }
}
