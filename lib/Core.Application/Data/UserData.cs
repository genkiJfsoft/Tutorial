using System.Linq.Expressions;
using ExpenseTracker.Extensions.Linq;
using ExpenseTracker.Extensions.Linq.Expressions;

namespace ExpenseTracker.Core.Application.Data;

public class UserData
{
    public string? Id { get; init; }
    public string? UserName { get; init; }
    public string? DisplayName { get; init; }
    public string? Email { get; init; }
    public string? AssignedRole { get; init; }

    /// <summary>
    /// The expression used to map <see cref="User"/> to <see cref="UserData"/>.
    /// </summary>
    /// <remarks>
    /// This manually maps the <see cref="User"/> properties to the <see cref="UserData"/> properties.
    /// <para />
    /// For complex mapping, consider using a mapper like <see href="https://docs.automapper.org/">AutoMapper</see>.
    /// </remarks>
    public static Expression<Func<User, UserData>> Mapper => o => new UserData
    {
        Id = o.Id,
        UserName = o.UserName,
        DisplayName = o.DisplayName,
        Email = o.Email,
        AssignedRole = o.UserRoles.AsQueryable().Select(e => e.Role.Name).FirstOrDefault(),
    };

    /// <summary>
    /// Create an instance of <see cref="UserData"/> from the given <see cref="User"/>
    /// </summary>
    [ReplaceWithExpression(PropertyName = nameof(Mapper))]
    public static UserData Make(User o) => Mapper.Compile().Invoke(o);
    
    [ReplaceWithExpression(PropertyName = nameof(Mapper))]
    public static UserData? MakeOrNull(User? o) => o != null ?  Mapper.Compile().Invoke(o) : null;
}
