namespace ExpenseTracker.Core.Domain;

public class Expense : IEntity, ITimestampable
{
    public string Id { get; set; }
    public required string Title { get; set; }
    public required decimal Amount { get; set; }
    public string? Remarks { get; set; }
    public string? TransactionBy { get; set; }
    public required string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? TransactionAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public virtual User? TransactionByUser { get; } = default;
    public virtual User CreatedByUser { get; } = null!;
    public virtual User? UpdatedByUser { get; } = default;

    public Expense()
    {
        Id = Guid.CreateVersion7().ToString();
    }
}

