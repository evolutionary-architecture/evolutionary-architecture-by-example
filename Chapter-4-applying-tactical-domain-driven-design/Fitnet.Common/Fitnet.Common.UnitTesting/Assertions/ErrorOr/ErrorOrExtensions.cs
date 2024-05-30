namespace EvolutionaryArchitecture.Fitnet.Common.UnitTesting.Assertions.ErrorOr;

public static class ErrorOrExtensions
{
    public static ErrorOrSuccessAssertions Should(this ErrorOr<Success> instance) => new(instance);
}
