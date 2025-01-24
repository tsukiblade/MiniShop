using MediatR;
using Order.Abstractions;

namespace Order.Features.GetUserOrders;

public class GetUserOrdersEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders", async (
                [AsParameters] GetUserOrdersQuery query,
                IMediator mediator) =>
            {
                var result = await mediator.Send(query);
                return Results.Ok(result.Value);
            })
            .WithName("GetUserOrders")
            .WithTags("Orders");
    }
}