using Inventory.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Features.GetInventory;

public class GetInventoryQueryHandler : IRequestHandler<GetInventoryQuery, List<InventoryItemDto>>
{
    private readonly InventoryContext _context;

    public GetInventoryQueryHandler(InventoryContext context)
    {
        _context = context;
    }

    public async Task<List<InventoryItemDto>> Handle(GetInventoryQuery query, CancellationToken ct)
    {
        return await _context.Items
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(i => new InventoryItemDto(
                i.Id,
                i.Sku,
                i.Name,
                i.AvailableQuantity,
                i.ReservedQuantity,
                i.Price))
            .ToListAsync(ct);
    }
}