using MediatR;
using Order.Abstractions;

namespace Order.Features.PayOrder;

public class PayOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orders/{orderId}/pay", async (
                Guid orderId,
                IMediator mediator) =>
            {
                var result = await mediator.Send(new PayOrderCommand(orderId));
                return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
            })
            .WithName("PayOrder")
            .WithTags("Orders")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithOpenApi();
    }
}