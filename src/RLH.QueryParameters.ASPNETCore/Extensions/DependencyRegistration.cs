
using Microsoft.Extensions.DependencyInjection;
using RLH.QueryParameters.Core.Entities;
using RLH.QueryParameters.Core.Services;
using RLH.QueryParameters.Entities;
using RLH.QueryParameters.Factories;
using RLH.QueryParameters.Options;
using RLH.QueryParameters.Services;

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

            services.Configure<ValidationOptions>(x =>
            {
                x.SupportedTypes = BuiltInSupportedTypes;
            });
            services.AddScoped<IQueryParametersValidator, OptionsQueryParametersValidator>();
            return services;
        }

        public static IServiceCollection AddDynamicLinqQuerying(this IServiceCollection services,Dictionary<Type,SupportedType> additionalSupportedTypes)
        {

            services.Configure<ValidationOptions>(x =>
            {
                x.SupportedTypes = GetBuiltInSupportedTypesWithAdditionalSupportedTypes(additionalSupportedTypes);
            });
            services.AddScoped<IQueryParametersValidator, OptionsQueryParametersValidator>();
            return services;
        }



        private static Dictionary<Type, ISupportedType> BuiltInSupportedTypes
        {
            get
            {
                using (var factory = new ValidationOptionsFactory())
                {
                    return factory.GetSupportedTypes();
                };
            }
        }

        private static Dictionary<Type, ISupportedType> GetBuiltInSupportedTypesWithAdditionalSupportedTypes(Dictionary<Type, SupportedType> additionalSupportedTypes)
        {
            // Get the built in types
            var supportedTypes = BuiltInSupportedTypes;

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
