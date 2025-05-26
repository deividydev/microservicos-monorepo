using Application.Order.Events;
using MessageBus.RabbitMQ.Events;

namespace Application.Order.EventHandlers;

public class PaymentRejectedEventHandler : IEventHandler<PaymentRejectedEvent>
{
    public Task HandleAsync(PaymentRejectedEvent @event)
    {
        Console.WriteLine($"Pagamento rejeitado para pedido {@event.OrderId}, motivo: {@event.Note}");
        return Task.CompletedTask;
    }
}

