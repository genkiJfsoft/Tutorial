namespace ExpenseTracker.Core.Domain;

/// <inheritdoc cref="IdentityUserLogin{TKey}" />
public class UserLogin : IdentityUserLogin<string>, IEntity
{
    public virtual User User { get; set; } = null!;
}
