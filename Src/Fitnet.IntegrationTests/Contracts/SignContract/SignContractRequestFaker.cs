using SuperSimpleArchitecture.Fitnet.Contracts.SignContract;

namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Contracts.SignContract;

internal sealed class SignContractRequestFaker : Faker<SignContractRequest>
{
    public SignContractRequestFaker()
    {
        CustomInstantiator((faker) =>
            new SignContractRequest(faker.Date.RecentOffset())
        );
    }
}