
using Application.Order.Commands;
using Application.Order.Events;
using MediatR;
using MessageBus.RabbitMQ.Publisher;

namespace Application.Order.Handlers;

public class CreateOrderHandler(RabbitMqPublisher publisher) : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly RabbitMqPublisher _publisher = publisher;

    public Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = Guid.NewGuid();
        _publisher.Publish(new CreatedOrderEvent(orderId, request.Value));
        return Task.FromResult(orderId);
    }
}
