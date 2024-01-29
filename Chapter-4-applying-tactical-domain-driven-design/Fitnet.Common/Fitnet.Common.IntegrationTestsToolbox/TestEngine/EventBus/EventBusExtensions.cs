namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTestsToolbox.TestEngine.EventBus;

using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

public static class EventBusExtensions
{
    public static ITestHarness GetTestExternalEventBus<T>(
        this WebApplicationFactory<T> webApplicationFactory) where T : class =>
        webApplicationFactory.Services.GetRequiredService<ITestHarness>();

    public static WebApplicationFactory<T> WithTestEventBus<T>(
        this WebApplicationFactory<T> webApplicationFactory,
        params Type[] consumerTypes)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
            webHostBuilder.ConfigureTestServices(services =>
            {
                services.AddMassTransitTestHarness(configurator =>
                {
                    foreach (var consumerType in consumerTypes)
                    {
                        configurator.AddConsumer(consumerType);
                    }
                });
            }));

    public static async Task<IReceivedMessage<TMessage>?> WaitToConsumeMessageAsync<TMessage>(
        this ITestHarness testHarness,
        CancellationToken cancellationToken = default)
        where TMessage : class
    {
        var result = await testHarness.WaitToConsumeMessagesAllAsync<TMessage>(1, cancellationToken: cancellationToken);

        return result.SingleOrDefault();
    }

    public static async Task<IEnumerable<IReceivedMessage<TMessage>>> WaitToConsumeMessagesAllAsync<TMessage>(
        this ITestHarness testHarness,
        int messageCount,
        int maxDelaySeconds = 15,
        CancellationToken cancellationToken = default)
        where TMessage : class
    {
        var consumedMessages = testHarness.Consumed!.Select<TMessage>(cancellationToken).ToList();
        for (var i = 0; i < maxDelaySeconds; i++)
        {
            var shouldWait = consumedMessages.Count < messageCount;
            if (shouldWait)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }

        return testHarness.Consumed!.Select<TMessage>(cancellationToken)!.ToList();
    }
}
