
using Application.Order.Commands;
using Application.Order.Events;
using MediatR;
using MessageBus.RabbitMQ.Publisher;

namespace Application.Order.Handlers;

public class CreateOrderHandler(IRabbitMqPublisher publisher) : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IRabbitMqPublisher _publisher = publisher;

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = Guid.NewGuid();
        var createdOrderEvent = new CreatedOrderEvent(orderId, request.Value);

        await _publisher.PublishAsync(createdOrderEvent);

        return orderId;
    }
}
