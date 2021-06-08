using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ZEDPAITA.SRV.Core
{
    [DataContract]
    public class RespuestaProceso<T> where T : class
    {
        [DataMember(Order = 1)]
        public int intCodigoRespuestaProceso { get; set; }
        [DataMember(Order = 2)]
        public string strMessage { get; set; }
        [DataMember(Order = 3)]
        public T data { get; set; }
    }
}
