using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using ZEDPAITA.SRV.Core.Common;
using ZEDPAITA.SRV.Core.ServiceInterfaces;
using ZEDPAITA.SRV.Core.Services;
using ZEDPAITA.SRV.Infrastructure.Repositories;
using ZEDPAITA.SRV.Util.Impresion;
using ZEDPAITA.SRV.Util.Log4Net;
using ZEDPAITA.SRV.Util.LoggingInterface;

namespace ZEDPAITA.SRV.Presentacion.WSNotificacion
{
    public partial class WSNotificacion : ServiceBase
    {
        #region Properties
        readonly double dblDelayInSeconds = Convert.ToDouble(ConfigurationManager.AppSettings["IntervaloEjecucionNotificacion"].ToString());
        readonly private int IntervaloVerificacionHilo = Convert.ToInt32(ConfigurationManager.AppSettings["IntervaloVerificacionHilo"]);
        readonly private int configuracionNumeroRegistrosObtener = Convert.ToInt32(ConfigurationManager.AppSettings["numeroFilasObtener"]);
        readonly private string usuarioNotificacion = ConfigurationManager.AppSettings["UsuarioServicio"].ToString();
        readonly private string ubicacionImpresionesPdf = ConfigurationManager.AppSettings["ubicacionImpresionesPdf"].ToString();
        readonly private string ubicacionReportes = ConfigurationManager.AppSettings["ubicacionReportes"];

        private readonly ILogger _logger;
        readonly System.Timers.Timer timer = new System.Timers.Timer();
        private static ConfiguracionNotificacion configuracionNotificacion;
        private readonly INotificacionService _INotificacionService;
        #endregion
        public WSNotificacion()
        {
            InitializeComponent();
            configuracionNotificacion = new ConfiguracionNotificacion();
            _logger = new Log4NetLogger();
            _INotificacionService = new NotificacionService(new NotificacionRepository(), new Log4NetLogger());
            timer.Interval = dblDelayInSeconds;
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
        }
        
        /// <summary>
        /// Terminado el tiempo transcurrido
        /// </summary>
        /// <param name="source">fuente</param>
        /// <param name="e">evento</param>
        public void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            Thread hiloEvento = new Thread(IniciarProcesoNotificacion);
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

        protected override void OnStart(string[] args)
        {
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
            _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.INFO, "Se detuvo el Proceso de Notificacion");
        }

        private void ConfiguracionServicioNotificacion()
        {
            try
            {
                if (!string.IsNullOrEmpty(ubicacionImpresionesPdf))
                {
                    configuracionNotificacion.UbicacionImpresionesPdf = ubicacionImpresionesPdf;
                }
                else
                {
                    if (!Directory.Exists(ubicacionImpresionesPdf))
                    {
                        throw new Exception(Enumerados.TipoErrorNotificacion.ErrorUbicacionPdfNoExiste);
                    }
                    else
                    {
                        throw new Exception(Enumerados.TipoErrorNotificacion.ErrorVariableConfiguracionUbicacionPdf);
                    }
                }

                if (configuracionNumeroRegistrosObtener > 0)
                {
                    configuracionNotificacion.configuracionNumeroRegistrosObtener = configuracionNumeroRegistrosObtener;
                }
                else
                {
                    throw new Exception(Enumerados.TipoErrorNotificacion.ErrorConfiguracionNumeroRegistrosObtener);
                }

                if (!string.IsNullOrEmpty(usuarioNotificacion))
                {
                    configuracionNotificacion.UsuarioServicioNotificacion = usuarioNotificacion;
                }
                else
                {
                    throw new Exception(Enumerados.TipoErrorNotificacion.ErrorConfiguracionUsuarioServicio);
                }
                
                if (!string.IsNullOrEmpty(ubicacionReportes))
                {
                    configuracionNotificacion.UbicacionReportes = ubicacionReportes;
                }
                else
                {
                    throw new Exception(Enumerados.TipoErrorNotificacion.ErrorVariableConfiguracionImpresorUbicacion);
                }


            }
            catch (Exception ex)
            {
                _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.ERROR, "ERROR en WSANotificacion: " + ex.Message);
            }
        }

        public void IniciarProcesoNotificacion()
        {
            ConfiguracionServicioNotificacion();
            _INotificacionService.ProcesarNotificacion(configuracionNotificacion);
        }
    }
}
