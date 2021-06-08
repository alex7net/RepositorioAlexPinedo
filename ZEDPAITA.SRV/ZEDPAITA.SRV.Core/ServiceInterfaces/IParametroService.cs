using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Models.Policy;

namespace ZEDPAITA.SRV.Core.ServiceInterfaces
{
    public interface IParametroService
    {
        InformacionDinamicaModel ConsultaParametroSeccionValor(Int32 CodigoParametro, string Conexion);
    }
}
