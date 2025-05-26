using Application.Order.Commands;
using Application.Order.Events;
using MediatR;
using MessageBus.RabbitMQ.Events;

namespace Application.Order.EventHandlers;

public class PaymentRejectedEventHandler(IMediator mediator) : IEventHandler<PaymentRejectedEvent>
{
    private readonly IMediator _mediator = mediator;

    public async Task HandleAsync(PaymentRejectedEvent @event)
    {
        Console.WriteLine($"Pagamento rejeitado para pedido {@event.OrderId}, motivo: {@event.Note}");

        var cmd = new CancelOrderCommand(@event.OrderId, @event.Note);

        await _mediator.Send(cmd);
    }
}

