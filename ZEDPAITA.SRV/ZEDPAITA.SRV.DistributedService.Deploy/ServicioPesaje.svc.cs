using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ZEDPAITA.SRV.Core;
using ZEDPAITA.SRV.Core.Models.ServicioObtenerPesaje;
using ZEDPAITA.SRV.Core.ServiceInterfaces;
using ZEDPAITA.SRV.Core.Services;

namespace ZEDPAITA.SRV.DistributedService.Deploy
{

    public class ServicioPesaje : IServicioPesaje
    {
        private IPesajeService _IPesajeService;

        public ServicioPesaje()
        {
            _IPesajeService = new PesajeService();
        }

        public RespuestaProceso<ObtenerPesaje> ObtenerPesajeCargaPesada(ObtenerPesajeIn datosPesaje)
        {
            return _IPesajeService.ObtenerPesajeCargaPesada(datosPesaje);
        }
    }
}
