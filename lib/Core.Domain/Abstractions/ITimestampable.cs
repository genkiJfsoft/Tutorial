namespace ExpenseTracker.Core.Domain.Abstractions;

/// <summary>
/// Represents an interface for entities with timestamps.
/// </summary>
/// <remarks>
/// Entities with this interface will have their timestamps
/// set when they are updated or first created.
/// </remarks>
public interface ITimestampable
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset? UpdatedAt { get; set; }
}
