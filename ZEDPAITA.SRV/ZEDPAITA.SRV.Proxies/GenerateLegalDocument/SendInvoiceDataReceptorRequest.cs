using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Proxies.GenerateLegalDocument
{
    public class SendInvoiceDataReceptorRequest
    {
        public string codigo_tipo_documento_identidad {get; set;}
        public string numero_documento { get; set;}
        public string apellidos_y_nombres_o_razon_social { get; set;}
        public string codigo_pais { get; set;}
        public string ubigeo { get; set;}
        public string direccion { get; set;}
        public string correo_electronico { get; set;}
        public string telefono { get; set;}
    }
}
