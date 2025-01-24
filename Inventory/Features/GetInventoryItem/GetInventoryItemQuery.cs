using MediatR;

namespace Inventory.Features.GetInventoryItem;

public record GetInventoryItemQuery(Guid Id) : IRequest<ItemDetailDto?>;