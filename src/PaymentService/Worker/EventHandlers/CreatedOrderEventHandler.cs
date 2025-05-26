using MessageBus.RabbitMQ.Events;
using MessageBus.RabbitMQ.Publisher;
using Worker.Events;

namespace Worker.EventHandlers;

/// <summary>
/// Handles the <see cref="CreatedOrderEvent"/> to simulate payment processing.
/// Publishes either an <see cref="ApprovedPaymentEvent"/> or a <see cref="PaymentRejectedEvent"/>.
/// </summary>
public class CreatedOrderEventHandler(IRabbitMqPublisher publisher) : IEventHandler<CreatedOrderEvent>
{
    private readonly IRabbitMqPublisher _publisher = publisher;

    /// <summary>
    /// Handles the <see cref="CreatedOrderEvent"/> by simulating payment approval or rejection.
    /// </summary>
    /// <param name="event">The created order event containing the order ID and value.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task HandleAsync(CreatedOrderEvent @event)
    {
        Console.WriteLine($"Processando pagamento para pedido {@event.OrderId} no valor {@event.Value}");

        var isApproved = new Random().Next(0, 2) == 1;

        if (isApproved)
        {
            await _publisher.PublishAsync(new ApprovedPaymentEvent(@event.OrderId));
            Console.WriteLine($"Pagamento aprovado para pedido {@event.OrderId}");
            return;
        }

        // await _publisher.PublishAsync(new PaymentRejectedEvent(@event.OrderId, "Saldo insuficiente"));
        // Console.WriteLine($"Pagamento rejeitado para pedido {@event.OrderId}");
    }
}
