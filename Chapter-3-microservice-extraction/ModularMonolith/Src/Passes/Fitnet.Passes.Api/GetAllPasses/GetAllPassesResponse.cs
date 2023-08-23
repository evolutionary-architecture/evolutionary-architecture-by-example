namespace EvolutionaryArchitecture.Fitnet.Passes.Api.GetAllPasses;

using DataAccess;

internal record GetAllPassesResponse(IReadOnlyCollection<PassDto> Passes)
{
    internal static GetAllPassesResponse Create(IReadOnlyCollection<PassDto> passes) => new(passes);
}

internal record PassDto(Guid Id, Guid CustomerId)
{
    internal static PassDto From(Pass contract) => new(contract.Id, contract.CustomerId);
}