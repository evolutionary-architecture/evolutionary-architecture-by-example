namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.UnitTests.SignContract.Signatures;
using EvolutionaryArchitecture.Fitnet.Contracts.Core.SignContract.Signatures;
using Core.SignContract.Signatures.Exceptions;

public sealed class SignatureTests
{
    [Theory]
    [InlineData("John Doe")]
    [InlineData("Kamil Baczek")]
    [InlineData("Maciej Jedrzejewski")]
    [InlineData("John David Smith")]
    internal void Given_create_signature_When_signature_is_valid_Then_should_not_throw(string value)
    {
        // Arrange
        var now = DateTimeOffset.Now;

        // Act
        var signature = Signature.From(now, value);

        // Assert
        signature.Value.ShouldBe(value);
        signature.Date.ShouldBe(now);
    }

    [Theory]
    [InlineData("invalidSignature!")]
    [InlineData("invalid@Signature")]
    internal void Given_create_signature_When_signature_has_forbidden_characters_Then_should_throw_exception(string value)
    {
        // Arrange
        var now = DateTimeOffset.Now;

        // Act
        void Act()
        {
            Signature.From(now, value);
        }

        // Assert
        Should.Throw<SignatureNotValidException>(Act);
    }
}
