using System;

namespace RLH.QueryParameters.Attributes
{
    /// <summary>
    /// Indicates that this Property can be filtered/ordered by DynamicLINQ via the request query string
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class QueryableAttribute : Attribute
    {
    }
}
