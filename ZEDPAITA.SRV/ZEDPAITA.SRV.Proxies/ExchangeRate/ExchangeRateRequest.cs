using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Proxies.ExchangeRate
{
    public class ExchangeRateRequest
    {
        public string FechaConsulta { get; set; }
        public string Ruta { get; set; }
        public string Token { get; set; }
    }
}
