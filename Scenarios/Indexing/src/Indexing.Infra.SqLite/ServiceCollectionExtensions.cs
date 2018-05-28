using Indexing.Domain;
using Indexing.Infra.SqLite.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Indexing.Infra.SqLite
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqLiteDependency(this IServiceCollection services)
        {
            services
                .AddTnfEntityFrameworkCore()
                .AddTnfDbContext<SampleContext>((config) =>
                {
                    if (Constants.IsDevelopment())
                    {
                        config.DbContextOptions.EnableSensitiveDataLogging();
                        config.DbContextOptions.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                        config.UseLoggerFactory();
                    }

                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseSqlite(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseSqlite(config.ConnectionString);
                });

            return services;
        }
    }
}
