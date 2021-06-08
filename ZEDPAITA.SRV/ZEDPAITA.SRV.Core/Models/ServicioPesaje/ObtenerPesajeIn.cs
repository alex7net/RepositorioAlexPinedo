using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Models.ServicioObtenerPesaje
{
    [DataContract]
    public class ObtenerPesajeIn
    {
        [DataMember(Order = 1, Name = "strPuerto")]
        public string PUERTO { get; set; }

        [DataMember(Order = 2, Name = "intVelocidadTransmision")]
        public int VELOCIDAD_TRANSMISION { get; set; }
        [DataMember(Order = 3, Name = "inTiempoEspera")]
        public int TIEMPO_ESPERA { get; set; }
    }
}
