using MessageBus.RabbitMQ.Events;

namespace MessageBus.RabbitMQ.Subscriber;

public interface IRabbitMqSubscriber
{
    void Subscribe<T>(IEventHandler<T> handler);
}