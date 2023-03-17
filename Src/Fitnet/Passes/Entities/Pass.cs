namespace SuperSimpleArchitecture.Fitnet.Passes.Entities;

internal record Pass(Guid Id, Guid CustomerId, DateTimeOffset From, DateTimeOffset To, PassType PassType)
{
    internal static Pass Register(Guid customerId, DateTimeOffset from, DateTimeOffset to, PassType passType) => 
        new(Guid.NewGuid(), customerId, from, to, passType);
}