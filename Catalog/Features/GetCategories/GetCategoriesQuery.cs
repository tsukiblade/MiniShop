using Catalog.Abstractions;
using MediatR;

namespace Catalog.Features.GetCategories;

public record GetCategoriesQuery() : IRequest<Result<List<CategoryDto>>>;