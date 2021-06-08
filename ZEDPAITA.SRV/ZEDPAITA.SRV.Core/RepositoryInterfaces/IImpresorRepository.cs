using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Models.Impresor;

namespace ZEDPAITA.SRV.Core.RepositoryInterfaces
{
    public interface IImpresorRepository
    {
        List<DocumentoImprimirDTO> obtenerDocumentos(String Agente, Int32 Filas);
        MovimientoCabceraDTO obtenerMovimientoCabecera(Int64 CodigoMovimiento);
        List<MovimientoDetalleDTO> obtenerMovimientoDetalle(Int64 CodigoMovimiento);
        List<MovimientoDocumentoDTO> obtenerMovimientoDocumento(Int64 CodigoMovimiento);
        bool actualizaDocumentoImprimir(Int64 CodigoDocumentoImprimir, String Usuario, String Terminal);
    }
}
