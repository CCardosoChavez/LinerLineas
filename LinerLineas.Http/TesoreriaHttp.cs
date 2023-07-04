using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LinerLineas.Entities;
using LinerLineas.Entities.Catalogos;
using LinerLineas.Entities.Complementarias;
using LinerLineas.Entities.Tablas;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using LinerLineasAPI.Utilities;
using Newtonsoft.Json;

namespace LinerLineas.Http
{
    public class TesoreriaHttp
    {
        //private readonly IConfiguration _configuration;
        private readonly string apiURL = "";
        RegistroLog log = new RegistroLog();
        public TesoreriaHttp()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            apiURL = builder.GetSection("APIs:LinerLineasAPI").Value;
        }

        public async Task<Result> GetReferenciasDatosBancarios(DatosBusqueda datos)
        {
            Result result = new Result();
            Result resultRespuesta = new Result();
            try
            {
                string url = string.Format($"{apiURL}/Tesoreria/GetReferenciasDatosBancarios?sTexto={datos.sTexto}&sFechaInicio={datos.sFechaInicial_Desde}&sFechaFin={datos.sFechaFinal_Hasta}&sRol={datos.sRol}&nPaginaActual={datos.rPAGINACION.nPaginaActual}&nRegistrosPorPagina={datos.rPAGINACION.nRegistrosPorPagina}&nIdTipoConsulta={datos.nIdTipoConsultaTesoseria}");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                resultRespuesta = JsonConvert.DeserializeObject<Result>(json);

                foreach (var resultReferencia in resultRespuesta.Objects)
                {
                    Datos_Bancarios_Referencia datosBancarios = JsonConvert.DeserializeObject<Datos_Bancarios_Referencia>(resultReferencia.ToString());
                    result.Objects.Add(datosBancarios);
                    result.Correct = true;
                }
                log.LogProceso($"TesoreriaHttp - GetReferenciasDatosBancarios()=> Result API: {result.Correct}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"TesoreriaHttp - GetReferenciasDatosBancarios() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "TesoreriaHttp", "GetReferenciasDatosBancarios()");
                result.Correct = false;
                return result;
            }
        }

        public async Task<Result> GetReferenciasDatosBancariosDescarga(DatosBusqueda datos)
        {
            Result result = new Result();
            try
            {
                string url = string.Format($"{apiURL}/Tesoreria/GetReferenciasDatosBancariosDescarga?sTexto={datos.sTexto}&sFechaInicio={datos.sFechaInicial_Desde}&sFechaFin={datos.sFechaFinal_Hasta}&sRol={datos.sRol}&nPaginaActual={datos.rPAGINACION.nPaginaActual}&nRegistrosPorPagina={datos.rPAGINACION.nRegistrosPorPagina}&nIdTipoConsulta={datos.nIdTipoConsultaTesoseria}");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                Result resultRespuesta = JsonConvert.DeserializeObject<Result>(json);

                foreach (var resultReferencia in resultRespuesta.Objects)
                {
                    Datos_Bancarios_Referencia datosBancarios = JsonConvert.DeserializeObject<Datos_Bancarios_Referencia>(resultReferencia.ToString());
                    result.Objects.Add(datosBancarios);
                    result.Correct = true;
                }
                log.LogProceso($"TesoreriaHttp - GetReferenciasDatosBancariosDescarga()=> Result API: {result.Correct}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"TesoreriaHttp - GetReferenciasDatosBancariosDescarga() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "TesoreriaHttp", "GetReferenciasDatosBancariosDescarga()");
                result.Correct = false;
                return result;
            }

            //Result result = new Result();
            //try
            //{
            //    using (var client = new HttpClient())
            //    {
            //        string url = "Tesoreria/GetReferenciasDatosBancariosDescarga";
            //        client.BaseAddress = new Uri(apiURL+"/");
            //        //var responsTask = client.PostAsJsonAsync<DatosBusqueda>(url, datos);//client.GetAsync(url);//client.PostAsJsonAsync<DatosBusqueda>(url, datos);
            //        //responsTask.Wait();

            //        //var resultTask = responsTask.Result;
            //        var postTask = client.PostAsJsonAsync<DatosBusqueda>(url, datos);
            //        postTask.Wait();

            //        var resultTask = postTask.Result;

            //        if (resultTask.IsSuccessStatusCode)
            //        {
            //            var readTask = resultTask.Content.ReadAsAsync<Result>();
            //            readTask.Wait();

            //            foreach (var resultReferencia in readTask.Result.Objects)
            //            {
            //                Datos_Bancarios_Referencia resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<Datos_Bancarios_Referencia>(resultReferencia.ToString());
            //                result.Objects.Add(resultItemList);
            //                result.Correct = true;
            //            }
            //        }
            //        else
            //        {
            //            result.Correct = false;
            //        }
            //    }
            //    return result;
            //}
            //catch (Exception ex)
            //{
            //    result.Correct = false;
            //    return result;
            //}
        }

        public async Task<Result> UpdateEstatusDatosBancarios(Datos_Bancarios_Referencia datosBancarios)
        {
            Result resultRespuesta = new Result();
            try
            {
                string url = $"{apiURL}/Tesoreria/UpdateEstatusDatosBancarios";
                HttpClient client = new HttpClient();
                var data = JsonConvert.SerializeObject(datosBancarios);
                HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    resultRespuesta = JsonConvert.DeserializeObject<Result>(result);
                }
                log.LogProceso($"TesoreriaHttp - UpdateEstatusDatosBancarios()=> Result API: {httpResponse.IsSuccessStatusCode}");
                return resultRespuesta;
            }
            catch (Exception ex)
            {
                log.LogProceso($"TesoreriaHttp - UpdateEstatusDatosBancarios() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "TesoreriaHttp", "UpdateEstatusDatosBancarios()");
                resultRespuesta.Correct = false;
                return resultRespuesta;
            }
        }

        public async Task<int> GetTotalReferenciasDatosBancarios()
        {
            int result = 0;
            try
            {
                string url = string.Format($"{apiURL}/Tesoreria/GetTotalReferenciasDatosBancarios");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                result = JsonConvert.DeserializeObject<int>(json);

                log.LogProceso($"TesoreriaHttp - GetTotalReferenciasDatosBancarios()=> Result API: True, Cantidad de BLs: {result}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"TesoreriaHttp - GetTotalReferenciasDatosBancarios()=> Result API: False");
                log.LogProceso($"TesoreriaHttp - GetTotalReferenciasDatosBancarios() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "TesoreriaHttp", "GetTotalReferenciasDatosBancarios()");
                return result;
            }
        }
    }
}
