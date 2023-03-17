namespace SuperSimpleArchitecture.Fitnet.Passes.Api.GetAll.ViewModels;

using Domain;
using Entities;

public record struct PassListItemDto(Guid Id, Guid CustomerId, DateTimeOffset From, DateTimeOffset To, PassType PassType);