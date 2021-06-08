using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Proxies.GenerateLegalDocument
{
    public class GenerateLegalDocumentProxy : IGenerateLegalDocumentProxy
    {

        public SendInvoiceGenerateResponse EnviarDocumentoLegalProxy(SendInvoiceGenerateRequest trama, string token, string ruta) 
        {
            SendInvoiceGenerateResponse response = null;
            try
            {
                HttpWebRequest httpWebRequest = WebRequest.Create(ruta) as HttpWebRequest;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
                httpWebRequest.Method = "POST";
                httpWebRequest.Accept = "application/json";

                string json = JsonConvert.SerializeObject(trama);

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();//ESPERA RESPUESTA DE SUNAT
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    dynamic jsonObj = JsonConvert.DeserializeObject(result);

                    var Message = jsonObj["success"].ToString();
                    var Document = jsonObj["links"].ToString();
                    var xml = jsonObj["links"]["xml"].ToString();
                    var pdf = jsonObj["links"]["pdf"].ToString();
                    var factura = jsonObj["data"]["number"].ToString();
                    string[] facturasplit = factura.Split('-');

                    response = new SendInvoiceGenerateResponse();
                    response.PDF = pdf;
                    response.FACTURA = "F" + "-" + facturasplit[0] + "-" +facturasplit[1].PadLeft(6, '0');
                    response.JSON = json;
                    response.NUMERO_FACTURA = facturasplit[1].PadLeft(6, '0');
                    response.NUMERO_FACURA_SERIE = Convert.ToInt32(facturasplit[1]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}
