namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.SignContract.RequestValidator;

using Api.Sign;
using Bogus;
using FluentValidation.TestHelper;

public sealed class SignContractRequestValidatorTests
{
    private readonly SignContractRequestValidator _validator = new();
    private readonly DateTimeOffset _fakeNow = new Faker().Date.RecentOffset();

    [Fact]
    public void Given_sign_contract_request_validation_When_request_is_valid_Then_result_should_have_no_errors()
    {
        // Arrange
        var request = new SignContractRequest(_fakeNow);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Given_sign_contract_request_validation_When_signed_at_not_provided_Then_result_should_have_error()
    {
        // Arrange
        var request = new SignContractRequest(default);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(signContractRequest => signContractRequest.SignedAt);
    }
}