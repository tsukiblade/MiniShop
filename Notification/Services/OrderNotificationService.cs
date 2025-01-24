using MassTransit;
using Notification.Services.Abstractions;
using Order.IntegrationEvents;

namespace Notification.Services;

public class OrderNotificationService : 
    IConsumer<OrderCreatedEvent>,
    IConsumer<OrderPaidEvent>,
    IConsumer<OrderShippedEvent>,
    IConsumer<OrderDeliveredEvent>
{
    private readonly IEmailService _emailService;
    private readonly IEmailTemplateService _templateService;
    private readonly ILogger<OrderNotificationService> _logger;

    public OrderNotificationService(
        IEmailService emailService,
        IEmailTemplateService templateService,
        ILogger<OrderNotificationService> logger)
    {
        _emailService = emailService;
        _templateService = templateService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var evt = context.Message;
        var template = _templateService.GetOrderCreatedTemplate(evt.OrderId, evt.TotalAmount);
        
        await _emailService.SendEmailAsync(
            evt.CustomerEmail,
            "Order Confirmation",
            template);
    }

    public async Task Consume(ConsumeContext<OrderPaidEvent> context)
    {
        var evt = context.Message;
        var template = _templateService.GetOrderPaidTemplate(evt.OrderId, evt.TotalAmount);
        
        await _emailService.SendEmailAsync(
            evt.CustomerEmail,
            "Payment Confirmation",
            template);
    }

    public async Task Consume(ConsumeContext<OrderShippedEvent> context)
    {
        var evt = context.Message;
        var template = _templateService.GetOrderShippedTemplate(evt.OrderId);
        
        await _emailService.SendEmailAsync(
            evt.CustomerEmail,
            "Order Shipped",
            template);
    }

    public async Task Consume(ConsumeContext<OrderDeliveredEvent> context)
    {
        var evt = context.Message;
        var template = _templateService.GetOrderDeliveredTemplate(evt.OrderId);
        
        await _emailService.SendEmailAsync(
            evt.CustomerEmail,
            "Order Delivered",
            template);
    }
}