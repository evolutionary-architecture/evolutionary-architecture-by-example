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
    public void GivenSignatureWhenValidatingThenDoesNotThrow(string signatureText)
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
    public void GivenInvalidSignatureWhenValidatingThenThrowsSignatureNotValidException(string signature)
    {
        // Act & Assert
        Action act = () => Signature.From(DateTimeOffset.Now, signature);
        act.Should().Throw<SignatureNotValidException>();
    }
}
