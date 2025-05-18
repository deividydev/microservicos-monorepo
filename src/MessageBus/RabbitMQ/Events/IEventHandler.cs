namespace MessageBus.RabbitMQ.Events;

public interface IEventHandler<T>
{
    Task HandleAsync(T @event);
}