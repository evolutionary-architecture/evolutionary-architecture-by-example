namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract.Signatures;

public sealed partial class DigitalSignature
{
    private static readonly Regex SignaturePattern = SignatureAllowedCharacters();
    public DateTimeOffset Date { get; }
    public string Signature { get; }

    private DigitalSignature(DateTimeOffset date, string signature)
    {
        Date = date;
        if (!SignaturePattern.IsMatch(signature))
        {
            throw new DigitalSignatureNotValidException(signature);
        }

        Signature = signature;
    }

    public static DigitalSignature From(DateTimeOffset signedAt, string signature) =>
        new(signedAt, signature);

    [GeneratedRegex(@"^[A-Za-z\s]+$")]
    private static partial Regex SignatureAllowedCharacters();
}
