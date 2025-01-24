using MediatR;
using Order.Abstractions;

namespace Order.Features.DeliverOrder;

public record DeliverOrderCommand(Guid OrderId) : IRequest<Result>;