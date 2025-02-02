using Catalog.Abstractions;
using Catalog.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Features.SearchProducts;

public class SearchProductsQueryHandler 
    : IRequestHandler<SearchProductsQuery, Result<PagedResult<ProductDto>>>
{
    private readonly CatalogContext _context;

    public SearchProductsQueryHandler(CatalogContext context) => _context = context;

    public async Task<Result<PagedResult<ProductDto>>> Handle(
        SearchProductsQuery request, CancellationToken ct)
    {
        var searchTerm = request.SearchTerm.ToLower();
        var query = _context.Products
            .Include(p => p.Category)
            .Where(p => p.Name.ToLower().Contains(searchTerm) || 
                        p.Description.ToLower().Contains(searchTerm))
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