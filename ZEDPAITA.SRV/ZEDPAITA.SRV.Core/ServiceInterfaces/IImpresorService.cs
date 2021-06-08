using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Models.Impresor;

namespace ZEDPAITA.SRV.Core.ServiceInterfaces
{
    public interface IImpresorService
    {
        /// <summary>
        /// Realiza el proceso de impresión.
        /// </summary>
        /// <param name="configuracionImpresor">Configuración de impresión</param>
        void ProcesarImpresion(ConfiguracionImpresor configuracionImpresor);
    }
}
