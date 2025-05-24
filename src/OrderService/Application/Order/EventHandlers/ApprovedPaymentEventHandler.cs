using Application.Order.Events;
using MessageBus.RabbitMQ.Events;

namespace Application.Order.EventHandlers;

public class ApprovedPaymentEventHandler : IEventHandler<ApprovedPaymentEvent>
{
    public Task HandleAsync(ApprovedPaymentEvent @event)
    {
        Console.WriteLine($"Pedido {@event.OrderId} pago com sucesso!");
        return Task.CompletedTask;
    }
}
