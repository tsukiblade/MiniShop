using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Abstractions;
using Order.Database;

namespace Order.Features.GetUserOrders;

public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, Result<PagedResult<OrderDto>>>
{
    private readonly OrderContext _context;
    private readonly IUserContext _userContext;

    public GetUserOrdersQueryHandler(OrderContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<Result<PagedResult<OrderDto>>> Handle(GetUserOrdersQuery query, CancellationToken ct)
    {
        var totalCount = await _context.Orders
            .Where(o => o.CustomerEmail == _userContext.Email)
            .CountAsync(ct);

        var orders = await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.CustomerEmail == _userContext.Email)
            .OrderByDescending(o => o.CreatedAt)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(o => new OrderDto(
                o.Id,
                o.Status.ToString(),
                o.TotalAmount,
                o.CreatedAt,
                o.PaidAt,
                o.Items.Select(i => new OrderItemDto(
                    i.Id,
                    i.ProductId,
                    i.ProductName,
                    i.Quantity,
                    i.Price
                )).ToList()
            ))
            .ToListAsync(ct);

        var hasNextPage = (query.Page * query.PageSize) < totalCount;

        var result = new PagedResult<OrderDto>(
            orders,
            totalCount,
            query.Page,
            query.PageSize,
            hasNextPage
        );

        return Result<PagedResult<OrderDto>>.Success(result);
    }
}