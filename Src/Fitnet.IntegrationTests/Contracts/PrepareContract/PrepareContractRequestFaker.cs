namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Contracts.PrepareContract;

using SuperSimpleArchitecture.Fitnet.Contracts.PrepareContract;

internal sealed class PrepareContractRequestFaker : Faker<PrepareContractRequest>
{
    public PrepareContractRequestFaker(int minAge, int maxAge, int minHeight, int maxHeight,
        DateTimeOffset? preparedAt = null)
    {
        CustomInstantiator(faker =>
            new PrepareContractRequest(
                faker.Random.Number(minAge, maxAge),
                faker.Random.Number(minHeight, maxHeight),
                preparedAt?.ToUniversalTime() ?? faker.Date.RecentOffset().ToUniversalTime()
            )
        );
    }
}