namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract.Signatures;

public sealed partial class Signature
{
    private static readonly Regex SignaturePattern = SignatureAllowedCharacters();
    public DateTimeOffset Date { get; }
    public string Value { get; }

    private Signature(DateTimeOffset date, string value)
    {
        Date = date;
        if (!SignaturePattern.IsMatch(value))
        {
            throw new SignatureNotValidException(value);
        }

        Value = value;
    }

    public static Signature From(DateTimeOffset signedAt, string signature) =>
        new(signedAt, signature);

    [GeneratedRegex(@"^[A-Za-z\s]+$")]
    private static partial Regex SignatureAllowedCharacters();
}
