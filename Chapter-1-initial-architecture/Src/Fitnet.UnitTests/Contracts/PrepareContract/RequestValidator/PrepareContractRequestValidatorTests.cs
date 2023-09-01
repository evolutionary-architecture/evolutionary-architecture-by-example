namespace EvolutionaryArchitecture.Fitnet.UnitTests.Contracts.PrepareContract.RequestValidator;

using EvolutionaryArchitecture.Fitnet.Contracts.PrepareContract;
using FluentValidation.TestHelper;

public sealed class PrepareContractRequestValidatorTests
{
    private readonly PrepareContractRequestValidator _validator = new();
    private readonly DateTimeOffset _fakeNow = new Faker().Date.RecentOffset();

    [Fact]
    internal void Given_prepare_contract_request_validation_When_request_is_valid_Then_result_should_have_no_errors()
    {
        // Arrange
        var validContractParameters = PrepareContractParameters.GetValid();
        var request = new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge, validContractParameters.MaxHeight, _fakeNow);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [ClassData(typeof(InvalidPrepareContractRequestTestCases))]
    internal void Given_prepare_contract_request_validation_When_property_is_valid_Then_result_should_have_error(PrepareContractRequest request, string expectedInvalidPropertyName)
    {
        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(expectedInvalidPropertyName);
    }
}
