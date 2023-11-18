namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure;

using Application;
using MediatR;

internal sealed class ContractsModule(ISender mediator) : IContractsModule
{
    public async Task ExecuteCommandAsync(ICommand command, CancellationToken cancellationToken = default) =>
        await mediator.Send(command, cancellationToken);

    public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default) =>
        await mediator.Send(command, cancellationToken);
}
