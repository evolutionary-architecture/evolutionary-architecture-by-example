namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Contracts.SignContract;

using SuperSimpleArchitecture.Fitnet.Contracts.SignContract;

internal sealed class SignContractRequestFaker : Faker<SignContractRequest>
{
    public SignContractRequestFaker(DateTimeOffset signedAt)
    {
        CustomInstantiator(_ =>
            new SignContractRequest(signedAt)
        );
    }
}