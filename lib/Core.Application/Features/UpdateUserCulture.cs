using System.Globalization;
using Ardalis.Result;
using ExpenseTracker.Core.Application.Services;

namespace ExpenseTracker.Core.Application.Features;

public record UpdateUserCulture : IRequest<bool>
{
    public required CultureInfo Culture { get; init; }

    internal class RequestHandler(IUserContext userContext) : IRequestHandler<UpdateUserCulture, bool>
    {
        public Task<bool> Handle(UpdateUserCulture request, CancellationToken cancellationToken)
        {
            // Save pref to db

            if (userContext.User == null) {
            }

            return Task.FromResult(true);
        }
    }
}
