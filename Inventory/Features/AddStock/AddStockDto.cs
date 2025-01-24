namespace Inventory.Features.AddStock;

public record AddStockDto(
    int Quantity,
    string? Reference,
    string? PurchaseOrderNumber,
    decimal UnitCost,
    string? Notes
);