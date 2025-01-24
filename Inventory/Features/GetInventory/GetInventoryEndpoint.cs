using Inventory.Abstractions;
using MediatR;

namespace Inventory.Features.GetInventory;

public class GetInventoryEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/inventory", async (
                [AsParameters] GetInventoryQuery query,
                IMediator mediator) =>
            {
                var result = await mediator.Send(query);
                return Results.Ok(result);
            })
            .WithName("GetInventory")
            .WithTags("Inventory")
            .Produces<List<InventoryItemDto>>()
            .WithOpenApi();
    }
}