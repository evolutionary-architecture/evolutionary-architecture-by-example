namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.PrepareContract;

using Api.PrepareContract;

internal sealed class PrepareContractRequestFaker : Faker<PrepareContractRequest>
{
    internal PrepareContractRequestFaker(int minAge, int maxAge, int minHeight, int maxHeight,
        Guid? customerId = null) => CustomInstantiator(faker =>
        new PrepareContractRequest(
            customerId ?? faker.Random.Guid(),
            faker.Random.Number(minAge, maxAge),
            faker.Random.Number(minHeight, maxHeight),
            faker.Date.RecentOffset().ToUniversalTime()
        )
    );
}
