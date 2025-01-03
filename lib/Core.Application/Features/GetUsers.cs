namespace ExpenseTracker.Core.Application.Features;

[Authorize]
public record GetUsers : IRequest<Result<List<UserData>>>
{
    public int Limit { get; init; } = 10;
    
    internal class RequestHandler(UserManager<User> userManager) : IRequestHandler<GetUsers, Result<List<UserData>>>
    {
        public async Task<Result<List<UserData>>> Handle(GetUsers request, CancellationToken cancellationToken)
        {
            var list = await userManager.Users.AsNoTracking()
                .Include(e => e.UserRoles)
                .ThenInclude(e => e.Role)
                .OrderBy(e => e.Id)
                .Take(request.Limit)
                .Select(UserData.Mapper)
                .ToListAsync(cancellationToken);

            return Result<List<UserData>>.Success(list);
        }
    }
}
