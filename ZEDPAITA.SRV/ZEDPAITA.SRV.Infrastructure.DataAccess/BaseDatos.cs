using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Infrastructure.DataAccess
{
    public class BaseDatos
    {
        private static string _nombreConexion;

        public BaseDatos(string nombreConexion)
        {
            _nombreConexion = nombreConexion;
        }

        private string GetConectionString()
        {
            return ConfigurationManager.ConnectionStrings[_nombreConexion].ConnectionString;
        }
        
        public MySqlParameter CrearParametro(string ParameterName, MySqlDbType SqlDbType, Object Value)
        {
            try
            {
                var sqlParametro = new MySqlParameter
                {
                    ParameterName = string.Concat("@", ParameterName),
                    Direction = System.Data.ParameterDirection.Input,
                    MySqlDbType = SqlDbType,
                    Value = Value
                };
                return sqlParametro;
            }
            catch (MySqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public MySqlParameter CrearParametroSalida(string ParameterName, Object Value)
        {
            try
            {
                var sqlParametro = new MySqlParameter
                {
                    ParameterName = string.Concat("@", ParameterName),
                    Direction = System.Data.ParameterDirection.Output,
                    Value = Value
                };
                return sqlParametro;
            }
            catch (MySqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int EjecutarConsulta(string consulta, System.Data.CommandType tipoComando, params object[] parametros)
        {
            int resultado = -1;
            MySqlConnection conexion = new MySqlConnection(GetConectionString());
            try
            {
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.CommandType = tipoComando;
                    if (parametros != null)
                    {
                        comando.Parameters.AddRange(parametros);
                    }
                    comando.CommandTimeout = 360;
                    if (conexion.State == System.Data.ConnectionState.Closed)
                    {
                        conexion.Open();
                    }
                    resultado = comando.ExecuteNonQuery();
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
            finally
            {
                conexion.Close();
            }
            return resultado;
        }

        public System.Data.DataTable EjecutarConsultaDataTable(string consulta, System.Data.CommandType tipoComando, params object[] parametros)
        {
            MySqlConnection conexion = new MySqlConnection(GetConectionString());
            System.Data.DataTable tabla = new System.Data.DataTable();
            try
            {
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.CommandType = tipoComando;
                    if (parametros != null)
                    {
                        comando.Parameters.AddRange(parametros);
                    }
                    comando.CommandTimeout = 360;
                    if (conexion.State == System.Data.ConnectionState.Closed)
                    {
                        conexion.Open();
                    }
                    using (var da = new MySqlDataAdapter(comando))
                    {
                        da.Fill(tabla);
                    }
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
            finally
            {
                conexion.Close();
            }
            return tabla;
        }
    }
}
