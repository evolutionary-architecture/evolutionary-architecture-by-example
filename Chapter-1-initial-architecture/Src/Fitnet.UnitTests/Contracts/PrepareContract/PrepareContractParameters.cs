namespace EvolutionaryArchitecture.Fitnet.UnitTests.Contracts.PrepareContract;

internal sealed record PrepareContractParameters(int MinAge, int MaxAge, int MinHeight, int MaxHeight)
{
    private const int MinimumAge = 18;
    private const int MaximumAge = 100;
    private const int MinimumHeight = 0;
    private const int MaximumHeight = 210;

    internal static PrepareContractParameters GetValid() => new(MinimumAge, MaximumAge, MinimumHeight, MaximumHeight);
}
