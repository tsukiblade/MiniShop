using Catalog.Abstractions;
using MediatR;

namespace Catalog.Features.GetProductsByCategory;

public record GetProductsByCategoryQuery(Guid CategoryId, int Page = 1, int PageSize = 20) 
    : IRequest<Result<PagedResult<ProductDto>>>;