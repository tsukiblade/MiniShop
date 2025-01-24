using Catalog.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Features.Products.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsResponse>
{
    private readonly CatalogContext _context;

    public GetProductsQueryHandler(CatalogContext context)
    {
        _context = context;
    }
    
    public async Task<GetProductsResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products.Select(p => new ProductDto(p.Id, p.Name, p.Description)).ToListAsync(cancellationToken);
        
        return new GetProductsResponse(products);
    }
}