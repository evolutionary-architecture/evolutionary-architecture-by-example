namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Contracts.PrepareContract;

internal sealed record PrepareContractRequestParameters(int MinAge, int MaxAge, int MinHeight, int MaxHeight)
{
    private const int MinimumAge = 18;
    private const int MaximumAge = 100;
    private const int MinimumHeight = 0;
    private const int MaximumHeight = 210;

    internal static PrepareContractRequestParameters GetValid() =>
        new(MinimumAge, MaximumAge, MinimumHeight, MaximumHeight);

    internal static PrepareContractRequestParameters GetWithInvalidAge() =>
        new(0, MinimumAge - 1, MinimumHeight, MaximumHeight);

    internal static PrepareContractRequestParameters GetWithInvalidHeight() =>
        new(MinimumAge, MaximumAge, MaximumHeight + 1, MaximumHeight + 1);
}
