using Catalog.Abstractions;
using MediatR;

namespace Catalog.Features.GetProduct;

public class GetProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products/{id}", async (
                Guid id,
                IMediator mediator) =>
            {
                var result = await mediator.Send(new GetProductQuery(id));
                return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound();
            })
            .WithName("GetProduct")
            .WithTags("Products")
            .Produces<ProductDetailsDto>()
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}