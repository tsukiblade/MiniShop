using Inventory.Abstractions;
using MediatR;

namespace Inventory.Features.AddStock;

public record AddStockCommand(Guid ItemId, int Quantity, string? Reference) : IRequest<Result>;

