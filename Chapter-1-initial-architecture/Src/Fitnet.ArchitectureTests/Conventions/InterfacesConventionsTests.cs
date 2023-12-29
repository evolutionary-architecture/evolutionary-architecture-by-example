namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests.Conventions;

using Common;

public sealed class InterfacesConventionsTests
{
    [Fact]
    internal void ShouldStartWithI()
    {
        // Arrange
        var rules = Solution.Types.That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I");

        // Act
        var result = rules.GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
