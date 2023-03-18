namespace SuperSimpleArchitecture.Fitnet.Passes.Register;

public record struct RegisterPassRequest(Guid CustomerId, DateTimeOffset From, DateTimeOffset To);