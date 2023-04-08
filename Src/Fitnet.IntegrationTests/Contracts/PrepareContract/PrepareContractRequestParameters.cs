namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Contracts.PrepareContract;

internal record PrepareContractRequestParameters(int MinAge, int MaxAge, int MinHeight, int MaxHeight)
{
    internal static PrepareContractRequestParameters GetValid() => new(18, 100, 0, 210);
}
    