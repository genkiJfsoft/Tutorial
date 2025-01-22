using System.Resources;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

public record SignIn(string Email, string Password, bool Remember) : IRequest<Result>
{
    internal class RequestHandler : IRequestHandler<SignIn, Result>
    {
 
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<RequestHandler> _logger;
        private readonly IStringLocalizer<ExpenseTracker.Extensions.Resources.Strings> L;

        public RequestHandler(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<RequestHandler> logger, IStringLocalizer<ExpenseTracker.Extensions.Resources.Strings> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            L = localizer;
        }

        public async Task<Result> Handle(SignIn request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return Result.Error(L["Email not found"]);
                }

                // Check if the user account is suspended
                if (user.Status == "Suspend")
                {
                    _logger.LogWarning("Login attempt for suspended account: {Email}", user.Email);
                    return Result.Error(L["AccountSuspended"]);
                } 

                if (string.IsNullOrEmpty(user.UserName))
                {
                    return Result.Error("Invalid login attempt. UserName is missing.");
                }

                var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, request.Remember, lockoutOnFailure: false);

                if (signInResult.Succeeded)
                {
                    // Clear existing claims
                    var existingClaims = await _userManager.GetClaimsAsync(user);
                    foreach (var claim in existingClaims)
                    {
                        _logger.LogInformation("Removing claim: {Type} = {Value}", claim.Type, claim.Value);
                        await _userManager.RemoveClaimAsync(user, claim);
                    }

                    // Add fresh claims
                    var claims = new List<Claim>
                    {
                        new Claim("Category", user.Category ?? "Unknown"),
                        new Claim("Email", user.Email ?? "Unknown"),
                        new Claim("Name", user.UserName ?? "Unknown"),
                        new Claim("FullName", user.FullName ?? "Unknown"),
                       new Claim("PreferredLanguage", user.PreferredLanguage ?? "Unknown")
                    };
                    await _userManager.AddClaimsAsync(user, claims);

                    // Force session refresh
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Log updated claims
                    var updatedClaims = await _userManager.GetClaimsAsync(user);
                    foreach (var claim in updatedClaims)
                    {
                        _logger.LogInformation("Updated claim: {Type} = {Value}", claim.Type, claim.Value);
                    }

                    return Result.Success();
                }
                else if (signInResult.RequiresTwoFactor)
                {
                    return Result.Unauthorized("Two-factor authentication is required.");
                }
                else if (signInResult.IsLockedOut)
                {
                    return Result.Forbidden("User account is locked out.");
                }

                return Result.Error(@L["WrongPassword"]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the sign-in request.");
                return Result.Error("An unexpected error occurred. Please try again later.");
            }
        }
    }
}



