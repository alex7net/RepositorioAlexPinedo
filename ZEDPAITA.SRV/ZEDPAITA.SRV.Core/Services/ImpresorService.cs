using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Base;
using ZEDPAITA.SRV.Core.Common;
using ZEDPAITA.SRV.Core.Models.Impresor;
using ZEDPAITA.SRV.Core.RepositoryInterfaces;
using ZEDPAITA.SRV.Core.ServiceInterfaces;
using ZEDPAITA.SRV.Util.LoggingInterface;

namespace ZEDPAITA.SRV.Core.Services
{
    public class ImpresorService : IImpresorService
    {
        /// <summary>
        /// Gestiona el log de la clase.
        /// </summary>   
        private readonly ILogger _logger;
        /// <summary>
        /// Almacena la configuración de impresión.
        /// </summary>
        private ConfiguracionImpresor _configuracionImpresor;
        /// <summary>
        /// Almacena la interfaz de repositorio de impresión.
        /// </summary>
        private readonly IImpresorRepository _impresorRepository;


        public ImpresorService(IImpresorRepository impresorRepository, ILogger logger)
        {
            _impresorRepository = impresorRepository;
            _logger = logger;
        }


        /// <summary>
        /// Realiza el proceso de impresión.
        /// </summary>
        /// <param name="configuracionImpresor">Configuración de impresión</param>
        public void ProcesarImpresion(ConfiguracionImpresor configuracionImpresor)
        {
            try
            {
                _configuracionImpresor = configuracionImpresor;
                EjecutarImpresion(_configuracionImpresor.NumeroDocumentosObtener, _configuracionImpresor.HostName);
            }
            catch (Exception ex)
            {
                _logger.WriteMessage(Enumerados.TipoLog.Impresion, LogLevel.ERROR, "ERROR en ProcesarImpresion", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Ejecuta impresión
        /// </summary>
        /// <param name="numeroDocumentosObtener">Referencia de número de documentos a obtener para una impresora</param>
        /// <param name="nombreAgente">nombre del agente impresor
        private void EjecutarImpresion(int numeroDocumentosObtener, string nombreAgente)
        {
            List<DocumentoImprimirDTO> listaDocumentosImprimir = new List<DocumentoImprimirDTO>();
            bool generadoCorrecto = false;
            try
            {
                listaDocumentosImprimir = _impresorRepository.obtenerDocumentos(nombreAgente, numeroDocumentosObtener);
                int cantidadDocumentosObtenidos = listaDocumentosImprimir.Count;
                if (listaDocumentosImprimir != null && cantidadDocumentosObtenidos > 0)
                {
                    foreach (var item in listaDocumentosImprimir)
                    {
                        var listMovimientoCabecera = _impresorRepository.obtenerMovimientoCabecera(item.CODIGO_MOVIMIENTO);
                        var listMovimientoDetalle = _impresorRepository.obtenerMovimientoDetalle(item.CODIGO_MOVIMIENTO);
                        var listMovimientoDocumento = _impresorRepository.obtenerMovimientoDocumento(item.CODIGO_MOVIMIENTO);

                        if (item.CODIGO_TIPO_DOCUMENTO_TRANSACCION == Enumerados.TipoDocumentoImprimirTransaccion.Ticket)
                        {
                            ImprimirTicket(listMovimientoCabecera, listMovimientoDetalle, listMovimientoDocumento, item.NOMBRE_IMPRESORA, item.SERIE_DOCUMENTO, item.NUMERO_DOCUMENTO);
                        }

                        _impresorRepository.actualizaDocumentoImprimir(item.CODIGO_MOVIMIENTO_IMPRIMIR, Enumerados.Servicios.WSAgente, Enumerados.Servicios.WSAgente);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.WriteMessage(Enumerados.TipoLog.Impresion, LogLevel.ERROR, "ERROR en Ejecutar impresión", ex);
                throw ex;
            }
        }

        private void ImprimirTicket(MovimientoCabceraDTO cabecera, List<MovimientoDetalleDTO> detalles, List<MovimientoDocumentoDTO> documentos, string nombreImpresora, string Serie, string NumeroDocumento)
        {
            try
            {
                CrearTicket ticket = new CrearTicket();  
                ticket.TextoCentro(cabecera.RAZON_SOCIAL_EMPRESA_PRINCIPAL);
                ticket.TextoIzquierda("Car. Paita Sullana Km 3, Zona Industrial Etapa II - Paita - RUC: 20484347205");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("Usuario: " + cabecera.RAZON_SOCIAL_USUARIO);
                ticket.TextoIzquierda("Tipo Movimiento: " + cabecera.TIPO_OPERACION);
                ticket.TextoExtremos("Ticket: " + Serie + "-" + NumeroDocumento, "Nro Bultos: " + cabecera.NUMERO_BULTO);
                ticket.TextoIzquierda("Chofer: " + cabecera.CHOFER);
                ticket.TextoExtremosPlaca("Placa: " + cabecera.PLACA, "DNI: " + cabecera.NUMERO_DOCUMENTO_IDENTIDAD_CHOFER);

                foreach (var documento in documentos)
                {
                    ticket.TextoIzquierda("Tipo de Doc: " + documento.TIPO_DOCUMENTO_FISCAL);
                    ticket.TextoIzquierda("Nro Docu: " + documento.NUMERO_DOCUMENTO_FISCAL);
                    ticket.TextoIzquierda("Proveedor: " + documento.RAZON_SOCIAL);
                }

                ticket.TextoIzquierda("Transportista: " + cabecera.EMPRESA_TRANSPORTISTA_USUARIO);

                if (cabecera.NUMERO_CONTENEDOR != null)
                {
                    ticket.TextoIzquierda("Contenedor: " + cabecera.NUMERO_CONTENEDOR);
                }
                if (cabecera.NUMERO_PRECINTO != null)
                {
                    ticket.TextoIzquierda("Precintos: " + cabecera.NUMERO_PRECINTO);
                }

                ticket.lineasAsteriscos();
                
                ticket.TextoIzquierda("Fecha de Ingreso: " + cabecera.FECHA_INGRESO + " " + cabecera.HORA_UNO);
                ticket.TextoIzquierda("Fecha de Salida:  " + cabecera.FECHA_SALIDA + " " + cabecera.HORA_DOS);
                ticket.TextoIzquierda("Primera Pesada(KG):   " + cabecera.PRIMERO_PESADA);
                ticket.TextoIzquierda("Segunda Pesada(KG):   " + cabecera.SEGUNDA_PESADA);
                ticket.TextoIzquierda("P.Bruto: " + cabecera.PESO_BRUTO + "  P.Tara: " + cabecera.PESO_TARA + "  P.Neto: " + cabecera.PESO_NETO);
             
                ticket.lineasAsteriscos();
                ticket.EncabezadoVenta();
                ticket.lineasAsteriscos();
            
                foreach (var detalle in detalles)
                {
                    ticket.TextoIzquierda(detalle.DESCRIPCION);
                    ticket.TextoIzquierda("                 " + detalle.NUMERO_PARTIDA + "  " + detalle.UNIDAD_MEDIDA + "   " + detalle.CANTIDAD);
                    
                }
                
                ticket.lineasIgual();
                ticket.TextoIzquierda("");
                ticket.TextoCentro("Ticket Autorizado por ZED PAITA y valido solo para tramites aduaneros correspondientes");
                ticket.TextoIzquierda("COD:" + cabecera.CODIGO_INGRESO);
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.CortaTicket();
                ticket.ImprimirTicket(nombreImpresora);
            }
            catch (Exception ex)
            {
                _logger.WriteMessage(Enumerados.TipoLog.Impresion, LogLevel.ERROR, "ERROR en Impresión de Ticketera", ex);
                throw ex;
            }
        }
    }
}