namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests;

using Common;

public sealed class ContractsArchitectureTests
{
    [Theory]
    [InlineData(Modules.Passes)]
    [InlineData(Modules.Offers)]
    [InlineData(Modules.Reports)]
    internal void Contracts_should_not_have_dependency_on_module(string moduleName)
    {
        // Arrange
        var contractsModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Contracts);

        var forbiddenModule = Solution.Types
            .That()
            .ResideInNamespace(moduleName);
        var forbiddenModuleTypes = forbiddenModule.GetModuleTypes();

        // Act
        var rules = contractsModule
            .Should()
            .NotHaveDependencyOnAny(forbiddenModuleTypes);
        var validationResult = rules!.GetResult();

        // Assert
        validationResult.FailingTypes.ShouldBeNull();
    }
}
