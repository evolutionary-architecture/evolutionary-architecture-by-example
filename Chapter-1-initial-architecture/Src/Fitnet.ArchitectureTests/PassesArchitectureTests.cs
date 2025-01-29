namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests;

using Common;

public sealed class PassesArchitectureTests
{
    private const string Event = "Event";

    [Theory]
    [InlineData(Modules.Offers)]
    [InlineData(Modules.Reports)]
    internal void Passes_should_not_have_dependency_on_module(string moduleName)
    {
        // Arrange
        var passesModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Passes);

        var forbiddenModule = Solution.Types
            .That()
            .ResideInNamespace(moduleName);
        var forbiddenModuleTypes = forbiddenModule.GetModuleTypes();

        // Act
        var rules = passesModule
            .Should()
            .NotHaveDependencyOnAny(forbiddenModuleTypes);
        var validationResult = rules!.GetResult();

        // Assert
        validationResult.FailingTypes.ShouldBeNull();
    }

    [Fact]
    internal void PassesShouldCommunicateWithContractViaEvents()
    {
        // Arrange
        var passesModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Passes);

        var shouldModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Contracts)
            .And()
            .DoNotHaveNameEndingWith(Event);
        var forbiddenModuleTypes = shouldModule.GetModuleTypes();

        // Act
        var rules = passesModule
            .Should()
            .NotHaveDependencyOnAny(forbiddenModuleTypes);
        var validationResult = rules!.GetResult();

        // Assert
        validationResult.FailingTypes.ShouldBeNull();
    }
}
