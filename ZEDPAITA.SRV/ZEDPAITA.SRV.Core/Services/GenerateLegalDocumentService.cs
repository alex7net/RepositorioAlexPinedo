using System;
using System.Collections.Generic;
using System.Linq;
using ZEDPAITA.SRV.Core.Common;
using ZEDPAITA.SRV.Core.Models.GenerateLegalDocument;
using ZEDPAITA.SRV.Core.RepositoryInterfaces;
using ZEDPAITA.SRV.Core.ServiceInterfaces;
using ZEDPAITA.SRV.Proxies.ExchangeRate;
using ZEDPAITA.SRV.Proxies.GenerateLegalDocument;
using ZEDPAITA.SRV.Util.LoggingInterface;

namespace ZEDPAITA.SRV.Core.Services
{
    public class GenerateLegalDocumentService : IGenerateLegalDocumentService
    {
        #region Properties
        /// <summary>
        /// Gestiona el log de la clase.
        /// </summary>   
        private readonly ILogger _logger;
        private readonly IParametrosRepository _IParametrosRepository;
        private IParametroService _IParametroService;
        private IGenerateLegalDocumentProxy _IGenerateLegalDocumentProxy;
        private IExchangeRateProxy _IExchangeRateProxy;
        private IGenerateLegalDocumentRepository _IGenerateLegalDocumentRepository;
        #endregion

        #region Constructor
        public GenerateLegalDocumentService(IGenerateLegalDocumentRepository IGenerateLegalDocumentRepository, IParametrosRepository IParametrosRepository, ILogger logger)
        {
            _IGenerateLegalDocumentRepository = IGenerateLegalDocumentRepository;
            _IParametrosRepository = IParametrosRepository;
            _logger = logger;
        }
        #endregion

        #region Operations
        public string ConsultaTiempoEjecucion() 
        {
            _IParametroService = new ParametroService(_IParametrosRepository);
            var parametroConfiguracionHoraEjecucion = _IParametroService.ConsultaParametroSeccionValor(Convert.ToInt32(Enumerados.Parametro.HoraEjecucionGenerateLegalDocumet), Enumerados.NombreCadenaConexion.ConexionGestion);
            return parametroConfiguracionHoraEjecucion.Datos[0].FirstOrDefault(x => x.Key == "2").Value.ToString();
        }

