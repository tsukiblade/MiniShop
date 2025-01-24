using Inventory.Abstractions;
using MediatR;

namespace Inventory.Features.ReserveStock;

public record ReserveStockCommand(Guid ItemId, int Quantity, string OrderReference) : IRequest<Result>;