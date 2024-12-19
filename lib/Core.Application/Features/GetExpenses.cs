using ExpenseTracker.Core.Application.Services;
using ExpenseTracker.Core.Domain.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Core.Application.Features;

[Authorize]
public record GetExpenses : IRequest<Result<List<ExpenseData>>>
{
    public int Limit { get; init; } = 10;
    
    internal class RequestHandler(IDbContext dbContext) : IRequestHandler<GetExpenses, Result<List<ExpenseData>>>
    {
        public async Task<Result<List<ExpenseData>>> Handle(GetExpenses request, CancellationToken cancellationToken)
        {
            var list = await dbContext.Expenses.AsNoTracking()
                .Include(e => e.TransactionByUser)
                .Include(e => e.CreatedByUser)
                .Include(e => e.UpdatedByUser)
                .OrderBy(e => e.Id)
                .Take(request.Limit)
                .Select((e) => ExpenseData.Create(e))
                .ToListAsync(cancellationToken);

            return Result<List<ExpenseData>>.Success(list);
        }
    }
}
