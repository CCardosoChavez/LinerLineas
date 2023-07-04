using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using LinerLineas.Entities.Complementarias;
using LinerLineas.Entities.Tablas;

namespace LinerLineasAPI.Utilities
{
    public class GenerarReporteExcel
    {
        RegistroLog log = new RegistroLog();
        public byte[] CrearExcel(Result result){
            byte[] streamReturn;
            try
            {
                using (DataTable dt = new DataTable())
                {
                    int idConsulta = Convert.ToInt32(result.Object);
                    //defines las columnas del datatable
                    dt.Columns.Add("Referencia");
                    dt.Columns.Add("Razón Social");
                    dt.Columns.Add("Banco");
                    dt.Columns.Add("Cuenta Bancaria");
                    dt.Columns.Add("Clave Bancaria");
                    dt.Columns.Add("Consignatarío");
                    dt.Columns.Add("Fecha de Solicitud");
                    dt.Columns.Add("Monto MNX");
                    dt.Columns.Add("Estatus de Devolución");
                    dt.Columns.Add("Solicitud Valida");
                    dt.Columns.Add("Email de Contacto");

                    if (idConsulta != 2 && idConsulta != 3)
                        dt.Columns.Add("Observaciones");
                  
                    foreach (Datos_Bancarios_Referencia itemDatos in result.Objects)
                    {
                        //creas una nueva row
                        DataRow row = dt.NewRow();
                        //asignas el dato a cada columna de la row
                        row["Referencia"] = itemDatos.rREFERENCIAS.sReferencia;
                        row["Razón Social"] = itemDatos.sFSRAZONSOCIAL;
                        row["Banco"] = itemDatos.sFSBANCO;
                        row["Cuenta Bancaria"] = itemDatos.sFSNUMERO_CUENTA;
                        row["Clave Bancaria"] = itemDatos.sFSNUMERO_CLAVE_CUENTA;
                        row["Consignatarío"] = itemDatos.rREFERENCIAS_TESORERIA.sNombreConsignatario;
                        row["Fecha de Solicitud"] = itemDatos.daFDAFECHA_SOLICITUD;
                        row["Monto MNX"] = itemDatos.rREFERENCIAS.dMontoMXN;
                        
                        //Para el estatus de devolución
                        if (itemDatos.rREFERENCIAS_TESORERIA.sEstatusDevolucionSaldo == 0)
                            row["Estatus de Devolución"] = "Sin estatus";
                        else if (itemDatos.rREFERENCIAS_TESORERIA.sEstatusDevolucionSaldo == 1)
                            row["Estatus de Devolución"] = "En proceso";
                        else
                            row["Estatus de Devolución"] = "Devuelto";
                        row["Solicitud Valida"] = itemDatos.rREFERENCIAS_TESORERIA.sEstatusSolicitudDatosBancarios;

                        if ( idConsulta != 2 && idConsulta != 3)
                            row["Observaciones"] = itemDatos.sFSOBSERVACIONES;

                        row["Email de Contacto"] = itemDatos.sFSEMAIL_CONTACTO;

                        dt.Rows.Add(row);
                    }

                    dt.TableName = "ReferenciasDatosBancarios";

                    using (XLWorkbook libro = new XLWorkbook())
                    {
                        var sheet = libro.Worksheets.Add(dt);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            libro.SaveAs(ms);
                            streamReturn = ms.ToArray();
                        }
                    }
                }
                return streamReturn;
            }
            catch (Exception ex)
            {
                log.LogProceso($"GenerarReporteExcel------- CrearExcel()=> : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "GenerarReporteExcel", "CrearExcel()");
                return null;
            }
            
        }
    }
}
