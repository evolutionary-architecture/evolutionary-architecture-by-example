namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.EventBus;

internal sealed class EventBusOptions
{
    public string? Uri { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
}
