using MessageBus.RabbitMQ.Events;
using MessageBus.RabbitMQ.Subscriber;
using Worker.Events;

namespace Worker;

public class PaymentWorker(RabbitMqSubscriber subscriber,
                           IEventHandler<CreatedOrderEvent> handler) : BackgroundService
{
    private readonly RabbitMqSubscriber _subscriber = subscriber;
    private readonly IEventHandler<CreatedOrderEvent> _handler = handler;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _subscriber.Subscribe(_handler);
        return Task.CompletedTask;
    }
}
