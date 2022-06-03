using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Entities
{
    /// <summary>
    /// Contains details of a 'Where' condition, used when querying data in a backing store
    /// </summary>
    public sealed class Where : Select
    {
        /// <summary>
        /// Create a new where condition with the provided property name/operator and value
        /// </summary>
        /// <param name="propertyName">Name of the base class property to order</param>
        /// <param name="logicalOperator">Logical operator to use, e.g. ==</param>
        /// <param name="propertyValue">Value of the property to search for</param>
        public Where(string propertyName, string logicalOperator, string propertyValue,bool external = true) : base(propertyName,external)
        {
            LogicalOperator = logicalOperator ?? logicalOperator;
            PropertyValue = propertyValue ?? propertyValue.ToLower();
        }
        /// <summary>
        /// Logical operator associated with this condition
        /// </summary>
        public readonly string LogicalOperator;
        /// <summary>
        /// Property value associated with this condition
        /// </summary>
        public readonly string PropertyValue;
    }
}
