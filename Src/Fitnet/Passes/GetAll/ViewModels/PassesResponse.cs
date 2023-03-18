namespace SuperSimpleArchitecture.Fitnet.Passes.GetAll.ViewModels;

public record struct PassesResponse(IReadOnlyCollection<PassListItemDto> Passes);