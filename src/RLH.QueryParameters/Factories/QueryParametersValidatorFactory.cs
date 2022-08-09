using RLH.QueryParameters.Core;

namespace RLH.QueryParameters
{
    public class QueryParametersValidatorFactory
    {
        public IQueryParametersValidator GetService(ISupportedTypeService supportedTypeService, int version = 1)
        {
            switch (version)
            {
                default:
                    return new QueryParametersValidator(supportedTypeService);
            }
        }
        public IQueryParametersValidator GetService(int version = 1)
        {
            return GetService(new SupportedTypeServiceFactory().GetService(), version);

        }
    }
}
