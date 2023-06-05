namespace EvolutionaryArchitecture.Fitnet.Reports.IntegrationTests.GenerateNewPassesRegistrationsPerMonthReport;

using Passes.Api.RegisterPass;

internal sealed class RegisterPassRequestFaker : Faker<RegisterPassRequest>
{
    internal RegisterPassRequestFaker(DateTimeOffset from, DateTimeOffset to)
    {
        CustomInstantiator(faker =>
            new RegisterPassRequest(
                faker.Random.Guid(),
                from,
                to
            )
        );
    }
}