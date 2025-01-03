using System.Linq.Expressions;

namespace ExpenseTracker.Core.Application.Data;

public record RoleData
{
    public required string Name { get; init; }
    public string? DisplayName { get; init; }
    public string? Description { get; init; }

    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }

    public int? UsersCount { get; init; }

    /// <summary>
    /// The expression used to map <see cref="Role"/> to <see cref="RoleData"/>.
    /// </summary>
    public static Expression<Func<Role, RoleData>> Mapper(bool includeUsersCount = false) => o => new RoleData
    {
        Name = o.Name!,
        DisplayName = o.DisplayName,
        Description = o.Description,
        CreatedAt = o.CreatedAt,
        UpdatedAt = o.UpdatedAt,
        UsersCount = includeUsersCount ? o.UserRoles.AsQueryable().Count() : null,
    };
}
