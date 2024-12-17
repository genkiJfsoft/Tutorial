namespace ExpenseTracker.Core.Domain;

/// <inheritdoc cref="IdentityRoleClaim{TKey}" />
public class RoleClaim : IdentityRoleClaim<string>, IEntity, ITimestampable
{
    public string? Description { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public virtual Role Role { get; set; } = null!;
}
