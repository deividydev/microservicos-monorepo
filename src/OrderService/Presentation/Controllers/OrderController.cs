using Application.Order.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="cmd">The command containing the order details.</param>
    /// <returns>
    /// Returns <see cref="Guid"/> of the newly created order if successful.
    /// Returns <see cref="BadRequestResult"/> if the request is invalid.
    /// Returns <see cref="UnprocessableEntityResult"/> if the request cannot be processed.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(string))]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand cmd)
    {
        var orderId = await _mediator.Send(cmd);
        return Ok(orderId);
    }

    /// <summary>
    /// Cancels an existing order.
    /// </summary>
    /// <param name="id">The unique identifier of the order to be canceled.</param>
    /// <param name="note">A note providing the reason for the cancellation.</param>
    /// <returns>
    /// Returns a success message if the order is successfully canceled.
    /// Returns <see cref="BadRequestResult"/> if the request is invalid.
    /// Returns <see cref="UnprocessableEntityResult"/> if the request cannot be processed.
    /// </returns>
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
