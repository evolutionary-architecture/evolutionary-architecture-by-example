namespace EvolutionaryArchitecture.Fitnet.Common.Core.UnitTests.BusinessRulesEngine;

using BussinessRules;
using ErrorOr;

internal sealed class FakeBusinessRule : IBusinessRule
{
    private readonly int _someNumber;

    internal FakeBusinessRule(int someNumber) =>
        _someNumber = someNumber;

    public bool IsMet() => _someNumber > 10;
    public Error Error => Error.Custom(1, "test code", "Fake business rule was not met");
}
