namespace SuperSimpleArchitecture.Fitnet.Contracts.Data;

internal record Contract(Guid Id)
{
    internal static Contract Prepare() => new(Guid.NewGuid());
}