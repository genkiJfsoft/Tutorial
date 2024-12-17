namespace ExpenseTracker.Core.Domain;

/// <inheritdoc cref="IdentityUserToken{TKey}" />
public class UserToken : IdentityUserToken<string>, IEntity
{
    public virtual User User { get; set; } = null!;
}
