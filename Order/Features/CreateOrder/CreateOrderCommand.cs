using MediatR;
using Order.Abstractions;

namespace Order.Features.CreateOrder;

public record CreateOrderCommand(List<OrderItemDto> Items) : IRequest<Result<Guid>>;
