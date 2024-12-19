using System.Security.Claims;
using ExpenseTracker.Core.Application.Services;

namespace ExpenseTracker.WebApp.Services;

public class HttpUserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpUserContext(IHttpContextAccessor contextAccessor)
    {
        _httpContextAccessor = contextAccessor;
    }
    
    public ClaimsPrincipal? User { get => _httpContextAccessor.HttpContext?.User; }
}
