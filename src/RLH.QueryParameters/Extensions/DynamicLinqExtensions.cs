using System.Linq;
using System.Linq.Dynamic.Core;

namespace RLH.QueryParameters.Extensions
{
    public static class DynamicLinqExtensions
    {

        /// <summary>
        /// Filter/sort a Iqueryable Collection of T based on the dynamicLINQ formatted FilterBy/OrderBy strings passed
        /// </summary>
        /// <typeparam name="T">Base type being queried</typeparam>
        /// <param name="source">IQueryable of T</param>
        /// <param name="filterBy">DynamicLinq formatted FilterBy string</param>
        /// <param name="orderBy">DynamicLinq formatted OrderBy string</param>
        /// <returns></returns>
        public static IQueryable<T> DynamicLinqFilterOrder<T>(this IQueryable<T> source,string filterBy, string orderBy)
        {

            if (string.IsNullOrWhiteSpace(filterBy) == false)
            {
                source = source.Where(filterBy);
            }
            if (string.IsNullOrWhiteSpace(orderBy) == false)
            {
                source = source.OrderBy(orderBy);
            }
            return source;



            /*
            // Set flags indicating what values, if any, have been passed.
            bool filterValues = !string.IsNullOrWhiteSpace(filterBy);
            bool orderValues = !string.IsNullOrWhiteSpace(orderBy);

            

            switch (filterValues)
            {
                // If there are filterby values to be applied
                case true:
                    // If there are ALSO orderby values to be applied
                    if (orderValues == true)
                    {
                        return source.Where(filterBy).OrderBy(orderBy);
                    }
                    // If there are NO orderBy values to be applied
                    else
                    {
                        return source.Where(filterBy);
                    }
                // If there are NO filterBy values to be applied
                case false:
                    // If there ARE orderBy values to be applied
                    if (orderValues == true)
                    {
                        return source.OrderBy(orderBy);
                    }
                    // If there are NO filter OR order values to be applied
                    else
                    {
                        return source;
                    }
            }
            */
        }



    }
}
