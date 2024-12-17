namespace ExpenseTracker.Core.Application.Services;

/// <summary>
/// Defines a sender that creates a new service scope to send a request through the Mediator pipeline to be handled by a single handler.
/// </summary>
public interface IScopedSender : ISender
{
}
