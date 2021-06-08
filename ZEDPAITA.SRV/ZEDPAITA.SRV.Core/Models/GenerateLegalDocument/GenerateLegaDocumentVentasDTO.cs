using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Models.GenerateLegalDocument
{
    public class GenerateLegaDocumentVentasDTO
    {
        public String Correo { get; set; }
        public DateTime FechaDocumento { get; set; }
        public String TipoComprobante { get; set; }
        public String Serie { get; set; }
        public String NumeroDocumento { get; set; }
        public String RucCliente { get; set; }
        public String TipoConcepto { get; set; }
        public String Moneda { get; set; }
        public Double SubDolar { get; set; }
        public Double IgvDolar { get; set; }
        public Double ImporteDolar { get; set; }
        public Double TipoCambio { get; set; }
        public Double SubTotal { get; set; }
        public Double Igv { get; set; }
        public Double TotalFactura { get; set; }
        public String Condicion { get; set; }
        public String FPrecepcion { get; set; }
        public DateTime FechaPago { get; set; }
        public String Glosa { get; set; }
        public String RazonSocial { get; set; }
        public String DireccionCliente { get; set; }
        public String Concepto { get; set; }
        public Int32 NroTarifario { get; set; }
        public Int32 FormaPago_Id { get; set; }
        public DateTime Fecha_Vencimiento { get; set; }
        public String Gracia { get; set; }
        public Int32 Estado_Vencimiento { get; set; }
        public Int32 Dias_Vencidos { get; set; }
        public Decimal Monto_Capital { get; set; }
        public Decimal Monto_Mora { get; set; }
        public Decimal Monto_Adeuda { get; set; }
    }
}
