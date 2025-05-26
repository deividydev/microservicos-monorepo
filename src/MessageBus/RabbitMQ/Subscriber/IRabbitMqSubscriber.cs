using MessageBus.RabbitMQ.Events;

namespace MessageBus.RabbitMQ.Subscriber;

/// <summary>
/// Represents a RabbitMQ subscriber capable of handling events of a specific type.
/// </summary>
public interface IRabbitMqSubscriber
{
    /// <summary>
    /// Subscribes to messages of the specified event type and processes them using the provided event handler.
    /// </summary>
    /// <typeparam name="T">The type of the event to subscribe to.</typeparam>
    /// <param name="handler">The event handler responsible for processing the incoming event messages.</param>
    void Subscribe<T>(IEventHandler<T> handler);
}
