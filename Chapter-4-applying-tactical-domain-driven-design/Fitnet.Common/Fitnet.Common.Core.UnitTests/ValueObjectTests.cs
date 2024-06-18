﻿namespace EvolutionaryArchitecture.Fitnet.Common.Core.UnitTests;

public class ValueObjectTests
{
    private const int DefaultIntProperty = 1;
    private const string DefaultStringProperty = "value";

    [Fact]
    internal void Given_two_same_type_objects_When_values_are_the_same_Then_should_be_equal()
    {
        // Arrange
        var firstObject = new FakeValueObject();
        var secondObject = new FakeValueObject();

        // Act & Assert
        firstObject.Should().Be(secondObject);
        firstObject.Equals(secondObject).Should().BeTrue();
        (firstObject == secondObject).Should().BeTrue();
        (firstObject != secondObject).Should().BeFalse();
    }

    [Fact]
    internal void Given_two_same_type_objects_When_values_are_not_the_same_Then_should_not_be_equal()
    {
        // Arrange
        var firstObject = new FakeValueObject();
        var secondObject = new FakeValueObject(property1: 2);
        var thirdObject = new FakeValueObject(property2: Guid.NewGuid().ToString());

        // Act & Assert
        firstObject.Should().NotBe(secondObject);
        firstObject.Equals(secondObject).Should().BeFalse();
        (firstObject == secondObject).Should().BeFalse();
        (firstObject != secondObject).Should().BeTrue();
        firstObject.Should().NotBe(thirdObject);
        firstObject.Equals(thirdObject).Should().BeFalse();
        (firstObject == thirdObject).Should().BeFalse();
        (firstObject != thirdObject).Should().BeTrue();
    }

    [Fact]
    internal void Given_two_same_type_objects_When_values_are_the_same_Then_should_have_same_hash_code()
    {
        // Arrange
        var firstObject = new FakeValueObject();
        var secondObject = new FakeValueObject();

        // Act & Assert
        firstObject.GetHashCode().Should().Be(secondObject.GetHashCode());
    }

    [Fact]
    internal void Given_two_different_type_objects_When_values_are_the_same_Then_should_not_be_equal()
    {
        // Arrange
        var firstObject = new FakeValueObject();
        var secondObject = new AnotherTypeFakeValueObject();

        // Act & Assert
        firstObject.Should().NotBe(secondObject);
        firstObject.Equals(secondObject).Should().BeFalse();
        (firstObject == secondObject).Should().BeFalse();
        (firstObject != secondObject).Should().BeTrue();
    }

    private class FakeValueObject(int property1 = DefaultIntProperty, string property2 = DefaultStringProperty) : ValueObject
    {
        private int Property1 { get; } = property1;
        private string Property2 { get; } = property2;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Property1;
            yield return Property2;
        }
    }

    private class AnotherTypeFakeValueObject : ValueObject
    {
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DefaultIntProperty;
            yield return DefaultStringProperty;
        }
    }
}
