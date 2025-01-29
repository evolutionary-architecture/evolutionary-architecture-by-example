namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests;

using Common;

public sealed class OffersArchitectureTests
{
    private const string Event = "Event";

    [Theory]
    [InlineData(Modules.Contracts)]
    [InlineData(Modules.Reports)]
    internal void Offers_should_not_have_dependency_on_module(string moduleName)
    {
        // Arrange
        var offersModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Offers);

        var forbiddenModule = Solution.Types
            .That()
            .ResideInNamespace(moduleName);
        var forbiddenModuleTypes = forbiddenModule.GetModuleTypes();

        // Act
        var rules = offersModule
            .Should()
            .NotHaveDependencyOnAny(forbiddenModuleTypes);
        var validationResult = rules!.GetResult();

        // Assert
        validationResult.FailingTypes.ShouldBeNull();
    }

    [Fact]
    public void OffersShouldCommunicateWithPassesViaEvents()
    {
        // Arrange
        var offersModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Offers);
        var shouldModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Passes)
            .And()
            .DoNotHaveNameEndingWith(Event);
        var forbiddenModuleTypes = shouldModule.GetModuleTypes();

        // Act
        var rules = offersModule
            .Should()
            .NotHaveDependencyOnAny(forbiddenModuleTypes);
        var validationResult = rules!.GetResult();

        // Assert
        validationResult.FailingTypes.ShouldBeNull();
    }
}
