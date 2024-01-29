namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Prepare;

using FluentValidation;

internal sealed class PrepareContractRequestValidator : AbstractValidator<PrepareContractRequest>
{
    public PrepareContractRequestValidator()
    {
        RuleFor(request => request.CustomerId).NotEmpty();
        RuleFor(request => request.CustomerAge).GreaterThan(0);
        RuleFor(request => request.CustomerHeight).GreaterThan(0);
        RuleFor(request => request.PreparedAt).NotEmpty();
    }
}