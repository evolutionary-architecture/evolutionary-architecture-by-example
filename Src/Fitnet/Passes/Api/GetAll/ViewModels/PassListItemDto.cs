namespace SuperSimpleArchitecture.Fitnet.Passes.Api.GetAll.ViewModels;

public record struct PassListItemDto(Guid Id, Guid CustomerId, DateTimeOffset From, DateTimeOffset To);