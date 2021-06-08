using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Models.GenerateLegalDocument
{
    public class GenerateLegalDocumentDetalleDTO
    {
        public String TipoComprobante_Id { get; set; }
        public String Serie_Id { get; set; }
        public String NumeroDocumento { get; set; }
        public Int32 Cantidad { get; set; }
        public String Medida { get; set; }
        public String Concepto { get; set; }
        public String Moneda { get; set; }
        public Double PrecioUnitario { get; set; }
        public Double Importe { get; set; }
    }
}
