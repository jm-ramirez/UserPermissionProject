public interface IKafkaProducer
{
    Task ProduceAsync(KafkaMessageDto message);
}
