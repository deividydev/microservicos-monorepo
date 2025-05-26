namespace MessageBus.RabbitMQ.Events;

/// <summary>
/// Represents a handler for processing events of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of event to handle.</typeparam>
public interface IEventHandler<T>
{
    /// <summary>
    /// Handles the specified event asynchronously.
    /// </summary>
    /// <param name="event">The event instance to handle.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task HandleAsync(T @event);
}
