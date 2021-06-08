using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Models.Impresor
{
    public class MovimientoCabceraDTO
    {
        public String RAZON_SOCIAL_EMPRESA_PRINCIPAL { get; set; }
        public String DIRECCION_EMPRESA_PRINCIPAL { get; set; }
        public String RAZON_SOCIAL_USUARIO { get; set; }
        public String TIPO_OPERACION { get; set; }
        public String NUMERO_TICKET { get; set; }
        public Int32 NUMERO_BULTO { get; set; }
        public String CHOFER { get; set; }
        public String PLACA { get; set; }
        public String NUMERO_DOCUMENTO_IDENTIDAD_CHOFER { get; set; }
        public String EMPRESA_TRANSPORTISTA_USUARIO { get; set; }
        public String TIPO_CARGA { get; set; }
        public String NUMERO_PRECINTO { get; set; }
        public String FECHA_INGRESO { get; set; }
        public String FECHA_SALIDA { get; set; }
        public String HORA_UNO { get; set; }
        public String HORA_DOS { get; set; }
        public String NUMERO_CONTENEDOR { get; set; }
        public Int32 PRIMERO_PESADA { get; set; }
        public Int32 SEGUNDA_PESADA { get; set; }
        public Int32 PESO_BRUTO { get; set; }
        public Int32 PESO_TARA { get; set; }
        public Int32 PESO_NETO { get; set; }
        public String CODIGO_INGRESO { get; set; }
    }
}
