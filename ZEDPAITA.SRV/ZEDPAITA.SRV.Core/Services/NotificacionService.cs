using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ZEDPAITA.SRV.Core.Common;

using ZEDPAITA.SRV.Core.RepositoryInterfaces;
using ZEDPAITA.SRV.Core.ServiceInterfaces;
using ZEDPAITA.SRV.FileManager;
using ZEDPAITA.SRV.Util.Impresion;
using ZEDPAITA.SRV.Util.LoggingInterface;

namespace ZEDPAITA.SRV.Core.Services
{
    public class NotificacionService : INotificacionService
    {
        #region Properties
        /// <summary>
        /// Gestiona el log de la clase.
        /// </summary>   
        private readonly ILogger _logger;

        /// <summary>
        /// Almacena la interfaz de repositorio de impresión.
        /// </summary>
        private readonly INotificacionRepository _INotificacionRepository;
        private ICustomFileManager _ICustomFileManager;
        #endregion

        #region Constructor
        public NotificacionService(INotificacionRepository INotificacionRepository, ILogger logger)
        {
            _INotificacionRepository = INotificacionRepository;
            _logger = logger;
        }
        #endregion

        #region Operations
        public void ProcesarNotificacion(ConfiguracionNotificacion configuracionNotificacion )
        {
            try
            {
                var documentosProcesar = _INotificacionRepository.obtenerDocumentosNotificacion(configuracionNotificacion.configuracionNumeroRegistrosObtener, configuracionNotificacion.UsuarioServicioNotificacion);
                _ICustomFileManager = new CustomFileManager();
                foreach (var item in documentosProcesar)
                {
                    string archivoPdf = null;
                    string rutaPdfCertiManufactura = null;
                    List<string> listaArchivos = new List<string>();

                    #region GenerarPDF
                    if (item.GENERAR_PDF == Enumerados.IndicadorGenerarPDF.GenerarPDF)
                    {
                        int responseValidacionCarpeta = 0;
                        Reporteador reporteador = new Reporteador();
                        DataSet DsMetaData = _INotificacionRepository.ObtenerConjuntoDatosXml(item.TRAMA);

                        if (item.CODIGO_TIPO_CONTROL == Enumerados.TipoControl.GenerarPDFManufactura)
                        {
                            string archivoReporte = string.Format(@"{0}\{1}", configuracionNotificacion.UbicacionReportes, item.RLDC);
                            var obtenerCarpetaServidor = _INotificacionRepository.obtenerCarpetaServidor(Enumerados.CodigoCarpetasServidor.GenerarPDFCEM).FirstOrDefault();
                            NetworkCredential credentials = new NetworkCredential(obtenerCarpetaServidor.USUARIO_CONEXION, obtenerCarpetaServidor.CONTRASENA_CONEXION);

                            string ubicacionArchivoEmpresa = obtenerCarpetaServidor.RUTA_COMPLETA + @"\" + item.CARPETA;
                            rutaPdfCertiManufactura = obtenerCarpetaServidor.RUTA_HOST + @"\" + item.CARPETA;

                            responseValidacionCarpeta = _ICustomFileManager.ValidateDirectoryExists(credentials, obtenerCarpetaServidor.RUTA_COMPLETA, ubicacionArchivoEmpresa);
                            if (responseValidacionCarpeta == Enumerados.ValidacionCarpeta.NoExisteCarpeta)
                            {
                                Directory.CreateDirectory(ubicacionArchivoEmpresa);
                            }

                            archivoPdf = string.Format(@"{0}\{1}.{2}", ubicacionArchivoEmpresa, item.CODIGO_INGRESO, Enumerados.FormatoArchivo.FormatoPDF);
                            rutaPdfCertiManufactura = string.Format(@"{0}\{1}.{2}", rutaPdfCertiManufactura, item.CODIGO_INGRESO, Enumerados.FormatoArchivo.FormatoPDF);

                            if (File.Exists(archivoPdf))
                            {
                                DateTime fecha = DateTime.Now;
                                var fechaAhora = fecha.ToString("ddMMyyyyHHmmss");
                                var nuevoArchivo = string.Format(@"{0}\{1}.{2}", ubicacionArchivoEmpresa, item.CODIGO_INGRESO + fechaAhora, Enumerados.FormatoArchivo.FormatoPDF);
                                _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.INFO, "Nuevo Archivo " + nuevoArchivo);
                                File.Move(archivoPdf, nuevoArchivo);
                            }

                            reporteador.ConvertirReportePdf(archivoPdf, archivoReporte, DsMetaData);
                            _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.INFO, "Se genera el PDF del código: " + item.CODIGO_INGRESO);
                            listaArchivos.Add(archivoPdf);
                        }
                        else
                        {
                            string archivoReporte = string.Format(@"{0}\{1}", configuracionNotificacion.UbicacionReportes, item.RLDC);
                            string ubicacionImpresion = Path.Combine(configuracionNotificacion.UbicacionImpresionesPdf, item.CARPETA);

                            if (!Directory.Exists(ubicacionImpresion))
                            {
                                Directory.CreateDirectory(ubicacionImpresion);
                            }
                            archivoPdf = string.Format(@"{0}\{1}.{2}", ubicacionImpresion, item.CODIGO_INGRESO, Enumerados.FormatoArchivo.FormatoPDF);

                            if (File.Exists(archivoPdf))
                            {
                                DateTime fecha = DateTime.Now;
                                var fechaAhora = fecha.ToString("ddMMyyyyHHmmss");
                                var nuevoArchivo = string.Format(@"{0}\{1}.{2}", ubicacionImpresion, item.CODIGO_INGRESO + fechaAhora, Enumerados.FormatoArchivo.FormatoPDF);
                                _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.INFO, "Nuevo Archivo " + nuevoArchivo);
                                File.Move(archivoPdf, nuevoArchivo);
                            }

                            reporteador.ConvertirReportePdf(archivoPdf, archivoReporte, DsMetaData);
                            _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.INFO, "Se genera el PDF del código: " + item.CODIGO_INGRESO);
                            listaArchivos.Add(archivoPdf);
                        }
                    }
                    #endregion
                    
