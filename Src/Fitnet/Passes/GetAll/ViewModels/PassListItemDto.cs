namespace SuperSimpleArchitecture.Fitnet.Passes.GetAll.ViewModels;

public record struct PassListItemDto(Guid Id, Guid CustomerId, DateTimeOffset From, DateTimeOffset To);