using System.Security.Claims;

namespace ExpenseTracker.Extensions.Authorization;

public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Get the user identifier of the user represented by the claims principal.
    /// </summary>
    /// <returns>The user identifier of the user.</returns>
    public static string? GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    /// <summary>
    /// Get the username of the user represented by the claims principal.
    /// </summary>
    /// <returns>The username of the user.</returns>
    public static string? GetUsername(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.Name);
    }

    /// <summary>
    /// Get the display name of the user represented by the claims principal.
    /// </summary>
    /// <returns>The display name of the user.</returns>
    public static string? GetDisplayName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.GivenName);
    }
}
