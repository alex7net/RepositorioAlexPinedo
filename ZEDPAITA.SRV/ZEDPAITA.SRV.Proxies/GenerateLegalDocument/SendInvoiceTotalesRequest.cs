using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Proxies.GenerateLegalDocument
{
    public class SendInvoiceTotalesRequest
    {
        public decimal total_exportacion {get; set;}
        public decimal total_operaciones_gravadas { get; set;}
        public decimal total_operaciones_inafectas { get; set;}
        public decimal total_operaciones_exoneradas { get; set;}
        public decimal total_operaciones_gratuitas { get; set;}
        public decimal total_igv { get; set;}
        public decimal total_impuestos { get; set;}
        public decimal total_valor { get; set;}
        public decimal total_venta { get; set;}
    }
}

