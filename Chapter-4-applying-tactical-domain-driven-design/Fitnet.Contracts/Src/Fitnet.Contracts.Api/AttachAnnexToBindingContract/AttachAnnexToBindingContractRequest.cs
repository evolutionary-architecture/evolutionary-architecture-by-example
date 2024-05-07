namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.AttachAnnexToBindingContract;

using Application.AttachAnnexToBindingContract;

internal sealed record AttachAnnexToBindingContractRequest(DateTimeOffset ValidFrom)
{
    internal AttachAnnexToBindingContractCommand ToCommand(Guid bindingContractId) =>
        new(bindingContractId, ValidFrom);
}
