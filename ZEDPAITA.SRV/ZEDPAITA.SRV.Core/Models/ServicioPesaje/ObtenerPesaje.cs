using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Models.ServicioObtenerPesaje
{
    [DataContract]
    public class ObtenerPesaje
    {
        [DataMember]
        public Int32 intPeso { get; set; }
    }
}
