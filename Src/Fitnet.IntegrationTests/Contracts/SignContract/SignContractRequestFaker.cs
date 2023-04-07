using SuperSimpleArchitecture.Fitnet.Contracts.SignContract;

namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Contracts.SignContract;

internal sealed class SignContractRequestFaker : Faker<SignContractRequest>
{
    public SignContractRequestFaker()
    {
        CustomInstantiator(_ =>
            new SignContractRequest(DateTimeOffset.Now)
        );
    }
}