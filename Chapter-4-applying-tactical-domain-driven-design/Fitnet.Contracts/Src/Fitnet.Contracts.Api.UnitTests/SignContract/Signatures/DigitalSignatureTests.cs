namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.UnitTests.SignContract.Signatures;
using EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract.Signatures;

using Core.SignContract.Signatures.Exceptions;
using FluentAssertions;

public sealed class DigitalSignatureTests
{
    [Theory]
    [InlineData("John Doe")]
    [InlineData("Kamil Baczek")]
    [InlineData("Maciej Jedrzejewski")]
    [InlineData("John David Smith")]
    internal void Given_create_signature_When_signature_is_valid_Then_should_not_throw(string signature)
    {
        // Arrange
        var now = DateTimeOffset.Now;

        // Act
        var digitalSignature = DigitalSignature.From(now, signature);

        // Assert
        digitalSignature.Signature.Should().Be(signature);
        digitalSignature.Date.Should().Be(now);
    }

    [Theory]
    [InlineData("invalidSignature!")]
    [InlineData("invalid@Signature")]
    internal void Given_create_signature_When_signature_has_forbidden_characters_Then_should_throw_exception(string signature)
    {
        // Arrange
        var now = DateTimeOffset.Now;

        // Act
        Action act = () => DigitalSignature.From(now, signature);

        // Assert
        act.Should().Throw<DigitalSignatureNotValidException>();
    }
}
