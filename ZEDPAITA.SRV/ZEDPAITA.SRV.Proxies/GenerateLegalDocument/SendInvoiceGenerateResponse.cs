using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Proxies.GenerateLegalDocument
{
    public class SendInvoiceGenerateResponse
    {
        public string PDF { get; set; }
        public string FACTURA { get; set; }
        public string JSON { get; set; }
        public string NUMERO_FACTURA { get; set; }
        public Int32 NUMERO_FACURA_SERIE { get; set; }
    }
}
