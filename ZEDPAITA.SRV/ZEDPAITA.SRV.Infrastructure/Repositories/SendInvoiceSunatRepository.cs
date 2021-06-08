using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDPAITA.SRV.Core.Models.EnvioFacturaSunat;
using ZEDPAITA.SRV.Core.RepositoryInterfaces;
using ZEDPAITA.SRV.Infrastructure.Base;
using ZEDPAITA.SRV.Infrastructure.DataAccess;

namespace ZEDPAITA.SRV.Infrastructure.Repositories
{
    public class SendInvoiceSunatRepository : ISendInvoiceSunatRepository
    {

        public List<EnvioFacturaSunatDTO> obtenerCodigoEnviar(Int32 Dias, Int32 Limite, string Conexion)
        {
            List<EnvioFacturaSunatDTO> listaDocumentos = new List<EnvioFacturaSunatDTO>();

            try
            {
                BaseDatos baseDatos = new BaseDatos(Conexion);

                using (DataTable datos = baseDatos.EjecutarConsultaDataTable("USP_OBTENER_DOCUMENTOS_ENVIAR_GOBIERNO", CommandType.StoredProcedure,
                    baseDatos.CrearParametro("Dias", MySqlDbType.Int32, Dias),
                    baseDatos.CrearParametro("Top", MySqlDbType.Int32, Limite)))
                {

                    listaDocumentos = DataMapper.DatatableToList<EnvioFacturaSunatDTO>(datos).ToList();
                }
            }
            catch (MySqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return listaDocumentos;
        }


    }
}
