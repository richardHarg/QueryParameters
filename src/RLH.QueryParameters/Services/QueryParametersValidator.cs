
using System.Reflection;
using RLH.QueryParameters.Core;

namespace RLH.QueryParameters
{
    public class QueryParametersValidator : IQueryParametersValidator
    {
        private bool disposedValue;
        private ISupportedTypeService _supportedTypeService;

        public QueryParametersValidator(ISupportedTypeService supportedTypeService)
        {
            _supportedTypeService = supportedTypeService;
        }

        public Dictionary<string, string> Validate<T>(IQueryingParameters queryParameters)
        {
            ValidateWhereConditions(queryParameters, typeof(T));

            ValidateOrderByConditions(queryParameters, typeof(T));

            return queryParameters.ValidationErrors;
        }

        private void ValidateWhereConditions(IQueryingParameters queryParameters, Type initialType)
        {
            foreach (Where operation in queryParameters.WhereConditions.Where(x => x.External))
            {
                // Using the provided ProprtyName and a starting class type attempt to locate a supported type
                var supportedTypeInfo = _supportedTypeService.FindSupportedTypeForProperty(initialType, operation.PropertyName);

                // If null report and break now with no further validation.
                if (supportedTypeInfo == null)
                {
                    queryParameters.AddValidationError(operation.PropertyName, $"PropertyName '{operation.PropertyName}' is not a valid queryable property.'");
                    break;
                }

                // Check the logical operator value against the support types valid operators
                if (supportedTypeInfo.Operators.Contains(operation.LogicalOperator) == false)
                {
                    queryParameters.AddValidationError(operation.PropertyName, $"LogicalOperator '{operation.LogicalOperator}' is not supported on Type '{supportedTypeInfo.Type.Name}'. Valid operators are: {string.Join(',', supportedTypeInfo.Operators)}");
                }

                try
                {
                    supportedTypeInfo.TypeConverter.ConvertFrom(operation.PropertyValue);
                }
                catch
                {
                    queryParameters.AddValidationError(operation.PropertyName, $"Error parsing PropertyValue '{operation.PropertyValue}' to type '{supportedTypeInfo.Type.Name}'");
                }


                /*
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

                */
            }
        }
        private void ValidateOrderByConditions(IQueryingParameters queryParameters,Type initialType)
        {
            foreach (OrderBy operation in queryParameters.OrderByConditions.Where(x => x.External))
            {
                // Supported Type is used for later validation of logical operators/type conversion and by proxy also 
                // confirms if the PropertyName provided is valid AND the data type of this property is supported
                var supportedTypeInfo = _supportedTypeService.FindSupportedTypeForProperty(initialType, operation.PropertyName);

                if (supportedTypeInfo == null)
                {
                    queryParameters.AddValidationError(operation.PropertyName, $"PropertyName '{operation.PropertyName}' is not a valid queryable property.'");
                    break;
                }

                // Check the sortOrder is of the correct two possible values
                if (operation.SortOrder != "ascending" && operation.SortOrder != "descending")
                {
                    queryParameters.AddValidationError(operation.PropertyName, $"Invalid SortOrder value '{operation.SortOrder}'. Must be asc/desc or omitted (default ascending)");
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

        
        
    }
}
