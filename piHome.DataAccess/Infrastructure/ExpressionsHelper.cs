using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace piHome.DataAccess.Infrastructure
{
    public static class ExpressionsHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> action)
        {
            var body = action.Body as MemberExpression;

            if (body == null)
            {
                var ubody = (UnaryExpression)action.Body;
                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
        }
    }
}
