namespace EvolutionaryArchitecture.Fitnet.Offers.DataAccess.Database;

using System.ComponentModel.DataAnnotations;

internal sealed class OffersPersistenceOptions
{
    public const string SectionName = "Modules:Offers:ConnectionStrings";

    [Required]
    public string Primary { get; init; } = string.Empty;
}
