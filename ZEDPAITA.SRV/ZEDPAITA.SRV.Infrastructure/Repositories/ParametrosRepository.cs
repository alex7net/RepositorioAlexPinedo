using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Common;
using ZEDPAITA.SRV.Core.Models.Policy;
using ZEDPAITA.SRV.Core.RepositoryInterfaces;
using ZEDPAITA.SRV.Infrastructure.Base;
using ZEDPAITA.SRV.Infrastructure.DataAccess;

namespace ZEDPAITA.SRV.Infrastructure.Repositories
{ 
    public class ParametrosRepository : IParametrosRepository
    {
        public List<ParametroSeccionDTO> ConsultaParametroSeccion(Int32 Codigoparametro, string NombreConexion)
        {
            List<ParametroSeccionDTO> listaSeccion = new List<ParametroSeccionDTO>();
            try
            {
                BaseDatos baseDatos = new BaseDatos(NombreConexion);

                using (DataTable datos = baseDatos.EjecutarConsultaDataTable("USP_POL_PARAMETRO_SECCION_CODIGO_SEL", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("CODIGO_PARAMETRO", MySqlDbType.Int32, Codigoparametro)))
                {

                    listaSeccion = DataMapper.DatatableToList<ParametroSeccionDTO>(datos).ToList();
                }
            }
            catch (MySqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return listaSeccion;
        }


        public List<ParametroValorDTO> ConsultaParametroSeccionValor(Int32 Codigoparametro, string NombreConexion)
        {
            List<ParametroValorDTO> listaValor = new List<ParametroValorDTO>();
            try
            {
                BaseDatos baseDatos = new BaseDatos(NombreConexion);

                using (DataTable datos = baseDatos.EjecutarConsultaDataTable("USP_POL_PARAMETRO_SECCION_VALOR_SEL", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("CODIGO_PARAMETRO", MySqlDbType.Int32, Codigoparametro),
                    baseDatos.CrearParametro("ESTADO_ANULADO", MySqlDbType.Int32, Enumerados.EstadoAnulado.NoAnulado)))
                {

                    listaValor = DataMapper.DatatableToList<ParametroValorDTO>(datos).ToList();
                }
            }
            catch (MySqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return listaValor;
        }


    }
}
