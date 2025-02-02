using Catalog.Abstractions;
using MediatR;

namespace Catalog.Features.GetProducts;

public record GetProductsQuery(int Page = 1, int PageSize = 20) : IRequest<Result<PagedResult<ProductDto>>>;