namespace MessageBus.RabbitMQ.Publisher;

/// <summary>
/// Provides functionality to publish messages to a RabbitMQ exchange.
/// </summary>
public interface IRabbitMqPublisher
{
    /// <summary>
    /// Publishes a message of the specified type to the configured exchange.
    /// </summary>
    /// <typeparam name="T">The type of the message to publish.</typeparam>
    /// <param name="message">The message instance to publish.</param>
    /// <returns>A completed task.</returns>
    Task PublishAsync<T>(T message);
}