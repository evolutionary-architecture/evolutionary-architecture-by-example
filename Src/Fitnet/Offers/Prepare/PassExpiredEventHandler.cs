namespace SuperSimpleArchitecture.Fitnet.Offers.Prepare;

using Passes.MarkPassAsExpired.Events;
using Shared.Events;

internal sealed class PassExpiredEventHandler: IIntegrationEventHandler<PassExpiredEvent>
{
    public Task Handle(PassExpiredEvent notification, CancellationToken cancellationToken) => 
        Task.CompletedTask;
}