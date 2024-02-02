namespace EvolutionaryArchitecture.Fitnet.DomainDrivenDesign.BuildingBlocks;

public abstract class Entity
{
    private List<IDomainEvent>? _events;
    public IReadOnlyCollection<IDomainEvent> Events => _events?.AsReadOnly() ?? new List<IDomainEvent>().AsReadOnly();

    protected void RecordEvent(IDomainEvent domainEvent)
    {
        _events ??= [];
        _events.Add(domainEvent);
    }
}