                    #region EnviarEmail
                    if (item.ENVIAR_EMAIL == Enumerados.IndicadorEnviarEmail.EnviarEmail)
                    {
                        var tramaXmlEmail = item.TRAMA_EMAIL;
                        MailMessage Email;
                        try
                        {
                            Email = new MailMessage();
                            XElement xmldoc = XElement.Parse(tramaXmlEmail);
                            Email.IsBodyHtml = Convert.ToBoolean(xmldoc.Element("eshtml").Value);
                            Email.From = new MailAddress(xmldoc.Element("de").Value);
                            Email.Body = xmldoc.Element("mensaje").Value;
                            Email.Subject = xmldoc.Element("asunto").Value;
                            Email.BodyEncoding = Encoding.UTF8;
                            Email.Priority = MailPriority.Normal;
                            char caracter = ';';
                            string[] Emailsto = xmldoc.Element("para").Value.Split(caracter);

                            foreach (var emailto in Emailsto)
                            {
                                Email.To.Add(emailto.Trim());
                            }

                            if (xmldoc.Element("concopia").Value != "")
                            {
                                string[] EmailsCC = xmldoc.Element("concopia").Value.Split(caracter);
                                foreach (var emailCC in EmailsCC)
                                {
                                    Email.CC.Add(emailCC.Trim());
                                }
                            }

                            if (listaArchivos.Count() > 0)
                            {
                                foreach (var a in listaArchivos)
                                {
                                    if (File.Exists(a))
                                    {
                                        Email.Attachments.Add(new Attachment(a));
                                    }
                                }
                            }

                            SmtpClient smtpMail = new SmtpClient(xmldoc.Element("host").Value)
                            {
                                EnableSsl = true,
                                UseDefaultCredentials = true,
                                Host = xmldoc.Element("host").Value,
                                Port = Convert.ToInt32(xmldoc.Element("puerto").Value),
                                Credentials = new System.Net.NetworkCredential( xmldoc.Element("de").Value, xmldoc.Element("contrasenia").Value),
                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                Timeout = 15000
                            };


                            smtpMail.Send(Email);
                            smtpMail.Dispose();
                            Email.Dispose();
                            _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.INFO, "Se Notificó correo con código: " + item.CODIGO_INGRESO);
                        }
                        catch (Exception ex)
                        {
                            _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.ERROR, "ERROR al enviar Email", ex);
                        }
                    }
                    #endregion

                    #region Actualizar Codigo
                    _INotificacionRepository.actualizaDocumentoNotificacion(item.CODIGO_MOVIMIENTO_NOTIFICACION, Enumerados.Servicios.WSNotificacion, Enumerados.Servicios.WSNotificacion);

                    if (item.CODIGO_TIPO_CONTROL == Enumerados.TipoControl.GenerarPDFManufactura)
                    {

                        _INotificacionRepository.actualizaCertificadoManufactura(
                            rutaPdfCertiManufactura, 
                            item.CODIGO_INGRESO, 
                            Enumerados.EstadoCertificadoManufactura.Aprobado,
                            Enumerados.ObtenerHostNamePC(),
                            Enumerados.Servicios.WSNotificacion,
                            item.CODIGO_CERTIFICADO_MANUFACTURA
                            );
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                _logger.WriteMessage(Enumerados.TipoLog.Notificacion, LogLevel.ERROR, "ERROR en ProcesarNotificacion" + ex.InnerException, ex);
                throw ex;
            }
        }
        #endregion
    }
}
