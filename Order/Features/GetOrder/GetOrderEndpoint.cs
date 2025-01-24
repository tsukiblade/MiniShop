using MediatR;
using Order.Abstractions;

namespace Order.Features.GetOrder;

public class GetOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders/{orderId}", async (
                Guid orderId,
                IMediator mediator) =>
            {
                var result = await mediator.Send(new GetOrderQuery(orderId));
                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.NotFound(result.Error);
            })
            .WithName("GetOrder")
            .WithTags("Orders");
    }
}