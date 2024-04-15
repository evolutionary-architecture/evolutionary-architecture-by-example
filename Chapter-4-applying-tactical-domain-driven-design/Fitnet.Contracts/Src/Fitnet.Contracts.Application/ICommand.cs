namespace EvolutionaryArchitecture.Fitnet.Contracts.Application;

using ErrorOr;

public interface ICommand<out TResult> : IRequest<TResult>
{ }

public interface ICommand : IRequest<ErrorOr<Unit>>
{ }
