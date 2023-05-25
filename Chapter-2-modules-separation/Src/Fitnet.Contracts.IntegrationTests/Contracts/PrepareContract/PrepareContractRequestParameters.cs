namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.Contracts.PrepareContract;

internal sealed record PrepareContractRequestParameters(int MinAge, int MaxAge, int MinHeight, int MaxHeight)
{
    private const int _minAge = 18;
    private const int _maxAge = 100;
    private const int _minHeight = 0;
    private const int _maxHeight = 210;

    internal static PrepareContractRequestParameters GetValid() => new(_minAge, _maxAge, _minHeight, _maxHeight);

    internal static PrepareContractRequestParameters GetWithInvalidAge() =>
        new(0, _minAge - 1, _minHeight, _maxHeight);
    
    internal static PrepareContractRequestParameters GetWithInvalidHeight() =>
        new(_minAge, _maxAge, _maxHeight + 1, _maxHeight + 1);
}