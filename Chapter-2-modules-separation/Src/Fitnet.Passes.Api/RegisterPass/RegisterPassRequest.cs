namespace EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;

public record RegisterPassRequest(Guid CustomerId, DateTimeOffset From, DateTimeOffset To);