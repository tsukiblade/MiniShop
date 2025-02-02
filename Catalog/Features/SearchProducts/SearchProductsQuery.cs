using Catalog.Abstractions;
using MediatR;

namespace Catalog.Features.SearchProducts;

public record SearchProductsQuery(string SearchTerm, int Page = 1, int PageSize = 20) 
    : IRequest<Result<PagedResult<ProductDto>>>;