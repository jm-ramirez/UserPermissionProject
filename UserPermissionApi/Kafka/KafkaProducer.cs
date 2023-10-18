using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Nest;
using System.Text.Json;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<string, string> _producer;
    private readonly IOptions<KafkaConfiguration> _kafkaConfiguration;

    public KafkaProducer(IOptions<KafkaConfiguration> kafkaConfiguration)
    {
        _kafkaConfiguration = kafkaConfiguration;
        var settings = _kafkaConfiguration.Value;
        var bootstrapServers = new ConnectionSettings(new Uri(settings.BootstrapServers));
        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers.ToString()
        };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task ProduceAsync(KafkaMessageDto message)
    {
        var settings = _kafkaConfiguration.Value;
        var serializedMessage = JsonSerializer.Serialize(message);
        await _producer.ProduceAsync(settings.TopicName, new Message<string, string>
        {
            Key = message.Id.ToString(),
            Value = serializedMessage
        });
    }
}
