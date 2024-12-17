namespace ExpenseTracker.Core.Application.Data;

public record ExpenseData
{
    public required string Title { get; init; }
    public required decimal Amount { get; init; }
    public string? Remarks { get; init; }
    public string? TransactionBy { get; init; }
    public required string CreatedBy { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTimeOffset? TransactionAt { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
    
    /// <summary>
    /// Create a <see cref="ExpenseData"/> from the given <see cref="Expense"/>.
    /// </summary>
    /// <remarks>
    /// This manually maps the <see cref="Expense"/> properties to the <see cref="ExpenseData"/> properties.
    /// <para />
    /// For complex mapping, consider using a mapper like <see href="https://docs.automapper.org/">AutoMapper</see>.
    /// </remarks>
    public static ExpenseData Create(Expense o)
    {
        return new ExpenseData
        {
            Title = o.Title,
            Amount = o.Amount,
            Remarks = o.Remarks,
            TransactionBy = o.TransactionBy,
            CreatedBy = o.CreatedBy,
            UpdatedBy = o.UpdatedBy,
            TransactionAt = o.TransactionAt,
            CreatedAt = o.CreatedAt,
            UpdatedAt = o.UpdatedAt
        };
    }
}
