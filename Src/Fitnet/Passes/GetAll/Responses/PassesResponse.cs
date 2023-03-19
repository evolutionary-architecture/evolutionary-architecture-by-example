namespace SuperSimpleArchitecture.Fitnet.Passes.GetAll.Responses;

public record struct PassesResponse(IReadOnlyCollection<PassListItemDto> Passes);