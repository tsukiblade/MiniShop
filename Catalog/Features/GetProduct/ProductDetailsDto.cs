namespace Catalog.Features.GetProduct;

public record ProductDetailsDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    Guid CategoryId,
    string CategoryName,
    int StockQuantity,
    bool IsAvailable);