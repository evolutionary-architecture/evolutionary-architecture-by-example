namespace EvolutionaryArchitecture.Fitnet.Common.Core;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(ValueObject? other) => other is not null && IsSequenceEqual(other);

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return IsSequenceEqual(other);
    }

    public override int GetHashCode() => GetEqualityComponents()
            .Aggregate(0, (current, obj) => current ^ (obj?.GetHashCode() ?? 0));

    public static bool operator ==(ValueObject left, ValueObject right) => left.Equals(right);

    public static bool operator !=(ValueObject left, ValueObject right) => !(left == right);

    private bool IsSequenceEqual(ValueObject other) => GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
}

