namespace EvolutionaryArchitecture.Fitnet.Common.Api.Validations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public static class EndpointBuilderExtensions
{
    public static RouteHandlerBuilder ValidateRequest<TRequest>(this RouteHandlerBuilder builder) where TRequest : class =>
        builder.AddEndpointFilter<RequestValidationApiFilter<TRequest>>();
}
