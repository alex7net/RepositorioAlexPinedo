using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.Models.Base
{
    public class Entity
    {
        /// <summary>
        /// Metodo Enumerado de Caracteres
        /// </summary>
        /// <typeparam name="TValue">Tipo Valor</typeparam>
        /// <param name="propertiesId">Id Propiedad</param>
        /// <returns>Lista de Caracteres</returns>
        public static IEnumerable<string> GetPropertyName<TValue>(params Expression<Func<TValue, object>>[] propertiesId)
        {
            var result = propertiesId.Select(p => p.Body is UnaryExpression ? ((MemberExpression)((UnaryExpression)p.Body).Operand).Member.Name : ((MemberExpression)p.Body).Member.Name);
            return result;
        }
    }
}
