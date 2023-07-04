using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinerLineas.Entities;
using LinerLineas.Entities.Catalogos;
using LinerLineas.Entities.Complementarias;
using LinerLineas.Entities.Tablas;
using LinerLineas.Http;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using LinerLineasAPI.Utilities;
using Microsoft.AspNetCore.Cors;

namespace LinerLineas.Controllers
{
    //[Authorize(Roles = "CUSTOMER, AGENT, GERENTE")]
    public class ReferenciasController : Controller
    {
        ReferenciasHttp http = new ReferenciasHttp();
        RegistroLog log = new RegistroLog();
        private readonly string _pathSavePDF = "";

        public ReferenciasController()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _pathSavePDF = builder.GetSection("RutaPDFReciboDepositos:Path").Value;
        }

        public IActionResult Index()
        {
            //Obtener los bancos para el drop down list. 
            Result result = http.GetAllBancos().Result;
            Datos_Bancarios_Referencia datos = new Datos_Bancarios_Referencia();
            datos.rBancos_Datos_Bancarios = new Bancos_Datos_Bancarios();
            datos.rBancos_Datos_Bancarios.liBancos = result.Objects;

            if (TempData["EstatusDatosBancarios"] != null)
            {
                ViewBag.EstatusDatosGuardados = TempData["EstatusDatosBancarios"].ToString();
            }
            return View(datos);
        }

        public Task<Result> ObtenerReferencias(Referencias referencia)
        {
            return http.ObtenerReferencias(referencia);
        }

        public Task<ReferenciaAgregada> InsertarReferenciaPago(Referencias referencia)
        {
            return http.InsertarReferenciaPago(referencia);
        }

        public Task<Result> ObtenerDatosBL(Referencias referencia)
        {
            return http.ObtenerDatosBL(referencia);
        }

        public Task<int> ComprobarBLValido(BL_I bli)
        {
            return http.ComprobarBLValido(bli);
        }

        public Task<Result> ObtenerConsignatario(DatosBusqueda datos)
        {
            return http.ObtenerConsignatario(datos);
        }

        public Task<int> ObtenerNumeroTotalReferencias(Referencias referencia)
        {
            return http.ObtenerNumeroTotalReferencias(referencia);
        }

        public Task<Result> ObtenerContenedoresPorReferencia(Referencias referencia)
        {
            return http.ObtenerContenedoresPorReferencia(referencia);
        }

        public ActionResult DescargarPDF(int idReferencia)
        {
            MemoryStream ms = http.MostrarPDFDescarga(idReferencia);
            return new FileStreamResult(ms, "application/pdf");
        }

        public Task<Result> ValidarAdeudosBL(DatosBusqueda datos)
        {
            return http.ValidarAdeudosBL(datos);
        }

        public Task<Result> AddDatosBancarios(Datos_Bancarios_Referencia datosBancarios)
        {
            return http.AddDatosBancarios(datosBancarios);
        }

