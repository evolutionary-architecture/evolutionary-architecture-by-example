namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.UnitTests.SignContract;

using EvolutionaryArchitecture.Fitnet.Contracts.Api.SignContract;
using FluentValidation.TestHelper;

public sealed class SignContractRequestValidatorTests
{
    private const string ValidSignature = "John Doe";
    private const int SignatureCharacterLimit = 100;
    private readonly SignContractRequestValidator _validator = new();
    private readonly DateTimeOffset _fakeNow = new Faker().Date.RecentOffset();

    [Fact]
    internal void Given_sign_contract_request_validation_When_request_is_valid_Then_result_should_have_no_errors()
    {
        // Arrange
        var request = new SignContractRequest(_fakeNow, ValidSignature);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    internal void Given_sign_contract_request_validation_When_signed_at_not_provided_Then_result_should_have_error()
    {
        // Arrange
        var request = new SignContractRequest(default, ValidSignature);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(signContractRequest => signContractRequest.SignedAt);
    }


    [Fact]
    internal void Given_sign_contract_request_validation_When_signature_is_to_long_Then_result_should_have_error()
    {
        // Arrange
        var tooLongSignature = GenerateTooLongSignature();
        var request = new SignContractRequest(default, tooLongSignature);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(signContractRequest => signContractRequest.SignedAt);
    }

    private static string GenerateTooLongSignature() => new('a', SignatureCharacterLimit + 1);
}
