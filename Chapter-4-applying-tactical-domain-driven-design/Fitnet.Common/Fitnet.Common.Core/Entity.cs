namespace EvolutionaryArchitecture.Fitnet.Common.Core;

/// <summary>
/// Base abstract class representing an entity in the domain model.
/// An entity is an object that is defined by its identity and continues to exist over time.
/// </summary>
public abstract class Entity
{
    private IList<IDomainEvent>? _events;

    /// <summary>
    /// Gets the collection of domain events recorded for this entity.
    /// Domain events represent significant state changes or occurrences within the entity's lifecycle.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> Events => _events?.AsReadOnly() ?? new List<IDomainEvent>().AsReadOnly();

    /// <summary>
    /// Records a domain event for this entity.
    /// Domain events are recorded to track changes or actions that have occurred within the entity.
    /// </summary>
    /// <param name="domainEvent">The domain event to be recorded.</param>
    protected void RecordEvent(IDomainEvent domainEvent)
    {
        _events ??= [];
        _events.Add(domainEvent);
    }
}
