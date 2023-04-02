namespace SuperSimpleArchitecture.Fitnet.Contracts.Data;

internal sealed class Contract
{
    public Guid Id { get; init; }

    private Contract(Guid id)
    {
        Id = id;
    }

    internal static Contract Prepare() => new(Guid.NewGuid());
}