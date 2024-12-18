using ExpenseTracker.Core.Application.Features.Auth;
using ExpenseTracker.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebApp.Endpoints;

public static class AuthEndpoints
{
    public const string LogoutRoute = "/Logout";
    
    public static void MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        endpoints.MapPost(LogoutRoute, async (IScopedSender mediator, [FromForm] string returnUrl) =>
        {
            await mediator.Send(new SignOut());
            return TypedResults.LocalRedirect($"~/{returnUrl}");
        });
    }
}
