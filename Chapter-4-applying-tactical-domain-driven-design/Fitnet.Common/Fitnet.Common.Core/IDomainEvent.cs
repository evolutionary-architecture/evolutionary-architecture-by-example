namespace EvolutionaryArchitecture.Fitnet.Common.Core;

/// <summary>
/// Represents a domain event, which encapsulates a significant occurrence or state change within the domain model.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets the unique identifier of the domain event.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the timestamp when the domain event occurred.
    /// </summary>
    DateTime OccuredAt { get; }
}
