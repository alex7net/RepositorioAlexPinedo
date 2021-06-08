using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Models.ServicioEmail;

namespace ZEDPAITA.SRV.Core.ServiceInterfaces
{
    public interface IEmailService
    {
        RespuestaProceso<SendEmail> SendEmail(SendEmailIn request);
    }
}
