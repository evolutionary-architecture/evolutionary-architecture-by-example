namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests;

using Common;

public class PassesArchitectureTests
{
    private const string Event = "Event";

    [Theory]
    [InlineData(Modules.Offers)]
    [InlineData(Modules.Reports)]
    public void PassesShouldNotHaveDependencyOnModule(string moduleName)
    {
        // Arrange
        var contractsModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Passes);

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
        validationResult.FailingTypes.Should().BeNull();
    }

    [Fact]
    public void PassesShouldCommunicateWithContractViaEvents()
    {
        // Arrange
        var contractsModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Passes);

        var shouldModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Contracts)
            .And()
            .DoNotHaveNameEndingWith(Event);
        var forbiddenModuleTypes = shouldModule.GetModuleTypes();

        // Act
        var rules = contractsModule
            .Should()
            .NotHaveDependencyOnAny(forbiddenModuleTypes);
        var validationResult = rules!.GetResult();

        // Assert
        validationResult.FailingTypes.Should().BeNull();
    }
}
