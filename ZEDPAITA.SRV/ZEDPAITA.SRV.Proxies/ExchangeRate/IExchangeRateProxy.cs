using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Proxies.ExchangeRate
{
    public interface IExchangeRateProxy
    {
        ExchangeRateResponse ConsultaExchangeRateProxy(ExchangeRateRequest datos);
    }
}
