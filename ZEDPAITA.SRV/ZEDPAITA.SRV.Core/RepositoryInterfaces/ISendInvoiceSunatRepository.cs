using System;
using System.Collections.Generic;
using ZEDPAITA.SRV.Core.Models.EnvioFacturaSunat;

namespace ZEDPAITA.SRV.Core.RepositoryInterfaces
{
    public interface ISendInvoiceSunatRepository
    {
        List<EnvioFacturaSunatDTO> obtenerCodigoEnviar(Int32 Dias, Int32 Limite, string Conexion);
    }
}
