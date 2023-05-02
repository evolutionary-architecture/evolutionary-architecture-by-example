namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Common.TestEngine.IntegrationEvents.Handlers;

using MediatR;
using SuperSimpleArchitecture.Fitnet.Shared.Events;

internal static class WebApplicationFactoryExtensions
{
    internal static IIntegrationEventHandler<TIntegrationEvent> GetIntegrationEventHandler<TIntegrationEvent>(
        this WebApplicationFactory<Program> applicationInMemoryFactory)
        where TIntegrationEvent : IIntegrationEvent
    {
        var integrationEventConsumer =
            applicationInMemoryFactory.Services.GetRequiredService<INotificationHandler<TIntegrationEvent>>();

        return (IIntegrationEventHandler<TIntegrationEvent>)integrationEventConsumer;
    }
}