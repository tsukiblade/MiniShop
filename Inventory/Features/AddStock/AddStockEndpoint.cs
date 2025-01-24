using Inventory.Abstractions;
using MediatR;

namespace Inventory.Features.AddStock;

public class AddStockEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/inventory/add-stock", async (
                AddStockCommand command,
                IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
            })
            .WithName("AddStock")
            .WithTags("Inventory")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithOpenApi();
    }
}