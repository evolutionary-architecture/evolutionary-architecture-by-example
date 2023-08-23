namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.IntegrationEvents.Handlers;

using Infrastructure.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public sealed class IntegrationEventHandlerScope<TIntegrationEvent> : IDisposable 
    where TIntegrationEvent : IIntegrationEvent
{
    private readonly IServiceScope _serviceScope;
    private readonly IIntegrationEventHandler<TIntegrationEvent> _integrationEventHandler;
    
    public IntegrationEventHandlerScope(WebApplicationFactory<Program> applicationInMemoryFactory)
    {
        _serviceScope = applicationInMemoryFactory.Services.CreateScope();
        _integrationEventHandler = (IIntegrationEventHandler<TIntegrationEvent>)_serviceScope
            .ServiceProvider
            .GetRequiredService<INotificationHandler<TIntegrationEvent>>();
    }
    
    public async Task Consume(TIntegrationEvent @event, CancellationToken cancellationToken = default) =>
        await _integrationEventHandler.Handle(@event, cancellationToken);

    public void Dispose() => 
        _serviceScope.Dispose();
}