namespace ExpenseTracker.Core.Providers.Persistence;

public static partial class DbInitializer
{
    /// <summary>
    /// Initializes the database at runtime by applying any pending migrations.
    /// Will create the database if it does not already exist.
    /// </summary>
    /// <remarks>
    /// See <see href="https://learn.microsoft.com/en-gb/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli#apply-migrations-at-runtime">Apply migrations at runtime</see> for more information.
    /// </remarks>
    public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
    {
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("DbInitializer");

        try
        {
            var dataContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<DefaultDbContext>>();
            await using var dbContext = await dataContextFactory.CreateDbContextAsync();

            logger.LogInformation("Initializing database...");
            
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                logger.LogInformation("Applying pending migrations...");
                await dbContext.Database.MigrateAsync();
                logger.LogInformation("Migrations applied successfully.");
            }
            else
            {
                logger.LogInformation("No pending migrations found.");
            }

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }
}