        [HttpPost]
        public async Task<ActionResult> SolicitarDevolucion(IFormFile fileToUpload, Datos_Bancarios_Referencia datosBancarios)
        {
            try
            {
                if (!string.IsNullOrEmpty(datosBancarios.sFSRAZONSOCIAL) && datosBancarios.rBancos_Datos_Bancarios.nFIIDBANCO != 0 && !string.IsNullOrEmpty(datosBancarios.sFSNUMERO_CUENTA) && !string.IsNullOrEmpty(datosBancarios.sFSNUMERO_CLAVE_CUENTA) && !string.IsNullOrEmpty(datosBancarios.sFSEMAIL_CONTACTO))
                {
                    string pathForPDF = _pathSavePDF;

                    if (datosBancarios.sFSNUMERO_CUENTA.Length >= 10)
                    {
                        if (datosBancarios.sFSNUMERO_CLAVE_CUENTA.Length == 18)
                        {
                            if (fileToUpload != null)
                            {
                                datosBancarios.rARCHIVO_RECIBO = new Archivo_Recibo_Deposito_En_Garantia();
                                var extension = fileToUpload.FileName.Split('.');
                                string nameFile = extension[0];
                                datosBancarios.rARCHIVO_RECIBO.sFSEXTENCION = extension[1].ToUpper();
                                string newFileName = nameFile + "_" + datosBancarios.rREFERENCIAS.sReferencia + ".pdf";

                                GuardarArchivo ga = new GuardarArchivo();
                                if (ga.SaveFileToDisk(fileToUpload, pathForPDF, newFileName))
                                {

                                    datosBancarios.rARCHIVO_RECIBO.sFSRUTA = pathForPDF;
                                    datosBancarios.rARCHIVO_RECIBO.sFSNOMBRE = newFileName;//fileToUpload.FileName;
                                    datosBancarios.rARCHIVO_RECIBO.sFSTIPO = fileToUpload.ContentType;
                                    datosBancarios.rARCHIVO_RECIBO.sFSRUTA_COMPLETA_ARCHIVO = Path.Combine(pathForPDF, newFileName);

                                    Result result = new Result();
                                    //if (datosBancarios.nFIIDDATOS_BANCARIOS == 0)
                                    //{
                                    result = await http.AddDatosBancarios(datosBancarios);
                                    //}
                                    //else
                                    //{
                                    //    result = http.UpdateDatosBancarios(datosBancarios).Result;
                                    //}

                                    //TempData["EstatusDatosBancarios"] = result.Correct ? 1 : 2; // Se envio la solicitud con exito  -- No se pudo solicitar la devolución
                                    if (result.Correct)
                                    {
                                        log.LogProceso($"SolicitarDevolucion()=> Se envió la solicitud con exito con los datos bancarios: Razon Social: {datosBancarios.sFSRAZONSOCIAL}, Banco: {datosBancarios.sFSBANCO}, Clave Bancaria: {datosBancarios.sFSNUMERO_CLAVE_CUENTA}, Cuenta Bancaria: {datosBancarios.sFSNUMERO_CUENTA} y el archivo {newFileName} ");
                                        TempData["EstatusDatosBancarios"] = 1; // Se envio la solicitud con exito
                                    }
                                    else
                                    {
                                        log.LogProceso($"SolicitarDevolucion()=> No se pudo enviar la solicitud de devolución a tesorería ");
                                        TempData["EstatusDatosBancarios"] = 2; // No se pudo solicitar la devolución
                                    }
                                }
                                else
                                {

                                    log.LogProceso($"SolicitarDevolucion()=> No se pudo guardar el documento {nameFile} para poder solicitar la devolución");
                                    TempData["EstatusDatosBancarios"] = 3; //Error: No se pudo guardar el documento
                                }

                            }
                            else
                            {

                                log.LogProceso($"SolicitarDevolucion()=> No se pudo leer el archivo {fileToUpload.FileName} cargado en el formulario para poder solicitar la devolución");
                                TempData["EstatusDatosBancarios"] = 4; //No se pudo leer el archivo.
                            }
                        }
                        else
                        {
                            log.LogProceso($"SolicitarDevolucion()=> No se pudo generar la solicitud debido a que la clabe de la cuenta debe de contar con 18 dígitos");
                            TempData["EstatusDatosBancarios"] = 6; //No se pudo leer el archivo.
                        }
                    }
                    else
                    {
                        log.LogProceso($"SolicitarDevolucion()=> No se pudo generar la solicitud debido a que la cuenta debe de contar con 10 dígitos como minimo");
                        TempData["EstatusDatosBancarios"] = 7; //No se pudo leer el archivo.
                    }
                }
                else
                {

                    log.LogProceso($"SolicitarDevolucion()=> : No deben de existir campos vacios en el formulario");
                    TempData["EstatusDatosBancarios"] = 5; //No puede haber campos vacios, intente de nuevo.
                }
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                TempData["EstatusDatosBancarios"] = 0; //Error al guardar la información. 
                log.LogProceso($"SolicitarDevolucion()=> : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "ReferenciasController", "SolicitarDevolucion()");
                return RedirectToAction("Index");
            }
        }
    }
}
