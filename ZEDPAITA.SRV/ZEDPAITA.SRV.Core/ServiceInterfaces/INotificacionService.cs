using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Util.Impresion;

namespace ZEDPAITA.SRV.Core.ServiceInterfaces
{
    public interface INotificacionService
    {
        void ProcesarNotificacion(ConfiguracionNotificacion configuracionNotificacion);
    }
}
