using Indexing.Infra.SqLite.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Indexing.Infra.SqLite
{
    public static class MigrationExtensions
    {
        public static void MigrateDatabase(this IServiceProvider provider)
        {
            Task.Factory.StartNew(() =>
            {
                var context = provider.GetRequiredService<SampleContext>();
                context.Database.Migrate();
            });
        }
    }
}
