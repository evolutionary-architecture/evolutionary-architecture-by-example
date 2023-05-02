namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Common.TestEngine.IntegrationEvents.Handlers;

using MediatR;
using SuperSimpleArchitecture.Fitnet.Shared.Events;

internal sealed class IntegrationEventHandlerScope : IDisposable
{
    private readonly IServiceScope _serviceScope;

    public IntegrationEventHandlerScope(WebApplicationFactory<Program> applicationInMemoryFactory) => 
        _serviceScope = applicationInMemoryFactory.Services.CreateScope();

    public IIntegrationEventHandler<TIntegrationEvent> GetIntegrationEventHandler<TIntegrationEvent>()
        where TIntegrationEvent : IIntegrationEvent
    {
        var integrationEventConsumer =
            _serviceScope.ServiceProvider.GetRequiredService<INotificationHandler<TIntegrationEvent>>();

        return (IIntegrationEventHandler<TIntegrationEvent>)integrationEventConsumer;
    }

    public void Dispose() => 
        _serviceScope.Dispose();
}