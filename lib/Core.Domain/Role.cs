namespace ExpenseTracker.Core.Domain;

/// <summary>
/// Represents a role in the application.
/// </summary>
/// <remarks>
/// Derived from the default implementation of <see cref="IdentityRole"/>
/// which uses string as the primary key.
/// </remarks>
public class Role : IdentityRole, IEntity, ITimestampable
{
    public Role(string roleName) : base(roleName)
    {
    }

    public Role() : base()
    {

    }

    public string? DisplayName { get; set; }
    
    public string? Description { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public virtual ICollection<RoleClaim> RoleClaims { get; set; } = [];

    public virtual ICollection<UserRole> UserRoles { get; set; } = [];
}
