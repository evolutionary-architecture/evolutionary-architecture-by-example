namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.UnitTests.Prepare.RequestValidator;

using EvolutionaryArchitecture.Fitnet.Contracts.Api.Prepare;

internal sealed class InvalidPrepareContractRequestTestCases : TheoryData<string, int, int, DateTimeOffset, string>
{
    private readonly Faker _faker = new();
    private readonly DateTimeOffset _fakeNow = new Faker().Date.RecentOffset();

    public InvalidPrepareContractRequestTestCases()
    {
        var validContractParameters = PrepareContractRequestParameters.GetValid();
        Add(Guid.Empty.ToString(), validContractParameters.MinAge, validContractParameters.MaxHeight, _fakeNow, nameof(PrepareContractRequest.CustomerId));
        Add(Guid.NewGuid().ToString(), default, validContractParameters.MaxHeight, _fakeNow, nameof(PrepareContractRequest.CustomerAge));
        Add(Guid.NewGuid().ToString(), _faker.Random.Number(-100, -1), validContractParameters.MaxHeight, _fakeNow, nameof(PrepareContractRequest.CustomerAge));
        Add(Guid.NewGuid().ToString(), validContractParameters.MinAge, default, _fakeNow, nameof(PrepareContractRequest.CustomerHeight));
        Add(Guid.NewGuid().ToString(), validContractParameters.MinAge, _faker.Random.Number(-100, -1), _fakeNow, nameof(PrepareContractRequest.CustomerHeight));
        Add(Guid.NewGuid().ToString(), validContractParameters.MinAge, validContractParameters.MaxHeight, default, nameof(PrepareContractRequest.PreparedAt));
        Add(Guid.NewGuid().ToString(), validContractParameters.MinAge, validContractParameters.MaxHeight, default, nameof(PrepareContractRequest.PreparedAt));
    }
}
