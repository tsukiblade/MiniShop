namespace Order.Abstractions;

public interface IUserContext
{
    string Email { get; }
    bool IsAuthenticated { get; }
}