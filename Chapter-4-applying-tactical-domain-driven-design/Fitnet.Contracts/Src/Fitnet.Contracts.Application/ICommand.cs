namespace EvolutionaryArchitecture.Fitnet.Contracts.Application;

public interface ICommand<out TResult> : IRequest<TResult>
{ }

public interface ICommand : IRequest<ErrorOr<Unit>>
{ }
