﻿namespace EvolutionaryArchitecture.Fitnet.Common.Api.BussinessRules;

using ErrorOr;

public static class BusinessRuleValidator
{
    public static ErrorOr<Success> Validate(params IBusinessRule[] rules)
    {
        var errors = rules
            .Where(rule => !rule.IsMet())
            .Select(rule => rule.Error)
            .ToList();

        return errors.Count != 0 ? (ErrorOr<Success>)errors : new Success();
    }
}
