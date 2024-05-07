namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.UnitTests.AttachAnnexToBindingContract;

using EvolutionaryArchitecture.Fitnet.Contracts.Api.AttachAnnexToBindingContract;
using FluentValidation.TestHelper;

public sealed class AttachAnnexToBindingContractRequestValidatorTests
{
    private readonly AttachAnnexToBindingContractRequestValidator _validator = new();
    private readonly DateTimeOffset _fakeNow = new Faker().Date.RecentOffset();

    [Fact]
    internal void Given_attach_annex_request_validation_When_request_is_valid_Then_result_should_have_no_errors()
    {
        // Arrange
        var request = new AttachAnnexToBindingContractRequest(_fakeNow);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    internal void Given_attach_annex_request_validation_When_valid_from_is_invalid_Then_result_should_have_error()
    {
        // Arrange
        var request = new AttachAnnexToBindingContractRequest(default);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.ValidFrom);
    }
}
