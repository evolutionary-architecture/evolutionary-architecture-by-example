namespace EvolutionaryArchitecture.Fitnet.Contracts.SignContract;

using FluentValidation;

internal sealed class SignContractRequestValidator : AbstractValidator<SignContractRequest>
{
    public SignContractRequestValidator() => RuleFor(signContractRequest => signContractRequest.SignedAt)
            .NotEmpty();
}
