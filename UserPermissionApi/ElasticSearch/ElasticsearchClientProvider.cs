using Microsoft.Extensions.Options;
using Nest;

namespace UserPermissionApi.ElasticSearch
{
    public class ElasticsearchClientProvider
    {
        private readonly ElasticClient _client;

        public ElasticsearchClientProvider(IOptions<ElasticsearchSettings> elasticsearchSettings)
        {
            var settings = elasticsearchSettings.Value;
            var connectionSettings = new ConnectionSettings(new Uri(settings.Uri))
                .DefaultIndex(settings.DefaultIndex);
            _client = new ElasticClient(connectionSettings);
        }

        public ElasticClient GetClient()
        {
            return _client;
        }
    }
}
