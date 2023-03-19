namespace SuperSimpleArchitecture.Fitnet.Passes.GetAll.Dtos;

public record struct PassesItemDto(Guid Id, Guid CustomerId, DateTimeOffset From, DateTimeOffset To);