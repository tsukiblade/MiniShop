using MediatR;
using Order.Abstractions;

namespace Order.Features.ShipOrder;

public class ShipOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orders/{orderId}/ship", async (
                Guid orderId,
                IMediator mediator) =>
            {
                var result = await mediator.Send(new ShipOrderCommand(orderId));
                return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
            })
            .WithName("ShipOrder")
            .WithTags("Orders")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithOpenApi();
    }
}