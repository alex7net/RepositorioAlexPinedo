using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Models.CarpetaServidor
{
    public class CarpetaServidorDTO
    {
        public Int32 CODIGO_CARPETA_SERVIDOR { get; set; }
        public string CODIGO_CARPETA { get; set; }
        public string DESCRIPCION_CODIGO_CARPETA { get; set; }
        public string RUTA_COMPLETA { get; set; }
        public string RUTA_HOST { get; set; }
        public string USUARIO_CONEXION { get; set; }
        public string CONTRASENA_CONEXION { get; set; }
    }
}
