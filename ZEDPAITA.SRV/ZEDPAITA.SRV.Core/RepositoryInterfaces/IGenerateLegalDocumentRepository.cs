using System;
using System.Collections.Generic;
using ZEDPAITA.SRV.Core.Models.GenerateLegalDocument;

namespace ZEDPAITA.SRV.Core.RepositoryInterfaces
{
    public interface IGenerateLegalDocumentRepository
    {
        List<GenerateLegalDocumentDTO> obtenerDocumentosGenerados();
        void insertarArchivoFacturacionElectronica(string Factura, string Ruta, string Json, string JsonError, Int32 FlagSunat);
        void insertarVentas(GenerateLegaDocumentVentasDTO ventas);
        void insertarItemsV(GenerateLegalDocumentDetalleDTO detalle);
        void ActualizarEstadoEnviadoDocumentoLegalemision(Int32 CorrelativoDoucumentoLegal, string Descripcion, decimal TipoCambioVenta, decimal TotalConvertido, Int32 EstadoEnvio, Int32 NumeroFacturaSerie);
        void ActualizaFechaSiguienteEjecucion(Int32 CodigoParametro, Int32 CodigoSeccion, string Valor);
    }
}
