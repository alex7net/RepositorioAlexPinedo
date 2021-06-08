using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ZEDPAITA.SRV.Core;
using ZEDPAITA.SRV.Core.Models.ServicioEmail;

namespace ZEDPAITA.SRV.DistributedService.Deploy
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicioEmail" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioEmail
    {
        [OperationContract]
        RespuestaProceso<SendEmail> SendEmail(SendEmailIn request);
    }
}
