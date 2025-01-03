namespace ExpenseTracker.Core.Application.Features;

public record CreateUser : IRequest<Result<UserData>>
{
    public required UserData User { get; init; }
    
    internal class RequestHandler(UserManager<User> userManager) : IRequestHandler<CreateUser, Result<UserData>>
    {
        public async Task<Result<UserData>> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var usernameConflict = userManager.Users.SingleOrDefault(u => u.UserName == request.User.UserName);
            if (usernameConflict != null)
            {
                return Result<UserData>.Conflict("Username is already taken");
            }
            var emailConflict = userManager.Users.SingleOrDefault(u => u.Email == request.User.Email);
            if (emailConflict != null)
            {
                return Result<UserData>.Conflict("Email is already in use");
            }
            
            var user = new User
            {
                UserName = request.User.UserName, Email = request.User.Email, DisplayName = request.User.DisplayName
            };
            
            var result = await userManager.CreateAsync(user);

            if (result.Succeeded && !string.IsNullOrEmpty(request.User.AssignedRole))
            {
                await userManager.AddToRoleAsync(user, request.User.AssignedRole);
            }
            
            return !result.Succeeded ? Result<UserData>.Conflict("Failed to create user") : Result.Success(UserData.Make(user));
        }
    }
}
