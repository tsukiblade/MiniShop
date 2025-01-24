using Catalog.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Features.Products.GetProducts;

public class GetProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products",
            async ([FromServices]IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new GetProductsQuery(), cancellationToken);

                return Results.Ok(result);
            })
            .WithOpenApi();
    }
}