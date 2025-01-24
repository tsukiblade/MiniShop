using MediatR;

namespace Catalog.Features.Products.GetProducts;

public record GetProductsQuery : IRequest<GetProductsResponse>;