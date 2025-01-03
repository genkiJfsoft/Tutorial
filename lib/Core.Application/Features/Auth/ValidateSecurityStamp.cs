using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace ExpenseTracker.Core.Application.Features.Auth;

public record ValidateSecurityStamp(ClaimsPrincipal Principal) : IRequest<Result>
{
    internal class RequestHandler(UserManager<User> userManager, IOptions<IdentityOptions> options) : IRequestHandler<ValidateSecurityStamp, Result>
    {
        public async Task<Result> Handle(ValidateSecurityStamp request, CancellationToken cancellationToken)
        {
            var user = await userManager.GetUserAsync(request.Principal);

            if (user is null) return Result.Conflict();
            if (!userManager.SupportsUserSecurityStamp) return Result.Success();

            var principalStamp = request.Principal.FindFirstValue(options.Value.ClaimsIdentity.SecurityStampClaimType);
            var userStamp = await userManager.GetSecurityStampAsync(user);

            return principalStamp == userStamp ? Result.Success() : Result.Conflict();
        }
    }
}
