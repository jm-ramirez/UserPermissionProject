using Confluent.Kafka;
using Microsoft.Extensions.Options;

public class KafkaConsumer : BackgroundService
{
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly ILogger<KafkaConsumer> _logger;
    private readonly IOptions<KafkaConfiguration> _kafkaConfiguration;

    public KafkaConsumer(ILogger<KafkaConsumer> logger, IOptions<KafkaConfiguration> kafkaConfiguration)
    {
        _logger = logger;
        _kafkaConfiguration = kafkaConfiguration;

        var settings = _kafkaConfiguration.Value;
        var config = new ConsumerConfig
        {
            GroupId = "your-consumer-group-id", // Nombre único para el grupo de consumidores.
            BootstrapServers = settings.BootstrapServers, 
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };

        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var settings = _kafkaConfiguration.Value;
        _consumer.Subscribe(settings.TopicName);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                // Procesa el mensaje recibido, que se encuentra en consumeResult.

                _logger.LogInformation($"Received message: {consumeResult.Message.Value}");
            }
            catch (OperationCanceledException)
            {
                // El token de cancelación se ha activado, lo que significa que la aplicación se está cerrando.
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while consuming messages from Kafka.");
            }
        }

        _consumer.Close();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _consumer.Close();
        await base.StopAsync(cancellationToken);
    }
}
