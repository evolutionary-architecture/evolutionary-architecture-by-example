namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests.Common.Predicates;

using NetArchTest.Rules;

internal static class PredicatesExtensions
{
    internal static string[] GetNamespaces(this PredicateList predicates) =>
        predicates
            .GetTypes()
            .Select(type => type.Namespace)
            .Distinct()
            .ToArray()!;
}
