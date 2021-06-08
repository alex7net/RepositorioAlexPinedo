using System;
using System.Linq;
using System.Text;
using ZEDPAITA.SRV.Core.Models.ServicioEmail;
using ZEDPAITA.SRV.Core.ServiceInterfaces;
using System.Net.Mail;
using System.IO;
using ZEDPAITA.SRV.Util.LoggingInterface;
using ZEDPAITA.SRV.Core.Common;

namespace ZEDPAITA.SRV.Core.Services
{
    public class EmailService : IEmailService
    {

        /// <summary>
        /// Gestiona el log de la clase.
        /// </summary>   
        private readonly ILogger _logger;

        public EmailService(ILogger logger)
        {
            _logger = logger;
        }

        public RespuestaProceso<SendEmail> SendEmail(SendEmailIn request)
        {
            RespuestaProceso<SendEmail> response = new RespuestaProceso<SendEmail>();
            MailMessage Email;
            try
            {
                Email = new MailMessage();
                Email.IsBodyHtml = request.IsBodyHtml;
                Email.From = new MailAddress(request.From);
                Email.Body = request.Message.Replace("<![CDATA[", "").Replace("]]>", ""); ;
                Email.Subject = request.Subject;
                Email.IsBodyHtml = request.IsBodyHtml;
                Email.BodyEncoding = Encoding.UTF8;
                Email.Priority = MailPriority.Normal;

                _logger.WriteMessage(Enumerados.TipoLog.SendEmail, LogLevel.INFO, "De:" + request.From + " A: " + request.To + 
                                     " CC: " + request.Cc + " Asunto: " + request.Subject + " Mensaje: " + request.Message
                                     + " Password: " + request.Pass + " Host:" +  request.Host + " Port: " + request.Port.ToString(), null);

                char caracter = ';';


                string[] Emailsto = request.To.Split(caracter);
                foreach (var emailto in Emailsto)
                {
                    Email.To.Add(emailto.Trim());
                }
                string[] EmailsCC = request.Cc.Split(caracter);
                foreach (var emailCC in EmailsCC)
                {
                    Email.CC.Add(emailCC.Trim());
                }
                if (request.Files.Count() > 0)
                {
                    foreach (var item in request.Files)
                    {
                        if (File.Exists(item.Route))
                        {
                            Email.Attachments.Add(new Attachment(item.Route));
                        }
                    }
                }
                SmtpClient smtpMail = new SmtpClient(request.Host)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = true, 
                    Host = request.Host, 
                    Port = request.Port, 
                    Credentials = new System.Net.NetworkCredential(request.From, request.Pass),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 15000
                };

                smtpMail.Send(Email);
                smtpMail.Dispose();

                response.data = new SendEmail
                {
                    Respuesta = "Envío Correcto de Email"
                };
                response.intCodigoRespuestaProceso = 0;
            }
            catch (Exception ex)
            {
                response.data = new SendEmail();
                response.data.Respuesta = ex.Message;
                response.intCodigoRespuestaProceso = -1;
                _logger.WriteMessage(Enumerados.TipoLog.SendEmail, LogLevel.ERROR, "ERROR al enviar Email", ex);
            }
            return response;
        }
    }
}
