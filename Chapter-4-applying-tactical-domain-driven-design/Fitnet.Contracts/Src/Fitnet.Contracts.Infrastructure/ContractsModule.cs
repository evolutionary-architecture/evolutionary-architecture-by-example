namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure;

using Application;
using ErrorOr;
using MediatR;

internal sealed class ContractsModule(IMediator mediator) : IContractsModule
{
    public async Task<ErrorOr<Unit>> ExecuteCommandAsync(ICommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result;
    }

    public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command,
        CancellationToken cancellationToken = default) =>
        await mediator.Send(command, cancellationToken);
}
