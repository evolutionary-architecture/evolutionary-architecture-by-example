namespace SuperSimpleArchitecture.Fitnet.Passes.GetAll.Responses;

public record struct PassListItemDto(Guid Id, Guid CustomerId, DateTimeOffset From, DateTimeOffset To);