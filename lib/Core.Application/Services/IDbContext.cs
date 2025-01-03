using ExpenseTracker.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Core.Application.Services;

public interface IDbContext
{
    DbSet<Expense> Expenses { get; set; }
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
