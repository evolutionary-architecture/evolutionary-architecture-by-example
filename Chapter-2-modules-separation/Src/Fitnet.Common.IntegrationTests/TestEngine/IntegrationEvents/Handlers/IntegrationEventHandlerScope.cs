namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.IntegrationEvents.Handlers;

using Infrastructure.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public sealed class IntegrationEventHandlerScope<TIntegrationEvent> : IDisposable 
where TIntegrationEvent : IIntegrationEvent
{
    private readonly IServiceScope _serviceScope;
    public readonly IIntegrationEventHandler<TIntegrationEvent> IntegrationEventHandler;
    
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