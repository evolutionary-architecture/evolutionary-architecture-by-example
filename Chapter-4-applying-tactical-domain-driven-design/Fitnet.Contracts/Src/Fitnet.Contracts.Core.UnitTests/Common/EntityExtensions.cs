namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.Common;

using Fitnet.Common.Core;

internal static class EntityExtensions
{
    public static TEvent? GetPublishedEvent<TEvent>(this Entity entity) where TEvent : class, IDomainEvent => entity.Events.OfType<TEvent>()
        .MinBy(domainEvent => domainEvent.OccuredAt);
}
