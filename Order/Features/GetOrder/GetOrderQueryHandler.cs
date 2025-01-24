using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Abstractions;
using Order.Database;

namespace Order.Features.GetOrder;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Result<OrderDto>>
{
    private readonly OrderContext _context;
    private readonly IUserContext _userContext;

    public GetOrderQueryHandler(OrderContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<Result<OrderDto>> Handle(GetOrderQuery query, CancellationToken ct)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == query.OrderId, ct);

        if (order == null || order.CustomerEmail != _userContext.Email)
            return Result<OrderDto>.Failure("Order not found");

        var orderDto = new OrderDto(
            order.Id,
            order.Status.ToString(),
            order.TotalAmount,
            order.CreatedAt,
            order.PaidAt,
            order.Items.Select(i => new OrderItemDto(
                i.Id,
                i.ProductId,
                i.ProductName,
                i.Quantity,
                i.Price
            )).ToList()
        );

        return Result<OrderDto>.Success(orderDto);
    }
}