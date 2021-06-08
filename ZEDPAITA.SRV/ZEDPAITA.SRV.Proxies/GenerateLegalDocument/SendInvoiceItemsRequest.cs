using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Proxies.GenerateLegalDocument
{
    public class SendInvoiceItemsRequest
    {
        public string codigo_interno {get; set;}
        public string descripcion { get; set;}
        public string codigo_producto_sunat { get; set;}
        public string codigo_producto_gsl { get; set;}
        public string unidad_de_medida { get; set;}
        public Int32 cantidad { get; set;}
        public decimal valor_unitario { get; set;}
        public string codigo_tipo_precio { get; set;}
        public decimal precio_unitario {get; set;}
        public string codigo_tipo_afectacion_igv { get; set;}
        public decimal total_base_igv { get; set;}
        public decimal porcentaje_igv { get; set;}
        public decimal total_igv { get; set;}
        public decimal total_impuestos { get; set;}
        public decimal total_valor_item { get; set;}
        public decimal total_item { get; set;}
    }
}