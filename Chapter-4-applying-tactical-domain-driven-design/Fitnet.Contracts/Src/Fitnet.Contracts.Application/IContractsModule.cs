﻿namespace EvolutionaryArchitecture.Fitnet.Contracts.Application;

public interface IContractsModule
{
    Task ExecuteCommandAsync(ICommand command, CancellationToken cancellationToken = default);

    Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
}