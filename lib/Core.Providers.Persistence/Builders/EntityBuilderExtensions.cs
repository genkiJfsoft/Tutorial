using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ExpenseTracker.Core.Providers.Persistence.Builders;

internal static class EntityBuilderExtensions
{
    /// <summary>
    /// Checks if any of the owned entities of the given <see cref="EntityEntry"/> have been modified.
    /// </summary>
    /// <returns>True, if any of the owned entities have been modified; otherwise, false.</returns>
    public static bool HasModifiedOwnedEntities(this EntityEntry entry)
    {
        return entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            r.TargetEntry.State is EntityState.Added or EntityState.Modified);
    }
}
