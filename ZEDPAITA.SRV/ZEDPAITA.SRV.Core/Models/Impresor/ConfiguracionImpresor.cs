using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Models.Impresor
{
    public class ConfiguracionImpresor
    {
        /// <summary>
        /// ImpresorasLocales, almacena la lista de nombres de impresoras locales.
        /// </summary>
        public List<string> ImpresorasLocales { get; set; }
        /// <summary>
        /// NumeroDocumentosObtener, almacena el número de documentos que se obtendran de la base datos remoto.
        /// </summary>
        public int NumeroDocumentosObtener { get; set; }
        /// <summary>
        /// NombreAgente, almacena el nombre del agente
        /// </summary>
        public String NombreAgente { get; set; }
        /// <summary>
        /// Nombre del hostname del agente
        /// </summary>
        public String HostName { get; set; }
    }
}
