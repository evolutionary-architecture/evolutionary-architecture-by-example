namespace SuperSimpleArchitecture.Fitnet.Passes.GetAll.Dtos;

public record struct PassesResponse(IReadOnlyCollection<PassesItemDto> Passes);