namespace ExpenseTracker.Core.Providers.Persistence;

public static partial class DbInitializer
{
    /// <summary>
    /// Defines a custom initialization logic to seed dummy data into the database.
    /// </summary>
    /// <remarks>
    /// See also <see href="https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding#model-managed-data">Model managed data</see>, for managing required data as part of 
    /// a model configuration with EF Core migrations.
    /// </remarks>
    public static class DummyData
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var timeProvider = serviceProvider.GetRequiredService<TimeProvider>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("DbInitializer.RequiredData");

            try
            {
                var dataContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<DefaultDbContext>>();
                await using var db = await dataContextFactory.CreateDbContextAsync();

                await SeedDataAsync(logger, db, timeProvider);

                db.ChangeTracker.Clear();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }
        
        private static async Task SeedDataAsync(ILogger logger, DefaultDbContext db, TimeProvider timeProvider)
        {
            if (!db.Expenses.Any())
            {
                logger.LogInformation("Seeding dummy expenses...");

                for (int i = 1; i <= 10; i++)
                {
                    db.Expenses.Add(new Expense
                    {
                        Amount = 100m,
                        Title = $"Expense #{i}",
                        CreatedBy = Identity.DefaultUserId,
                        CreatedAt = timeProvider.GetLocalNow(),
                    });
                }

                await db.SaveChangesAsync();
            }
        }
    }
}
