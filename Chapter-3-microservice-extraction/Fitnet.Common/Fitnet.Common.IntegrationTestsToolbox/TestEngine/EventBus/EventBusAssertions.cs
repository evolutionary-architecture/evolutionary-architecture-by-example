namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.EventBus;

using MassTransit.Testing;
using Shouldly;

public static class EventBusAssertions
{
    public static void EnsureConsumed<TEvent>(this ITestHarness harness) where TEvent : class =>
        harness.Consumed.Select<TEvent>().Any().ShouldBeTrue();
}
