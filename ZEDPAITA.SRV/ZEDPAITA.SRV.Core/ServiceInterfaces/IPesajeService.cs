using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Models.ServicioObtenerPesaje;

namespace ZEDPAITA.SRV.Core.ServiceInterfaces
{
    public interface IPesajeService
    {
        RespuestaProceso<ObtenerPesaje> ObtenerPesajeCargaPesada(ObtenerPesajeIn datosPesaje);
    }
}
