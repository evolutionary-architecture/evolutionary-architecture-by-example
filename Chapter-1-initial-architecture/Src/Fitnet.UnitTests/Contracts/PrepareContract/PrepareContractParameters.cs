namespace EvolutionaryArchitecture.Fitnet.UnitTests.Contracts.PrepareContract;

internal sealed record PrepareContractParameters(int MinAge, int MaxAge, int MinHeight, int MaxHeight)
{
    private const int _minAge = 18;
    private const int _maxAge = 100;
    private const int _minHeight = 0;
    private const int _maxHeight = 210;

    internal static PrepareContractParameters GetValid() => new(_minAge, _maxAge, _minHeight, _maxHeight);
}