namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.SignContract;

using EvolutionaryArchitecture.Fitnet.Contracts.Application.SignContract;

internal sealed record SignContractRequest(DateTimeOffset SignedAt)
{
    internal SignContractCommand ToCommand(Guid id) =>
        new(id, SignedAt);
}
