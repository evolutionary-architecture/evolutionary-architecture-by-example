namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.Common.Assertions.ErrorOr;

internal static class ErrorOrExtensions
{
    public static ErrorOrSuccessAssertions Should(this ErrorOr<Success> instance) => new(instance);
}
