using MediatR;
using Order.Abstractions;

namespace Order.Features.ShipOrder;

public record ShipOrderCommand(Guid OrderId) : IRequest<Result>;