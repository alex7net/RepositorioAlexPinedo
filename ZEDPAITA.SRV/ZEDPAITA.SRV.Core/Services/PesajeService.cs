using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.ServiceInterfaces;
using ZEDPAITA.SRV.Core.Models.ServicioObtenerPesaje;
using ZEDPAITA.SRV.Core.Common;
using System.IO.Ports;

namespace ZEDPAITA.SRV.Core.Services
{
    public class PesajeService : IPesajeService
    {
        #region Properties
        
        #endregion

        #region Constructor
        public PesajeService()
        {
        }
        #endregion

        #region Operation
        public RespuestaProceso<ObtenerPesaje> ObtenerPesajeCargaPesada(ObtenerPesajeIn datosPesaje)
        {
            RespuestaProceso<ObtenerPesaje> response = new RespuestaProceso<ObtenerPesaje>();
            try
            {
                SPHandler spHandler = new SPHandler();
                spHandler.SetPort(datosPesaje.PUERTO, datosPesaje.VELOCIDAD_TRANSMISION, datosPesaje.TIEMPO_ESPERA);
                spHandler.Open();
                String obtenido = spHandler.ReadResponse();
                //String obtenido = String.Empty;
                spHandler.Close();

                
                //SerialPort puertobalanza = new SerialPort(datosPesaje.PUERTO, 9600, Parity.None, 8, StopBits.One);
                //puertobalanza.Open();
                //int bytes = 30;
                //char[] buffer = new char[bytes];
                //puertobalanza.Read(buffer, 0, bytes);
                //puertobalanza.DiscardInBuffer();
                //puertobalanza.Close();
                

                obtenido = obtenido.Replace("$", "");
                obtenido = obtenido.Replace("?", "");
                obtenido = (obtenido == string.Empty ? "0" : obtenido);
                int cantidad = obtenido.Length;
                int peso = 0;
                if (cantidad == 6)
                {
                    obtenido = obtenido.Substring(cantidad - 5, 5);
                }
                peso = Convert.ToInt32(obtenido);
                response.intCodigoRespuestaProceso = Enumerados.CodigoRespuestaProceso.ProcesadoExito;
                response.data = new ObtenerPesaje();
                response.data.intPeso = peso;
            }
            catch (Exception ex)
            {
                response.intCodigoRespuestaProceso = Enumerados.CodigoRespuestaProceso.ProcesadoError;
                response.strMessage = ex.Message;
                response.data = new ObtenerPesaje();
                response.data.intPeso = 0;
            }
            return response;
        }
        #endregion
    }
}
