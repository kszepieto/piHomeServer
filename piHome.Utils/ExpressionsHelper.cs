using System;
using System.Linq.Expressions;

namespace piHome.Utils
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
