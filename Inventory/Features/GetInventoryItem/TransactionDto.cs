using Inventory.Entities;

namespace Inventory.Features.GetInventoryItem;

public record TransactionDto(
    Guid Id,
    TransactionType Type,
    int Quantity,
    string Reference,
    DateTime CreatedAt,
    string CreatedBy
);