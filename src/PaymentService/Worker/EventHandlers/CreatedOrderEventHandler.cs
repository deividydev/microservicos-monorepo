using MessageBus.RabbitMQ.Events;
using MessageBus.RabbitMQ.Publisher;
using Worker.Events;

namespace Worker.EventHandlers;

public class CreatedOrderEventHandler : IEventHandler<CreatedOrderEvent>
{
    private readonly RabbitMqPublisher _publisher;

    public CreatedOrderEventHandler(RabbitMqPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task HandleAsync(CreatedOrderEvent @event)
    {
        Console.WriteLine($"Processando pagamento para pedido {@event.OrderId} no valor {@event.Value}");

        var isApproved = new Random().Next(0, 2) == 1;

        if (isApproved)
        {
            _publisher.Publish(new ApprovedPaymentEvent(@event.OrderId));
            Console.WriteLine($"Pagamento aprovado para pedido {@event.OrderId}");
            return Task.CompletedTask;
        }

        _publisher.Publish(new PaymentRejectedEvent(@event.OrderId, "Saldo insuficiente"));
        Console.WriteLine($"Pagamento rejeitado para pedido {@event.OrderId}");

        return Task.CompletedTask;
    }
}
