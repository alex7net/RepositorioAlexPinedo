using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Proxies.SendInvoiceSunat
{
    public class SendinvoiceRequest
    {
        [JsonPropertyName("external_id")]
        public String external_id { get; set; }
    }
}
