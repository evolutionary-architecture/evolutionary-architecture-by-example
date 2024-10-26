namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract.Signatures.Exceptions;

public sealed class DigitalSignatureNotValidException(string signatureText) : InvalidOperationException($"Signature: '{signatureText}' contains invalid characters.");
