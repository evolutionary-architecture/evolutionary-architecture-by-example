namespace EvolutionaryArchitecture.Fitnet.DomainModel;

using System;

/// <summary>
/// This abstract base class represents an identifier for entities within the domain model.
/// By employing non-primitive typed entity identifiers, it enhances the clarity and richness of the domain model,
/// effectively mitigating primitive obsession. Utilizing non-primitive identifiers aligned with domain concepts leads
/// to improved communication and alignment between code and business requirements.
/// </summary>
public abstract class EntityIdentifier<T> : IEquatable<EntityIdentifier<T>>
{
    public Guid Value { get; }

    protected EntityIdentifier(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException($"{typeof(T).Name} cannot be empty.", nameof(value));
        }

        Value = value;
    }

    /// <summary>
    /// Compares the current EntityIdentifier object with another object to determine if they are equal.
    /// </summary>
    public bool Equals(EntityIdentifier<T>? other) =>
        other is not null && (ReferenceEquals(this, other) || Value.Equals(other.Value));

    /// <summary>
    /// Overrides the Equals method of the Object class
    /// </summary>
    public override bool Equals(object? obj) =>
        ReferenceEquals(this, obj) || (obj is EntityIdentifier<T> other && Equals(other));

    /// <summary>
    /// Overrides the GetHashCode method to provide a hash code based on the Value property.
    /// </summary>
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// Overrides the equality operator == to provide equality comparison between EntityIdentifier objects.
    /// </summary>
    public static bool operator ==(EntityIdentifier<T> left, EntityIdentifier<T> right) => Equals(left, right);

    /// <summary>
    /// Overrides the equality operator != to provide equality comparison between EntityIdentifier objects.
    /// </summary>
    public static bool operator !=(EntityIdentifier<T> left, EntityIdentifier<T> right) => !Equals(left, right);

    /// <summary>
    /// Overrides the ToString method to return the string representation of the Value property.
    /// </summary>
    public override string ToString() => Value.ToString();
}
