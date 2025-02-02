using Catalog.Abstractions;
using Catalog.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Features.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<ProductDetailsDto>>
{
    private readonly CatalogContext _context;

    public GetProductQueryHandler(CatalogContext context) => _context = context;

    public async Task<Result<ProductDetailsDto>> Handle(GetProductQuery request, CancellationToken ct)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.Id, ct);

        if (product == null)
            return Result<ProductDetailsDto>.Failure("Product not found");

        return Result<ProductDetailsDto>.Success(new ProductDetailsDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Category.Id,
            product.Category.Name,
            0,
            true));
    }
}