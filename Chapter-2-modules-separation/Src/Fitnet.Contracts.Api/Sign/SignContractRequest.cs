namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Sign;

using Application.Commands.Sign;

public sealed record SignContractRequest(DateTimeOffset SignedAt)
{
    internal SignContractCommand ToCommand(Guid id) => 
        new(id, SignedAt);
}