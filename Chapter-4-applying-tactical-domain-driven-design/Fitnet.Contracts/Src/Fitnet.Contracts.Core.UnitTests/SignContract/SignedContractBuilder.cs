namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract;

using Common;

internal sealed class SignedContractBuilder(Contract parentBuilder)
{
    private DateTimeOffset? _signDay;
    private DateTimeOffset? _fakeToday;

    public SignedContractBuilder SignedOn(DateTimeOffset signDay, DateTimeOffset fakeToday)
    {
        _signDay = signDay;
        _fakeToday = fakeToday;

        return this;
    }

    private BindingContract Build()
    {
        var signDay = _signDay ?? FakeContractDates.SignDay;
        var fakeToday = _fakeToday ?? FakeContractDates.SignDay;
        var bindingContract = parentBuilder.Sign(signDay, fakeToday).Value;

        return bindingContract;
    }

    public static implicit operator BindingContract(SignedContractBuilder builder) => builder.Build();
}
