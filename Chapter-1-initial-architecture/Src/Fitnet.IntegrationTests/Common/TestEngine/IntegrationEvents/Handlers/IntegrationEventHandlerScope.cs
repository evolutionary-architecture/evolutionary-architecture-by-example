namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Common.TestEngine.IntegrationEvents.Handlers;

using MediatR;
using SuperSimpleArchitecture.Fitnet.Shared.Events;

internal sealed class IntegrationEventHandlerScope<TIntegrationEvent> : IDisposable 
where TIntegrationEvent : IIntegrationEvent
{
    private readonly IServiceScope _serviceScope;
    internal readonly IIntegrationEventHandler<TIntegrationEvent> IntegrationEventHandler;
    
    public IntegrationEventHandlerScope(WebApplicationFactory<Program> applicationInMemoryFactory)
    {
        _serviceScope = applicationInMemoryFactory.Services.CreateScope();
        IntegrationEventHandler = (IIntegrationEventHandler<TIntegrationEvent>)_serviceScope
            .ServiceProvider
            .GetRequiredService<INotificationHandler<TIntegrationEvent>>();
    }

    public void Dispose() => 
        _serviceScope.Dispose();
}