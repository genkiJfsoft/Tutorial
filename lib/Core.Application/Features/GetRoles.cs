namespace ExpenseTracker.Core.Application.Features;

[Authorize]
public record GetRoles : IRequest<Result<List<RoleData>>>
{
    public bool IncludeUsersCount { get; init; } = false;
    
    internal class RequestHandler(RoleManager<Role> roleManager) : IRequestHandler<GetRoles, Result<List<RoleData>>>
    {
        public async Task<Result<List<RoleData>>> Handle(GetRoles request, CancellationToken cancellationToken)
        {
            var list = await roleManager.Roles.AsNoTracking()
                .Include(e => e.UserRoles)
                .Select(RoleData.Mapper(request.IncludeUsersCount))
                .ToListAsync(cancellationToken);

            return Result<List<RoleData>>.Success(list);
        }
    }
}
