namespace SuperSimpleArchitecture.Fitnet.Passes.Api.GetAll.ViewModels;

public record struct PassesResponse(IReadOnlyCollection<PassListItemDto> Passes);