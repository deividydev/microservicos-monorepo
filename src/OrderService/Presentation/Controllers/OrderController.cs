using Application.Order.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(string))]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand cmd)
    {
        var orderId = await _mediator.Send(cmd);
        return Ok(orderId);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(string))]
    public async Task<IActionResult> Cancel(Guid id, [FromBody] string note)
    {
        var cmd = new CancelOrderCommand(id, note);
        await _mediator.Send(cmd);
        return Ok("Cancelado com sucesso!");
    }
}
