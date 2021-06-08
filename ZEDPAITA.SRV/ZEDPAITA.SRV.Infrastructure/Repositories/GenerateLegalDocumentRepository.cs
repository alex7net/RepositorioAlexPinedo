using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ZEDPAITA.SRV.Core.Models.GenerateLegalDocument;
using ZEDPAITA.SRV.Core.RepositoryInterfaces;
using ZEDPAITA.SRV.Infrastructure.Base;
using ZEDPAITA.SRV.Infrastructure.DataAccess;

namespace ZEDPAITA.SRV.Infrastructure.Repositories
{
    public class GenerateLegalDocumentRepository : IGenerateLegalDocumentRepository
    {
        /// <summary>
        /// Nombre de cadena de conexión local.
        /// </summary>
        private readonly string nombreCadenaConexion = "CadenaGestionERP";

        public List<GenerateLegalDocumentDTO> obtenerDocumentosGenerados()
        {
            List<GenerateLegalDocumentDTO> listaDocumentos = new List<GenerateLegalDocumentDTO>();
            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexion);

                using (DataTable datos = baseDatos.EjecutarConsultaDataTable("USP_ADM_GENERAR_DOCUMENTO_LEGAL_EMISION", CommandType.StoredProcedure))
                {
                    listaDocumentos = DataMapper.DatatableToList<GenerateLegalDocumentDTO>(datos).ToList();
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

        public void insertarArchivoFacturacionElectronica(string Factura, string Ruta, string Json, string JsonError, Int32 FlagSunat)
        {
            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexion);
                baseDatos.EjecutarConsulta("Insert_Archivo_Fe", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("factura_ex", MySqlDbType.String, Factura),
                    baseDatos.CrearParametro("ruta_ex", MySqlDbType.String, Ruta),
                    baseDatos.CrearParametro("json_ex", MySqlDbType.String, Json),
                    baseDatos.CrearParametro("json_error_ex", MySqlDbType.String, JsonError),
                    baseDatos.CrearParametro("flag_sunat_ex", MySqlDbType.Int32, FlagSunat),
                    baseDatos.CrearParametroSalida("newid", 0)
                    );
            }
            catch (MySqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void insertarVentas(GenerateLegaDocumentVentasDTO ventas) 
        {
            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexion);
                baseDatos.EjecutarConsulta("Insert_Ventas", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("fedoc_ex", MySqlDbType.DateTime, ventas.FechaDocumento),
                    baseDatos.CrearParametro("tdoc_ex", MySqlDbType.String, ventas.TipoComprobante),
                    baseDatos.CrearParametro("serie_ex", MySqlDbType.String, ventas.Serie),
                    baseDatos.CrearParametro("numdoc_ex", MySqlDbType.String, ventas.NumeroDocumento),
                    baseDatos.CrearParametro("rucclie_ex", MySqlDbType.String, ventas.RucCliente),

                    baseDatos.CrearParametro("tipocon_ex", MySqlDbType.String, ventas.TipoConcepto),
                    baseDatos.CrearParametro("moneda_ex", MySqlDbType.String, ventas.Moneda),
                    baseDatos.CrearParametro("subtdol_ex", MySqlDbType.Double, ventas.SubDolar),
                    baseDatos.CrearParametro("igvdol_ex", MySqlDbType.Double, ventas.IgvDolar),

                    baseDatos.CrearParametro("importedol_ex", MySqlDbType.Double, ventas.ImporteDolar),
                    baseDatos.CrearParametro("tipocambio_ex", MySqlDbType.Double, ventas.TipoCambio),
                    baseDatos.CrearParametro("subtotal_ex", MySqlDbType.Double, ventas.SubTotal),
                    baseDatos.CrearParametro("igv_ex", MySqlDbType.Double, ventas.Igv),
                    baseDatos.CrearParametro("totalf_ex", MySqlDbType.Double, ventas.TotalFactura),

                    baseDatos.CrearParametro("condi_ex", MySqlDbType.String, ventas.Condicion),
                    baseDatos.CrearParametro("fprece_ex", MySqlDbType.String, ventas.FPrecepcion),

                    baseDatos.CrearParametro("fec_fp_ex", MySqlDbType.DateTime, ventas.FechaPago),
                    baseDatos.CrearParametro("glosa_ex", MySqlDbType.String, ventas.Glosa),

                    baseDatos.CrearParametro("forma_pago_id_ex", MySqlDbType.Int32, ventas.FormaPago_Id),
                    baseDatos.CrearParametro("fecha_vencimiento_ex", MySqlDbType.DateTime, ventas.Fecha_Vencimiento),
                    baseDatos.CrearParametro("gracia_ex", MySqlDbType.String, ventas.Gracia),
                    baseDatos.CrearParametro("estado_vencimiento_ex", MySqlDbType.Int32, ventas.Estado_Vencimiento),
                    baseDatos.CrearParametro("monto_capital_ex", MySqlDbType.Decimal, ventas.Monto_Capital),
                    baseDatos.CrearParametro("monto_adeuda_ex", MySqlDbType.Decimal, ventas.Monto_Adeuda));
            }
            catch (MySqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception exception)
            {
                throw exception;
            }


        }

