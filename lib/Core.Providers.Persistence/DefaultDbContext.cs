using System.Reflection;
using ExpenseTracker.Core.Application.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ExpenseTracker.Core.Providers.Persistence;

internal class DefaultDbContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IDbContext
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
