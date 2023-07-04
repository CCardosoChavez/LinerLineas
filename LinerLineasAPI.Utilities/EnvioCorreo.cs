using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace LinerLineasAPI.Utilities
{
    public class EnvioCorreo
    {
        RegistroLog log = new RegistroLog();
        private readonly string _correo = "";
        private readonly string _password = "";
        private readonly string _servidor = "";
        private readonly string _puerto = "";
        private readonly string _time = "";

        public EnvioCorreo()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _correo = builder.GetSection("DatosCorreo:Correo").Value;
            _password = builder.GetSection("DatosCorreo:Password").Value;
            _servidor = builder.GetSection("DatosCorreo:Servidor").Value;
            _puerto = builder.GetSection("DatosCorreo:Puerto").Value;
            _time = builder.GetSection("DatosCorreo:TimeOut").Value;
        }

        public void EnviarCorreo(string mensaje, string asunto, string sCorreoDestino = "", string sNombreArchivo = "", MemoryStream ms = null)
        {
            Attachment at = null;
            try
            {
                string correoSalida = _correo; //"donotreply@maritimex.com.mx"; // ConfigurationManager.AppSettings["CorreoSalida"].ToString();
                string pwdCorreoSalida = _password;//"donotreply"; // ConfigurationManager.AppSettings["PwdCorreoSalida"].ToString();
                string servidorCorreo = _servidor;//"smtp.gmail.com"; // ConfigurationManager.AppSettings["ServidorCorreo"].ToString();
                string puertoSalidaCorreo = _puerto;//"587"; // ConfigurationManager.AppSettings["PuertoSalidaCorreo"].ToString();
                string timeout = _time;//"20000"; // ConfigurationManager.AppSettings["Timeout"].ToString();

                string correoDestino = sCorreoDestino;
                string asuntoCorreo = asunto;
                string cuerpoCorreo = mensaje;

                //Se genera un objeto de tipo correo
                MailMessage correo = new MailMessage();
                correo.To.Add(new MailAddress(correoDestino));
                correo.From = new MailAddress(correoSalida);
                correo.Subject = asuntoCorreo;
                correo.Body = cuerpoCorreo;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                if (ms != null)
                {
                    ms.Position = 0;
                    at = new Attachment(ms, sNombreArchivo, "application/pdf");
                    //at = new Attachment(ms, new System.Net.Mime.ContentType("application/pdf"));
                    correo.Attachments.Add(at);
                }

                
                SmtpClient smtp = new SmtpClient();
                smtp.Host = servidorCorreo;
                smtp.Port = int.Parse(puertoSalidaCorreo);
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(correoSalida, pwdCorreoSalida);
                smtp.Timeout = int.Parse(timeout);
                //smtp.Send(correoSalida, correoDestino, asuntoCorreo, cuerpoCorreo);
                smtp.Send(correo);

                log.LogProceso($"EnvioCorreo - Utilidades ------- EnviarCorreo()=> : Se envío el correo para {sCorreoDestino} con el asunto {asunto} y el archvio {sNombreArchivo}");
            }
            catch (Exception ex)
            {
                log.LogProceso($"EnvioCorreo - Utilidades ------- EnviarCorreo()=> : Entro al chatch. Exception: {ex.Message}");
                log.LogError($"{ex.Message} || {ex.Source} || {ex.StackTrace}", "EnvioCorreo - Utilidades", "EnviarCorreo()");
            }
        }
    }
}
