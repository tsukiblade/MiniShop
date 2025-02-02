using Catalog.Abstractions;
using MediatR;

namespace Catalog.Features.GetProduct;

public record GetProductQuery(Guid Id) : IRequest<Result<ProductDetailsDto>>;