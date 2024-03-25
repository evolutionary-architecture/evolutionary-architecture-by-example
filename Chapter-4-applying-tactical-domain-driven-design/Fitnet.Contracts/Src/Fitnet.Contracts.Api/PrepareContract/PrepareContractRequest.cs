namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.PrepareContract;

using EvolutionaryArchitecture.Fitnet.Contracts.Application.PrepareContract;

internal sealed record PrepareContractRequest(Guid CustomerId, int CustomerAge, int CustomerHeight, DateTimeOffset PreparedAt)
{
    internal PrepareContractCommand ToCommand() => new(CustomerId, CustomerAge, CustomerHeight, PreparedAt);
}
