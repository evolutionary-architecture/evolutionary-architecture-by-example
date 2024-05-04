namespace EvolutionaryArchitecture.Fitnet.Contracts.Core.UnitTests.PrepareContract.BusinessRules;

using AttachAnnexToBindingContract.BusinessRules;
using Core.PrepareContract.BusinessRules;

public sealed class ContractCanBePreparedOnlyForAdultRuleTests
{
    [Fact]
    internal void Given_customer_age_which_is_less_than_18_Then_validation_should_throw()
    {
        // Arrange

        // Act
        var result = BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(17));

        // Assert
        var error = Error.Validation(nameof(ContractCanBePreparedOnlyForAdultRule),
            "Contract can not be prepared for a person who is not adult");
        result.Errors
            .Should()
            .ContainSingle()
            .Which
            .Should()
            .BeEquivalentTo(error);
    }

    [Fact]
    internal void Given_customer_age_which_is_equal_to_18_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var result = BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(18));

        // Assert
        result.ShouldBeSuccess();
    }

    [Fact]
    internal void Given_customer_age_which_is_greater_than_18_Then_validation_should_pass()
    {
        // Arrange

        // Act
        var result = BusinessRuleValidator.Validate(new ContractCanBePreparedOnlyForAdultRule(19));

        // Assert
        result.ShouldBeSuccess();
    }
}
