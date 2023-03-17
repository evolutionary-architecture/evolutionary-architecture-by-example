namespace SuperSimpleArchitecture.Fitnet.Passes.Api.GetAll.ViewModels;

public record struct PassesListViewModel(IReadOnlyCollection<PassListItemDto> Passes);