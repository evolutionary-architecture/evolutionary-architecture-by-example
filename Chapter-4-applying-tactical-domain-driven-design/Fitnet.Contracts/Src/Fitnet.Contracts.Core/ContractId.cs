namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

public readonly record struct ContractId(Guid Value);

public readonly record struct BindingContractId(Guid Value)
{
    internal static BindingContractId Create() => new(Guid.NewGuid());
}
