namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.SignContract;

using EvolutionaryArchitecture.Fitnet.Contracts.Application.SignContract;

internal sealed record SignContractRequest(DateTimeOffset SignedAt, string SignatureText)
{
    internal SignContractCommand ToCommand(Guid id) =>
        new(id, SignatureText, SignedAt);
}
