namespace EvolutionaryArchitecture.Fitnet.Reports.DataAccess;

using System.ComponentModel.DataAnnotations;

internal sealed class ReportsPersistenceOptions
{
    public const string SectionName = "Modules:Reports:ConnectionStrings";

    [Required]
    public string Primary { get; init; } = string.Empty;
}
