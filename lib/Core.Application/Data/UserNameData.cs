using System.Linq.Expressions;
using ExpenseTracker.Extensions.Linq;
using ExpenseTracker.Extensions.Linq.Expressions;

namespace ExpenseTracker.Core.Application.Data;

public class UserNameData
{
    public string? Id { get; init; }
    public string? UserName { get; init; }
    public string? DisplayName { get; init; }

    /// <summary>
    /// The expression used to map <see cref="User"/> to <see cref="UserNameData"/>.
    /// </summary>
    /// <remarks>
    /// This manually maps the <see cref="User"/> properties to the <see cref="UserNameData"/> properties.
    /// <para />
    /// For complex mapping, consider using a mapper like <see href="https://docs.automapper.org/">AutoMapper</see>.
    /// </remarks>
    public static Expression<Func<User, UserNameData>> Mapper => o => new UserNameData
    {
        Id = o.Id,
        UserName = o.UserName,
        DisplayName = o.DisplayName,
    };

    /// <summary>
    /// Create an instance of <see cref="UserNameData"/> from the given <see cref="User"/>
    /// </summary>
    [ReplaceWithExpression(PropertyName = nameof(Mapper))]
    public static UserNameData Make(User o) => Mapper.Compile().Invoke(o);
    
    [ReplaceWithExpression(PropertyName = nameof(Mapper))]
    public static UserNameData? MakeOrNull(User? o) => o != null ?  Mapper.Compile().Invoke(o) : null;
}
