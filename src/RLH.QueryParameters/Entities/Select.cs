using RLH.QueryParameters.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters
{
    /// <summary>
    /// Contains details of a 'Select' condition, used when querying data in a backing store
    /// </summary>
    public class Select : ISelect
    {
        /// <summary>
        /// Creates a new Select with a given property name
        /// </summary>
        /// <param name="propertyName">Name of the base class property to select</param>
        internal Select(string propertyName,bool external)
        {
            PropertyName = propertyName ?? propertyName.ToLower();
            External = external;
        }
        /// <summary>
        /// Name of the base class property
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// Flag to indicate if this operation was created externally
        /// (via query string parsing) or manually from calling
        /// the 'AddOrderBy' or 'AddWhere' methods
        /// </summary>
        public bool External { get; private set; }
    }
}
