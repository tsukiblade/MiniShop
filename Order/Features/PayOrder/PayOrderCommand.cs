using MediatR;
using Order.Abstractions;

namespace Order.Features.PayOrder;

public record PayOrderCommand(Guid OrderId) : IRequest<Result>;