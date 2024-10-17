namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract.Signatures;

using System.Text.RegularExpressions;
using Exceptions;

public sealed partial class Signature
{
    private static readonly Regex SignatureTextPattern = SignatureAllowedCharacters();
    public DateTimeOffset Date { get; }
    public string Text { get; }

    private Signature(DateTimeOffset date, string text)
    {
        Date = date;
        if (!SignatureTextPattern.IsMatch(text))
        {
            throw new SignatureNotValidException(text);
        }

        Text = text;
    }

    public static Signature From(DateTimeOffset signedAt, string signatureText) =>
        new(signedAt, signatureText);

    [GeneratedRegex(@"^[A-Za-z\s]+$")]
    private static partial Regex SignatureAllowedCharacters();
}
