using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Common;
using ZEDPAITA.SRV.Core.RepositoryInterfaces;
using ZEDPAITA.SRV.Core.ServiceInterfaces;
using ZEDPAITA.SRV.Proxy.SendInvoiceSunat;
using ZEDPAITA.SRV.Util.LoggingInterface;

namespace ZEDPAITA.SRV.Core.Services
{
    public class SendInvoiceSunatService : ISendInvoiceSunatService
    {

        #region Properties
        /// <summary>
        /// Gestiona el log de la clase.
        /// </summary>   
        private readonly ILogger _logger;

        /// <summary>
        /// Almacena la interfaz de repositorio de envío de factura a gobierno.
        /// </summary>
        private readonly ISendInvoiceSunatRepository _SendInvoiceSunatRepository;
        private readonly IParametrosRepository _IParametrosRepository;

        private IParametroService _IParametroService;
        private ISendInvoiceSunatProxy _ISendInvoiceSunatProxy;
        #endregion

        #region Constructor
        public SendInvoiceSunatService(ISendInvoiceSunatRepository ISendInvoiceSunatRepository, IParametrosRepository IParametrosRepository, ILogger logger)
        {
            _SendInvoiceSunatRepository = ISendInvoiceSunatRepository;
            _IParametrosRepository = IParametrosRepository;
            _logger = logger;
        }
        #endregion

        #region Operations
        public void ProcesarEnvioCompranteElectronicoSunat(Int32 configuracionNumeroRegistrosObtener)
        {
            _IParametroService = new ParametroService(_IParametrosRepository);
            _ISendInvoiceSunatProxy = new SendInvoiceSunatProxy();

            try
            {
                var parametroConfiguracionEnvioFactura = _IParametroService.ConsultaParametroSeccionValor(Convert.ToInt32(Enumerados.Parametro.ConfiguracionLEnvioComprobantElectronico), Enumerados.NombreCadenaConexion.ConexionGestion);
                string ruta = parametroConfiguracionEnvioFactura.Datos[0].FirstOrDefault(x => x.Key == "2").Value.ToString();
                string token = parametroConfiguracionEnvioFactura.Datos[0].FirstOrDefault(x => x.Key == "3").Value.ToString();
                Int32 dias = Convert.ToInt32(parametroConfiguracionEnvioFactura.Datos[0].FirstOrDefault(x => x.Key == "4").Value.ToString());
                
                var documentos = _SendInvoiceSunatRepository.obtenerCodigoEnviar(dias, configuracionNumeroRegistrosObtener, Enumerados.NombreCadenaConexion.ConexionFE);

                foreach (var documento in documentos)
                {
                    try
                    {
                        _ISendInvoiceSunatProxy.EnviarCodigoComprobanteElectronico(token, ruta, documento.CODIGO_EXTERNO);
                        _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.INFO, "Se envió código a sunat: " + documento.CODIGO_EXTERNO);
                    }
                    catch (Exception ex)
                    {
                        _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.ERROR, "ERROR al enviar codigo: " + documento.CODIGO_EXTERNO + " " +  ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.ERROR, "ERROR en el proceso: " + ex.ToString());
            }
        }
        #endregion
    }
}
