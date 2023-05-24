namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Passes.RegisterPass;

using EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;

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