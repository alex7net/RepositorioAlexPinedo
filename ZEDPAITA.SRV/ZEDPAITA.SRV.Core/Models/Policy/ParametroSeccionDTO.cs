using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Models.Policy
{
    public class ParametroSeccionDTO
    {
        public Int32 CodigoSeccion { get; set; }
        public Int32 CodigoParametro { get; set; }
        public String NombreParametroSeccion { get; set; }
        public bool PermiteAgregarValor { get; set; }
        public bool PermiteModificarValor { get; set; }
    }
}
