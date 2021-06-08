using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using ZEDPAITA.SRV.Proxies.SendInvoiceSunat;

namespace ZEDPAITA.SRV.Proxy.SendInvoiceSunat
{
    public class SendInvoiceSunatProxy : ISendInvoiceSunatProxy
    {
        public void EnviarCodigoComprobanteElectronico(string token, string ruta, string codigo)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(ruta) as HttpWebRequest;
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + token);
                request.Method = "POST";
                request.Accept = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    //string json = "{\r\n \"external_id\": \"" + codigo + "\"\r\n}";

                    var jsonstr = new SendinvoiceRequest
                    {
                        external_id = codigo
                    };

                    string json = JsonConvert.SerializeObject(jsonstr);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    dynamic jsonObj = JsonConvert.DeserializeObject(result);
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
