using ExpenseTracker.Core.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebApp.Infrastructure;

public class CustomExceptionHandler : IExceptionHandler
{
    private readonly Dictionary<Type, Func<HttpContext, Exception, CancellationToken, ValueTask<bool>>> _exceptionHandlers;

    public CustomExceptionHandler()
    {
        // Register known exception types and handlers.
        _exceptionHandlers = new()
            {
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            };
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();

        if (_exceptionHandlers.TryGetValue(exceptionType, out Func<HttpContext, Exception, CancellationToken, ValueTask<bool>>? handler))
        {
            return await handler.Invoke(httpContext, exception, cancellationToken);
        }

        return false;
    }

    private static ValueTask<bool> HandleUnauthorizedAccessException(HttpContext httpContext, Exception ex, CancellationToken cancellationToken)
    {
        // DO something;
        
        // Return false to continue with the default behavior
        // - or - return true to signal that this exception is handled
        return ValueTask.FromResult(false);
    }

    private static ValueTask<bool> HandleForbiddenAccessException(HttpContext httpContext, Exception ex, CancellationToken cancellationToken)
    {
        // DO something;
        
        // Return false to continue with the default behavior
        // - or - return true to signal that this exception is handled
        return ValueTask.FromResult(false);
    }
}
