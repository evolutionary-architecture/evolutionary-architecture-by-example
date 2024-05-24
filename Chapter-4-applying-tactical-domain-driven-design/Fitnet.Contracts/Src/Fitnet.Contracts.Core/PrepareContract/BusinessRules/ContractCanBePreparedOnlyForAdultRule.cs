namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.PrepareContract.BusinessRules;

using Common.BussinessRules;

internal sealed class ContractCanBePreparedOnlyForAdultRule : IBusinessRule
{
    private readonly int _age;

    internal ContractCanBePreparedOnlyForAdultRule(int age) => _age = age;

    public bool IsMet() => _age >= 18;

    public Error Error => BusinessRuleError.Create(nameof(ContractCanBePreparedOnlyForAdultRule), "Contract can not be prepared for a person who is not adult");
}
