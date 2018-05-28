using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using Tnf;

namespace Indexing.Infra.Elasticsearch
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddElasticDependency(this IServiceCollection services, IConfiguration configuration)
        {
            var elasticUrl = configuration["elastic.url"];

            Check.NotNullOrWhiteSpace(elasticUrl, nameof(elasticUrl));

            // Esse registro é aberto como está no driver NEST do Elastic.
            // Existem várias configurações como pool de conexões, autenticação e timeout por exemplo
            // que devem ser definidas a nível de aplicação
            services.AddTnfElasticsearch(new ConnectionSettings(new Uri(elasticUrl))
                //.BasicAuthentication()
                .ThrowExceptions());

            return services;
        }
    }
}
