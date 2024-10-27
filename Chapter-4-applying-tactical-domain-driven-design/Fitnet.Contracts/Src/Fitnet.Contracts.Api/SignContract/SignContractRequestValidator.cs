namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.SignContract;

using FluentValidation;

internal sealed class SignContractRequestValidator : AbstractValidator<SignContractRequest>
{
    private const int SignatureMaximumLength = 100;

    public SignContractRequestValidator()
    {
        RuleFor(signContractRequest => signContractRequest.Signature)
            .NotEmpty()
            .MaximumLength(SignatureMaximumLength);

        RuleFor(signContractRequest => signContractRequest.SignedAt)
            .NotEmpty();
    }
}
