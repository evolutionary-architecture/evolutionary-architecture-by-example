namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract.Signatures.Exceptions;

public sealed class SignatureNotValidException(string signatureText) : InvalidOperationException($"Signature text: '{signatureText}' contains invalid characters.");
