namespace EvolutionaryArchitecture.Fitnet.DomainDrivenDesign.BuildingBlocks;

public interface IDomainEvent
{
    Guid Id { get; }

    DateTime OccurredOn { get; }
}
