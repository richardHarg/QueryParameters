
using Microsoft.Extensions.DependencyInjection;
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

            using (var factory = new ValidationOptionsFactory())
            {
                services.Configure<ValidationOptions>(x =>
                {
                    x.SupportedTypes = factory.GetSupportedTypes();
                });
            }
            services.AddScoped<IQueryParametersValidator, QueryParametersValidator>();
            return services;
        }
    }
}
