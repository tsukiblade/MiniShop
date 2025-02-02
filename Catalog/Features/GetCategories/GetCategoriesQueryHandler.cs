using Catalog.Abstractions;
using Catalog.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Features.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Result<List<CategoryDto>>>
{
    private readonly CatalogContext _context;

    public GetCategoriesQueryHandler(CatalogContext context) => _context = context;

    public async Task<Result<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken ct)
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .Select(c => new CategoryDto(
                c.Id,
                c.Name,
                c.Description,
                c.Products.Count))
            .ToListAsync(ct);

        return Result<List<CategoryDto>>.Success(categories);
    }
}