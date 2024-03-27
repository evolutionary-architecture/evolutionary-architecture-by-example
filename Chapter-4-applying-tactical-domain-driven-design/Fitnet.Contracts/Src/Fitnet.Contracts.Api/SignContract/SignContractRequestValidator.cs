namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.SignContract;

using FluentValidation;

internal sealed class SignContractRequestValidator : AbstractValidator<SignContractRequest>
{
    public SignContractRequestValidator() => RuleFor(signContractRequest => signContractRequest.SignedAt)
        .NotEmpty();
}
