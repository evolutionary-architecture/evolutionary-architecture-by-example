namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests;

using Common;

public class OffersArchitectureTests
{
    private const string Event = "Event";

    [Theory]
    [InlineData(Modules.Contracts)]
    [InlineData(Modules.Reports)]
    public void OffersShouldNotHaveDependencyOnModule(string moduleName)
    {
        // Arrange
        var contractsModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Offers);

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
    public void OffersShouldCommunicateWithPassesViaEvents()
    {
        // Arrange
        var contractsModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Offers);

        var shouldModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Passes)
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
