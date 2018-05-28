using Indexing.Domain.Entities;
using Tnf.Configuration;

namespace Indexing.Infra.Elasticsearch
{
    public static class TnfConfigurationExtensions
    {
        public static ITnfConfiguration ConfigureElasticsearch(this ITnfConfiguration configuration)
        {
            // Por convenção o elasticsearch admite que o identificador do objeto é um propriedade "Id".
            // Caso o objeto não contenha uma propriedade "Id", defina para cada objeto indexado como no exemplo abaixo.
            configuration.Indexing(config =>
            {
                // Convenção para criação do indice.
                // O nome da entidade será colocado no formato estipulado abaixo
                config.IndexNamingConvention("indexing_sample_{0}");

                // Configuração de uma entidade
                config.For<Employee>(e =>
                {
                    // Identificador dessa entidade.
                    e.Id(k => k.EmployeeId);

                    // Ao definir essa configuração apenas os campos informados serão marcados como storable no elasticsearch.
                    // Caso contrário todos os campos são salvos
                    //e.StorableMembers(sm => new { sm.Name, sm.Salary });
                });
            });

            return configuration;
        }
    }
}
