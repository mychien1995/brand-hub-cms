using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BrandHub.Utilities.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TSource> OrderByProperty<TSource>(this IQueryable<TSource> source, string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource));
            Expression orderByProperty = Expression.Property(parameter, propertyName);

            LambdaExpression lambda = Expression.Lambda(orderByProperty, new[] { parameter });
            MethodInfo genericMethod = OrderByMethod.MakeGenericMethod
                (new[] { typeof(TSource), orderByProperty.Type });
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<TSource>)ret;
        }

        private static readonly MethodInfo OrderByMethod =
        typeof(Queryable).GetMethods()
            .Where(method => method.Name == "OrderBy")
            .Where(method => method.GetParameters().Length == 2)
            .Single();
    }
}
