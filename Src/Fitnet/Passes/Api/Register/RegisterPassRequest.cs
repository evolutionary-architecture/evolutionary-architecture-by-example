namespace SuperSimpleArchitecture.Fitnet.Passes.Api.Register;

public record struct RegisterPassRequest(Guid CustomerId, DateTimeOffset From, DateTimeOffset To);