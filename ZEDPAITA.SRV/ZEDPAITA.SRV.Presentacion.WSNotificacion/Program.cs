using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Presentacion.WSNotificacion
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {

#if DEBUG
            WSNotificacion wsNotificacion = new WSNotificacion();
            wsNotificacion.IniciarProcesoNotificacion();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WSNotificacion()
            };
            ServiceBase.Run(ServicesToRun);

#endif
        }
    }
}
