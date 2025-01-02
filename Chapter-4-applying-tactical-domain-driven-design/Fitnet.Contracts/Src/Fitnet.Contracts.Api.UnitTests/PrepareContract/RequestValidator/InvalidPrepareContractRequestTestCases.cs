namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.UnitTests.PrepareContract.RequestValidator;

using System.Collections;
using EvolutionaryArchitecture.Fitnet.Contracts.Api.PrepareContract;

internal sealed class InvalidPrepareContractRequestTestCases : IEnumerable<object[]>
{
    private readonly List<object[]> _testCases = [];
    private readonly Faker _faker = new();
    private readonly DateTimeOffset _fakeNow = new Faker().Date.RecentOffset();

    public InvalidPrepareContractRequestTestCases()
    {
        var validContractParameters = PrepareContractRequestParameters.GetValid();
        _testCases.Add([
            new PrepareContractRequest(Guid.Empty, validContractParameters.MinAge, validContractParameters.MaxHeight, _fakeNow),
            nameof(PrepareContractRequest.CustomerId)
        ]);
        _testCases.Add([
            new PrepareContractRequest(Guid.NewGuid(), default, validContractParameters.MaxHeight, _fakeNow),
            nameof(PrepareContractRequest.CustomerAge)
        ]);
        _testCases.Add([
            new PrepareContractRequest(Guid.NewGuid(), _faker.Random.Number(-100, -1), validContractParameters.MaxHeight, _fakeNow),
            nameof(PrepareContractRequest.CustomerAge)
        ]);
        _testCases.Add([
            new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge, default, _fakeNow),
            nameof(PrepareContractRequest.CustomerHeight)
        ]);
        _testCases.Add([
            new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge, _faker.Random.Number(-100, -1), _fakeNow),
            nameof(PrepareContractRequest.CustomerHeight)
        ]);
        _testCases.Add([
            new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge, validContractParameters.MaxHeight, default),
            nameof(PrepareContractRequest.PreparedAt)
        ]);
        _testCases.Add([
            new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge, validContractParameters.MaxHeight, default),
            nameof(PrepareContractRequest.PreparedAt)
        ]);
    }

    public IEnumerator<object[]> GetEnumerator() => _testCases.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
