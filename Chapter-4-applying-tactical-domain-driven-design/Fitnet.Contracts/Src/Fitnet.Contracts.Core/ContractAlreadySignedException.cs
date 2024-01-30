namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

internal sealed class ContractAlreadySignedException() : InvalidOperationException("Contract is already signed");
