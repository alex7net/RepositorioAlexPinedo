using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Models.Impresor
{
    public class MovimientoDetalleDTO
    {
        public String DESCRIPCION { get; set; }
        public Int64 NUMERO_PARTIDA { get; set; }
        public String UNIDAD_MEDIDA { get; set; }
        public Int32 CANTIDAD { get; set; }
    }
}
