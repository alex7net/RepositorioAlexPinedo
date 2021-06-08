using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ZEDPAITA.SRV.Core;
using ZEDPAITA.SRV.Core.Models.ServicioObtenerPesaje;

namespace ZEDPAITA.SRV.DistributedService.Deploy
{
    [ServiceContract]
    public interface IServicioPesaje
    {
        [OperationContract]
        RespuestaProceso<ObtenerPesaje> ObtenerPesajeCargaPesada(ObtenerPesajeIn datosPesaje);
    }
}
