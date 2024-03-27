namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.UnitTests.PrepareContract.RequestValidator;

using EvolutionaryArchitecture.Fitnet.Contracts.Api.PrepareContract;

internal sealed class InvalidPrepareContractRequestTestCases : TheoryData<PrepareContractRequest>
{
    private readonly Faker _faker = new();
    private readonly DateTimeOffset _fakeNow = new Faker().Date.RecentOffset();

    public InvalidPrepareContractRequestTestCases()
    {
        var validContractParameters = PrepareContractRequestParameters.GetValid();
        AddRow(
            new PrepareContractRequest(Guid.Empty, validContractParameters.MinAge, validContractParameters.MaxHeight,
                _fakeNow), nameof(PrepareContractRequest.CustomerId));
        AddRow(new PrepareContractRequest(Guid.NewGuid(), default, validContractParameters.MaxHeight, _fakeNow),
            nameof(PrepareContractRequest.CustomerAge));
        AddRow(
            new PrepareContractRequest(Guid.NewGuid(), _faker.Random.Number(-100, -1),
                validContractParameters.MaxHeight, _fakeNow), nameof(PrepareContractRequest.CustomerAge));
        AddRow(new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge, default, _fakeNow),
            nameof(PrepareContractRequest.CustomerHeight));
        AddRow(
            new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge, _faker.Random.Number(-100, -1),
                _fakeNow), nameof(PrepareContractRequest.CustomerHeight));
        AddRow(
            new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge,
                validContractParameters.MaxHeight, default), nameof(PrepareContractRequest.PreparedAt));
        AddRow(
            new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge,
                validContractParameters.MaxHeight, default), nameof(PrepareContractRequest.PreparedAt));
    }
}
