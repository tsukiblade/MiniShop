using Inventory.Abstractions;
using MediatR;

namespace Inventory.Features.GetInventoryItem;

public class GetInventoryItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/inventory/{id}", async (
                Guid id,
                IMediator mediator) =>
            {
                var result = await mediator.Send(new GetInventoryItemQuery(id));
                return result != null ? Results.Ok(result) : Results.NotFound();
            })
            .WithName("GetInventoryItem")
            .WithTags("Inventory")
            .Produces<ItemDetailDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
    }
}