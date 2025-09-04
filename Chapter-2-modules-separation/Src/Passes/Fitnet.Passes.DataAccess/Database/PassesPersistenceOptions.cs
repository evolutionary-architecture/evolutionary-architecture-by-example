namespace EvolutionaryArchitecture.Fitnet.Passes.DataAccess.Database;

using System.ComponentModel.DataAnnotations;

internal sealed class PassesPersistenceOptions
{
    public const string SectionName = "Modules:Passes:ConnectionStrings";

    [Required]
    public string Primary { get; init; } = string.Empty;
}
