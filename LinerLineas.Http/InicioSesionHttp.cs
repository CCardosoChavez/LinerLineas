using LinerLineas.Entities.Complementarias;
using LinerLineas.Entities.Extranet.Catalogos;
using LinerLineas.Http.Utilidades;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinerLineas.Http
{
    public class InicioSesionHttp
    {
        //private readonly IConfiguration _configuration;
        private readonly string apiURL = "";

        public InicioSesionHttp()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            apiURL = builder.GetSection("APIs:LinerLineasAPI").Value;
        }


        public async Task<Result> IniciarSesion(Usuarios usuario)
        {
            Security security = new Security();
            FormatosTexto formato = new FormatosTexto();
            string sURL = $"{apiURL}/InicioSesion/IniciarSesion";

            HttpClient client = new HttpClient();

            usuario.sFSCONTRASENA = formato.StringToBase64(security.Encriptar(usuario.sFSCONTRASENA));

            var data = JsonSerializer.Serialize<Usuarios>(usuario);

            HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync(sURL, content);

            Result result = new Result();
            if (httpResponse.IsSuccessStatusCode)
            {
                var resultJson = await httpResponse.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<Result>(resultJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return result;
        }
    }
}
