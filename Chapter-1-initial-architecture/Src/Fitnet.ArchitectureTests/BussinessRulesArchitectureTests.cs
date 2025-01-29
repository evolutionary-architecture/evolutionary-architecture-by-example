namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests;

using Common;
using Fitnet.Common.BusinessRulesEngine;

public class BusinessRulesArchitectureTests
{
    [Theory]
    [InlineData("Endpoint")]
    [InlineData("Consumer")]
    internal void BusinessRules_Should_Not_Have_Dependency_On_Module(string forbiddenTypeEnding)
    {
        // Arrange
        var businessRules = Solution.Types.That().ImplementInterface(typeof(IBusinessRule)).GetModuleTypes();
        var forbiddenTypes = Solution
            .Types
            .That()
            .HaveNameEndingWith(forbiddenTypeEnding);

        // Act
        var validationResult = forbiddenTypes!.Should().NotHaveDependencyOnAny(businessRules).GetResult();

        // Assert
        validationResult.FailingTypes.ShouldBeNull();
    }
}
