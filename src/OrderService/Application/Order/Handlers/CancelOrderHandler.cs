
using Application.Order.Commands;
using Application.Order.Events;
using MediatR;
using MessageBus.RabbitMQ.Publisher;

namespace Application.Order.Handlers;

public class CancelOrderHandler(RabbitMqPublisher publisher) : IRequestHandler<CancelOrderCommand>
{
    private readonly RabbitMqPublisher _publisher = publisher;

    public Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        _publisher.Publish(new CanceledOrderEvent(request.OrderId, request.Note ?? string.Empty));
        return Task.CompletedTask;
    }
}
