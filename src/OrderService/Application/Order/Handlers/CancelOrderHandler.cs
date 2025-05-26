
using Application.Order.Commands;
using Application.Order.Events;
using MediatR;
using MessageBus.RabbitMQ.Publisher;

namespace Application.Order.Handlers;

public class CancelOrderHandler(IRabbitMqPublisher publisher) : IRequestHandler<CancelOrderCommand>
{
    private readonly IRabbitMqPublisher _publisher = publisher;

    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        // var canceledEvent = new CanceledOrderEvent(request.OrderId, request.Note ?? string.Empty);
        // await _publisher.PublishAsync(canceledEvent);
    }
}
