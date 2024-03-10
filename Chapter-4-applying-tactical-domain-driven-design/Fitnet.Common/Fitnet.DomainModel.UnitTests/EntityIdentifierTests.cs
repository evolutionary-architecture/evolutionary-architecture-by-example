namespace EvolutionaryArchitecture.Fitnet.DomainModel.UnitTests;

public class EntityIdentifierTests
{
    [Fact]
    internal void Given_valid_guid_Then_instance_should_be_successfully_created()
    {
        // Arrange
        var validGuid = Guid.NewGuid();

        // Act
        EntityIdentifier<TestEntity> result = new TestEntity(validGuid);

        // Assert
        result.Value.Should().Be(validGuid);
    }

    [Fact]
    internal void Given_empty_guid_Then_instance_creation_should_throw()
    {
        // Arrange
        var emptyGuid = Guid.Empty;

        // Act
        var act = () => new TestEntity(emptyGuid);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage($"{nameof(TestEntity)} cannot be empty. (Parameter '{nameof(TestEntity.Value)}')");
    }

    [Fact]
    internal void Given_two_entities_with_the_same_identifier_Then_equals_should_return_true()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var entityIdentifier1 = new TestEntity(guid);
        var entityIdentifier2 = new TestEntity(guid);

        // Act
        var result = entityIdentifier1.Equals(entityIdentifier2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    internal void Given_two_entities_with_different_identifiers_Then_equals_should_return_false()
    {
        // Arrange
        var entityIdentifier1 = new TestEntity(Guid.NewGuid());
        var entityIdentifier2 = new TestEntity(Guid.NewGuid());

        // Act
        var result = entityIdentifier1.Equals(entityIdentifier2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    internal void Given_entity_comparison_with_null_Then_equals_should_return_false()
    {
        // Arrange
        var entityIdentifier = new TestEntity(Guid.NewGuid());

        // Act
        var result = entityIdentifier.Equals(null);

        // Assert
        Assert.False(result.Equals(null));
    }

    [Fact]
    internal void Given_two_entities_with_the_same_identifier_Then_should_return_same_hash_codes()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var entityIdentifier1 = new TestEntity(guid);
        var entityIdentifier2 = new TestEntity(guid);

        // Act
        var firstHashcode = entityIdentifier1.GetHashCode();
        var secondHashcode = entityIdentifier2.GetHashCode();

        // Assert
        firstHashcode.Should().Be(secondHashcode);
    }

    [Fact]
    internal void Given_two_entities_with_different_identifiers_Then_should_return_different_hash_codes()
    {
        // Arrange
        var entityIdentifier1 = new TestEntity(Guid.NewGuid());
        var entityIdentifier2 = new TestEntity(Guid.NewGuid());

        // Act
        var firstHashcode = entityIdentifier1.GetHashCode();
        var secondHashcode = entityIdentifier2.GetHashCode();

        // Assert
        firstHashcode.Should().NotBe(secondHashcode);
    }

    [Fact]
    internal void Given_valid_entity_Then_should_return_guid_string()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var entityIdentifier = new TestEntity(guid);

        // Act & Assert
        guid.ToString().Should().Be(entityIdentifier.ToString());
    }
}

public class TestEntity(Guid value) : EntityIdentifier<TestEntity>(value);
