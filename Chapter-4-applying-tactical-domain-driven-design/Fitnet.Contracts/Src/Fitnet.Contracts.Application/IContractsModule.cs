﻿namespace EvolutionaryArchitecture.Fitnet.Contracts.Application;

using ErrorOr;

public interface IContractsModule
{
    Task<ErrorOr<Unit>> ExecuteCommandAsync(ICommand command, CancellationToken cancellationToken = default);

    Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
}
