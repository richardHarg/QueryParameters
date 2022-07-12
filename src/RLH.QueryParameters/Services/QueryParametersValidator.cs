using System;
using System.Reflection;
using RLH.QueryParameters.Core.Attributes;
using RLH.QueryParameters.Core.Entities;
using RLH.QueryParameters.Core.Options;
using RLH.QueryParameters.Core.Services;
using RLH.QueryParameters.Entities;

using RLH.QueryParameters.Options;

namespace RLH.QueryParameters.Services
{
    public class QueryParametersValidator : IQueryParametersValidator
    {
        private bool disposedValue;

        private IValidationOptions _options;

        public QueryParametersValidator(IValidationOptions options)
        {
            _options = options;
        }


        public Dictionary<string, string> Validate<T>(IQueryingParameters queryParameters)
        {
            // Get (from cache) OR if exists in cache get the queryable properties for this data type T
            var classQueryableProperties = GetCurrentClassQueryableProperties<T>();

            ValidateWhereConditions(queryParameters, classQueryableProperties);

            ValidateOrderByConditions(queryParameters, classQueryableProperties);

            return queryParameters.ValidationErrors;
        }

        private void ValidateWhereConditions(IQueryingParameters queryParameters, Dictionary<string, Type> parameters)
        {
            foreach (Where operation in queryParameters.WhereConditions.Where(x => x.External))
            {
                // Supported Type is used for later validation of logical operators/type conversion and by proxy also 
                // confirms if the PropertyName provided is valid AND the data type of this property is supported
                var supportedTypeInfo = FindSupportedTypeForQueryableProperty(parameters,operation.PropertyName);

                // If a value was returned then continue validation
                if (supportedTypeInfo != null)
                {
                    // Check the logical operator value against the support types valid operators
                    if (supportedTypeInfo.Operators.Contains(operation.LogicalOperator) == false)
                    {
                        queryParameters.AddValidationError(operation.PropertyName, $"LogicalOperator '{operation.LogicalOperator}' is not supported. Valid operators are: {string.Join(',', supportedTypeInfo.Operators)}");
                    }

                    // Check if the PropertyValue field contains some data....
                    if (string.IsNullOrWhiteSpace(operation.PropertyValue) == false)
                    {
                        try
                        {
                            supportedTypeInfo.TypeConverter.ConvertFromInvariantString(operation.PropertyValue);
                        }
                        catch
                        {
                            queryParameters.AddValidationError(operation.PropertyName, $"Error parsing PropertyValue '{operation.PropertyValue}' to type '{supportedTypeInfo.Type.Name}'");
                        }
                    }
                    else
                    {
                        queryParameters.AddValidationError(operation.PropertyName, $"PropertyValue cannot be blank or whitespace");
                    }
                }
                else
                {
                    queryParameters.AddValidationError(operation.PropertyName, $"PropertyName '{operation.PropertyName}' is not a valid queryable property.'");
                }
            }
        }
        private void ValidateOrderByConditions(IQueryingParameters queryParameters, Dictionary<string, Type> parameters)
        {
            foreach (OrderBy operation in queryParameters.OrderByConditions.Where(x => x.External))
            {
                // Supported Type is used for later validation of logical operators/type conversion and by proxy also 
                // confirms if the PropertyName provided is valid AND the data type of this property is supported
                var supportedTypeInfo = FindSupportedTypeForQueryableProperty(parameters,operation.PropertyName);

                // If a value was returned then continue validation
                if (supportedTypeInfo != null)
                {
                    // Check the sortOrder is of the correct two possible values
                    if (operation.SortOrder != "ascending" && operation.SortOrder != "descending")
                    {
                        queryParameters.AddValidationError(operation.PropertyName, $"Invalid SortOrder value '{operation.SortOrder}'. Must be asc/desc or omitted (default ascending)");
                    }
                }
                else
                {
                    queryParameters.AddValidationError(operation.PropertyName, $"PropertyName '{operation.PropertyName}' is not a valid queryable property.'");
                }
            }
        }







        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~QueryParametersValidator()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private Dictionary<string, Type> GetCurrentClassQueryableProperties<T>()
        {
            Dictionary<string, Type> types = new Dictionary<string, Type>();
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.CustomAttributes.Any(x => x.AttributeType == typeof(QueryableAttribute)))
                {
                    types.Add(property.Name.ToLower(), property.PropertyType);
                }
            }
            return types;
        }

        /// <summary>
        ///  Checks the provided dictionary of queryable class properties for the given 
        ///  PropertyName key and, if found returns the type of this property.
        ///  
        ///  If above key located checks the Options 'SupportedTypes' dictionary for the property type obtained above
        ///  and, if found returns the 'SupportedType' instance associated with this data type.
        /// </summary>
        /// <param name="propertyName">Name of Property to search for</param>
        /// <param name="queryableProperties">Dictionary with PropertyNames/Types of valid queryable properties</param>
        /// <returns></returns>
        private ISupportedType FindSupportedTypeForQueryableProperty(Dictionary<string, Type> properteies, string propertyName)
        {
            // Check if the class specific list of properties contains a key which matches the provided PropertyName
            if (properteies.TryGetValue(propertyName.ToLower(), out Type type) == true)
            {
                if (_options.SupportedTypes.TryGetValue(type, out ISupportedType value) == true)
                {
                    return value;
                }
            }
            return null;
        }
    }
}
