using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ZEDPAITA.SRV.Core;
using ZEDPAITA.SRV.Core.Models.ServicioEmail;
using ZEDPAITA.SRV.Core.ServiceInterfaces;
using ZEDPAITA.SRV.Core.Services;
using ZEDPAITA.SRV.Util.Log4Net;
using ZEDPAITA.SRV.Util.LoggingInterface;

namespace ZEDPAITA.SRV.DistributedService.Deploy
{
    public class ServicioEmail : IServicioEmail
    {
        private IEmailService _IEmailService;
        private readonly ILogger _logger;

        public ServicioEmail()
        {
            _logger = new Log4NetLogger();
            _IEmailService = new EmailService(new Log4NetLogger());
        }

        public RespuestaProceso<SendEmail> SendEmail(SendEmailIn request)
        {
            return _IEmailService.SendEmail(request);
        }
    }
}
