using MediatR;

namespace Application.Order.Commands;

public record CreateOrderCommand(decimal Value) : IRequest<Guid>;
public record CancelOrderCommand(Guid OrderId, string? Note) : IRequest;
