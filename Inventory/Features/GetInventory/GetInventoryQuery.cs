using MediatR;

namespace Inventory.Features.GetInventory;

public record GetInventoryQuery(int Page, int PageSize) : IRequest<List<InventoryItemDto>>;