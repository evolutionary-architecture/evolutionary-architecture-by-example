namespace EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

public class BusinessRuleValidationException(string message) : InvalidOperationException(message);
