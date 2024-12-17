namespace ExpenseTracker.Core.Domain;

/// <inheritdoc cref="IdentityUserClaim{TKey}" />
public class UserClaim : IdentityUserClaim<string>, IEntity, ITimestampable
{
    public string? Description { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
