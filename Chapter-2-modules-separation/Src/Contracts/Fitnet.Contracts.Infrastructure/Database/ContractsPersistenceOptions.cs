namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure.Database;

using System.ComponentModel.DataAnnotations;

internal sealed class ContractsPersistenceOptions
{
    public const string SectionName = "Modules:Contracts:ConnectionStrings";

    [Required]
    public string Primary { get; init; } = string.Empty;
}
