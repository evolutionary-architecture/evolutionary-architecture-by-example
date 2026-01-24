namespace EvolutionaryArchitecture.Fitnet.UnitTests.Contracts.PrepareContract.RequestValidator;

using EvolutionaryArchitecture.Fitnet.Contracts.PrepareContract;

internal sealed class InvalidPrepareContractRequestTestCases : TheoryData<PrepareContractRequest, string>
{
    private readonly Faker _faker = new();
    private readonly DateTimeOffset _fakeNow = new Faker().Date.RecentOffset();

    public InvalidPrepareContractRequestTestCases()
    {
        var validContractParameters = PrepareContractParameters.GetValid();

        Add(new PrepareContractRequest(Guid.Empty, validContractParameters.MinAge, validContractParameters.MaxHeight, _fakeNow), nameof(PrepareContractRequest.CustomerId));
        Add(new PrepareContractRequest(Guid.NewGuid(), default, validContractParameters.MaxHeight, _fakeNow), nameof(PrepareContractRequest.CustomerAge));
        Add(new PrepareContractRequest(Guid.NewGuid(), _faker.Random.Number(-100, -1), validContractParameters.MaxHeight, _fakeNow), nameof(PrepareContractRequest.CustomerAge));
        Add(new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge, default, _fakeNow), nameof(PrepareContractRequest.CustomerHeight));
        Add(new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge, _faker.Random.Number(-100, -1), _fakeNow), nameof(PrepareContractRequest.CustomerHeight));
        Add(new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge, validContractParameters.MaxHeight, default), nameof(PrepareContractRequest.PreparedAt));
        Add(new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge, validContractParameters.MaxHeight, default), nameof(PrepareContractRequest.PreparedAt));
    }
}
