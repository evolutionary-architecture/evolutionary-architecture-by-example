namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests;

using Common;

public sealed class ContractsArchitectureTests
{
    private readonly Assembly _solution = typeof(Program).Assembly;

    [Theory]
    [InlineData(Modules.Passes)]
    [InlineData(Modules.Offers)]
    [InlineData(Modules.Reports)]
    internal void Contracts_should_not_have_dependency_on_module(string moduleName)
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
