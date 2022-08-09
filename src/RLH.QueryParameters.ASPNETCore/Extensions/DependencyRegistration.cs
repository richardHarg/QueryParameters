
using Microsoft.Extensions.DependencyInjection;
using RLH.QueryParameters.Core;

namespace RLH.QueryParameters.ASPNETCore.Extensions
{
    public static class DependencyRegistration
    {
       
        /// <summary>
        /// Add DynamicLinqQuerying with default options
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDynamicLinqQuerying(this IServiceCollection services)
        {

            services.Configure<SupportedTypeOptions>(x =>
            {
                x.SupportedTypes = new SupportedTypeOptionsFactory().GetSupportedTypes();
            });
            services.AddScoped<IQueryParametersValidator, QueryParametersValidator>();
            services.AddScoped<ISupportedTypeService, OptionsSupportedTypeService>();

            return services;
        }

        public static IServiceCollection AddDynamicLinqQuerying(this IServiceCollection services,Dictionary<Type,SupportedType> additionalSupportedTypes)
        {

            services.Configure<SupportedTypeOptions>(x =>
            {
                x.SupportedTypes = GetBuiltInSupportedTypesWithAdditionalSupportedTypes(additionalSupportedTypes);
            });
            services.AddScoped<IQueryParametersValidator, QueryParametersValidator>();
            services.AddScoped<ISupportedTypeService, OptionsSupportedTypeService>();
            return services;
        }



       

        private static Dictionary<Type, ISupportedType> GetBuiltInSupportedTypesWithAdditionalSupportedTypes(Dictionary<Type, SupportedType> additionalSupportedTypes)
        {
            // Get the built in types
            var supportedTypes = new SupportedTypeOptionsFactory().GetSupportedTypes();

            // Check the additional types, if an entry exists for this type then replace it, if not add it
            foreach (var additionalType in additionalSupportedTypes)
            {
                // Check for and remove if exists
                if (supportedTypes.ContainsKey(additionalType.Key) == true)
                {
                    supportedTypes.Remove(additionalType.Key);
                }

                // Add the additional/replacement type
                supportedTypes.Add(additionalType.Key, additionalType.Value);
            }

            return supportedTypes;
        }
    }
}
