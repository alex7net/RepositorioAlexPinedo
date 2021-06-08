using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Common
{
    public class Enumerados
    {
        public static class CodigoRespuestaProceso
        {
            public const int ProcesadoExito = 0;
            public const int ProcesadoError = -1;
        }

        public static class TipoErrorAgenteImpresion
        {
            /// <summary>
            /// Mensaje de Error en metadata DataXml
            /// </summary>
            public const string ErrorDataXml = "El campo DATA_XML del Documento {0} de tipo {1} es nulo o está dañado.";
            /// <summary>
            /// Mensaje de Error en variables de configuración de impresión en número de registros.
            /// </summary>
            public const string ErrorVariablesConfiguracionImpresorNumeroRegistros = "Error: Variables de configuracion NumeroRegistrosObtener estan vacías o no existen en el archivo de configuración";
            /// <summary
            /// Mensaje de Error en variables de configuración de transferencia de datos en número de registros.
            /// </summary>
            public const string ErrorVariablesConfiguracionTransferenciaNumeroRegistros = "variables de configuracion NumeroRegistrosObtener y/o NumeroRegistrosInsertar estan vacias o no existen.";
            /// <summary>
            /// Mensaje de Error en variable de configuración de impresión ubicación de reportes.
            /// </summary>
            public const string ErrorVariableConfiguracionImpresorUbicacion = "Error: Variable de configuracion ubicacionReportes  está vacía o no existe en el archivo de configuración";
            /// <summary>
            /// Mensaje de Error en variable de configuración de márgenes.
            /// </summary>
            public const string ErrorVariableConfiguracionMargenes = "Error: Variables de márgenes del reporte estan vacías o no existen en el archivo de configuración";
            /// <summary>
            /// Mensaje de Error en variable de configuración de ubicación PDF.
            /// </summary>
            public const string ErrorVariableConfiguracionUbicacionPdf = "Error: Variable de configuracion ubicacionImpresionesPdf  está vacía o no existe en el archivo de configuración";
            /// <summary>
            /// Mensaje de Error en variable de configuración de ubicación Texto.
            /// </summary>
            public const string ErrorVariableConfiguracionUbicacionTexto = "Error: Variable de configuracion ubicacionImpresionesTxt  está vacía o no existe en el archivo de configuración";
            /// <summary>
            /// Mensaje de Error de ubicación PDF no existe.
            /// </summary>
            public const string ErrorUbicacionPdfNoExiste = "Error: la ubicación para impresiones PDF no se existe. Verifique por favor.";
            /// <summary>
            /// Mensaje de Error de ubicación Texto no existe.
            /// </summary>
            public const string ErrorUbicacionTextoNoExiste = "Error: la ubicación para impresiones Texto no existe. Verifique por favor.";
            /// <summary>
            /// Mensaje de Error de configuración de impresora genérica.
            /// </summary>
            public const string ErrorVariableConfiguracionGenericoImpresoraPdf = "Error: Variable de configuracion genericoImpresoraPdf  está vacía o no existe en el archivo de configuración";
            /// <summary>
            /// Mensaje de Error de variable de configuración de cores utilizados.
            /// </summary>
            public const string ErrorVariablesConfiguracionImpresorCoreUtilizados = "Error: Variable de configuracion coresUtilizar  está vacía o no existe en el archivo de configuración";
            /// <summary>
            /// Mensaje de Error de flujo de impresión.
            /// </summary>
            public const string ErrorReporteFlujo = "Error: No hay flujo para imprimir.";
            /// <summary>
            /// Mensaje de Error no encuentra impresora resgsrada en ADD.
            /// </summary>
            public const string NoEncuentraImpresora = "La impresora {0} no se encuentra registrada en ADD.";
            /// <summary>
            /// Mensaje de Error no encuentra reporte en blanco
            /// </summary>
            public const string NoEncuentraReporteBlancoFRX = "No se encuentra el nombre del reporte en blanco FRX.";
            /// <summary>
            /// Mensaje de Error no encuentra reporte en blanco
            /// </summary>
            public const string NoEncuentraEscalaReporteFRX = "No se encuentra escala impresión reporte FRX.";
            /// <summary>
            /// Mensaje de Error no se ha configurador el nombre del agente
            /// </summary>
            public const string NoSeHaConfiguradoNombreAgente = "No se ha configurado el nombre del agente";

        }

  
        public static class TipoErrorNotificacion
        {
            /// <summary>
            /// Mensaje de Error en metadata DataXml
            /// </summary>
            public const string ErrorDataXml = "El campo DATA_XML del Documento {0} de tipo {1} es nulo o está dañado.";
            /// <summary>
            /// Mensaje de Error en variables de configuración de impresión en número de registros.
            /// </summary>
            public const string ErrorVariablesConfiguracionImpresorNumeroRegistros = "Error: Variables de configuracion NumeroRegistrosObtener estan vacías o no existen en el archivo de configuración";
            /// <summary
            /// Mensaje de Error en variables de configuración de transferencia de datos en número de registros.
            /// </summary>
            public const string ErrorVariablesConfiguracionTransferenciaNumeroRegistros = "variables de configuracion NumeroRegistrosObtener y/o NumeroRegistrosInsertar estan vacias o no existen.";
            /// <summary>
            /// Mensaje de Error en variable de configuración de impresión ubicación de reportes.
            /// </summary>
            public const string ErrorVariableConfiguracionImpresorUbicacion = "Error: Variable de configuracion ubicacionReportes  está vacía o no existe en el archivo de configuración";
            /// <summary>
            /// Mensaje de Error en variable de configuración de márgenes.
            /// </summary>
            public const string ErrorVariableConfiguracionMargenes = "Error: Variables de márgenes del reporte estan vacías o no existen en el archivo de configuración";
            /// <summary>
            /// Mensaje de Error en variable de configuración de ubicación PDF.
            /// </summary>
            public const string ErrorVariableConfiguracionUbicacionPdf = "Error: Variable de configuracion ubicacionImpresionesPdf  está vacía o no existe en el archivo de configuración";
            /// <summary>
            /// Mensaje de Error en variable de configuración de ubicación Texto.
            /// </summary>
            public const string ErrorVariableConfiguracionUbicacionTexto = "Error: Variable de configuracion ubicacionImpresionesTxt  está vacía o no existe en el archivo de configuración";
            /// <summary>
            /// Mensaje de Error de ubicación PDF no existe.
            /// </summary>
            public const string ErrorUbicacionPdfNoExiste = "Error: la ubicación para impresiones PDF no se existe. Verifique por favor.";
            /// <summary>
            /// Mensaje de Error de ubicación Texto no existe.
            /// </summary>
            public const string ErrorUbicacionTextoNoExiste = "Error: la ubicación para impresiones Texto no existe. Verifique por favor.";
            /// <summary>
            /// Mensaje de Error de configuración de impresora genérica.
            /// </summary>
            public const string ErrorVariableConfiguracionGenericoImpresoraPdf = "Error: Variable de configuracion genericoImpresoraPdf  está vacía o no existe en el archivo de configuración";
            /// <summary>
            /// Mensaje de Error de variable de configuración de cores utilizados.
            /// </summary>
            public const string ErrorVariablesConfiguracionImpresorCoreUtilizados = "Error: Variable de configuracion coresUtilizar  está vacía o no existe en el archivo de configuración";
            /// <summary>
            /// Mensaje de Error de flujo de impresión.
            /// </summary>
            public const string ErrorReporteFlujo = "Error: No hay flujo para imprimir.";
            /// <summary>
            /// Mensaje de Error no encuentra impresora resgsrada en ADD.
            /// </summary>
            public const string NoEncuentraImpresora = "La impresora {0} no se encuentra registrada en ADD.";
            /// <summary>
            /// Mensaje de Error no encuentra reporte en blanco
            /// </summary>
            public const string NoEncuentraReporteBlancoFRX = "No se encuentra el nombre del reporte en blanco FRX.";
            /// <summary>
            /// Mensaje de Error no encuentra reporte en blanco
            /// </summary>
            public const string NoEncuentraEscalaReporteFRX = "No se encuentra escala impresión reporte FRX.";
            /// <summary>
            /// Mensaje de Error no se ha configurador el nombre del agente
            /// </summary>
            public const string NoSeHaConfiguradoNombreAgente = "No se ha configurado el nombre del agente";
            /// <summary>
            /// Mensaje de Error no se ha configurado el número de registros obtener
            /// </summary>
            public const string ErrorConfiguracionNumeroRegistrosObtener = "No se ha configurado el número de registros a obtener notificación";
            /// <summary>
            /// Mensaje de Error no se ha configurado el usuario del servicio de notificacion
            /// </summary>
            public const string ErrorConfiguracionUsuarioServicio = "No se ha configurado el usuario del Servicio de Notificación";
        }


        /// <summary>
        /// Clase que contiene constantes de log.
        /// </summary>
        public static class TipoLog
        {
            /// <summary>
            /// Constante de log general.
            /// </summary>
            public const string General = "General";
            /// <summary>
            /// Constante de log Data Access.
            /// </summary>
            public const string DataAccess = "Data Access";
            /// <summary>
            /// Constante de log impresión.
            /// </summary>
            public const string Impresion = "Impresion";
            /// <summary>
            /// Constante de log EnvíoEmail.
            /// </summary>
            public const string SendEmail = "SendEmail";
            /// <summary>
            /// Constante de log Notificacion.
            /// </summary>
            public const string Notificacion = "NotificacionInformacion";
            /// <summary>
            /// Constante de log Notificacion.
            /// </summary>
            public const string GenerateLegalDocument = "GenerateLegalDocumentooInformacion";

        }

        /// <summary>
        /// Clase que contiene los tipo documento imprimir transaccion
        /// </summary>
        public static class TipoDocumentoImprimirTransaccion
        {
            /// <summary>
            /// Tipo documento ticket
            /// </summary>
            public const int Ticket = 1;            
        }

        /// <summary>
        /// Estados activo
        /// </summary>
        public static class EstadosActivo
        {
            /// <summary>
            /// Estado activo
            /// </summary>
            public const int Activo = 1;
            /// <summary>
            /// Estado Inactivo
            /// </summary>
            public const int Desactivado = 1;
        }

        /// <summary>
        /// Estado Anulado
        /// </summary>
        public static class EstadoAnulado
        {
            /// <summary>
            /// Estado Anulado
            /// </summary>
            public const bool Anulado = true;
            /// <summary>
            /// Estado No Anulado
            /// </summary>
            public const bool NoAnulado = false;
        }

        /// <summary>
        /// Indicador Generación de PDF
        /// </summary>
        public static class IndicadorGenerarPDF
        {
            /// <summary>
            /// Generar PDF
            /// </summary>
            public const String GenerarPDF = "S";
            /// <summary>
            /// No Generar PDF
            /// </summary>
            public const String NoGenerarPDF = "N";

        }


        /// <summary>
        /// Indicador Tipo control
        /// </summary>
        public static class TipoControl
        {
            /// <summary>
            /// GENERACION DE PDF MANUFACTURA
            /// </summary>
            public const int GenerarPDFManufactura = 10;
        }

        public static class CodigoCarpetasServidor
        {
            /// <summary>
            /// Codigo Para generación de PDF Manufactura
            /// </summary>
            public const String GenerarPDFCEM = "CEM";
        }

        public struct ValidacionCarpeta
        {
            public const Int32 UsuarioContraseniaNoSonCorrectos = -1;
            public const Int32 ExisteCarpeta = 1;
            public const Int32 NoExisteCarpeta = 2;
            public const Int32 OcurrioUnError = 0;
        }

        public static class IndicadorEnviarEmail
        {
            /// <summary>
            /// Enviar por email
            /// </summary>
            public const String EnviarEmail = "S";
            /// <summary>
            /// No enviar por email
            /// </summary>
            public const String NoEnviarEmail = "N";
        }

        /// <summary>
        /// Clase de Formato Archivo
        /// </summary>
        public static class FormatoArchivo
        {
            /// <summary>
            /// Constante de formato PDF
            /// </summary>
            public const string FormatoPDF = "pdf";

            /// <summary>
            /// Constante de formato texto
            /// </summary>
            public const string FormatoTexto = "txt";
        }

        /// <summary>
        /// Clase de Formato Archivo
        /// </summary>
        public static class Servicios
        {
            /// <summary>
            /// Servicio Notificacion
            /// </summary>
            public const string WSNotificacion = "WSNotificacion";

            /// <summary>
            /// Servicio Agente
            /// </summary>
            public const string WSAgente = "WSAgenteImpresion";
        }

        /// <summary>
        /// Permite obtener el HostName del terminal local
        /// </summary>
        /// <returns>Nombre de HostName</returns>
        public static string ObtenerHostNamePC()
        {
            string hostname = Dns.GetHostName();
            return hostname;
        }

        public enum Parametro
        {
            ConfiguracionLEnvioComprobantElectronico = 1,
            ConfiguracionRutaTipoCambio = 2,
            HoraEjecucionGenerateLegalDocumet = 3,
            ConfiguracionLGeneracionComprobantElectronico = 4,
        }


        public static class NombreCadenaConexion
        {
            public const string ConexionGestion = "CadenaGestionERP";
            public const string ConexionERP = "CadenaConexionERP";
            public const string ConexionFE = "CadenaFE";
        }

        public static class IndicadorConversion
        {
            public const bool SI = true;
            public const bool NO = false;
        }

        public static class EstadoEnvio
        {
            public const Int32 Enviado = 1;
            public const Int32 NoEnviado = 0;
        }

        public struct EstadoCertificadoManufactura
        {
            public const Int32 EnProceso = 1;
            public const Int32 Rechazado = 2;
            public const Int32 Aprobado = 3;
        }
    }
}
