namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.AttachAnnexToBindingContract;

using FluentValidation;

internal sealed class AttachAnnexToBindingContractRequestValidator : AbstractValidator<AttachAnnexToBindingContractRequest>
{
    public AttachAnnexToBindingContractRequestValidator() => RuleFor(request => request.ValidFrom).NotEmpty();
}
