using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Models.Policy;

namespace ZEDPAITA.SRV.Core.RepositoryInterfaces
{
    public interface IParametrosRepository
    {
        List<ParametroSeccionDTO> ConsultaParametroSeccion(Int32 Codigoparametro, string NombreConexion);
        List<ParametroValorDTO> ConsultaParametroSeccionValor(Int32 Codigoparametro, string NombreConexion);
    }
}
