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
    public class ReferenciasHttp
    {
        RegistroLog log = new RegistroLog();
        private readonly string apiURL = "";

        public ReferenciasHttp()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            apiURL = builder.GetSection("APIs:LinerLineasAPI").Value;
        }

        public async Task<Result> ObtenerReferencias(Entities.Tablas.Referencias referencia)
        {

            Result result = new Result();
            try
            {
                string url = string.Format($"{apiURL}/Referencias/ObtenerReferencias?sTexto={referencia.rDATOS_BUSQUEDA.sTexto}&sFechaInicio={referencia.rDATOS_BUSQUEDA.sFechaInicial_Desde}&sFechaFin={referencia.rDATOS_BUSQUEDA.sFechaFinal_Hasta}&sRol={referencia.rASPNET_ROLES.sName}&sIdUsuario={referencia.rASPNET_USERS.sId}&nPaginaActual={referencia.rPAGINACION.nPaginaActual}&nRegistrosPorPagina={referencia.rPAGINACION.nRegistrosPorPagina}");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                Result resultRespuesta = JsonConvert.DeserializeObject<Result>(json);

                foreach (var resultReferencia in resultRespuesta.Objects)
                {
                    Entities.Tablas.Referencias referenciaResult = JsonConvert.DeserializeObject<Entities.Tablas.Referencias>(resultReferencia.ToString());
                    result.Objects.Add(referenciaResult);
                    result.Correct = true;
                }
                log.LogProceso($"ReferenciasHttp - ObtenerReferencias()=> Result API: {result.Correct}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ReferenciasHttp - ObtenerReferencias() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ReferenciasHttp", "ObtenerReferencias()");
                result.Correct = false;
                return result;
            }
        }

        public async Task<ReferenciaAgregada> InsertarReferenciaPago(Referencias referencia)
        {

            ReferenciaAgregada result = new ReferenciaAgregada();

            try
            {
                string url = $"{apiURL}/Referencias/InsertarReferenciaPago";
                HttpClient client = new HttpClient();
                var data = JsonConvert.SerializeObject(referencia);
                HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var resultRespuesta = await httpResponse.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ReferenciaAgregada>(resultRespuesta);
                }

                log.LogProceso($"ReferenciasHttp - InsertarReferenciaPago()=> Result API: {httpResponse.IsSuccessStatusCode}");

                DatosPDF datosPDF = await ObtenerDatosPDF(result.nIdReferencia);
                CrearPDF cp = new CrearPDF();
                MemoryStream ms = cp.CrearArchvioPDF(datosPDF);

                EnvioCorreo ec = new EnvioCorreo();
                string nombreArchivo = result.sReferencia + "_" + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf";
                ec.EnviarCorreo(result.Mensaje, "Referencia de Pago de Depósito en Garantía", result.Correo, nombreArchivo, ms);
                log.LogProceso($"ReferenciasHttp------- InsertarReferenciaPago()=> Se genero el PDF de la referencia {datosPDF.sNUMERO_REFERENCIA} con el nombre del archivo {nombreArchivo} para adjuntar al envio de correo con la dirección: {result.Correo} y asunto 'Referencia de Pago de Depósito en Garantía'");

                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ReferenciasHttp - InsertarReferenciaPago() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ReferenciasHttp", "InsertarReferenciaPago()");
                result.Correct = false;
                return result;
            }
        }

        public async Task<Result> ObtenerDatosBL(Referencias referencia)
        {
            Result result = new Result();

            try
            {
                string url = string.Format($"{apiURL}/Referencias/ObtenerDatosBL?sNumeroBL={referencia.sNUM_BL}");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                Result resultRespuesta = JsonConvert.DeserializeObject<Result>(json);

                Referencias referenciaResult = JsonConvert.DeserializeObject<Referencias>(resultRespuesta.Object.ToString());
                result.Object = referenciaResult;
                result.Correct = true;
                
                log.LogProceso($"ReferenciasHttp - ObtenerDatosBL()=> Result API: {result.Correct}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ReferenciasHttp - ObtenerDatosBL() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ReferenciasHttp", "ObtenerDatosBL()");
                result.Correct = false;
                return result;
            }
        }

        public async Task<int> ComprobarBLValido(BL_I bli)
        {
            int result = 0;
            try
            {
                string url = $"{apiURL}/Referencias/ComprobarBLValido";
                HttpClient client = new HttpClient();
                var data = JsonConvert.SerializeObject(bli);
                HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var resultRespuesta = await httpResponse.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<int>(resultRespuesta);
                }
                log.LogProceso($"ReferenciasHttp - ComprobarBLValido()=> Result API: {httpResponse.IsSuccessStatusCode}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ReferenciasHttp - ComprobarBLValido() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ReferenciasHttp", "ComprobarBLValido()");
                return result;
            }
        }

        public async Task<Result> ObtenerConsignatario(DatosBusqueda datos)
        {
            Result result = new Result();
            try
            {
                string url = string.Format($"{apiURL}/Referencias/ObtenerConsignatario?sTexto={datos.sTexto}&sNumeroBL={datos.sNumBL}");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                result = JsonConvert.DeserializeObject<Result>(json);

                log.LogProceso($"ReferenciasHttp - ObtenerConsignatario()=> Result API: {result.Correct}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ReferenciasHttp - ObtenerConsignatario() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ReferenciasHttp", "ObtenerConsignatario()");
                result.Correct = false;
                return result;
            }

        }

        public async Task<int> ObtenerNumeroTotalReferencias(Referencias referencia)
        {

            int result = 0;
            try
            {
                string url = string.Format($"{apiURL}/Referencias/ObtenerNumeroTotalReferencias?nIdCliente={referencia.rCLIENTE.nFIIDCLIETE}&sIdUsuario={referencia.rASPNET_USERS.sId}");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                result = JsonConvert.DeserializeObject<int>(json);

                if(result != 0)
                    log.LogProceso($"ReferenciasHttp - ObtenerNumeroTotalReferencias()=> Result API: True, Referencias obtenidas {result}");
                else
                    log.LogProceso($"ReferenciasHttp - ObtenerNumeroTotalReferencias()=> Result API: False");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ReferenciasHttp - ObtenerNumeroTotalReferencias() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ReferenciasHttp", "ObtenerNumeroTotalReferencias()");
                return result;
            }

        }

        public async Task<Result> ObtenerContenedoresPorReferencia(Referencias referencia)
        {
            Result result = new Result();
            try
            {
                string url = string.Format($"{apiURL}/Referencias/ObtenerContenedoresPorReferencia?sNumeroBL={referencia.sNUM_BL}&nItinerario={referencia.nNUM_ITINE}&sReferencia={referencia.sReferencia}");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                Result resultRespuesta = JsonConvert.DeserializeObject<Result>(json);

                Referencias refResult = JsonConvert.DeserializeObject<Referencias>(resultRespuesta.Object.ToString());
                result.Object = refResult;

                foreach (var resultReferencia in resultRespuesta.Objects)
                {
                    Contenedor contenedor = JsonConvert.DeserializeObject<Contenedor>(resultReferencia.ToString());
                    result.Objects.Add(contenedor);
                    result.Correct = true;
                }
                log.LogProceso($"ReferenciasHttp - ObtenerContenedoresPorReferencia()=> Result API: {result.Correct}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ReferenciasHttp - ObtenerContenedoresPorReferencia() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ReferenciasHttp", "ObtenerContenedoresPorReferencia()");
                result.Correct = false;
                return result;
            }
        }

        public MemoryStream MostrarPDFDescarga(int idReferencia)
        {
            try{
                DatosPDF datosPDF = ObtenerDatosPDF(idReferencia).Result;
                CrearPDF cp = new CrearPDF();
                return cp.CrearArchvioPDF(datosPDF);
            }
            catch(Exception ex)
            {
                log.LogProceso($"ReferenciasHttp - MostrarPDFDescarga() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ReferenciasHttp", "MostrarPDFDescarga()");
                return null;
            }
        }

        public async Task<DatosPDF> ObtenerDatosPDF(int idReferencia)
        {

            DatosPDF datosPDF = new DatosPDF();
            datosPDF.liCONTENEDORES = new List<object>();
            try
            {
                string url = string.Format($"{apiURL}/Referencias/ObtenerDatosPDF?nIdReferencia={idReferencia}");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                datosPDF = JsonConvert.DeserializeObject<DatosPDF>(json);

                List<Object> liContenedores = new List<object>();
                foreach (var resultReferencia in datosPDF.liCONTENEDORES)
                {
                    Contenedor contenedorResult = JsonConvert.DeserializeObject<Contenedor>(resultReferencia.ToString());
                    liContenedores.Add(contenedorResult);
                }
                datosPDF.liCONTENEDORES = liContenedores;

                if (datosPDF == null)
                    log.LogProceso($"ReferenciasHttp - ObtenerDatosPDF()=> Result API: False");
                else
                    log.LogProceso($"ReferenciasHttp - ObtenerDatosPDF()=> Result API: True");

                log.LogProceso($"ReferenciasHttp - ObtenerDatosPDF()=> : Se obtuvieron los datos para generar el PDF de la referencia {datosPDF.sNUMERO_REFERENCIA} ");
                return datosPDF;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ReferenciasHttp - ObtenerDatosPDF() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ReferenciasHttp", "ObtenerDatosPDF()");
                return datosPDF;
            }

            //try
            //{
            //    DatosPDF datosPDF = new DatosPDF();
            //    datosPDF.liCONTENEDORES = new List<object>();

            //    string sURL = $"{apiURL}/";
            //    using (var client = new HttpClient())
            //    {
            //        client.BaseAddress = new Uri(sURL);
            //        var responseTask = client.GetAsync($"Referencias/ObtenerDatosPDF?nIdReferencia={idReferencia}");
            //        responseTask.Wait();

            //        var resultTask = responseTask.Result;
            //        if (resultTask.IsSuccessStatusCode)
            //        {
            //            var readTask = resultTask.Content.ReadAsAsync<DatosPDF>();
            //            readTask.Wait();
            //            datosPDF = readTask.Result;
            //            List<Object> liContenedores = new List<object>();
            //            foreach (var resultItem in readTask.Result.liCONTENEDORES)
            //            {
            //                Contenedor contenedores = Newtonsoft.Json.JsonConvert.DeserializeObject<Contenedor>(resultItem.ToString());
            //                liContenedores.Add(contenedores);
            //            }
            //            datosPDF.liCONTENEDORES = liContenedores;
            //        }
            //    }
            //    log.LogProceso($"ReferenciasHttp ------- ObtenerDatosPDF()=> : Se obtuvieron los datos para generar el PDF de la referencia {datosPDF.sNUMERO_REFERENCIA} ");
            //    return datosPDF;
            //}
            //catch (Exception ex)
            //{
            //    log.LogProceso($"ReferenciasHttp ------- ObtenerDatosPDF()=> : Entro al chatch. Exception: {ex.Message}");
            //    log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ReferenciasHttp", "ObtenerDatosPDF()");
            //    return null;
            //}
        }

        public async Task<Result> ValidarAdeudosBL(DatosBusqueda datos)
        {
            Result result = new Result();
            try
            {
                string url = string.Format($"{apiURL}/Referencias/ValidarAdeudosBL?sNumeroBL={datos.sNumBL}&nItinerario={datos.nItinerario}");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                result = JsonConvert.DeserializeObject<Result>(json);

                CamposExtra datosResult = JsonConvert.DeserializeObject<CamposExtra>(result.Object.ToString());
                result.Object = datosResult;
                
                
                log.LogProceso($"ReferenciasHttp - ValidarAdeudosBL()=> Result API: {result.Correct}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ReferenciasHttp - ValidarAdeudosBL() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ReferenciasHttp", "ValidarAdeudosBL()");
                
                return result;
            }

        }

        public async Task<Result> AddDatosBancarios(Datos_Bancarios_Referencia datosBancarios)
        {

            Result result = new Result();
            try
            {
                string url = $"{apiURL}/Referencias/AddDatosBancarios";
                HttpClient client = new HttpClient();
                var data = JsonConvert.SerializeObject(datosBancarios);
                HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var resultRespuesta = await httpResponse.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Result>(resultRespuesta);
                }
                log.LogProceso($"ReferenciasHttp - AddDatosBancarios()=> Result API: {httpResponse.IsSuccessStatusCode}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ReferenciasHttp - AddDatosBancarios() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ReferenciasHttp", "AddDatosBancarios()");
                return result;
            }
        }

        public async Task<Result> GetAllBancos()
        {
            Result result = new Result();
            try
            {
                string url = string.Format($"{apiURL}/Referencias/GetAllBancos");
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);
                Result resultRespuesta = JsonConvert.DeserializeObject<Result>(json);

                foreach (var resultReferencia in resultRespuesta.Objects)
                {
                    Bancos_Datos_Bancarios banco = JsonConvert.DeserializeObject<Bancos_Datos_Bancarios>(resultReferencia.ToString());
                    result.Objects.Add(banco);
                    result.Correct = true;
                }
                log.LogProceso($"ReferenciasHttp - GetAllBancos()=> Result API: {result.Correct}");
                return result;
            }
            catch (Exception ex)
            {
                log.LogProceso($"ReferenciasHttp - GetAllBancos() => : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ReferenciasHttp", "GetAllBancos()");
                result.Correct = false;
                return result;
            }

            //Result result = new Result();
            //try
            //{
            //    using (var client = new HttpClient())
            //    {
            //        string url = "Referencias/GetAllBancos";
            //        client.BaseAddress = new Uri(apiURL + "/");
            //        var responsTask = client.GetAsync(url);
            //        responsTask.Wait();

            //        var resultTask = responsTask.Result;

            //        if (resultTask.IsSuccessStatusCode)
            //        {
            //            var readTask = resultTask.Content.ReadAsAsync<Result>();
            //            readTask.Wait();

            //            foreach(var item in readTask.Result.Objects)
            //            {
            //                Bancos_Datos_Bancarios itemResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Bancos_Datos_Bancarios>(item.ToString());
            //                result.Objects.Add(itemResult);
            //            }
                        
            //            result.Correct = true;
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

    }
}
