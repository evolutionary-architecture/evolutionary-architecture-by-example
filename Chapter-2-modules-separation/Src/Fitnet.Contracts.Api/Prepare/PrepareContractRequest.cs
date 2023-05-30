namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Prepare;

using Application.Commands.Prepare;

internal sealed record PrepareContractRequest(int CustomerAge, int CustomerHeight, DateTimeOffset PreparedAt)
{
   internal PrepareContractCommand ToCommand() => new(CustomerAge, CustomerHeight, PreparedAt);
}