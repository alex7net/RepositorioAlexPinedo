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

namespace ZEDPAITA.SRV.Presentacion.WSGenerateLegalDocument
{
    public partial class WSGenerateLegalDocument : ServiceBase
    {
        #region Properties

        readonly double dblDelayInSeconds = Convert.ToDouble(ConfigurationManager.AppSettings["IntervaloEjecucionGenerateLegalDocument"].ToString());
        readonly private int IntervaloVerificacionHilo = Convert.ToInt32(ConfigurationManager.AppSettings["IntervaloVerificacionHilo"]);
        private readonly ILogger _logger;
        readonly System.Timers.Timer timer = new System.Timers.Timer();

        private readonly IGenerateLegalDocumentService _IGenerateLegalDocumentService;
        #endregion

        public WSGenerateLegalDocument()
        {
            InitializeComponent();
            _logger = new Log4NetLogger();
            _IGenerateLegalDocumentService = new GenerateLegalDocumentService(new GenerateLegalDocumentRepository(), new ParametrosRepository(), new Log4NetLogger());
            timer.Interval = dblDelayInSeconds;
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
        }

        public void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            Thread hiloEvento = new Thread(ValidarGenerarDocumentoLegal);
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
                _logger.WriteMessage(Enumerados.TipoLog.GenerateLegalDocument, LogLevel.ERROR, "ERROR en WSGenerateLegalDocument", ex);
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
            _logger.WriteMessage(Enumerados.TipoLog.GenerateLegalDocument, LogLevel.INFO, "Se detuvo el Proceso de Envio de WSGenerateLegalDocument");
        }

        public void ValidarGenerarDocumentoLegal()
        {
            try
            {
                string FechaEjecucion = _IGenerateLegalDocumentService.ConsultaTiempoEjecucion();
                string FechaActual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
                if (FechaEjecucion == FechaActual)
                {
                    EjecutarGenerarDocumentoLegal();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteMessage(Enumerados.TipoLog.GenerateLegalDocument, LogLevel.ERROR, ex.ToString());
            }
        }

        public void EjecutarGenerarDocumentoLegal()
        {
            try
            {
                _logger.WriteMessage(Enumerados.TipoLog.GenerateLegalDocument, LogLevel.INFO, "Inicio del servicio GenerateLegalDocument");
                _IGenerateLegalDocumentService.ProcesarGenerateLegalDocument();
                _logger.WriteMessage(Enumerados.TipoLog.GenerateLegalDocument, LogLevel.INFO, "Finalizó el servicio GenerateLegalDocument");
            }
            catch (Exception ex)
            {
                _logger.WriteMessage(Enumerados.TipoLog.GenerateLegalDocument, LogLevel.ERROR, ex.ToString());
            }
        }
    }
}
