namespace MessageBus.RabbitMQ.Publisher;

public interface IRabbitMqPublisher
{
    void Publish<T>(T message);
}