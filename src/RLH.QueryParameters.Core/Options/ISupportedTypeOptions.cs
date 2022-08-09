
namespace RLH.QueryParameters.Core
{
    public interface ISupportedTypeOptions
    {
        public Dictionary<Type, ISupportedType> SupportedTypes { get; }
    }
}
