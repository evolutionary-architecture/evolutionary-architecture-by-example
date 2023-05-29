namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Commands;

using MediatR;

public interface ICommand<TResult> : IRequest<TResult>
{ }

public interface ICommand : IRequest
{ }