using ExpenseTracker.Core.Application.Features.Auth;
using ExpenseTracker.Core.Application.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

namespace ExpenseTracker.WebApp.Services;

// This is a server-side AuthenticationStateProvider that revalidate the security stamp for the connected user
// every 30 minutes an interactive circuit is connected.
internal sealed class IdentityRevalidatingAuthenticationStateProvider(
    ILoggerFactory loggerFactory,
    IScopedSender mediator)
    : RevalidatingServerAuthenticationStateProvider(loggerFactory)
{
    protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

    protected override async Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ValidateSecurityStamp(authenticationState.User), cancellationToken);
        return result.IsSuccess;
    }
}
