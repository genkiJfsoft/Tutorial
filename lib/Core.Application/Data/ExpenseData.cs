using System.Linq.Expressions;

namespace ExpenseTracker.Core.Application.Data;

public record ExpenseData
{
    public required string Title { get; init; }
    public required decimal Amount { get; init; }
    public string? Remarks { get; init; }
    public UserNameData? TransactionBy { get; init; }
    public required UserNameData CreatedBy { get; init; }
    public UserNameData? UpdatedBy { get; init; }
    public DateTimeOffset? TransactionAt { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }

    /// <summary>
    /// The expression used to map <see cref="Expense"/> to <see cref="ExpenseData"/>.
    /// </summary>
    public static Expression<Func<Expense, ExpenseData>> Mapper => o => new ExpenseData
    {
        Title = o.Title,
        Amount = o.Amount,
        Remarks = o.Remarks,
        TransactionBy = UserNameData.MakeOrNull(o.TransactionByUser),
        CreatedBy = UserNameData.Make(o.CreatedByUser),
        UpdatedBy = UserNameData.MakeOrNull(o.UpdatedByUser),
        TransactionAt = o.TransactionAt,
        CreatedAt = o.CreatedAt,
        UpdatedAt = o.UpdatedAt
    };
}
