using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Presentacion.WSGenerateLegalDocument
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {

#if DEBUG
            WSGenerateLegalDocument wSGenerateLegalDocument = new WSGenerateLegalDocument();
            wSGenerateLegalDocument.EjecutarGenerarDocumentoLegal();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WSGenerateLegalDocument()
            };
            ServiceBase.Run(ServicesToRun);

#endif

        }
    }
}
