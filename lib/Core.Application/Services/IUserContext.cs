using System.Security.Claims;

namespace ExpenseTracker.Core.Application.Services;

public interface IUserContext
{
    /// <summary>
    /// Gets the claims principal that describes the current user.
    /// </summary>
    ClaimsPrincipal? User { get; }
}
