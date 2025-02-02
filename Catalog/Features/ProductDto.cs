namespace Catalog.Features;

public record ProductDto(
    Guid Id,
    string Name,
    decimal Price,
    string CategoryName);