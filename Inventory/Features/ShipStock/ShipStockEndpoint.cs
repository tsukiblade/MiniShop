using Inventory.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Features.ShipStock;

public class ShipStockEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/inventory/{id}/ship", async (
                Guid id, [FromBody] ShipStockRequestBody body,
                IMediator mediator) =>
            {
                var command = new ShipStockCommand(id, body.Quantity, body.ShipmentReference);
                
                var result = await mediator.Send(command);
                return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
            })
            .WithName("ShipStock")
            .WithTags("Inventory")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithOpenApi();
    }
}

public record ShipStockRequestBody(int Quantity, string ShipmentReference);