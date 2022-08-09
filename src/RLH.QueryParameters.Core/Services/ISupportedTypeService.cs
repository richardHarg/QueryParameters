
namespace RLH.QueryParameters.Core
{
    public interface ISupportedTypeService
    {
        public ISupportedType FindSupportedTypeForProperty(Type initialType, string propertyName);
    }
}
