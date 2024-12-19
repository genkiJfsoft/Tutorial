using System.Net;

namespace ExpenseTracker.Core.Application.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException(string message = "Access forbidden") : base(message)
    {
    }
}
