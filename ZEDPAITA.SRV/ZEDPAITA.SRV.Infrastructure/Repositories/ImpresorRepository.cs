using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Models.Impresor;
using ZEDPAITA.SRV.Core.RepositoryInterfaces;
using ZEDPAITA.SRV.Infrastructure.Base;
using ZEDPAITA.SRV.Infrastructure.DataAccess;

namespace ZEDPAITA.SRV.Infrastructure.Repositories
{
    public class ImpresorRepository : IImpresorRepository
    {
        /// <summary>
        /// Nombre de cadena de conexión local.
        /// </summary>
        private readonly string nombreCadenaConexionERP = "CadenaConexionERP";

        public List<DocumentoImprimirDTO> obtenerDocumentos(String Agente, Int32 Filas)
        {
            List<DocumentoImprimirDTO> listaDocumentos = new List<DocumentoImprimirDTO>();
            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexionERP);

                using (DataTable datos = baseDatos.EjecutarConsultaDataTable("USP_OPR_MOVIMIENTO_OBTENER_IMPRIMIR_AGENTE", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("AGENTE", MySqlDbType.VarChar, Agente),
                    baseDatos.CrearParametro("FILAS", MySqlDbType.Int32, Filas)))
                {

                    listaDocumentos = DataMapper.DatatableToList<DocumentoImprimirDTO>(datos).ToList();
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

            return listaDocumentos;
        }

        public MovimientoCabceraDTO obtenerMovimientoCabecera(Int64 CodigoMovimiento)
        {
            MovimientoCabceraDTO Documento = new MovimientoCabceraDTO();
            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexionERP);

                using (DataTable datos = baseDatos.EjecutarConsultaDataTable("USP_OPR_MOVIMIENTO_CABECERA_IMPRIMIR_AGENTE", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("CODIGO_MOVIMIENTO", MySqlDbType.Int64, CodigoMovimiento)))
                {
                   Documento = DataMapper.DatatableToList<MovimientoCabceraDTO>(datos)[0];
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

            return Documento;
        }

        public List<MovimientoDetalleDTO> obtenerMovimientoDetalle(Int64 CodigoMovimiento)
        {
            List<MovimientoDetalleDTO> listaDetalle = new List<MovimientoDetalleDTO>();
            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexionERP);

                using (DataTable datos = baseDatos.EjecutarConsultaDataTable("USP_OPR_MOVIMIENTO_DETALLE_IMPRIMIR_AGENTE", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("CODIGO_MOVIMIENTO", MySqlDbType.Int64, CodigoMovimiento)))
                {

                    listaDetalle = DataMapper.DatatableToList<MovimientoDetalleDTO>(datos).ToList();
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
            return listaDetalle;
        }

        public List<MovimientoDocumentoDTO> obtenerMovimientoDocumento(Int64 CodigoMovimiento)
        {
            List<MovimientoDocumentoDTO> listaDocumento = new List<MovimientoDocumentoDTO>();
            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexionERP);

                using (DataTable datos = baseDatos.EjecutarConsultaDataTable("USP_OPR_MOVIMIENTO_DOCUMENTO_IMPRIMIR_AGENTE", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("CODIGO_MOVIMIENTO", MySqlDbType.Int64, CodigoMovimiento)))
                {
                    listaDocumento = DataMapper.DatatableToList<MovimientoDocumentoDTO>(datos).ToList();
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
            return listaDocumento;
        }

        public bool actualizaDocumentoImprimir(Int64 CodigoDocumentoImprimir, String Usuario, String Terminal)
        {
            bool resultado = false;
            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexionERP);
                baseDatos.EjecutarConsulta("USP_OPR_MOVIMIENTO_ACTUALIZA_CODIGO_IMPRESION", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("CODIGO_MOVIMIENTO_IMPRIMIR", MySqlDbType.Int64, CodigoDocumentoImprimir),
                    baseDatos.CrearParametro("USUARIO_REGISTRO", MySqlDbType.String, Usuario),
                    baseDatos.CrearParametro("TERMINAL", MySqlDbType.String, Terminal));
                resultado = true;
            }
            catch (MySqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return resultado;
        }
    }
}