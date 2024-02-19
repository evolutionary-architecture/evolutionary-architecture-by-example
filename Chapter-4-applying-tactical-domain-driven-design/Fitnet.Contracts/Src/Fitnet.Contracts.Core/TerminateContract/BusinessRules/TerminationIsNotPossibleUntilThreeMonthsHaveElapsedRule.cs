﻿namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.TerminateContract.BusinessRules;

using EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

internal sealed class TerminationIsNotPossibleUntilThreeMonthsHaveElapsedRule(DateTimeOffset bindingFrom, DateTimeOffset terminatedAt)
    : IBusinessRule
{
    private const int ThreeMonths = 3;

    public bool IsMet()
    {
        var threeMonthsFromBinding = bindingFrom.AddMonths(ThreeMonths);

        return terminatedAt >= threeMonthsFromBinding;
    }

    public string Error
    {
        get
        {
            var threeMonthsFromSignDate = bindingFrom.AddMonths(ThreeMonths);
            var daysRemaining = (threeMonthsFromSignDate - terminatedAt).Days;

            var error = $"Termination is not possible until three months have elapsed. '{daysRemaining}' days remaining.";

            return error;
        }
    }
}
