﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Presentacion.WSSendInvoiceSunat
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>

        static void Main()
        {
#if DEBUG
            WSSendInvoiceSunat wsSendInvoiceSunat = new WSSendInvoiceSunat();
            wsSendInvoiceSunat.IniciarProcesoSendInvoiceSunat();
#else

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WSSendInvoiceSunat()
            };
            ServiceBase.Run(ServicesToRun);

#endif
        }



    }
}
