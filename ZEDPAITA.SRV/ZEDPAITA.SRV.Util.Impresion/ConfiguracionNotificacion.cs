using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Util.Impresion
{
    public class ConfiguracionNotificacion
    {
        /// <summary>
        /// UbicacionReportes, almacena las rutas de ubicación de los reportes rdlc.
        /// </summary>
        public string UbicacionReportes { get; set; }
        /// <summary>
        /// UbicacionImpresionesPdf, almacena la ruta de ubicación de carpeta para las impresiones PDF.
        /// </summary>
        public string UbicacionImpresionesPdf { get; set; }
        /// <summary>
        /// Configuracion de número registros a obtener
        /// </summary>
        public Int32 configuracionNumeroRegistrosObtener { get; set; }
        /// <summary>
        /// Usuario servicio Notificacion
        /// </summary>
        public string UsuarioServicioNotificacion { get; set; }
    }
}
