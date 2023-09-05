namespace EvolutionaryArchitecture.Fitnet.Common.Api.Validation.Requests;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public static class EndpointBuilderExtensions
{
    public static RouteHandlerBuilder ValidateRequest<TRequest>(this RouteHandlerBuilder builder) where TRequest : class =>
        builder.AddEndpointFilter<RequestValidationApiFilter<TRequest>>();
}
