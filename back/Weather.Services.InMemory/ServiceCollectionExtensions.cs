using Microsoft.Extensions.DependencyInjection;
using Weather.Application.VisualCrossing;

namespace Weather.Services.VisualCrossing
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFileDataServices(this IServiceCollection services)
        {
            services.AddSingleton<IFileDataStore, FileDataStore>();

            return services;
        }
    }
}
