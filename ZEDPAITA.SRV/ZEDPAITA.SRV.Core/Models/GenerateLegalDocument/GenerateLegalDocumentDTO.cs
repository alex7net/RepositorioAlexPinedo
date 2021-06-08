using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Models.GenerateLegalDocument
{
    public class GenerateLegalDocumentDTO
    {
        public Int32 CODIGO_DOCUMENTO_LEGAL { get; set; }
        public Int32 CODIGO_DOCUMENTO_LEGAL_PROGRAMADO { get; set; }
        public string SERIE_DOCUMENTO { get; set; }
        public string NUMERO_DOCUMENTO { get; set; }
        public DateTime FECHA_EMISION { get; set; }
        public string HORA_EMISION { get; set; }
        public string CODIGO_TIPO_OPERACION { get; set; }
        public string CODIGO_TIPO_DOCUMENTO { get; set; }
        public string CODIGO_TIPO_MONEDA { get; set; }
        public DateTime FECHA_VENCIMIENTO { get; set; }
        public string NUMERO_ORDEN_COMPRA { get; set; }
        public string CODIGO_PAIS_EMISOR { get; set; }
        public string UBIGEO_EMISOR { get; set; }
        public string DIRECCION_EMISOR { get; set; }
        public string CORREO_ELECTRONICO_EMISOR { get; set; }
        public string TELEFONO_EMISOR { get; set; }
        public string CODIGO_DOMICILIO_FISCAL_EMISOR { get; set; }
        public string CODIGO_TIPO_DOCUMENTO_IDENTIDAD_CLIENTE { get; set; }
        public string NUMERO_DOCUMENTO_CLIENTE { get; set; }
        public string APELLIDOS_RAZON_SOCIAL_CLIENTE { get; set; }
        public string CODIGO_PAIS_CLIENTE { get; set; }
        public string UBIGEO_CLIENTE { get; set; }
        public string DIRECCION_CLIENTE { get; set; }
        public string CORREO_ELECTRONICO_CLIENTE { get; set; }
        public string TELEFONO_CLIENTE { get; set; }
        public decimal TOTAL_EXPORTACION { get; set; }
        public decimal TOTAL_OPERACIONES_GRAVADAS { get; set; }
        public decimal TOTAL_OPERACIONES_INAFECTAS { get; set; }
        public decimal TOTAL_OPERACIONES_EXONERADAS { get; set; }
        public decimal TOTAL_OPERACIONES_GRATUITAS { get; set; }
        public decimal TOTAL_IGV { get; set; }
        public decimal TOTAL_IMPUESTOS { get; set; }
        public decimal TOTAL_VALOR { get; set; }
        public decimal TOTAL_VENTA { get; set; }
        public string CODIGO_INTERNO { get; set; }
        public string DESCRIPCION { get; set; }
        public string CODIGO_PRODUCTO_SUNAT { get; set; }
        public string CODIGO_PRODUCTO_GSL { get; set; }
        public string UNIDAD_MEDIDA { get; set; }
        public Int32 CANTIDAD { get; set; }
        public decimal VALOR_UNITARIO { get; set; }
        public string CODIGO_TIPO_PRECIO { get; set; }
        public decimal PRECIO_UNITARIO { get; set; }
        public string CODIGO_TIPO_AFECTACION_IGV { get; set; }
        public decimal TOTAL_BASE_IGV { get; set; }
        public decimal PORCENTAJE_IGV { get; set; }
        public decimal TOTAL_IGV_DETALLE { get; set; }
        public decimal TOTAL_IMPUESTOS_DETALLE { get; set; }
        public decimal TOTAL_VALOR_ITEM { get; set; }
        public decimal TOTAL_ITEM { get; set; }
        public string INFORMACION_ADICIONAL { get; set; }
        public bool ENVIAR_EMAIL { get; set; }
        public bool ENVIAR_XML_FIRMADO { get; set; }
        public Int32 CODIGO_TIPO_CONCEPTO { get; set; }
        public bool INDICADOR_CONVERSION { get; set; }
    }
}
