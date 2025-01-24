using Notification.Services.Abstractions;

namespace Notification.Services;

public class EmailTemplateService : IEmailTemplateService
{
    public string GetOrderCreatedTemplate(Guid orderId, decimal totalAmount)
        => $@"
            <h2>Order Confirmation</h2>
            <p>Thank you for your order!</p>
            <p>Order ID: {orderId}</p>
            <p>Total Amount: ${totalAmount}</p>
            <p>Please proceed with the payment to process your order.</p>";

    public string GetOrderPaidTemplate(Guid orderId, decimal totalAmount)
        => $@"
            <h2>Payment Confirmation</h2>
            <p>We've received your payment for order {orderId}.</p>
            <p>Amount paid: ${totalAmount}</p>
            <p>We'll start processing your order right away!</p>";

    public string GetOrderShippedTemplate(Guid orderId)
        => $@"
            <h2>Order Shipped</h2>
            <p>Good news! Your order {orderId} has been shipped.</p>
            <p>You'll receive another notification when it's delivered.</p>";

    public string GetOrderDeliveredTemplate(Guid orderId)
        => $@"
            <h2>Order Delivered</h2>
            <p>Your order {orderId} has been delivered.</p>
            <p>Thank you for shopping with us!</p>";
}