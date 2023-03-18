namespace SuperSimpleArchitecture.Fitnet.Passes.Register;

public record RegisterPassRequest(Guid CustomerId, DateTimeOffset From, DateTimeOffset To);