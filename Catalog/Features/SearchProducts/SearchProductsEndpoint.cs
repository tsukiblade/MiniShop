using Catalog.Abstractions;
using MediatR;

namespace Catalog.Features.SearchProducts;

public class SearchProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products/search", async (
                string searchTerm,
                [AsParameters] PaginationParams pagination,
                IMediator mediator) =>
            {
                var result = await mediator.Send(
                    new SearchProductsQuery(searchTerm, pagination.Page, pagination.PageSize));
                return Results.Ok(result.Value);
            })
            .WithName("SearchProducts")
            .WithTags("Products")
            .Produces<PagedResult<ProductDto>>();
    }
}