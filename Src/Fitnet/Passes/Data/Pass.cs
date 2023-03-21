namespace SuperSimpleArchitecture.Fitnet.Passes.Data;

internal record Pass(Guid Id, Guid CustomerId, DateTimeOffset From, DateTimeOffset To)
{
    internal static Pass Register(Guid customerId, DateTimeOffset from, DateTimeOffset to) => 
        new(Guid.NewGuid(), customerId, from, to);
}