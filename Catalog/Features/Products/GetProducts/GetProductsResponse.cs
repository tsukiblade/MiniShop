namespace Catalog.Features.Products.GetProducts;

public record GetProductsResponse(IEnumerable<ProductDto> Data);