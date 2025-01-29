namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests.Conventions;

using Common;

public sealed class EndpointsConventionsTests
{
    private const string Endpoint = "Endpoint";

    [Fact]
    internal void Should_be_static()
    {
        // Arrange
        var rules = Solution.Types.That()
            .AreClasses()
            .And()
            .HaveNameEndingWith(Endpoint)
            .Should()
            .BeStatic();

        // Act
        var result = rules.GetResult();

        // Assert
        result.IsSuccessful.ShouldBeTrue();
    }
}
