using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Common;
using ZEDPAITA.SRV.Core.Models.Base;
using ZEDPAITA.SRV.Core.Models.Policy;
using ZEDPAITA.SRV.Core.RepositoryInterfaces;
using ZEDPAITA.SRV.Core.ServiceInterfaces;

namespace ZEDPAITA.SRV.Core.Services
{
    public class ParametroService : IParametroService
    {
        private readonly IParametrosRepository _parametroRepository;

        public ParametroService(IParametrosRepository _IParametrosRepository)
        {
            _parametroRepository = _IParametrosRepository;
        }

        public InformacionDinamicaModel ConsultaParametroSeccionValor(Int32 CodigoParametro, string Conexion)
        {
            InformacionDinamicaModel Dinamica = new InformacionDinamicaModel();

            try
            {
                var parametros = _parametroRepository.ConsultaParametroSeccion(CodigoParametro, Conexion);
                var columnas = parametros.Select(t => new Definicion { Campo = t.CodigoSeccion.ToString(), Nombre = t.NombreParametroSeccion }).ToList();
                var listaParametroSeccionValor = _parametroRepository.ConsultaParametroSeccionValor(CodigoParametro, Conexion);
                Dinamica.Definiciones = columnas;
                int valorQuiebre = default(int);
                Dictionary<string, object> itemDictironary = null;
                foreach (var itemGrid in listaParametroSeccionValor)
                {
                    if (itemGrid.CodigoValor != valorQuiebre)
                    {
                        itemDictironary = new Dictionary<string, object>();
                        itemDictironary.Add(Entity.GetPropertyName<ParametroValorDTO>(a => a.CodigoValor).First(), itemGrid.CodigoValor);
                        Dinamica.Datos.Add(itemDictironary);
                        valorQuiebre = itemGrid.CodigoValor;
                        Dinamica.PermisoAgregar = itemGrid.PermiteAgregarValor;
                        Dinamica.PermisoModificar = itemGrid.PermiteModificarValor;
                    }
                    itemDictironary.Add(itemGrid.CodigoSeccion.ToString(), itemGrid.ValorParametroValor);
                }
            }
            catch (Exception ex)
            {

            }

            return Dinamica;
        }

    }
}
