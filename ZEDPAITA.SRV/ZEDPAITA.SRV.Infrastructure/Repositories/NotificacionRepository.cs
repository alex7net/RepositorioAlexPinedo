using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Models.Impresor;
using ZEDPAITA.SRV.Core.RepositoryInterfaces;
using ZEDPAITA.SRV.Infrastructure.Base;
using ZEDPAITA.SRV.Infrastructure.DataAccess;
using ZEDPAITA.SRV.Core.Models.CarpetaServidor;

namespace ZEDPAITA.SRV.Infrastructure.Repositories
{
    public class NotificacionRepository : INotificacionRepository
    {
        /// <summary>
        /// Nombre de cadena de conexión local.
        /// </summary>
        private readonly string nombreCadenaConexionERP = "CadenaConexionERP";

        public List<DocumentoNotificacionDTO> obtenerDocumentosNotificacion(Int32 Filas, String Usuario)
        {
            List<DocumentoNotificacionDTO> listaDocumentos = new List<DocumentoNotificacionDTO>();

            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexionERP);

                using (DataTable datos = baseDatos.EjecutarConsultaDataTable("USP_OPR_MOVIMIENTO_DOCUMENTO_OBTENER_NOTIFICACION", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("FILAS", MySqlDbType.Int32, Filas)))
                {

                    listaDocumentos = DataMapper.DatatableToList<DocumentoNotificacionDTO>(datos).ToList();
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

        public List<CarpetaServidorDTO> obtenerCarpetaServidor(string CodigoCarpeta)
        {
            List<CarpetaServidorDTO> listaCarpetaServidor = new List<CarpetaServidorDTO>();

            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexionERP);

                using (DataTable datos = baseDatos.EjecutarConsultaDataTable("USP_CARPETA_SERVIDOR_CODIGO_CARPETA_SEL", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("CodigoCarpeta", MySqlDbType.VarChar, CodigoCarpeta)))
                {

                    listaCarpetaServidor = DataMapper.DatatableToList<CarpetaServidorDTO>(datos).ToList();
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

            return listaCarpetaServidor;
        }

        public bool actualizaDocumentoNotificacion(Int64 CodigoDocumentoNotificacion, String Usuario, String Terminal)
        {
            bool resultado = false;
            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexionERP);
                baseDatos.EjecutarConsulta("USP_OPR_MOVIMIENTO_ACTUALIZA_CODIGO_NOTIFICACION", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("CODIGO_MOVIMIENTO_NOTIFICACION", MySqlDbType.Int64, CodigoDocumentoNotificacion),
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

        public bool actualizaCertificadoManufactura(string RutaPDF, string NombreCertificado, Int32 EstadoAutorizado, string TerminalModificacion, string UsuarioModificacion, Int32 CodigoCertificadoManufactura)
        {
            bool resultado = false;
            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexionERP);
                baseDatos.EjecutarConsulta("USP_ACTUALIZA_CERTIFICADO_MANUFACTURA_SERVICIO_NOTI", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("RutaPdf", MySqlDbType.String, RutaPDF),
                    baseDatos.CrearParametro("NombreCertificado", MySqlDbType.String, NombreCertificado),
                    baseDatos.CrearParametro("EstadoAutorizado", MySqlDbType.String, EstadoAutorizado),
                    baseDatos.CrearParametro("TerminalModificacion", MySqlDbType.String, TerminalModificacion),
                    baseDatos.CrearParametro("UsuarioModificacion", MySqlDbType.String, UsuarioModificacion),
                    baseDatos.CrearParametro("CodigoCertificadoManufactura", MySqlDbType.String, CodigoCertificadoManufactura));
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



        /// <summary>
        /// Permite obtener un conjunto de datos desde una cadena de texto con formato XML.
        /// </summary>
        /// <param name="datosXml">Cadena de texto con formato XML</param>
        /// <returns>DataSet resultante</returns>
        public DataSet ObtenerConjuntoDatosXml(string datosXml)
        {
            try
            {
                datosXml = EscapeXMLValue(datosXml);
                DataSet ds = new System.Data.DataSet();
                string cabecera = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                string xml = string.Concat(@cabecera, datosXml);
                StringReader lector = new StringReader(xml);
                ds.ReadXml(lector);
                return ds;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public static string EscapeXMLValue(string xmlString)
        {

            if (xmlString == null)
                throw new ArgumentNullException("xmlString");

            return xmlString.Replace("&", "&amp;");
        }
    }
}