        public void ProcesarGenerateLegalDocument()
        {
            try
            {
                _IParametroService = new ParametroService(_IParametrosRepository);
                _IGenerateLegalDocumentProxy = new GenerateLegalDocumentProxy();
                _IExchangeRateProxy = new ExchangeRateProxy();
                var ListaDocumento = _IGenerateLegalDocumentRepository.obtenerDocumentosGenerados();

                var parametroConfiguracionEnvioFactura = _IParametroService.ConsultaParametroSeccionValor(Convert.ToInt32(Enumerados.Parametro.ConfiguracionLGeneracionComprobantElectronico), Enumerados.NombreCadenaConexion.ConexionGestion);
                string ruta = parametroConfiguracionEnvioFactura.Datos[0].FirstOrDefault(x => x.Key == "2").Value.ToString();
                string token = parametroConfiguracionEnvioFactura.Datos[0].FirstOrDefault(x => x.Key == "3").Value.ToString();

                var parametroConfiguracionConsultaTipoCambio = _IParametroService.ConsultaParametroSeccionValor(Convert.ToInt32(Enumerados.Parametro.ConfiguracionRutaTipoCambio), Enumerados.NombreCadenaConexion.ConexionGestion);
                string rutaTipoCambio = parametroConfiguracionConsultaTipoCambio.Datos[0].FirstOrDefault(x => x.Key == "2").Value.ToString();
                string tokenTipoCambio = parametroConfiguracionConsultaTipoCambio.Datos[0].FirstOrDefault(x => x.Key == "3").Value.ToString();

                foreach (var documento in ListaDocumento) 
                {
                    try
                    {
                        _logger.WriteMessage(Enumerados.TipoLog.GenerateLegalDocument, LogLevel.INFO, "Procesando Documento Código: " + documento.CODIGO_DOCUMENTO_LEGAL.ToString());

                        decimal TipoCambioVenta = 0;
                        
                        ExchangeRateResponse respuestaTipoCambio = _IExchangeRateProxy.ConsultaExchangeRateProxy(new ExchangeRateRequest
                        {
                            FechaConsulta = documento.FECHA_EMISION.ToString("yyyy-MM-dd"),
                            Ruta = rutaTipoCambio,
                            Token = tokenTipoCambio
                        });

                        TipoCambioVenta = respuestaTipoCambio.TipoCambioVenta;

                        SendInvoiceGenerateRequest enviarDocumentos = new SendInvoiceGenerateRequest();
                        enviarDocumentos.serie_documento = documento.SERIE_DOCUMENTO;
                        enviarDocumentos.numero_documento = documento.NUMERO_DOCUMENTO;
                        enviarDocumentos.fecha_de_emision = documento.FECHA_EMISION.ToString("yyyy-MM-dd");
                        enviarDocumentos.hora_de_emision = documento.HORA_EMISION;
                        enviarDocumentos.codigo_tipo_operacion = documento.CODIGO_TIPO_OPERACION;
                        enviarDocumentos.codigo_tipo_documento = documento.CODIGO_TIPO_DOCUMENTO;
                        enviarDocumentos.codigo_tipo_moneda = documento.CODIGO_TIPO_MONEDA;
                        enviarDocumentos.fecha_de_vencimiento = documento.FECHA_VENCIMIENTO.ToString("yyyy-MM-dd");
                        enviarDocumentos.numero_orden_de_compra = documento.NUMERO_ORDEN_COMPRA;

                        enviarDocumentos.datos_del_emisor = new SendInvoiceDataEmisorRequest();
                        enviarDocumentos.datos_del_emisor.codigo_pais = documento.CODIGO_PAIS_EMISOR;
                        enviarDocumentos.datos_del_emisor.ubigeo = documento.UBIGEO_EMISOR;
                        enviarDocumentos.datos_del_emisor.direccion = documento.DIRECCION_EMISOR;
                        enviarDocumentos.datos_del_emisor.correo_electronico = documento.CORREO_ELECTRONICO_EMISOR;
                        enviarDocumentos.datos_del_emisor.telefono = documento.TELEFONO_EMISOR;
                        enviarDocumentos.datos_del_emisor.codigo_del_domicilio_fiscal = documento.CODIGO_DOMICILIO_FISCAL_EMISOR;

                        enviarDocumentos.datos_del_cliente_o_receptor = new SendInvoiceDataReceptorRequest();
                        enviarDocumentos.datos_del_cliente_o_receptor.codigo_tipo_documento_identidad = documento.CODIGO_TIPO_DOCUMENTO_IDENTIDAD_CLIENTE;
                        enviarDocumentos.datos_del_cliente_o_receptor.numero_documento = documento.NUMERO_DOCUMENTO_CLIENTE;
                        enviarDocumentos.datos_del_cliente_o_receptor.apellidos_y_nombres_o_razon_social = documento.APELLIDOS_RAZON_SOCIAL_CLIENTE;
                        enviarDocumentos.datos_del_cliente_o_receptor.codigo_pais = documento.CODIGO_PAIS_CLIENTE;
                        enviarDocumentos.datos_del_cliente_o_receptor.ubigeo = documento.UBIGEO_CLIENTE;
                        enviarDocumentos.datos_del_cliente_o_receptor.direccion = documento.DIRECCION_CLIENTE;
                        enviarDocumentos.datos_del_cliente_o_receptor.correo_electronico = documento.CORREO_ELECTRONICO_CLIENTE;
                        enviarDocumentos.datos_del_cliente_o_receptor.telefono = documento.TELEFONO_CLIENTE;


                        enviarDocumentos.totales = new SendInvoiceTotalesRequest();
                        if (documento.INDICADOR_CONVERSION == Enumerados.IndicadorConversion.SI)
                        {
                            enviarDocumentos.totales.total_exportacion = documento.TOTAL_EXPORTACION;
                            enviarDocumentos.totales.total_operaciones_gravadas = documento.TOTAL_OPERACIONES_GRAVADAS;
                            enviarDocumentos.totales.total_operaciones_inafectas = documento.TOTAL_OPERACIONES_INAFECTAS;
                            enviarDocumentos.totales.total_operaciones_exoneradas = documento.TOTAL_OPERACIONES_EXONERADAS * TipoCambioVenta;
                            enviarDocumentos.totales.total_operaciones_gratuitas = documento.TOTAL_OPERACIONES_GRATUITAS;
                            enviarDocumentos.totales.total_igv = documento.TOTAL_IGV;
                            enviarDocumentos.totales.total_impuestos = documento.TOTAL_IMPUESTOS;
                            enviarDocumentos.totales.total_valor = documento.TOTAL_VALOR * TipoCambioVenta;
                            enviarDocumentos.totales.total_venta = documento.TOTAL_VENTA * TipoCambioVenta;

                        }
                        else
                        {
                            enviarDocumentos.totales.total_exportacion = documento.TOTAL_EXPORTACION;
                            enviarDocumentos.totales.total_operaciones_gravadas = documento.TOTAL_OPERACIONES_GRAVADAS;
                            enviarDocumentos.totales.total_operaciones_inafectas = documento.TOTAL_OPERACIONES_INAFECTAS;
                            enviarDocumentos.totales.total_operaciones_exoneradas = documento.TOTAL_OPERACIONES_EXONERADAS;
                            enviarDocumentos.totales.total_operaciones_gratuitas = documento.TOTAL_OPERACIONES_GRATUITAS;
                            enviarDocumentos.totales.total_igv = documento.TOTAL_IGV;
                            enviarDocumentos.totales.total_impuestos = documento.TOTAL_IMPUESTOS;
                            enviarDocumentos.totales.total_valor = documento.TOTAL_VALOR;
                            enviarDocumentos.totales.total_venta = documento.TOTAL_VENTA;

                        }

                        enviarDocumentos.items = new List<SendInvoiceItemsRequest>();
                        SendInvoiceItemsRequest item = new SendInvoiceItemsRequest();
                        item.codigo_interno = documento.CODIGO_INTERNO;

                        if (documento.INDICADOR_CONVERSION == Enumerados.IndicadorConversion.SI)
                        {
                            item.descripcion = documento.DESCRIPCION + " USD " + documento.TOTAL_VENTA.ToString() + " T.C " + TipoCambioVenta.ToString();
                            item.codigo_producto_sunat = documento.CODIGO_PRODUCTO_SUNAT;
                            item.codigo_producto_gsl = documento.CODIGO_PRODUCTO_GSL;
                            item.unidad_de_medida = documento.UNIDAD_MEDIDA;
                            item.cantidad = documento.CANTIDAD;
                            item.valor_unitario = documento.VALOR_UNITARIO * TipoCambioVenta;
                            item.codigo_tipo_precio = documento.CODIGO_TIPO_PRECIO;
                            item.precio_unitario = documento.PRECIO_UNITARIO * TipoCambioVenta;
                            item.codigo_tipo_afectacion_igv = documento.CODIGO_TIPO_AFECTACION_IGV;
                            item.total_base_igv = documento.TOTAL_BASE_IGV;
                            item.porcentaje_igv = documento.PORCENTAJE_IGV;
                            item.total_igv = documento.TOTAL_IGV;
                            item.total_impuestos = documento.TOTAL_IMPUESTOS;
                            item.total_valor_item = documento.TOTAL_VALOR_ITEM * TipoCambioVenta;
                            item.total_item = documento.TOTAL_ITEM * TipoCambioVenta;
                        }
                        else
                        {
                            item.descripcion = documento.DESCRIPCION;
                            item.codigo_producto_sunat = documento.CODIGO_PRODUCTO_SUNAT;
                            item.codigo_producto_gsl = documento.CODIGO_PRODUCTO_GSL;
                            item.unidad_de_medida = documento.UNIDAD_MEDIDA;
                            item.cantidad = documento.CANTIDAD;
                            item.valor_unitario = documento.VALOR_UNITARIO;
                            item.codigo_tipo_precio = documento.CODIGO_TIPO_PRECIO;
                            item.precio_unitario = documento.PRECIO_UNITARIO;
                            item.codigo_tipo_afectacion_igv = documento.CODIGO_TIPO_AFECTACION_IGV;
                            item.total_base_igv = documento.TOTAL_BASE_IGV;
                            item.porcentaje_igv = documento.PORCENTAJE_IGV;
                            item.total_igv = documento.TOTAL_IGV;
                            item.total_impuestos = documento.TOTAL_IMPUESTOS;
                            item.total_valor_item = documento.TOTAL_VALOR_ITEM;
                            item.total_item = documento.TOTAL_ITEM;
                        }

                        enviarDocumentos.items.Add(item);

                        enviarDocumentos.informacion_adicional = documento.INFORMACION_ADICIONAL;
                        enviarDocumentos.acciones = new SendInvoiceDataAccionesRequest();
                        enviarDocumentos.acciones.enviar_email = documento.ENVIAR_EMAIL;
                        enviarDocumentos.acciones.enviar_xml_firmado = documento.ENVIAR_XML_FIRMADO;

                        var response = _IGenerateLegalDocumentProxy.EnviarDocumentoLegalProxy(enviarDocumentos, token, ruta);

                        _IGenerateLegalDocumentRepository.insertarArchivoFacturacionElectronica(response.FACTURA, response.PDF, response.JSON, "", 1);

                        //Insert_Ventas
                        _IGenerateLegalDocumentRepository.insertarVentas(
                            new GenerateLegaDocumentVentasDTO
                            {
                                FechaDocumento = Convert.ToDateTime(documento.FECHA_EMISION.ToString("yyyy-MM-dd")),
                                TipoComprobante = "F",
                                Serie = documento.SERIE_DOCUMENTO,
                                NumeroDocumento = response.NUMERO_FACTURA,
                                RucCliente = documento.NUMERO_DOCUMENTO_CLIENTE,
                                TipoConcepto = documento.CODIGO_TIPO_CONCEPTO.ToString().PadLeft(2, '0'),
                                Moneda = (documento.CODIGO_TIPO_MONEDA == "USD") ? "D" : "S",
                                SubDolar = (documento.CODIGO_TIPO_MONEDA == "USD") ? Convert.ToDouble(documento.TOTAL_VENTA) : 0,
                                IgvDolar = 0,
                                ImporteDolar = (documento.CODIGO_TIPO_MONEDA == "USD") ? Convert.ToDouble(documento.TOTAL_VENTA) : 0,
                                TipoCambio = (documento.CODIGO_TIPO_MONEDA == "USD") ? Convert.ToDouble(TipoCambioVenta) : 0, //ACAAAAAAAAAA
                                SubTotal = Convert.ToDouble(documento.TOTAL_VENTA * TipoCambioVenta), //ACA simepre en soles
                                Igv = 0,
                                TotalFactura = Convert.ToDouble(documento.TOTAL_VENTA * TipoCambioVenta), //ACA siempre en soles
                                Condicion = "02",
                                FPrecepcion = "",
                                FechaPago = Convert.ToDateTime(documento.FECHA_EMISION.ToString("yyyy-MM-dd")),
                                FormaPago_Id = 2,
                                Fecha_Vencimiento = Convert.ToDateTime(documento.FECHA_VENCIMIENTO.ToString("yyyy-MM-dd")),
                                Gracia = "0",
                                Estado_Vencimiento = 0,
                                Monto_Capital = (documento.CODIGO_TIPO_MONEDA == "USD") ? documento.TOTAL_VENTA : (documento.TOTAL_VENTA * TipoCambioVenta), // ACA
                                Monto_Adeuda = (documento.CODIGO_TIPO_MONEDA == "USD") ? documento.TOTAL_VENTA : (documento.TOTAL_VENTA * TipoCambioVenta) // ACA
                            });

                        //Insert_Itemsv
                        _IGenerateLegalDocumentRepository.insertarItemsV(
                            new GenerateLegalDocumentDetalleDTO
                            {
                                TipoComprobante_Id = "F",
                                Serie_Id = documento.SERIE_DOCUMENTO,
                                NumeroDocumento = response.NUMERO_FACTURA,
                                Cantidad = documento.CANTIDAD,
                                Medida = "",
                                Concepto = item.descripcion,
                                Moneda = (documento.CODIGO_TIPO_MONEDA == "USD") ? "D" : "S",
                                PrecioUnitario = (documento.CODIGO_TIPO_MONEDA == "USD") ? Convert.ToDouble(documento.TOTAL_VENTA) : Convert.ToDouble(documento.TOTAL_VENTA * TipoCambioVenta), //aca
                                Importe = (documento.CODIGO_TIPO_MONEDA == "USD") ? Convert.ToDouble(documento.TOTAL_VENTA) : Convert.ToDouble(documento.TOTAL_VENTA * TipoCambioVenta) //aca
                            });

                        //actualizar correlativo
                        _IGenerateLegalDocumentRepository.ActualizarEstadoEnviadoDocumentoLegalemision(
                                documento.CODIGO_DOCUMENTO_LEGAL,
                                item.descripcion,
                                TipoCambioVenta,
                                (documento.TOTAL_VENTA * TipoCambioVenta),
                                Convert.ToInt32(Enumerados.EstadoEnvio.Enviado),
                                response.NUMERO_FACURA_SERIE
                            );
                    }
                    catch (Exception ex)
                    {
                        _logger.WriteMessage(Enumerados.TipoLog.GenerateLegalDocument, LogLevel.ERROR, "ERROR al enviar el documento correlativo emision: " + documento.CODIGO_DOCUMENTO_LEGAL.ToString() + " "+ ex.ToString());
                    }
                }

                ProgramarFechaSiguienteEjecucion();
            }
            catch (Exception ex)
            {
                _logger.WriteMessage(Enumerados.TipoLog.GenerateLegalDocument, LogLevel.ERROR, "ERROR en el proceso: " + ex.ToString());
            }
        }

        public void ProgramarFechaSiguienteEjecucion() 
        {
            var parametroConfiguracionHoraEjecucion = _IParametroService.ConsultaParametroSeccionValor(Convert.ToInt32(Enumerados.Parametro.HoraEjecucionGenerateLegalDocumet), Enumerados.NombreCadenaConexion.ConexionGestion);
            string FechaHoraEjecucion = parametroConfiguracionHoraEjecucion.Datos[0].FirstOrDefault(x => x.Key == "2").Value.ToString();
            DateTime FechaEjecucion = Convert.ToDateTime(FechaHoraEjecucion);
            DateTime FechaSiguienteEjecucion = FechaEjecucion.AddMonths(1);

            string CadenaFechaSiguienteEjecucion = FechaSiguienteEjecucion.ToString("yyyy-MM-dd HH:mm:00");

            _IGenerateLegalDocumentRepository.ActualizaFechaSiguienteEjecucion(Convert.ToInt32(Enumerados.Parametro.HoraEjecucionGenerateLegalDocumet), 2, CadenaFechaSiguienteEjecucion);

        }

        #endregion
    }
}
