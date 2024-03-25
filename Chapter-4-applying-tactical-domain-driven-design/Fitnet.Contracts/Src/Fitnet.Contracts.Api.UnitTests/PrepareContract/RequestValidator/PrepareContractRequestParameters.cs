namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.UnitTests.PrepareContract.RequestValidator;

internal sealed record PrepareContractRequestParameters(int MinAge, int MaxAge, int MinHeight, int MaxHeight)
{
    private const int MinimumAge = 18;
    private const int MaximumAge = 100;
    private const int MinimumHeight = 0;
    private const int MaximumHeight = 210;

    internal static PrepareContractRequestParameters GetValid() =>
        new(MinimumAge, MaximumAge, MinimumHeight, MaximumHeight);
}