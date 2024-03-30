﻿namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract;

internal sealed class SignContractBuilder(Contract parentBuilder)
{
    private DateTimeOffset _signDay;
    private DateTimeOffset _fakeToday;

    public SignContractBuilder SignedOn(DateTimeOffset signDay, DateTimeOffset fakeToday)
    {
        _signDay = signDay;
        _fakeToday = fakeToday;

        return this;
    }

    private BindingContract Build()
    {
        var bindingContract = parentBuilder.Sign(_signDay, _fakeToday);

        return bindingContract;
    }

    public static implicit operator BindingContract(SignContractBuilder builder) => builder.Build();
}
