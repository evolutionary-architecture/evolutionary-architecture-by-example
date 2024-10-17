namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.Common.Builders;

using Core.SignContract.Signatures;

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
        var signature = Signature.From(_signDay, "John Doe");
        var bindingContract = parentBuilder.Sign(signature, _fakeToday);

        return bindingContract.Value;
    }

    public static implicit operator BindingContract(SignContractBuilder builder) => builder.Build();
}
