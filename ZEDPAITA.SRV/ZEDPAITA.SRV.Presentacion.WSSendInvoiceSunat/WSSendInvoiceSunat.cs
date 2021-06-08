using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
using ZEDPAITA.SRV.Util.Log4Net;
using ZEDPAITA.SRV.Util.LoggingInterface;

namespace ZEDPAITA.SRV.Presentacion.WSSendInvoiceSunat
{
    public partial class WSSendInvoiceSunat : ServiceBase
    {
        #region Properties
        readonly double dblDelayInSeconds = Convert.ToDouble(ConfigurationManager.AppSettings["IntervaloEjecucionEnvio"].ToString());
        readonly private int IntervaloVerificacionHilo = Convert.ToInt32(ConfigurationManager.AppSettings["IntervaloVerificacionHilo"]);
        readonly private int configuracionNumeroRegistrosObtener = Convert.ToInt32(ConfigurationManager.AppSettings["numeroFilasObtener"]);
        readonly private string usuarioNotificacion = ConfigurationManager.AppSettings["UsuarioServicio"].ToString();

        private readonly ISendInvoiceSunatService _ISendInvoiceSunatService;
        private readonly ILogger _logger;
        readonly System.Timers.Timer timer = new System.Timers.Timer();
        #endregion

        public WSSendInvoiceSunat()
        {
            InitializeComponent();
            _logger = new Log4NetLogger();
            _ISendInvoiceSunatService = new SendInvoiceSunatService( new SendInvoiceSunatRepository(), new ParametrosRepository(), new Log4NetLogger());
            timer.Interval = dblDelayInSeconds;
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
        }

        public void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            Thread hiloEvento = new Thread(IniciarProcesoSendInvoiceSunat);
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
                _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.ERROR, "ERROR en SednInvoiceSunat", ex);
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
            _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.INFO, "Se detuvo el Proceso de Envio de factura sunat");
        }

        public void IniciarProcesoSendInvoiceSunat()
        {
            _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.INFO, "Inicio del servicio sendinvoicesunat");
            _ISendInvoiceSunatService.ProcesarEnvioCompranteElectronicoSunat(configuracionNumeroRegistrosObtener);
            _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.INFO, "Finalizó el servicio sendinvoicesunat");
        }
    }
}
