namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using System.Text.RegularExpressions;

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

public sealed class SignatureNotValidException(string signatureText) : InvalidOperationException($"Signature text: '{signatureText}' contains invalid characters.");
