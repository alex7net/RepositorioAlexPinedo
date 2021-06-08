using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Infrastructure.Base
{
    public class DataMapper
    {
        protected DataMapper() { }
        /// <summary>
        /// Permite convertir un objeto DataTable a objeto List según el tipo.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto resultante</typeparam>
        /// <param name="Table">Tabla que se desea convertir</param>
        /// <returns>Lista genérica de objetos de tipo resultante</returns>
        public static IList<T> DatatableToList<T>(DataTable Table) where T : class, new()
        {
            if (Table == null || Table.Rows.Count == 0)
                return new List<T>();

            Type classType = typeof(T);
            IList<PropertyInfo> propertyList = classType.GetProperties();

            if (propertyList.Count == 0)
                return new List<T>();

            List<string> columnNames = Table.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();

            List<T> result = new List<T>();
            try
            {
                foreach (DataRow row in Table.Rows)
                {
                    T classObject = new T();
                    foreach (PropertyInfo property in propertyList)
                    {
                        if (property != null && property.CanWrite && columnNames.Contains(property.Name) && (row[property.Name] != System.DBNull.Value))
                        {
                            object propertyValue = System.Convert.ChangeType(
                                              row[property.Name],
                                              property.PropertyType
                                          );
                            property.SetValue(classObject, propertyValue, null);
                        }
                    }
                    result.Add(classObject);
                }
                return result;
            }
            catch(Exception ex)
            {
                return new List<T>();
            }
        }

    }
}
