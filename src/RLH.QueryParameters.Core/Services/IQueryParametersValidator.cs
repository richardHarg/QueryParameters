using RLH.QueryParameters.Core.Entities;

namespace RLH.QueryParameters.Core.Services
{
    public interface IQueryParametersValidator : IDisposable
    {
        public Dictionary<string,string> Validate<T>(IQueryingParameters queryParameters);
    }
}
