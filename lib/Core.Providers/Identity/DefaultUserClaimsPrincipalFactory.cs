using System.Security.Claims;
using ExpenseTracker.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ExpenseTracker.Core.Providers.Identity;

internal class DefaultUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<User, Role>(userManager, roleManager, options)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        // TODO: add other custom claims.

        identity.AddClaim(new Claim(ClaimTypes.GivenName, user.DisplayName ?? "N/A"));

        return identity;
    }
}
