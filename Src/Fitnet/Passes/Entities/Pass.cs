namespace SuperSimpleArchitecture.Fitnet.Passes.Entities;

internal record Pass(Guid Id, Guid CustomerId, DateTimeOffset From, DateTimeOffset To, PassType PassType)
{
}