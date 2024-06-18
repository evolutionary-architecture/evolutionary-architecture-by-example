namespace EvolutionaryArchitecture.Fitnet.Common.Core;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(ValueObject? other) =>
        other is not null && GetType() == other.GetType() && IsSequenceEqual(other);

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
        .Aggregate(default(int), (current, obj) => HashCode.Combine(current, obj.GetHashCode()));

    public static bool operator ==(ValueObject left, ValueObject right) => left.Equals(right);

    public static bool operator !=(ValueObject left, ValueObject right) => !(left == right);

    private bool IsSequenceEqual(ValueObject other) => GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
}

