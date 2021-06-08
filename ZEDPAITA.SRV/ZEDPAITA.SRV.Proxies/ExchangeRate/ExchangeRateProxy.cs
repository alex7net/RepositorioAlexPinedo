using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Proxies.ExchangeRate
{
    public class ExchangeRateProxy : IExchangeRateProxy
    {
        public ExchangeRateResponse ConsultaExchangeRateProxy(ExchangeRateRequest datos)
        {
            ExchangeRateResponse response = null;
            try
            {
                HttpWebRequest httpWebRequest = WebRequest.Create(datos.Ruta + "/" + datos.FechaConsulta) as HttpWebRequest;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization", "Bearer " + datos.Token);
                httpWebRequest.Method = "GET";

                //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                //{
                //    streamWriter.Flush();
                //    streamWriter.Close();
                //}

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();//ESPERA RESPUESTA DE SUNAT
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    dynamic jsonObj = JsonConvert.DeserializeObject(result);

                    var sale = jsonObj["sale"].ToString();
                    response = new ExchangeRateResponse();
                    response.TipoCambioVenta = Convert.ToDecimal(sale);
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
