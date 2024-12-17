using ExpenseTracker.Core.Domain.Abstractions;
using ExpenseTracker.Core.Providers.Persistence.Builders;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ExpenseTracker.Core.Providers.Persistence.Interceptors;

public class TimestampableEntityInterceptor(TimeProvider dateTime) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<ITimestampable>())
        {
            if (entry.State is EntityState.Added or EntityState.Modified || entry.HasModifiedOwnedEntities())
            {
                var now = dateTime.GetLocalNow();
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                }
                entry.Entity.UpdatedAt = now;
            }
        }
    }
}
