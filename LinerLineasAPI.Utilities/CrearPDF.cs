using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using LinerLineas.Entities.Tablas;
using LinerLineas.Entities.Complementarias;
using LinerLineasAPI.Utilities;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace LinerLineasAPI.Utilities
{
    public class CrearPDF
    {
        RegistroLog log = new RegistroLog();

        private readonly string _rutaImagen = "";

        public CrearPDF()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _rutaImagen = builder.GetSection("RutaImagenPDF:Path_Imagen").Value;
        }

        public MemoryStream CrearArchvioPDF(DatosPDF datos)
        {
            try
            {
                CantidadMoneda cantidad = new CantidadMoneda();
                string paginaHTML_Texto = Properties.Resources.PlantillaPDFReferencia.ToString();

                paginaHTML_Texto = paginaHTML_Texto.Replace("@RutaImagen", _rutaImagen);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@REFERENCIA", datos.sNUMERO_REFERENCIA);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@IMPORTE_PAGO", "$" + cantidad.FormatearCantidad(datos.dMONTO_MXN));

                paginaHTML_Texto = paginaHTML_Texto.Replace("@RFC", datos.sRFC_LINEA);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@RAZON_SOCIAL", datos.sRAZON_SOCIAL_LINEA);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@BANCO", datos.sBANCO);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@CONVENIO", datos.sCONVENIO);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@CLABE_INTERBANCARIA", datos.sCLAVE_INTERBANCARIA);

                paginaHTML_Texto = paginaHTML_Texto.Replace("@NOMBRE_CLIENTE", datos.sNOMBRE_CLIENTE);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@REGISTRO_FEDERAL_CONTRIBUYENTE_CLIENT", datos.sRFC_CLIENTE);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@ESTADO", datos.sESTADO);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@CALLE", datos.sCALLE);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@PAIS", datos.sPAIS);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@COLONIA", datos.sCOLONIA);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@CODIGO_POSTAL", datos.sCODIGO_POSTAL);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@MUNICIPIO", datos.sMUNICIPIO);

                paginaHTML_Texto = paginaHTML_Texto.Replace("@NUMERO_BL", datos.sNUMERO_BL);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@BUQUE", datos.sNOMBRE_BUQUE);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@VIAJE", datos.sNUMERO_VIAJE);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@PUERTO_DESCARGA", datos.sPUERTO_DESCARGA);
                paginaHTML_Texto = paginaHTML_Texto.Replace("@TIPO_CAMBIO","$" + cantidad.FormatearCantidad(datos.dTIPO_CAMBIO));
                paginaHTML_Texto = paginaHTML_Texto.Replace("@TOTAL_MXN", "$" + cantidad.FormatearCantidad(datos.dMONTO_MXN));
                paginaHTML_Texto = paginaHTML_Texto.Replace("@TOTAL_USD", "$" + cantidad.FormatearCantidad(datos.dMONTO_USD));


                //Contenedores
                Result result = new Result();
                Referencias referencia = new Referencias();
                referencia.sNUM_BL = datos.sNUMERO_BL;
                referencia.nNUM_ITINE = datos.nITINERARIO;

                decimal montoPorContenedorUSD = decimal.Round(datos.dMONTO_USD / datos.liCONTENEDORES.Count, 2);
                decimal montoPorContenedorMXN = decimal.Round(montoPorContenedorUSD * datos.dTIPO_CAMBIO, 2);
                

                string columna = "";
                foreach (Contenedor contenedor in datos.liCONTENEDORES)
                {
                    columna += "               <tr>\n" +
                                 "                       <td colspan=\"5\" style='text-align: right; padding: 5px'>" + contenedor.sNOMBRE_CONTENEDOR + "</td>\n" +
                                 "                       <td style='padding: 5px; text-align: right;'></td> \n" +
                                 "                       <td style='padding: 5px; text-align: right;'>$" + cantidad.FormatearCantidad(montoPorContenedorMXN) + "</td> \n" +
                                 "                       <td style='padding: 5px; text-align: right;'>$" + cantidad.FormatearCantidad(montoPorContenedorUSD) + "</td>\n" +
                              "                </tr>\n";
                }

                paginaHTML_Texto = paginaHTML_Texto.Replace("@Contenedores", columna);

                paginaHTML_Texto = paginaHTML_Texto.Replace("@Anio", "©" + DateTime.Now.ToString("yyyy"));

                MemoryStream ms = new MemoryStream();
                Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                pdfDoc.Open();

                //iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance("wwwroot/images/repmar.png");
                ////image1.ScalePercent(50f);
                //image1.ScaleAbsoluteWidth(20);
                //image1.ScaleAbsoluteHeight(20);
                //pdfDoc.Add(image1);

                using (StringReader sr = new StringReader(paginaHTML_Texto))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                }
                pdfDoc.Close();
                byte[] bytesStream = ms.ToArray();
                ms = new MemoryStream();
                ms.Write(bytesStream, 0, bytesStream.Length);
                ms.Position = 0;
                log.LogProceso($"CrearPDF - Utilidades --- CrearArchvioPDF() => Se genero el PDF en mermoria para la referencia {datos.sNUMERO_REFERENCIA}");
                return ms;
            }
            catch (Exception ex)
            {
                log.LogProceso($"CrearArchvioPDF()=> : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "CrearPDF - Utilidades", "CrearArchvioPDF()");
                return null;
            }
        }
    }
}
