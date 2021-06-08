using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Models.Impresor
{
    public class DocumentoImprimirDTO
    {
        /// <summary>
        /// Almacena el número correlativo del código movimiento imprimir
        /// </summary>
        public Int64 CODIGO_MOVIMIENTO_IMPRIMIR { get; set; }
        /// <summary>
        /// Almacena el número correlativo del código movimiento
        /// </summary>
        public Int64 CODIGO_MOVIMIENTO { get; set; }
        /// <summary>
        /// Almacena el código de tipo documento transacción
        /// </summary>
        public Int32 CODIGO_TIPO_DOCUMENTO_TRANSACCION { get; set; }
        /// <summary>
        /// Almacena la serie del documento
        /// </summary>
        public String SERIE_DOCUMENTO { get; set; }
        /// <summary>
        /// Almacena el número de documento
        /// </summary>
        public String NUMERO_DOCUMENTO { get; set; }
        /// <summary>
        /// Almacena el nombre de la impresora
        /// </summary>
        public String NOMBRE_IMPRESORA { get; set; }
        /// <summary>
        /// Almacena el indicador de generar pdf
        /// </summary>
        public int GENERAR_PDF { get; set; }
        /// <summary>
        /// Almacena el indicador de notificar email
        /// </summary>
        public int NOTIFICAR_EMAIL { get; set; }
    }
}
