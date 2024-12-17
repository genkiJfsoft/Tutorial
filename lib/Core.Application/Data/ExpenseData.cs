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
}
