using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Proxies.GenerateLegalDocument
{
    public interface IGenerateLegalDocumentProxy
    {
        SendInvoiceGenerateResponse EnviarDocumentoLegalProxy(SendInvoiceGenerateRequest trama, string token, string ruta);
    }
}
