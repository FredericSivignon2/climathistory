using Microsoft.Extensions.DependencyInjection;
using Weather.Application.Query;

namespace Weather.Services.InMemory
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddSingleton<IDataStore, DataStore>();

            return services;
        }
    }
}
