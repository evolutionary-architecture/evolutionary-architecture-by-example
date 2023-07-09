namespace EvolutionaryArchitecture.Fitnet.Contracts.Infrastructure;

using Application;
using MediatR;

internal sealed class ContractsModule : IContractsModule
{
    private readonly IMediator _mediator;

    public ContractsModule(IMediator mediator) =>
        _mediator = mediator;

    public async Task ExecuteCommandAsync(ICommand command, CancellationToken cancellationToken = default) => 
        await  _mediator.Send(command, cancellationToken);
    
    public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default) => 
        await _mediator.Send(command, cancellationToken);
}