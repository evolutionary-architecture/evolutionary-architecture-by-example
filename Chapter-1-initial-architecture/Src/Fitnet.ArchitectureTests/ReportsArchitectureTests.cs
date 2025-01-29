namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests;

using Common;

public sealed class ReportsArchitectureTests
{
    [Theory]
    [InlineData(Modules.Contracts)]
    [InlineData(Modules.Passes)]
    [InlineData(Modules.Offers)]
    internal void Reports_should_not_have_dependency_on_module(string moduleName)
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
        validationResult.FailingTypes.ShouldBeNull();
    }
}
