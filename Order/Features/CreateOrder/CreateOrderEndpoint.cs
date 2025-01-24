using MediatR;
using Order.Abstractions;

namespace Order.Features.CreateOrder;

public class CreateOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orders", async (
                CreateOrderCommand command,
                IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.IsSuccess 
                    ? Results.Ok(new { OrderId = result.Value }) 
                    : Results.BadRequest(result.Error);
            })
            .WithName("CreateOrder")
            .WithTags("Orders")
            .Produces<Guid>()
            .ProducesValidationProblem()
            .WithOpenApi();
    }
}