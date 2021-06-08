using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Proxies.GenerateLegalDocument
{
    public class SendInvoiceGenerateRequest
    {
        public string serie_documento { get; set; }
        public string numero_documento { get; set; }
        public string fecha_de_emision { get; set; }
        public string hora_de_emision { get; set; }
        public string codigo_tipo_operacion { get; set; }
        public string codigo_tipo_documento { get; set; }
        public string codigo_tipo_moneda { get; set; }
        public string fecha_de_vencimiento { get; set; }
        public string numero_orden_de_compra { get; set; }
        public SendInvoiceDataEmisorRequest datos_del_emisor { get; set; }
        public SendInvoiceDataReceptorRequest datos_del_cliente_o_receptor { get; set; }
        public SendInvoiceTotalesRequest totales { get; set; }
        public List<SendInvoiceItemsRequest> items { get; set; }
        public string informacion_adicional { get; set; }
        public SendInvoiceDataAccionesRequest acciones { get; set; }
    }
}
