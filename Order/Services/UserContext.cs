using Order.Abstractions;

namespace Order.Services;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string Email => _httpContextAccessor.HttpContext?.Request.Headers["X-User-Email"].FirstOrDefault() 
                           ?? throw new UnauthorizedAccessException("User email not provided");

    public bool IsAuthenticated => !string.IsNullOrEmpty(Email);
}