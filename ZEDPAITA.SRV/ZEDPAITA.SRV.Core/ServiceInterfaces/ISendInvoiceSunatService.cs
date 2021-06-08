using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.ServiceInterfaces
{
    public interface ISendInvoiceSunatService
    {
        void ProcesarEnvioCompranteElectronicoSunat(Int32 configuracionNumeroRegistrosObtener);
    }
}
