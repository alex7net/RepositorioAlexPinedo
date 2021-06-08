using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Models.Impresor
{
    public class DocumentoNotificacionDTO
    {
        public Int64 CODIGO_MOVIMIENTO_NOTIFICACION { get; set; }
        public Int64 CODIGO_MOVIMIENTO { get; set; }
        public Int32 CODIGO_CERTIFICADO_MANUFACTURA { get; set; }
        public String RLDC { get; set; }
        public String TRAMA { get; set; }
        public String GENERAR_PDF { get; set; }
        public String TRAMA_EMAIL { get; set; }
        public String ENVIAR_EMAIL { get; set; }
        public String CODIGO_INGRESO { get; set; }
        public String CARPETA { get; set; }
        public Int64 CODIGO_TIPO_CONTROL { get; set; }
    }
}
