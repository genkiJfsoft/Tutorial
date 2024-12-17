using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Core.Domain;

/// <summary>
/// Represents the application user.
/// </summary>
/// <remarks>
/// Derived from the default implementation of <see cref="IdentityUser"/>
/// which uses string as the primary key.
/// </remarks>
public class User : IdentityUser, IEntity, ITimestampable
{
    public User(string userName) : base(userName)
    {
    }

    public User() : base()
    {
    }

    public string? DisplayName { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public virtual ICollection<UserClaim> UserClaims { get; set; } = [];
    public virtual ICollection<UserLogin> Logins { get; set; } = [];
    public virtual ICollection<UserRole> UserRoles { get; set; } = [];
    public virtual ICollection<UserToken> Tokens { get; set; } = [];
}
