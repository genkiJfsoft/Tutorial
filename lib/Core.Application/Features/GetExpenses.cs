namespace ExpenseTracker.Core.Application.Features;

public record GetExpenses : IRequest<Result<List<ExpenseData>>>
{
    internal class RequestHandler() : IRequestHandler<GetExpenses, Result<List<ExpenseData>>>
    {
        public Task<Result<List<ExpenseData>>> Handle(GetExpenses request, CancellationToken cancellationToken)
        {
            var list = new List<ExpenseData>()
            {
                new ExpenseData
                {
                    Amount = 1000,
                    Title = "Purchase Item A",
                    CreatedBy = "N/A",
                    CreatedAt = DateTime.Now.AddDays(-1),
                }
            };

            return Task.FromResult(Result<List<ExpenseData>>.Success(list));
        }
    }
}
