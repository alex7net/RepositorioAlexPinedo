using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Models.Impresor;
using ZEDPAITA.SRV.Infrastructure.Base;
using ZEDPAITA.SRV.Core.Common;
using System.IO;
using ZEDPAITA.SRV.Util.LoggingInterface;
using ZEDPAITA.SRV.Util.Log4Net;
using ZEDPAITA.SRV.Core.ServiceInterfaces;
using System.Timers;
using System.Threading;
using ZEDPAITA.SRV.Core.Services;
using ZEDPAITA.SRV.Infrastructure.Repositories;

namespace ZEDPAITA.SRV.Presentacion.Agente.WSAImpresor
{
    public partial class WSAImpresor : ServiceBase
    {
        #region Properties
        readonly double dblDelayInSeconds = Convert.ToDouble(ConfigurationManager.AppSettings["IntervaloEjecucionImpresion"].ToString());
        readonly private int IntervaloVerificacionHilo = Convert.ToInt32(ConfigurationManager.AppSettings["IntervaloVerificacionHilo"]);
        readonly private string usuarioImpresion = ConfigurationManager.AppSettings["UsuarioServicio"].ToString();
        readonly private string configuracionNumeroRegistrosObtener = ConfigurationManager.AppSettings["numeroFilasObtener"];


        private readonly IImpresorService _ImpresorService;
        private static ConfiguracionImpresor configuracionImpresor;
        private readonly ILogger _logger;
        readonly System.Timers.Timer timer = new System.Timers.Timer();
        #endregion

        #region Constructor
        public WSAImpresor()
        {
            InitializeComponent();
            _logger = new Log4NetLogger();
            configuracionImpresor = new ConfiguracionImpresor();
            _ImpresorService = new ImpresorService(new ImpresorRepository(), new Log4NetLogger());
            timer.Interval = dblDelayInSeconds;
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
        }
        #endregion

        #region Operations
        /// <summary>
        /// Terminado el tiempo transcurrido
        /// </summary>
        /// <param name="source">fuente</param>
        /// <param name="e">evento</param>
        public void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            Thread hiloEvento = new Thread(IniciarProcesoImpresion);
            try
            {
                timer.Stop();
                hiloEvento.Start();
                while (hiloEvento.IsAlive)
                {
                    Thread.Sleep(IntervaloVerificacionHilo);
                }
                timer.Start();
            }
            catch (Exception ex)
            {
                hiloEvento.Abort();
                _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.ERROR, "ERROR en ProcesarNotificacion", ex);
                timer.Start();
            }
            finally
            {
                if (hiloEvento.IsAlive)
                {
                    hiloEvento.Abort();
                    hiloEvento = null;
                }
            }
        }

        private void ConfiguracionServicio()
        {
            try
            {
                configuracionImpresor.HostName = Enumerados.ObtenerHostNamePC();

                if (!string.IsNullOrEmpty(configuracionNumeroRegistrosObtener))
                {
                    configuracionImpresor.NumeroDocumentosObtener = Convert.ToInt32(configuracionNumeroRegistrosObtener);
                }
                else
                {
                    throw new Exception(Enumerados.TipoErrorAgenteImpresion.ErrorVariablesConfiguracionImpresorNumeroRegistros);
                }

                if (!string.IsNullOrEmpty(usuarioImpresion))
                {
                    configuracionImpresor.NombreAgente = usuarioImpresion;
                }
                else
                {
                    throw new Exception(Enumerados.TipoErrorAgenteImpresion.NoSeHaConfiguradoNombreAgente);
                }
            }
            catch (Exception ex)
            {
                _logger.WriteMessage(Enumerados.TipoLog.DataAccess, LogLevel.ERROR, "ERROR en WSAImpresor: " + ex.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            timer.Start();
            _logger.WriteMessage(Enumerados.TipoLog.Impresion, LogLevel.INFO, "Se Inicia el Proceso de Impresion");
        }

        protected override void OnStop()
        {
            timer.Stop();
            _logger.WriteMessage(Enumerados.TipoLog.Impresion, LogLevel.INFO, "Se detuvo el Proceso de Impresion");
        }

        public void IniciarProcesoImpresion()
        {
            ConfiguracionServicio();
            _logger.WriteMessage(Enumerados.TipoLog.Impresion, LogLevel.INFO, "Se inicia el servicio WSAImpresor");
            _ImpresorService.ProcesarImpresion(configuracionImpresor);
        }
        #endregion
    }
}
