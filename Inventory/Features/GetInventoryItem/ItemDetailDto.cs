namespace Inventory.Features.GetInventoryItem;

public record ItemDetailDto(
    Guid Id,
    string Sku,
    string Name,
    int AvailableQuantity,
    int ReservedQuantity,
    decimal Price,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    IEnumerable<TransactionDto> RecentTransactions
);