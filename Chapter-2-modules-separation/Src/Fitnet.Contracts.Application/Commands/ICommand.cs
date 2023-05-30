namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Commands;

public interface ICommand<out TResult> : IRequest<TResult>
{ }

public interface ICommand : IRequest
{ }