using ExpenseTracker.Core.Application.Services;

namespace ExpenseTracker.Core.Application.Features;

[Authorize]
public record GetExpenses : IRequest<Result<List<ExpenseData>>>
{
    public int Limit { get; init; } = 10;
    
    internal class RequestHandler(IDbContext dbContext) : IRequestHandler<GetExpenses, Result<List<ExpenseData>>>
    {
        public async Task<Result<List<ExpenseData>>> Handle(GetExpenses request, CancellationToken cancellationToken)
        {
            var list = await dbContext.Expenses
                .AsExpandable()
                .AsNoTracking()
                .Include(e => e.TransactionByUser)
                .Include(e => e.CreatedByUser)
                .Include(e => e.UpdatedByUser)
                .OrderBy(e => e.Id)
                .Take(request.Limit)
                .Select(ExpenseData.Mapper)
                .ToListAsync(cancellationToken);

            return Result<List<ExpenseData>>.Success(list);
        }
    }
}
