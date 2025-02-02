using Catalog.Abstractions;
using Catalog.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Features.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<PagedResult<ProductDto>>>
{
    private readonly CatalogContext _context;

    public GetProductsQueryHandler(CatalogContext context) => _context = context;

    public async Task<Result<PagedResult<ProductDto>>> Handle(GetProductsQuery request, CancellationToken ct)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .AsNoTracking();

        var totalItems = await query.CountAsync(ct);

        var products = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Category.Name))
            .ToListAsync(ct);
        
        var hasNextPage = (request.Page * request.PageSize) < totalItems;

        return Result<PagedResult<ProductDto>>.Success(
            new PagedResult<ProductDto>(products, totalItems, request.Page, request.PageSize, hasNextPage));
    }
}