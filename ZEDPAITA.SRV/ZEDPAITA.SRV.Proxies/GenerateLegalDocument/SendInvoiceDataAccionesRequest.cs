using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Proxies.GenerateLegalDocument
{
    public class SendInvoiceDataAccionesRequest
    {
        public bool enviar_email { get; set; }
        public bool enviar_xml_firmado { get; set; }
    }
}