namespace Catalog.Features.GetCategories;

public record CategoryDto(
    Guid Id,
    string Name,
    string Description,
    int ProductCount);