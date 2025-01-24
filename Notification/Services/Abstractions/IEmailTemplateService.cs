namespace Notification.Services.Abstractions;

public interface IEmailTemplateService
{
    string GetOrderCreatedTemplate(Guid orderId, decimal totalAmount);
    string GetOrderPaidTemplate(Guid orderId, decimal totalAmount);
    string GetOrderShippedTemplate(Guid orderId);
    string GetOrderDeliveredTemplate(Guid orderId);
}