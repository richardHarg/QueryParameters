
namespace RLH.QueryParameters.Core
{
    public interface IQueryParametersValidator : IDisposable
    {
        public Dictionary<string,string> Validate<T>(IQueryingParameters queryParameters);
    }
}
