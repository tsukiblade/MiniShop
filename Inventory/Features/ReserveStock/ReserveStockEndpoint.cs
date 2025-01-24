using Inventory.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Features.ReserveStock;

public class ReserveStockEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/inventory/{id}/reserve", async (
                Guid id, [FromBody] ReserveStockRequestBody body,
                IMediator mediator) =>
            {
                var command = new ReserveStockCommand(id, body.Quantity, body.OrderReference);
                var result = await mediator.Send(command);
                return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
            })
            .WithName("ReserveStock")
            .WithTags("Inventory")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithOpenApi();
    }
}

public record ReserveStockRequestBody(int Quantity, string OrderReference);