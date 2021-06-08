using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Models.Base;

namespace ZEDPAITA.SRV.Core.Models.Policy
{
    public class InformacionDinamicaModel
    {
        /// <summary>
        /// Constructor por defecto de implementación de la clase
        /// </summary>
        public InformacionDinamicaModel()
        {
            this.Definiciones = new List<Definicion>();
            this.Datos = new List<Dictionary<string, object>>();
            this.PermisoAgregar = new bool();
            this.PermisoModificar = new bool();
            this.IndicadorVisibleAgregar = new bool();
        }
        /// <summary>
        /// Definiciones de campos de los datos
        /// </summary>
        public List<Definicion> Definiciones { get; set; }
        /// <summary>
        /// Listados de datos
        /// </summary>
        public List<Dictionary<string, object>> Datos { get; set; }
        /// <summary>
        /// Indicador de Permiso de agregar
        /// </summary>
        public bool PermisoAgregar { get; set; }
        /// <summary>
        /// Indicador de Permiso de modificar
        /// </summary>
        public bool PermisoModificar { get; set; }
        /// <summary>
        /// Indicador de Visible
        /// </summary>
        public bool IndicadorVisibleAgregar { get; set; }
    }
}
