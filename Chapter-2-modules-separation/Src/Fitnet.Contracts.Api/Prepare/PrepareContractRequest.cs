namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Prepare;

using Application.Commands.Preapre;

internal sealed record PrepareContractRequest(Guid CustomerId, int CustomerAge, int CustomerHeight, DateTimeOffset PreparedAt)
{
   internal PrepareContractCommand ToCommand() => new(CustomerId, CustomerAge, CustomerHeight, PreparedAt);
}