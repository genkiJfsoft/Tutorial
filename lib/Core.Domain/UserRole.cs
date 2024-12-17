namespace ExpenseTracker.Core.Domain;

/// <inheritdoc cref="IdentityUserRole{TKey}" />
public class UserRole : IdentityUserRole<string>, IEntity
{
    public virtual User User { get; set; } = null!;
    public virtual Role Role { get; set; } = null!;
}
