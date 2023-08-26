namespace EvolutionaryArchitecture.Fitnet.UnitTests.Contracts.PrepareContract;

using EvolutionaryArchitecture.Fitnet.Contracts.PrepareContract;
using FluentValidation.TestHelper;

public sealed class PrepareContractRequestValidatorTests
{
    private readonly PrepareContractRequestValidator _validator = new();
    private readonly DateTimeOffset _fakeNow = new Faker().Date.RecentOffset();
    
    [Fact]
    public void Given_PrepareContractRequest_With_Invalid_CustomerId_Should_Have_Validation_Error()
    {
        // Arrange
        var validContractParameters = PrepareContractParameters.GetValid();
        var request = new PrepareContractRequest(Guid.Empty, validContractParameters.MinAge, validContractParameters.MaxHeight, _fakeNow);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(prepareContractRequest => prepareContractRequest.CustomerId);
    }

    [Fact]
    public void Given_PrepareContractRequest_With_Invalid_CustomerAge_Should_Have_Validation_Error()
    {
        // Arrange
        var validContractParameters = PrepareContractParameters.GetValid();
        var request = new PrepareContractRequest(Guid.NewGuid(), default, validContractParameters.MaxHeight, _fakeNow);
        
        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(prepareContractRequest => prepareContractRequest.CustomerAge);
    }

    [Fact]
    public void Given_PrepareContractRequest_With_Invalid_CustomerHeight_Should_Have_Validation_Error()
    {
        // Arrange
        var validContractParameters = PrepareContractParameters.GetValid();
        var request = new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge, default, _fakeNow);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(prepareContractRequest => prepareContractRequest.CustomerHeight);
    }

    [Fact]
    public void Given_PrepareContractRequest_with_Invalid_PreparedAt_Should_Have_Validation_Error()
    {
        // Arrange
        var validContractParameters = PrepareContractParameters.GetValid();
        var request = new PrepareContractRequest(Guid.NewGuid(), validContractParameters.MinAge, validContractParameters.MinHeight, default);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(prepareContractRequest => prepareContractRequest.PreparedAt);
    }
}