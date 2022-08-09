using RLH.QueryParameters.Core;

namespace RLH.QueryParameters
{
    /// <summary>
    /// Contains details of a 'Where' condition, used when querying data in a backing store
    /// </summary>
    public sealed class Where : Select , IWhere
    {
        /// <summary>
        /// Create a new where condition with the provided property name/operator and value
        /// </summary>
        /// <param name="propertyName">Name of the base class property to order</param>
        /// <param name="logicalOperator">Logical operator to use, e.g. ==</param>
        /// <param name="propertyValue">Value of the property to search for</param>
        internal Where(string propertyName, string logicalOperator, string propertyValue,bool external = true) : base(propertyName,external)
        {
            LogicalOperator = logicalOperator ?? logicalOperator;
            PropertyValue = propertyValue ?? propertyValue.ToLower();
        }
        /// <summary>
        /// Logical operator associated with this condition
        /// </summary>
        public string LogicalOperator { get; private set; }
        /// <summary>
        /// Property value associated with this condition
        /// </summary>
        public object PropertyValue { get; private set; }
    }
}
