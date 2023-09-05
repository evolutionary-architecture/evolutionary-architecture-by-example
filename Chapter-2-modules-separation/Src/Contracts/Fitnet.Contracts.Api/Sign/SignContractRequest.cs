namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Sign;

using Application.Sign;

internal sealed record SignContractRequest(DateTimeOffset SignedAt)
{
    internal SignContractCommand ToCommand(Guid id) =>
        new(id, SignedAt);
}
