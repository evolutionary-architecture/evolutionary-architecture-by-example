namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.EventBus;

using FluentAssertions;
using MassTransit.Testing;

public static class EventBusAssertions
{
    public static void EnsureConsumed<TEvent>(this ITestHarness harness) where TEvent : class =>
        harness.Consumed.Select<TEvent>().Any().Should().BeTrue();
}
