namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests;

using Common;

public class ReportsArchitectureTests
{
    [Theory]
    [InlineData(Modules.Contracts)]
    [InlineData(Modules.Passes)]
    [InlineData(Modules.Offers)]
    public void ReportsShouldNotHaveDependencyOnModule(string moduleName)
    {
        // Arrange
        var reportsModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Reports);

        var forbiddenModule = Solution.Types
            .That()
            .ResideInNamespace(moduleName);
        var forbiddenModuleTypes = forbiddenModule.GetModuleTypes();

        // Act
        var rules = reportsModule
            .Should()
            .NotHaveDependencyOnAny(forbiddenModuleTypes);
        var validationResult = rules!.GetResult();

        // Assert
        validationResult.FailingTypes.Should().BeNull();
    }
}
