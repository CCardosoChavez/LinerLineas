using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinerLineas.Entities.Tablas;
using LinerLineas.Entities.Complementarias;
using LinerLineas.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using LinerLineasAPI.Utilities;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using ClosedXML.Excel;

namespace LinerLineas.Controllers
{
    public class TesoreriaController : Controller
    {
        TesoreriaHttp http = new TesoreriaHttp();
        RegistroLog log = new RegistroLog();
        private readonly string _pathCaratulaBancaria = "";
        private readonly string _EmailSoporte;

        public TesoreriaController()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _pathCaratulaBancaria = builder.GetSection("RutaPDFReciboDepositos:Path_Mostrar_Caratula").Value;
            _EmailSoporte = builder.GetSection("CorreoSoporte:Email").Value;
        }
        public IActionResult Index()
        {
            ViewBag.PathCaratulaBancaria = _pathCaratulaBancaria;
            return View();
        }

        public Task<Result> GetReferenciasDatosBancarios(DatosBusqueda datos)
        {
            return http.GetReferenciasDatosBancarios(datos);
        }

        public Task<Result> UpdateEstatusDatosBancarios(Datos_Bancarios_Referencia datosBancarios)
        {
            return http.UpdateEstatusDatosBancarios(datosBancarios);
        }

        public Task<int> GetTotalReferenciasDatosBancarios()
        {
            return http.GetTotalReferenciasDatosBancarios();
        }

        public FileResult DownloadReporte(int opcionReferencias, DateTime daFechaDesde, DateTime daFechaHasta)
        {
            byte[] streamReturn;
            try
            {
                
                DatosBusqueda datos = new DatosBusqueda();
                datos.nIdTipoConsultaTesoseria = opcionReferencias;
                datos.rPAGINACION = new Paginacion();
                datos.sFechaInicial_Desde = daFechaDesde == Convert.ToDateTime("01/01/0001") ? null : daFechaDesde.ToString();
                datos.sFechaFinal_Hasta = daFechaHasta == Convert.ToDateTime("01/01/0001") ? null : daFechaHasta.ToString();

                Result result = http.GetReferenciasDatosBancariosDescarga(datos).Result;
                result.Object = opcionReferencias;
                GenerarReporteExcel re = new GenerarReporteExcel();
                streamReturn = re.CrearExcel(result);

                log.LogProceso($"TesoreriaController ------- DownloadReporte()=> Se generó el reporte Excel de los registros de datos bancarios con el ID Opcion de Busqueda: {opcionReferencias} con la fecha inicial {datos.sFechaInicial_Desde} y la fecha final {datos.sFechaFinal_Hasta}");
                return File(streamReturn,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Reporte_Referencias_"+DateTime.Now.ToString("dd-MM-yyyy")+".xlsx");

            }
            catch(Exception ex)
            {
                log.LogProceso($"TesoreriaController ------- DownloadReporte()=> : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "TesoreriaController", "DownloadReporte()");
                return null;
            }
        }


        public ActionResult SentEmail(Datos_Bancarios_Referencia datos)
        {
            try
            {
                string msg = string.Empty;

                msg = "Estimad@ cliente,<br/><br/>";
                msg += "Se le notifica que los datos bancarios que agrego a la referencia <strong> " + datos.rREFERENCIAS.sReferencia + " </strong>  del BL:  " + datos.rREFERENCIAS.sNUM_BL + " fueron rechazados " +
                    "debido a las siguientes observaciones: <strong>" + datos.sFSOBSERVACIONES + "</strong>. Favor de volver a capturar los datos y cargar la caratula bancaria para enviar nuevamente la solicitud de devolución de Depósito en Garantía. <br/>";
                msg += "<div style='width: 400px;'><table> <tbody>";
                msg += "<tr><td style='text-align: left;'><b>No. Referencia: </b></td>";
                msg += $"<td style='text-align: right;'>{datos.rREFERENCIAS.sReferencia}</td></tr>";

                //msg += "<tr><td style='text-align: left;'><b>Monto:</b></td>";
                //msg += $"<td style='text-align: right;'>${datos.rREFERENCIAS.dMontoUSD} dlls</td></tr>";

                msg += "<tr><td style='text-align: left;'><b>Razón Social / Nombre del titular:</b></td>";
                msg += $"<td style='text-align: right;'>{datos.sFSRAZONSOCIAL}</td></tr>";

                msg += "<tr><td style='text-align: left;'><b>Banco:</b></td>";
                msg += $"<td style='text-align: right;'>{datos.sFSBANCO}</td></tr>";

                msg += "<tr><td style='text-align: left;'><b>Número de Cuenta:</b></td>";
                msg += $"<td style='text-align: right;'>{datos.sFSNUMERO_CUENTA}</td></tr>";

                msg += "<tr><td style='text-align: left;'><b>Clabe Interbancaria:</b></td>";
                msg += $"<td style='text-align: right;'>{datos.sFSNUMERO_CLAVE_CUENTA}</td></tr>";

                msg += "</tbody></table></div>";

                msg += "</br>Cualquier duda o comentario favor de enviar un mensaje a la dirección de correo " + _EmailSoporte + " <br/><br/>";
                msg += "<i>Este es un mensaje automático generado por nuestro sistema. Por favor no responda este correo.</i>";

                EnvioCorreo ec = new EnvioCorreo();
                ec.EnviarCorreo(msg, "Datos bancarios para solicitud de depósito en garantía incorrectos", datos.rREFERENCIAS_TESORERIA.sCorreoUsuario);

                Result result = new Result();
                result.Correct = true;
                
                log.LogProceso($"TesoreriaController ------- SentEmail()=> Se envió el correo para {datos.rREFERENCIAS_TESORERIA.sCorreoUsuario} con el asunto: Datos bancarios para solicitud de depósito en garantía incorrectos ");
                return Ok(true);
            }
            catch (Exception ex)
            {
                log.LogProceso($"TesoreriaController ------- SentEmail()=> : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "TesoreriaController", "SentEmail()");
                return BadRequest(false);
            }
        }
    }
}

