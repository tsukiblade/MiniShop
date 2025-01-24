namespace Order.Middleware;

public class UserEmailAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public UserEmailAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var email = context.Request.Headers["X-User-Email"].FirstOrDefault();
        
        if (string.IsNullOrEmpty(email))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { error = "X-User-Email header is required" });
            return;
        }

        if (!IsValidEmail(email))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { error = "Invalid email format" });
            return;
        }

        await _next(context);
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}