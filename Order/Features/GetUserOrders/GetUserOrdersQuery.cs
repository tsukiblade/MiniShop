using MediatR;
using Order.Abstractions;

namespace Order.Features.GetUserOrders;

public record GetUserOrdersQuery(int Page = 1, int PageSize = 20) : IRequest<Result<PagedResult<OrderDto>>>;