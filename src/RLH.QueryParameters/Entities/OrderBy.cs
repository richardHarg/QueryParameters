using RLH.QueryParameters.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters
{
    /// <summary>
    /// Contains details of a 'OrderBy' condition, used when querying data in a backing store
    /// </summary>
    public sealed class OrderBy : Select , IOrderBy
    {
        /// <summary>
        /// Constructs a new OrderBy with a given property name and default sort
        /// order of 'ascending'
        /// </summary>
        /// <param name="propertyName">Name of the base class property to order</param>
        internal OrderBy(string propertyName, bool external = true) : base(propertyName,external)
        {
            SortOrder = "ascending";
        }

        /// <summary>
        /// Constructs a new OrderBy with a given property name and given sort order.
        /// Full 'ascending/descending' value can be passed or short version 'asc/desc'
        /// </summary>
        /// <param name="propertyName">Name of the base class property to order</param>
        /// <param name="sortOrder">Type of sort, either 'ascending' or 'descending'</param>
        internal OrderBy(string propertyName, string sortOrder, bool external = true) : base(propertyName,external)
        {
            if (string.IsNullOrWhiteSpace(sortOrder) == false)
            {
                switch (sortOrder.ToLower())
                {
                    case "asc":
                        SortOrder = "ascending";
                        break;
                    case "desc":
                        SortOrder = "descending";
                        break;
                    default: SortOrder = sortOrder.ToLower();
                        break;
                }
            }
        }
        /// <summary>
        /// Sort order associated with this condition
        /// </summary>
        public string SortOrder { get; private set; }
    }
}
