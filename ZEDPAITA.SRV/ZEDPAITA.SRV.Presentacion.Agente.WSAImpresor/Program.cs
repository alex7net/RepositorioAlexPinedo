using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Presentacion.Agente.WSAImpresor
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {

#if DEBUG
            WSAImpresor wsaImpresor = new WSAImpresor();
            wsaImpresor.IniciarProcesoImpresion();

#else


            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WSAImpresor()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
