using MediatR;
using Order.Abstractions;

namespace Order.Features.DeliverOrder;

public class DeliverOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orders/{orderId}/deliver", async (
                Guid orderId,
                IMediator mediator) =>
            {
                var result = await mediator.Send(new DeliverOrderCommand(orderId));
                return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
            })
            .WithName("DeliverOrder")
            .WithTags("Orders")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithOpenApi();
    }
}