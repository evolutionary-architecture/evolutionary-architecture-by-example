namespace EvolutionaryArchitecture.Fitnet.Contracts.Data.Database;

using System.ComponentModel.DataAnnotations;

internal sealed class ContractsPersistenceOptions
{
    public const string SectionName = "ConnectionStrings";

    [Required] public string Contracts { get; init; } = string.Empty;
}
