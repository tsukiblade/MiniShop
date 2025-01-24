using MediatR;
using Order.Abstractions;

namespace Order.Features.GetOrder;

public record GetOrderQuery(Guid OrderId) : IRequest<Result<OrderDto>>;