using MessageBus.RabbitMQ.Events;
using MessageBus.RabbitMQ.Subscriber;
using Worker.Events;

namespace Worker;

/// <summary>
/// Background service responsible for subscribing to <see cref="CreatedOrderEvent"/> messages
/// and handling them using the provided event handler.
/// </summary>
public class PaymentWorker(IRabbitMqSubscriber subscriber,
                           IEventHandler<CreatedOrderEvent> handler) : BackgroundService
{
    private readonly IRabbitMqSubscriber _subscriber = subscriber;
    private readonly IEventHandler<CreatedOrderEvent> _handler = handler;

    /// <summary>
    /// Starts the background task that subscribes to <see cref="CreatedOrderEvent"/> messages.
    /// </summary>
    /// <param name="stoppingToken">Token that signals when the service should stop.</param>
    /// <returns>A task that represents the asynchronous execution.</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _subscriber.Subscribe(_handler);

        while (!stoppingToken.IsCancellationRequested)
            await Task.Delay(1000, stoppingToken);
    }
}
