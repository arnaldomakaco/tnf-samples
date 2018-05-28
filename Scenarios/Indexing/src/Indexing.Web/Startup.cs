using System;
using System.IO;
using System.Threading.Tasks;
using Indexing.Application;
using Indexing.Domain;
using Indexing.Infra.Elasticsearch;
using Indexing.Infra.SqLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Indexing.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddTnfAspNetCore();

            services
                .AddApplicationServiceDependency()
                .AddElasticDependency(_configuration)
                .AddSqLiteDependency();

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Indexing API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Indexing.Web.xml"));
                });

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");

            // Configura o use do AspNetCore do Tnf
            app.UseTnfAspNetCore(options =>
            {
                // Adiciona as configurações de localização da aplicação
                options.UseDomainLocalization();

                // Configura quais entidades serão indexadas
                options.ConfigureElasticsearch();

                // Configura a connection string da aplicação
                options.DefaultNameOrConnectionString = _configuration["ConnectionStrings:Sqlite"];
            });

            app.ApplicationServices.MigrateDatabase();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Indexing API v1");
            });

            app.UseMvcWithDefaultRoute();
            app.UseResponseCompression();

            app.Run(context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });
        }
    }
}
