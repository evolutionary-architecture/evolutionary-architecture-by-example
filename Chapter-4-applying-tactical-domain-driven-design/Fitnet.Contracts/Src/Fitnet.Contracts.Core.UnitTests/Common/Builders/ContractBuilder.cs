﻿namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.PrepareContract;

using Common;
using SignContract;

internal sealed class ContractBuilder
{
    internal static ContractBuilder Prepared() => new();

    private DateTimeOffset? _preparedAt;

    public ContractBuilder PreparedAt(DateTimeOffset preparedAt)
    {
        _preparedAt = preparedAt;
        return this;
    }

    public SignedContractBuilder Signed() => new(Prepare());

    private Contract Prepare()
    {
        var preparedAt = _preparedAt ?? FakeContractDates.PreparedAt;
        var prepareContractParameters = PrepareContractParameters.GetValid();
        var contract = Contract.Prepare(
            Guid.NewGuid(),
            prepareContractParameters.MaxAge,
            prepareContractParameters.MaxHeight,
            preparedAt);

        return contract;
    }

    public static implicit operator Contract(ContractBuilder builder) => builder.Prepare();
}
