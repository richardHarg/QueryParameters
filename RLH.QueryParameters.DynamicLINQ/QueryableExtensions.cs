using RLH.QueryParameters.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace RLH.QueryParameters.DynamicLINQ
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> DynamicWhere<T>(this IQueryable<T> collection,IQueryingParameters queryingParameters)
        {
            if (queryingParameters.WhereConditions.Any())
            {
                return collection.Where(queryingParameters.BuildDynamicWhereString());
            }
            else
            {
                return collection;
            }
        }
        public static IQueryable<T> DynamicOrderBy<T>(this IQueryable<T> collection, IQueryingParameters queryingParameters)
        {
            if (queryingParameters.OrderByConditions.Any())
            {
                return collection.OrderBy(queryingParameters.BuildDynamicOrderByString());
            }
            else
            {
                return collection;
            }
        }
    }
}
