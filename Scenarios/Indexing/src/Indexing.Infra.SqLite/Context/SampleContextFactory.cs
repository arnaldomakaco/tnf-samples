using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tnf.Runtime.Session;

namespace Indexing.Infra.SqLite.Context
{
    public class SampleContextFactory : IDesignTimeDbContextFactory<SampleContext>
    {
        public SampleContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SampleContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.Development.json", false)
                                    .Build();

            builder.UseSqlite(configuration["ConnectionStrings:Sqlite"]);

            return new SampleContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
