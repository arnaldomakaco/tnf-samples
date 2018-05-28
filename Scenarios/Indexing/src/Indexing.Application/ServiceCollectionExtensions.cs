using Indexing.Application.Services;
using Indexing.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Indexing.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServiceDependency(this IServiceCollection services)
        {
            services.AddTnfIndexing();

            services.AddTransient<IEmployeeAppService, EmployeeAppService>();

            return services;
        }
    }
}
