using System.Reflection;
using ExpenseTracker.Core.Application.Services;

namespace ExpenseTracker.Core.Providers.Persistence;

public class DefaultDbContext : DbContext, IDbContext
{
    public DbSet<Expense> Expenses { get; set; } = null!;
    
    protected DefaultDbContext()
    {
    }
    
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
