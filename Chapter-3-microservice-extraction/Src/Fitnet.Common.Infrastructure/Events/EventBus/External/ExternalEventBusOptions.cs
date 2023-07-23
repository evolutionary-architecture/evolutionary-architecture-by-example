namespace EvolutionaryArchitecture.Fitnet.Common.Infrastructure.Events.EventBus.External;

internal sealed class ExternalEventBusOptions
{
    public string? Uri { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
}