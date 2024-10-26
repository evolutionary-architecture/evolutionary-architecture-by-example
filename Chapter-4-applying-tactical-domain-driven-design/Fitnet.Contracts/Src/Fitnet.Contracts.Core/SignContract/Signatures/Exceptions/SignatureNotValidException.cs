namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract.Signatures.Exceptions;

public sealed class SignatureNotValidException(string signature) : InvalidOperationException($"Signature: '{signature}' contains invalid characters.");
