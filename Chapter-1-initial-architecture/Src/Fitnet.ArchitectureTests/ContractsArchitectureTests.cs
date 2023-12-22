namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests;

using Common;

public class ContractsArchitectureTests
{
    private readonly Assembly _solution = typeof(Program).Assembly;

    [Theory]
    [InlineData(Modules.Passes)]
    [InlineData(Modules.Offers)]
    [InlineData(Modules.Reports)]
    public void ContractsShouldNotHaveDependencyOnModule(string moduleName)
    {
        // Arrange
        var contractsModule = Types.InAssembly(_solution)
            .That()
            .ResideInNamespace(Modules.Contracts);

        var forbiddenModule = Types.InAssembly(_solution)
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
}
