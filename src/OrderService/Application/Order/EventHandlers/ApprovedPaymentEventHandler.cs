using Application.Order.Events;
using MessageBus.RabbitMQ.Events;
using MediatR;
using Application.Order.Commands;

namespace Application.Order.EventHandlers;

public class PaymentRejectedEventHandler(IMediator mediator) : IEventHandler<PaymentRejectedEvent>
{
    private readonly IMediator _mediator = mediator;

    public async Task HandleAsync(PaymentRejectedEvent @event)
    {
        Console.WriteLine($"Pagamento rejeitado para pedido {@event.OrderId}, motivo: {@event.Note}");
        await _mediator.Send(new CancelOrderCommand(@event.OrderId, @event.Note));
    }
}

