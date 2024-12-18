namespace ExpenseTracker.Core.Application.Features.Auth;

public record SignOut : IRequest<Result>
{
    internal class RequestHandler(SignInManager<User> signInManager) : IRequestHandler<SignOut, Result>
    {
        public async Task<Result> Handle(SignOut request, CancellationToken cancellationToken)
        {
            await signInManager.SignOutAsync();
            return Result.Success();
        }
    }
}
