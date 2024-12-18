namespace ExpenseTracker.Core.Application.Features.Auth;

public record SignIn(string Username, string Password, bool Remember) : IRequest<Result>
{
    internal class RequestHandler(SignInManager<User> signInManager) : IRequestHandler<SignIn, Result>
    {
        public async Task<Result> Handle(SignIn request, CancellationToken cancellationToken)
        {
            var signInResult = await signInManager.PasswordSignInAsync(request.Username, request.Password, request.Remember, lockoutOnFailure: false);

            // TODO: better error handling
            if (signInResult.Succeeded)
            {
                return Result.Success();
            }
            else if (signInResult.RequiresTwoFactor)
            {
                return Result.Unauthorized();
            }
            else if (signInResult.IsLockedOut)
            {
                return Result.Forbidden();
            }

            return Result.Error("Invalid login attempt.");
        }
    }
}
