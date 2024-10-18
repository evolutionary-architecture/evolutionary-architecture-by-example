namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.UnitTests.SignContract.Signatures;
using EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract.Signatures;

using Core.SignContract.Signatures.Exceptions;
using FluentAssertions;

public sealed class SignatureTests
{
    [Theory]
    [InlineData("John Doe")]
    [InlineData("Kamil Baczek")]
    [InlineData("Maciej Jedrzejewski")]
    [InlineData("John David Smith")]
    internal void Given_create_signature_When_signature_is_valid_Then_should_not_throw(string signatureText)
    {
        // Arrange
        var dateTimeOffset = DateTimeOffset.Now;

        // Act
        var signature = Signature.From(dateTimeOffset, signatureText);

        // Assert
        signature.Text.Should().Be(signatureText);
        signature.Date.Should().Be(dateTimeOffset);
    }

    [Theory]
    [InlineData("invalidSignature!")]
    [InlineData("invalid@Signature")]
    internal void Given_create_signature_When_signature_has_forbidden_characters_Then_should_throw_exception(string signature)
    {
        // Act
        Action act = () => Signature.From(DateTimeOffset.Now, signature);

        // Assert
        act.Should().Throw<SignatureNotValidException>();
    }
}
