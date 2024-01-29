namespace EvolutionaryArchitecture.Fitnet.Common.Api.ErrorHandling;

public sealed class ResourceNotFoundException(Guid id) : InvalidOperationException($"Resource with '{id}' not found ");
