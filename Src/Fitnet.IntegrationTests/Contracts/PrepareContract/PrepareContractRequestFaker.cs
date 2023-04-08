namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Contracts.PrepareContract;

using SuperSimpleArchitecture.Fitnet.Contracts.PrepareContract;

internal sealed class PrepareContractRequestFaker : Faker<PrepareContractRequest>
{
    public PrepareContractRequestFaker(int minAge, int maxAge, int minHeight, int maxHeight)
    {
        CustomInstantiator(faker =>
            new PrepareContractRequest(
                faker.Random.Number(minAge, maxAge),
                faker.Random.Number(minHeight, maxHeight),
                faker.Date.RecentOffset().ToUniversalTime()
            )
        );
    }
}