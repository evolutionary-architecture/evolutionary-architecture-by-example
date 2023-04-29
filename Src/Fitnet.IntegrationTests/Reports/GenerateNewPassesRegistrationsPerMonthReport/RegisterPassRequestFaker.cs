namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Reports.GenerateNewPassesRegistrationsPerMonthReport;

using SuperSimpleArchitecture.Fitnet.Passes.RegisterPass;

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