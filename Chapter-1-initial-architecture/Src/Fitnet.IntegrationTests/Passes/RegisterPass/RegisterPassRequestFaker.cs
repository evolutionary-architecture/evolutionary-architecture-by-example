namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Passes.RegisterPass;

using SuperSimpleArchitecture.Fitnet.Passes.RegisterPass;

internal sealed class RegisterPassRequestFaker : Faker<RegisterPassRequest>
{
    public RegisterPassRequestFaker()
    {
        CustomInstantiator(faker =>
            new RegisterPassRequest(
                faker.Random.Guid(),
                faker.Date.Past().ToUniversalTime(),
                faker.Date.Future().ToUniversalTime()
            )
        );
    }
}