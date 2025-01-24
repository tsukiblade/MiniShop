using Inventory.Abstractions;
using MediatR;

namespace Inventory.Features.ShipStock;

public record ShipStockCommand(Guid ItemId, int Quantity, string ShipmentReference) : IRequest<Result>;