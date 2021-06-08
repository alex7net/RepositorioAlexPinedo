using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Models.CarpetaServidor;
using ZEDPAITA.SRV.Core.Models.Impresor;

namespace ZEDPAITA.SRV.Core.RepositoryInterfaces
{
    public interface INotificacionRepository
    {
        List<DocumentoNotificacionDTO> obtenerDocumentosNotificacion(Int32 Filas, String Usuario);
        bool actualizaDocumentoNotificacion(Int64 CodigoDocumentoNotificacion, String Usuario, String Terminal);
        DataSet ObtenerConjuntoDatosXml(string datosXml);
        List<CarpetaServidorDTO> obtenerCarpetaServidor(string CodigoCarpeta);
        bool actualizaCertificadoManufactura(string RutaPDF, string NombreCertificado, Int32 EstadoAutorizado, string TerminalModificacion, string UsuarioModificacion, Int32 CodigoCertificadoManufactura);
    }
}
