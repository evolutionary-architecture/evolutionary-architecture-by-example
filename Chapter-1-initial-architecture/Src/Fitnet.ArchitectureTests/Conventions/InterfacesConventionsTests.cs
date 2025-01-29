namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests.Conventions;

using Common;

public sealed class InterfacesConventionsTests
{
    [Fact]
    internal void Should_start_with_I()
    {
        // Arrange
        var rules = Solution.Types.That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I");

        // Act
        var result = rules.GetResult();

        // Assert
        result.IsSuccessful.ShouldBeTrue();
    }
}
