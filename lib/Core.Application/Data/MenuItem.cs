using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Application.Data;
public class MenuItem
{
    public int Id { get; set; } // Primary Key
    public string Title { get; set; } = ""; // The menu item title
    public string Route { get; set; } = ""; // Optional: Route for navigation
    public int ParentId { get; set; } // 0 for root items, or ID of the parent menu item
}

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<MenuItem> MenuItems { get; set; } // Represents the MenuItem table
}
