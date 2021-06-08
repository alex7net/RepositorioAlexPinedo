using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Proxy.SendInvoiceSunat
{
    public interface ISendInvoiceSunatProxy
    {
        void EnviarCodigoComprobanteElectronico(string token, string ruta, string codigo);
    }
}
