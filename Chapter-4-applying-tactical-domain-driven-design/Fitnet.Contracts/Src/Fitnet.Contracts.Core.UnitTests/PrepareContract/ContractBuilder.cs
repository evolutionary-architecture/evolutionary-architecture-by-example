namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.PrepareContract;

internal sealed class ContractBuilder
{
    public static ContractBuilder Create() => new();

    private DateTimeOffset _preparedAt;

    public ContractBuilder PreparedAt(DateTimeOffset preparedAt)
    {
        _preparedAt = preparedAt;
        return this;
    }

    public PreparedContractBuilder Prepared() => new(Build());

    private Contract Build()
    {
        var prepareContractParameters = PrepareContractParameters.GetValid();
        var contract = Contract.Prepare(
            Guid.NewGuid(),
            prepareContractParameters.MaxAge,
            prepareContractParameters.MaxHeight,
            _preparedAt);

        return contract;
    }

    public static implicit operator Contract(ContractBuilder builder) => builder.Build();
}
