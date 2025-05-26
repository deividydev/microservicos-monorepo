
using Application.Order.Commands;
using MediatR;

namespace Application.Order.Handlers;

public class CancelOrderHandler : IRequestHandler<CancelOrderCommand>
{
    public Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"########## APLICAR ESTORNO DOS PRODUTOS do PEDIDO: {request.OrderId} ##########");
        return Task.CompletedTask;
    }
}
