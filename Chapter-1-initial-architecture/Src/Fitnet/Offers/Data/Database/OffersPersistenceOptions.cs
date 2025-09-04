namespace EvolutionaryArchitecture.Fitnet.Offers.Data.Database;

using System.ComponentModel.DataAnnotations;

internal sealed class OffersPersistenceOptions
{
    public const string SectionName = "ConnectionStrings";

    [Required] public string Offers { get; init; } = string.Empty;
}
