using RLH.QueryParameters.Core.Options;
using RLH.QueryParameters.Core.Services;
using RLH.QueryParameters.Services;

namespace RLH.QueryParameters.Factories
{
    public class QueryParametersValidatorFactory
    {
        public IQueryParametersValidator GetService(IValidationOptions options, int version = 1)
        {
            switch (version)
            {
                default:
                    return new QueryParametersValidator(options);
            }
        }
        public IQueryParametersValidator GetService(int version = 1)
        {
            return GetService(new ValidationOptionsFactory().Create(), version);

        }
    }
}