        public void insertarItemsV(GenerateLegalDocumentDetalleDTO detalle) 
        {

            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexion);
                baseDatos.EjecutarConsulta("Insert_Itemsv", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("tipod_ex", MySqlDbType.String, detalle.TipoComprobante_Id),
                    baseDatos.CrearParametro("seried_ex", MySqlDbType.String, detalle.Serie_Id),
                    baseDatos.CrearParametro("numd_ex", MySqlDbType.String, detalle.NumeroDocumento),
                    baseDatos.CrearParametro("canti_ex", MySqlDbType.Int32, detalle.Cantidad),
                    baseDatos.CrearParametro("medida_ex", MySqlDbType.String, detalle.Medida),
                    baseDatos.CrearParametro("conceptod_ex", MySqlDbType.String, detalle.Concepto),
                    baseDatos.CrearParametro("moneda_ex", MySqlDbType.String, detalle.Moneda),
                    baseDatos.CrearParametro("precioun_ex", MySqlDbType.Double, detalle.PrecioUnitario),
                    baseDatos.CrearParametro("importe_ex", MySqlDbType.Double, detalle.Importe)
                    );
            }
            catch (MySqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void ActualizarEstadoEnviadoDocumentoLegalemision(Int32 CorrelativoDoucumentoLegal, string Descripcion, decimal TipoCambioVenta, decimal TotalConvertido, Int32 EstadoEnvio, Int32 NumeroFacturaSerie)
        {
            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexion);
                baseDatos.EjecutarConsulta("USP_ADM_ACTUALIZAR_DOCUMENTO_LEGAL_EMISION", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("CODIGO_DOCUMENTO_LEGAL_", MySqlDbType.Int32, CorrelativoDoucumentoLegal),
                    baseDatos.CrearParametro("DESCRIPCION_", MySqlDbType.String, Descripcion),
                    baseDatos.CrearParametro("TIPO_CAMBIO_VENTA_", MySqlDbType.Decimal, TipoCambioVenta),
                    baseDatos.CrearParametro("TOTAL_CONVERTIDO_", MySqlDbType.Decimal, TotalConvertido),
                    baseDatos.CrearParametro("ESTADO_ENVIADO_", MySqlDbType.Int32, EstadoEnvio),
                    baseDatos.CrearParametro("NUMERO_FACTURA_SERIE_", MySqlDbType.Int32, NumeroFacturaSerie)
                    );
            }
            catch (MySqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void ActualizaFechaSiguienteEjecucion(Int32 CodigoParametro, Int32 CodigoSeccion, string Valor)
        {
            try
            {
                BaseDatos baseDatos = new BaseDatos(nombreCadenaConexion);
                baseDatos.EjecutarConsulta("USP_ACTUALIZA_PARAMETRO_VALOR_PROCESO", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("CODIGO_SECCION_", MySqlDbType.Int32, CodigoSeccion),
                    baseDatos.CrearParametro("CODIGO_PARAMETRO_", MySqlDbType.Int32, CodigoParametro),
                    baseDatos.CrearParametro("VALOR_", MySqlDbType.String, Valor)
                    );
            }
            catch (MySqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

    }
}
