using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LinerLineas.Entities;
using LinerLineas.Entities.Complementarias;
using LinerLineas.Entities.Catalogos;
using LinerLineas.Entities.Tablas;

using System.Net.Http;
using System.Text.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using LinerLineasAPI.Utilities;

using Newtonsoft.Json;

namespace LinerLineas.Http
{
    public class ControlEquipoHttp
    {
        //private readonly IConfiguration _configuration;
        private readonly string apiURL = "";
        RegistroLog log = new RegistroLog();
        public ControlEquipoHttp()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            apiURL = builder.GetSection("APIs:LinerLineasAPI").Value;
        }

        public async Task<Result> AddEstatusContenedorSinDesperfecto(Entities.Tablas.Estatus_Dano_Contenedores danoContenedores)
        {
            Result resultRespuesta = new Result();
            try
            {
                string url = $"{apiURL}/ControlEquipo/AddEstatusContenedorSinDesperfecto";
                HttpClient client = new HttpClient();
                var data = JsonConvert.SerializeObject(danoContenedores);
                HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    resultRespuesta = JsonConvert.DeserializeObject<Result>(result);
                }
                log.LogProceso($"ControlEquipoHttp - AddEstatusContenedorSinDesperfecto()=> Result API: {httpResponse.IsSuccessStatusCode}");
                return resultRespuesta;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ControlEquipoHttp - AddEstatusContenedorSinDesperfecto() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ControlEquipoHttp", "AddEstatusContenedorSinDesperfecto()");
                resultRespuesta.Correct = false;
                return resultRespuesta;
            }

        }

       
        public async Task<Result> DeleteDesperfectoContenedor(CamposExtra camposExtra)
        {
            Result resultRespuesta = new Result();
            try
            {
                string url = $"{apiURL}/ControlEquipo/DeleteDesperfectoContenedor";
                HttpClient client = new HttpClient();
                var data = JsonConvert.SerializeObject(camposExtra);
                HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    resultRespuesta = JsonConvert.DeserializeObject<Result>(result);
                }
                log.LogProceso($"ControlEquipoHttp - DeleteDesperfectoContenedor()=> Result API: {httpResponse.IsSuccessStatusCode}");
                return resultRespuesta;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ControlEquipoHttp - DeleteDesperfectoContenedor() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ControlEquipoHttp", "DeleteDesperfectoContenedor()");
                resultRespuesta.Correct = false;
                return resultRespuesta;
            }

        }
        public async Task<Result> UpdateDesperfectoContenedor(CamposExtra datos)
        {

            Result resultRespuesta = new Result();
            try
            {
                string url = $"{apiURL}/ControlEquipo/UpdateDesperfectoContenedor";
                HttpClient client = new HttpClient();
                var data = JsonConvert.SerializeObject(datos);
                HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    resultRespuesta = JsonConvert.DeserializeObject<Result>(result);
                }
                log.LogProceso($"ControlEquipoHttp - UpdateDesperfectoContenedor()=> Result API: {httpResponse.IsSuccessStatusCode}");
                return resultRespuesta;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ControlEquipoHttp - UpdateDesperfectoContenedor() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ControlEquipoHttp", "UpdateDesperfectoContenedor()");
                resultRespuesta.Correct = false;
                return resultRespuesta;
            }

        }


        public async Task<Result> AgregarDesperfectoContenedor(Desperfecto_Contenedor desperfectoContenedor)
        {
            Result resultRespuesta = new Result();
            try
            {
                string url = $"{apiURL}/ControlEquipo/AgregarDesperfectoContenedor";
                HttpClient client = new HttpClient();
                var data = JsonConvert.SerializeObject(desperfectoContenedor);
                HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    resultRespuesta = JsonConvert.DeserializeObject<Result>(result);
                }
                log.LogProceso($"ControlEquipoHttp - AgregarDesperfectoContenedor()=> Result API: {httpResponse.IsSuccessStatusCode}");
                return resultRespuesta;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ControlEquipoHttp - AgregarDesperfectoContenedor() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ControlEquipoHttp", "AgregarDesperfectoContenedor()");
                resultRespuesta.Correct = false;
                return resultRespuesta;
            }
        }

        public async Task<Result> UpdateEstatusDesperfectoContenedores(Referencias referencia)
        {
            Result resultRespuesta = new Result();
            try
            {
                string url = $"{apiURL}/ControlEquipo/UpdateEstatusDesperfectoContenedores";
                HttpClient client = new HttpClient();
                var data = JsonConvert.SerializeObject(referencia);
                HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    resultRespuesta = JsonConvert.DeserializeObject<Result>(result);
                }
                log.LogProceso($"ControlEquipoHttp - UpdateEstatusDesperfectoContenedores()=> Result API: {httpResponse.IsSuccessStatusCode}");
                return resultRespuesta;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ControlEquipoHttp - AgregarDesperfectoContenedor() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ControlEquipoHttp", "UpdateEstatusDesperfectoContenedoresr()");
                resultRespuesta.Correct = false;
                return resultRespuesta;
            }
        }

        public async Task<Result> GetBLS(DatosBusqueda datosBusqueda)
        {
            Result result = new Result();
            try
            {
                string url = string.Format($"{apiURL}/ControlEquipo/GetBLS?sTexto={datosBusqueda.sTexto}&sFechaInicio={datosBusqueda.sFechaInicial_Desde}&sFechaFin={datosBusqueda.sFechaFinal_Hasta}&nLinea={datosBusqueda.nLinea}&nPaginaActual={datosBusqueda.rPAGINACION.nPaginaActual}&nRegistrosPorPagina={datosBusqueda.rPAGINACION.nRegistrosPorPagina}");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                Result resultRespuesta = JsonConvert.DeserializeObject<Result>(json);

                foreach (var resultReferencia in resultRespuesta.Objects)
                {
                    Stock bl = JsonConvert.DeserializeObject<Stock>(resultReferencia.ToString());
                    result.Objects.Add(bl);
                    result.Correct = true;
                }
                log.LogProceso($"ControlEquipoHttp - GetBLS()=> Result API: {result.Correct}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ControlEquipoHttp - GetBLS() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ControlEquipoHttp", "GetBLS()");
                result.Correct = false;
                return result;
            }
        }

        public async Task<Result> GetContenedores(DatosBusqueda datosBusqueda)
        {


            Result result = new Result();
            try
            {
                string url = string.Format($"{apiURL}/ControlEquipo/GetContenedores?sTexto={datosBusqueda.sTexto}&nItinerario={datosBusqueda.nItinerario}&sNumeroBL={datosBusqueda.sNumBL}");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                Result resultRespuesta = JsonConvert.DeserializeObject<Result>(json);

                foreach (var resultReferencia in resultRespuesta.Objects)
                {
                    Stock bl = JsonConvert.DeserializeObject<Stock>(resultReferencia.ToString());
                    result.Objects.Add(bl);
                    result.Correct = true;
                }
                log.LogProceso($"ControlEquipoHttp - GetContenedores()=> Result API: {result.Correct}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ControlEquipoHttp - GetContenedores() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ControlEquipoHttp", "GetContenedores()");
                result.Correct = false;
                result.Object = ex.Message;
                return result;
            }
        }


        public async Task<int> ObtenerNumeroTotalBLS(DatosBusqueda datosBusqueda)
        {
            int result = 0;
            try
            {
                string url = string.Format($"{apiURL}/ControlEquipo/ObtenerNumeroTotalBLS?nLinea={datosBusqueda.nLinea}");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                result = JsonConvert.DeserializeObject<int>(json);

                
                log.LogProceso($"ControlEquipoHttp - GetBLS()=> Result API: True, Cantidad de BLs: {result}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ControlEquipoHttp - GetBLS()=> Result API: False");
                log.LogProceso($"ControlEquipoHttp - ObtenerNumeroTotalBLS() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ControlEquipoHttp", "ObtenerNumeroTotalBLS()");
                return result;
            }
        }

    }
}
