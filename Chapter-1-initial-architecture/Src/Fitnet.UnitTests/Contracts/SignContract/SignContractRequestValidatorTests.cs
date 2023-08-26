namespace EvolutionaryArchitecture.Fitnet.UnitTests.Contracts.SignContract;

using Fitnet.Contracts.SignContract;
using FluentValidation.TestHelper;

public sealed class SignContractRequestValidatorTests
{
    private readonly SignContractRequestValidator _validator = new();

    [Fact]
    public void Given_SignContractRequest_With_Invalid_SignedAt_Should_Have_Validation_Error()
    {
        // Arrange
        var request = new SignContractRequest(default);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(signContractRequest => signContractRequest.SignedAt);
    }
}