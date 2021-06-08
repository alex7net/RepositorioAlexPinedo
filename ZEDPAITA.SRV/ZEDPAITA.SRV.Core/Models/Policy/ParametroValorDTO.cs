using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Models.Policy
{
    public class ParametroValorDTO
    {
        public Int32 CodigoSeccion { get; set; }
        public Int32 CodigoValor { get; set; }
        public String NombreParametroSeccion { get; set; }
        public String ValorParametroValor { get; set; }
        public bool PermiteAgregarValor { get; set; }
        public bool PermiteModificarValor { get; set; }
        public String Valor { get; set; }
    }
}
